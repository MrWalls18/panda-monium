using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyOnImpact : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Damage Health
            other.gameObject.GetComponent<PlayerStats>().Health -= damage;
        }
        
        PhotonNetwork.Destroy(this.gameObject);
    }
}
