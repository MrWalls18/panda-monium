using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private int duration;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().HasDoubleJump = true;

            StartCoroutine(SpawnJumpPickup());
        }
    }

    IEnumerator SpawnJumpPickup()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}
