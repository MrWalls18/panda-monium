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

    public int kills;

    private bool triggered;

    private void Awake()
    {
        triggered = false;
        view = GetComponent<PhotonView>();
        Hashtable hash = new Hashtable();
        hash.Add("kills", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
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

            if(this.kills >= GameManager.Instance.matchPoints && triggered)
            {
                GameManager.Instance.EndGame();
            }

            triggered = true;
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