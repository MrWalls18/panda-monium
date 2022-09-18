using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    [SerializeField] private GameObject killFeedItemPrefab;
    [SerializeField] private Transform killFeedContent;
    private PhotonView view;

    void Awake()
    {
        SingletonPattern();
        view = GetComponent<PhotonView>();

        PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadeSceneMode)
    {
            
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
        Destroy(this.gameObject);
        base.OnLeftRoom();
    }

    void SingletonPattern()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }    

    public void PlayerKilledPlayer(string shooter, string victim)
    {
        view.RPC(nameof(RPC_PlayerKilledPlayer), RpcTarget.All, shooter, victim);
    }

    [PunRPC]
    void RPC_PlayerKilledPlayer(string shooter, string victim)
    {
        GameObject killFeed = PhotonNetwork.Instantiate(killFeedItemPrefab.name, killFeedContent.position, Quaternion.identity);
        killFeed.GetComponent<Text>().text = (shooter + " killed " + victim);
        killFeed.transform.SetParent(killFeedContent);
        killFeed.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        killFeed.transform.SetAsFirstSibling();
    }
}
