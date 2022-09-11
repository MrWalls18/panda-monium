using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInput;
    [SerializeField] private InputField joinInput;
    [SerializeField] private GameObject[] menuScreens;
    [SerializeField] private Text roomNameText;
    [SerializeField] private GameObject playerListItemPrefab;
    [SerializeField] private Transform playerListContent;

    [SerializeField] private GameObject startGameButton;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        ChangeScreens("Create/Join Room");
    }

    public void CreateRoom()
    {
       // if (string.IsNullOrEmpty(createInput.text))
         //   return;

        PhotonNetwork.CreateRoom(createInput.text);

        ChangeScreens("Loading Screen");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
    }

    #region Join Room Functions
    public void JoinRoom()
    {
       // if (string.IsNullOrEmpty(joinInput.text))
         //   return;

        PhotonNetwork.JoinRoom(joinInput.text);        
    }

    public override void OnJoinedRoom()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        ChangeScreens("Room Menu");

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }  

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
    }
#endregion

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("LevelDesign");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ChangeScreens("Loading Screen");
    }

    public override void OnLeftRoom()
    {
        ChangeScreens("Create/Join Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void ChangeScreens(string menu)
    {
        foreach (GameObject screen in menuScreens)
        {
            if (screen.name != menu)
                screen.SetActive(false);
            else
                screen.SetActive(true);
        }
    }
}
