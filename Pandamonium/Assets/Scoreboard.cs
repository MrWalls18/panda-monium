using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    [SerializeField] CanvasGroup canvasGroup;

    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();


    private void Awake()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        scoreboardItems[player] = item;
    }

    void RemoveScoreboardItem(Player player)
    {
        Destroy(scoreboardItems[player].gameObject);
        scoreboardItems.Remove(player);
    }

    private void Update()
    {
        if(GameManager.Instance.isGameOver == false)
            ToggleScoreboard();

        SortList();
    }

    List<Transform> players = new List<Transform>();
    public void SortList()
    {
        if (players == null)
            return;

        players.Clear();

        foreach (Transform child in transform)
        {
            players.Add(child);
        }

        int i, j;
        int N = players.Count;
        for (j = 1; j < N; j++)
            for (i = j; i > 0 && players[i].gameObject.GetComponent<ScoreboardItem>().kills > players[i - 1].gameObject.GetComponent<ScoreboardItem>().kills; i--)
            {
                Transform temp;

                temp = players[i];
                players[i] = players[i - 1];
                players[i - 1] = temp;
            }

        for(i = 0; i < players.Count; i++)
        {
            players[i].SetSiblingIndex(i);
        }

    }

    void ToggleScoreboard()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasGroup.alpha = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            canvasGroup.alpha = 0;
        }
    }
}
