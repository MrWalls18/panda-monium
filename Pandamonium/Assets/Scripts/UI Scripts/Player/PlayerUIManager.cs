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
    [SerializeField] private GameObject playerHUD;

    private GameManager gm;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerStats = GetComponent<PlayerStats>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(view.IsMine)
        {
            canvas.enabled = true;

            //At start of game, all player input is disabled until countdown is done
            if (gm.timeToStartGameText.enabled)
            {
                DisableControls();
                StartCoroutine(StartGame());
            }
        }

        else
        {
            canvas.enabled = false;
        }    
    }

    private void Update()
    {
        if(view.IsMine && !gm.timeToStartGameText.enabled)
        {
            PauseScreen();
            DisplayPowerups();
        }
        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);

        EnableControls();
    }

    private void DisplayPowerups()
    {
        if (view.IsMine)
        {
            //Shows UI for double jump
            if (playerStats.HasDoubleJump)
            {
                doubleJump.color = new Color(0, 0, 0, 1);
            }
            else
            {
                doubleJump.color = new Color(0, 0, 0, 0.25f);
            }

            // Shows UI for speed boost
            if (playerStats.HasSpeedBoost)
            {
                speedBoost.color = new Color(0, 0, 0, 1);
            }
            else
            {
                speedBoost.color = new Color(0, 0, 0, 0.25f);
            }

            //Shows UI for bullets
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
            playerHUD.SetActive(false);
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

    public void DisableControls()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
        LookAround look = GetComponentInChildren<LookAround>();
        if (look != null)
            look.enabled = false;
    }

    public void EnableControls()
    {
        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerShooting>().enabled = true;
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
