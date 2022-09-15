using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rock : MonoBehaviour
{
    [SerializeField] private int damage;
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(PV.IsMine)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetPhotonView().IsMine)
                    return;
                else
                {
                    other.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
                }
            } 
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
