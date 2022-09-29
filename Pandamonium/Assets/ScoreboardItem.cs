using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text usernameText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text deathsText;

    Player player;
    public int kills;

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;
        killsText.text = "0";
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
