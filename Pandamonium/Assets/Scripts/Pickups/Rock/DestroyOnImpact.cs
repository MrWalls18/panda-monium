using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyOnImpact : MonoBehaviour
{
    [SerializeField] private int damage;
    PhotonView PV;

    private void Awake() {
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Damage Health
            other.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
        }

        if(PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
