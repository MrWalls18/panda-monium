using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        Transform tempSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        PhotonNetwork.Instantiate(player.name, tempSpawn.position, tempSpawn.rotation);
    }
}
