using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    private GameObject playerPrefab;

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        if (view.IsMine)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        //Gets all spawn points in scene to randomly spawn player
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        if(spawnPoints.Length <= 0)
            playerPrefab = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, new object[] {view.ViewID});
        else
        {
            int index = Random.Range(0, spawnPoints.Length);
            playerPrefab = PhotonNetwork.Instantiate("Player", spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0, new object[] {view.ViewID});
        }        
    }

    public void Die()
    {
        PhotonNetwork.Destroy(playerPrefab);
        SpawnPlayer();
    }
}
