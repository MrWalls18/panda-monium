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

    public void CreateRoom()
    {
       // if (string.IsNullOrEmpty(createInput.text))
         //   return;

        PhotonNetwork.CreateRoom(createInput.text);

        ChangeScreens("Loading Screen");
    }

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

        for(int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ChangeScreens("Error Menu");
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
      //  Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
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
