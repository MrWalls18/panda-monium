using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] menuScreens;

    public void DeathScreen()
    {
        ChangeScreens("DeathScreen");
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeScreens(string menu)
    {
        foreach (GameObject screen in menuScreens)
        {
            if (screen.name != menu)
                screen.SetActive(false);
            else
                screen.SetActive(true);
        }
    }
}
