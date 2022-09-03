using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Triggered");
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player hit");

            other.gameObject.GetComponent<Player>().HasDoubleJump = true;
        }
    }
}
