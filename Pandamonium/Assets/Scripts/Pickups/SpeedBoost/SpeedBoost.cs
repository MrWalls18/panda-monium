using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private int duration;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Boost(other));
        }
    }

    IEnumerator Boost(Collider player)
    {
        player.gameObject.GetComponent<Player>().Speed *= speedMultiplier;

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.gameObject.GetComponent<Player>().Speed /= speedMultiplier;

        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}
