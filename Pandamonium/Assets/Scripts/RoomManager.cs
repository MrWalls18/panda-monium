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

    void Awake()
    {
        SingletonPattern();

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
        Debug.Log("Kill feed");
        GameObject killFeed = Instantiate(killFeedItemPrefab, killFeedContent);
        killFeed.GetComponent<Text>().text = (shooter + " killed " + victim);
    }
}
