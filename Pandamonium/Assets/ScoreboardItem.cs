using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    [SerializeField] public Text usernameText;
    [SerializeField] public Text killsText;
    [SerializeField] private Text deathsText;

    private Player player;
    private PhotonView view;

    [HideInInspector] public int kills;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;
        UpdateStats();
    }

    void UpdateStats()
    {
        if(player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();
            this.kills = (int)kills;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if(changedProps.ContainsKey("kills"))
            {
                UpdateStats();
            }
        }
    }
}