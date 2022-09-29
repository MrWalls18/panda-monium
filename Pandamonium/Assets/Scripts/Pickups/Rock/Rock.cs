using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Rock : MonoBehaviour
{
    [HideInInspector] public Player bulletOwner;

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
                    other.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage, bulletOwner);
                }
            } 
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
