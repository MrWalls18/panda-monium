using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerUIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] menuScreens;
    private PlayerStats playerStats;
    private bool isPaused = false;
    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(view.IsMine)
        {
            PauseScreen();
        }
        
    }

    public void DeathScreen()
    {
        if (view.IsMine)
        {
            isPaused = false;
            ChangeScreens("None");
            DisableControls();
            ChangeScreens("DeathScreen");
            Cursor.lockState = CursorLockMode.None;
        }
    }

#region Pause Screen Functions
    public void PauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerStats.Health > 0)
        {
            if (isPaused)
            {
                ChangeScreens("None");
                EnableControls();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ChangeScreens("PauseScreen");
                DisableControls();
                Cursor.lockState = CursorLockMode.None;
            }
            isPaused = !isPaused;
        }
    }

    void DisableControls()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerInteractions>().enabled = false;
        LookAround look = GetComponentInChildren<LookAround>();
        if (look != null)
            look.enabled = false;
    }

    void EnableControls()
    {
        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerInteractions>().enabled = true;
        LookAround look = GetComponentInChildren<LookAround>();
        if (look != null)
            look.enabled = true;
    }
#endregion

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ChangeScreens("LoadingScreen");       
    }

    public void ChangeScreens(string menu)
    {
        foreach (GameObject screen in menuScreens)
        {
            if (screen.name == menu)
                screen.SetActive(true);
            else
                screen.SetActive(false);
        }
    }
}
