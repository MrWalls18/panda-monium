using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    private float matchTime = 300f;
    public Transform[] spawnPoints;

    [Header("Scoreboard")]
    [SerializeField] private GameObject scoreboard;

    [Header("Start Game Timer Fields")]
    [SerializeField] public Text timeToStartGameText;
    [SerializeField] public float timeToStartGame;

    [Header("Match Timer Fields")]
    [SerializeField] private Text timerText;
    [SerializeField] private float timerInSeconds;
    private bool isTimerRunning = false;

    [Header("Kill Feed Fields")]
    [SerializeField] private GameObject killFeedItemPrefab;
    [SerializeField] private Transform killFeedContent;


    private PhotonView view;
    ScoreboardItem[] scoreboardItems;
    void Awake()
    {
        timerInSeconds = matchTime;
        SingletonPattern();
        view = GetComponent<PhotonView>();

        PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    private void Start()
    {
        scoreboardItems = scoreboard.transform.GetComponentsInChildren<ScoreboardItem>();

        foreach(ScoreboardItem item in scoreboardItems)
        {
            if(item.usernameText.text == PhotonNetwork.LocalPlayer.NickName)
            {
                item.usernameText.color = Color.yellow;
                item.killsText.color = Color.yellow;
            }
        }
    }

    private void FixedUpdate()
    {
        CountdownToStartGame();
        //Timer();
    }

    void CountdownToStartGame()
    {
        if (timeToStartGameText.enabled)
        {
            if (timeToStartGame >= 0)
            {
                Mathf.FloorToInt(timeToStartGame);
                timeToStartGameText.text = timeToStartGame.ToString("#");
                timeToStartGame -= Time.deltaTime;
            }
            else
            {
                timeToStartGameText.enabled = false;
            }
        }
    }


#region Timer Functions
    private void Timer()
    {
        if (isTimerRunning)
        {
            timerInSeconds -= Time.deltaTime;
            if (timerInSeconds <= 0)
            {
                DisplayTime(0f);
                isTimerRunning = false;
                //end game screen
                //show victory/defeat
                //transition to post match scene
            }
            DisplayTime(timerInSeconds);
        }
    }

    private void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
#endregion

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

    public void KillFeed(Player shooter, string victim)
    {
        view.RPC(nameof(RPC_KillFeed), RpcTarget.All, shooter, victim);
    }
    
    

    [PunRPC]
    void RPC_KillFeed(Player shooter, string victim)
    {
        GameObject killFeed = PhotonNetwork.Instantiate(killFeedItemPrefab.name, killFeedContent.position, Quaternion.identity); //Spawns text prefab
        killFeed.GetComponent<Text>().text = (shooter.NickName + " killed " + victim); //Sets the text
        killFeed.transform.SetParent(killFeedContent); //Becomes child of the Content area
        killFeed.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f); //Resets scale
        killFeed.transform.SetAsFirstSibling(); //Moves to top of list
    }
}
