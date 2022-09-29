using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerUIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] menuScreens;
    private PlayerStats playerStats;
    private bool isPaused = false;
    private PhotonView view;

    [SerializeField] private Image doubleJump;
    [SerializeField] private Image speedBoost;
    [SerializeField] private Text bulletsText;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerStats = GetComponent<PlayerStats>();

        if(view.IsMine)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }    
    }

    private void Update()
    {
        if(view.IsMine)
        {
            PauseScreen();
            DisplayPowerups();
        }
        
    }

    private void DisplayPowerups()
    {
        if (view.IsMine)
        {
            if (playerStats.HasDoubleJump)
            {
                doubleJump.color = new Color(0, 0, 0, 1);
            }
            else
            {
                doubleJump.color = new Color(0, 0, 0, 0.25f);
            }

            if (playerStats.HasSpeedBoost)
            {
                speedBoost.color = new Color(0, 0, 0, 1);
            }
            else
            {
                speedBoost.color = new Color(0, 0, 0, 0.25f);
            }

            bulletsText.text = playerStats.ThrowablesLeft.ToString();
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
