using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PostGameUIScript : MonoBehaviour
{
    [SerializeField] private GameObject waitingText;
    [SerializeField] private GameObject restartButton;

    private void Update()
    {
       /* if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            waitingText.SetActive(false);
            restartButton.SetActive(true);
        }
        else
        {
            waitingText.SetActive(true);
            restartButton.SetActive(false);
        }
        */
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public void RestartLevel()
    {
        Destroy(GameManager.Instance.gameObject);
        /*
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            Destroy(player);
        foreach (GameObject playerManager in GameObject.FindGameObjectsWithTag("PlayerManager"))
            Destroy(playerManager);
            */
        PhotonNetwork.AutomaticallySyncScene = true;
        this.gameObject.SetActive(false);
        PhotonNetwork.LoadLevel("Testing");
        
    }

}
