using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menuScreens;
    public void Play()
    {
        SceneManager.LoadScene("Lobby");
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

    public void Quit()
    {
        Application.Quit();
    }
}
