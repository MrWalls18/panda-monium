using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    private GameObject playerPrefab;

    private int _kills;

    Player[] allPlayers;

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        StartGameSpawn();
    }

    //Prevents players from spawning at same place at start of game
    void StartGameSpawn()
    {
        allPlayers = PhotonNetwork.PlayerList;

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (allPlayers[i] == PhotonNetwork.LocalPlayer && view.IsMine)
            {
                playerPrefab = PhotonNetwork.Instantiate("Player", GameManager.Instance.spawnPoints[i].transform.position, GameManager.Instance.spawnPoints[i].transform.rotation, 0, new object[] { view.ViewID });
            }
        }
    }

    void SpawnPlayer()
    {
        if(GameManager.Instance.spawnPoints.Length <= 0 || GameManager.Instance.spawnPoints == null)
            playerPrefab = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, new object[] {view.ViewID});
        else
        {
            int index = Random.Range(0, GameManager.Instance.spawnPoints.Length);
            playerPrefab = PhotonNetwork.Instantiate("Player", GameManager.Instance.spawnPoints[index].transform.position, GameManager.Instance.spawnPoints[index].transform.rotation, 0, new object[] {view.ViewID});
        }        
    }

    public void GetKill()
    {
        view.RPC(nameof(RPC_GetKill), view.Owner);
    }

    [PunRPC]
    void RPC_GetKill()
    {
        _kills++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", _kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.view.Owner == player);
    }

    public void Die()
    {
        PhotonNetwork.Destroy(playerPrefab);
        SpawnPlayer();
    }
}
