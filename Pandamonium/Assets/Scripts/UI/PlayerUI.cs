using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] menuScreens;

    public void DeathScreen()
    {
        //Disables collider
        GetComponent<CharacterController>().enabled = false;

        // Disables renderer
        GetComponentInChildren<MeshRenderer>().enabled = false;

        //Disables player movement
        GetComponent<PlayerController>().enabled = false; 
        GetComponent<PlayerInteractions>().enabled = false;
        GetComponentInChildren<LookAround>().enabled = false;

        //Enables death screen
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
