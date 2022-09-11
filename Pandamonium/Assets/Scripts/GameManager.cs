using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : SingletonPattern<GameManager>
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject player;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        Transform tempSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        PhotonNetwork.Instantiate(player.name, tempSpawn.position, tempSpawn.rotation);
    }
}
