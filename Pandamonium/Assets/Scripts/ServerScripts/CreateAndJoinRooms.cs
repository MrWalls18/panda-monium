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
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject playerListItemPrefab;

    [SerializeField] private Text roomNameText;    
    [SerializeField] private Transform playerListContent;

    

#region Connecting to Servers
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
#endregion

#region Creating Rooms
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createInput.text))
            return;

        PhotonNetwork.CreateRoom(createInput.text);

        ChangeScreens("Loading Screen");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
    }
#endregion

#region Joining Rooms
    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(joinInput.text))
            return;

        PhotonNetwork.JoinRoom(joinInput.text);        
    }

    public override void OnJoinedRoom()
    {
        //Changes the displayed room name
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        ChangeScreens("Room Menu");

        //Gets list of players in room
        Player[] players = PhotonNetwork.PlayerList;

        //Destroys current player names in room
        //this prevents players that left still appearing on screen
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        //Displays names of players in room
        for(int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        //Allows the host to start the game and no one else
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Adds players name to list of players displayed
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
#endregion

#region Leaving Rooms
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ChangeScreens("Loading Screen");
    }

    public override void OnLeftRoom()
    {
        ChangeScreens("Create/Join Room");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
#endregion

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("LevelDesign");
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
