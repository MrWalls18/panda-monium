using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rock : MonoBehaviour
{
    [SerializeField] private int damage;
    private PhotonView PV;
    [HideInInspector] public string bulletOwner = "";

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (bulletOwner == other.gameObject.GetComponent<PhotonView>().Owner.NickName)
                return;
            else
            {
                Debug.Log(bulletOwner + " : " + other.gameObject.GetComponent<PhotonView>().Owner.NickName);
                //Damage Health
                other.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
            }
            
        }        

        if(PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
