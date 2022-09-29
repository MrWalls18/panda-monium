using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerNameManager : MonoBehaviour
{
    private static string usernamePrefKey = "Username";

    [SerializeField] private InputField usernameInput;

    private void Start()
    {
        if (PlayerPrefs.HasKey(usernamePrefKey))
        {
            usernameInput.text = PlayerPrefs.GetString(usernamePrefKey);
            PhotonNetwork.NickName = PlayerPrefs.GetString(usernamePrefKey);
        }
    }

    public void OnUsernameInputValueChanged()
    {
        PhotonNetwork.NickName = usernameInput.text;
        PlayerPrefs.SetString(usernamePrefKey, usernameInput.text);
    }
}
