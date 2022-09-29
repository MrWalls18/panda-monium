using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField] private Transform cameraPositionRef;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;

    PhotonView PV;

    private void Awake() 
    {
        playerStats = GetComponent<PlayerStats>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            Throw();
        }
    }

    void Throw()
    {
        if(Input.GetButtonDown("Fire1") && playerStats.ThrowablesLeft > 0)
        {
            //Spawn projectile and grab Rigidbody
            GameObject projectile = PhotonNetwork.Instantiate(objectToThrow.name, attackPoint.position, cameraPositionRef.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectile.GetComponent<Rock>().bulletOwner = PV.Owner;

            //Calculate direction
            Vector3 forceDirection = cameraPositionRef.forward;
            RaycastHit hit;
            if(Physics.Raycast(cameraPositionRef.position, cameraPositionRef.forward, out hit, Mathf.Infinity))
            {
                forceDirection = (hit.point - attackPoint.position);
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            //Calculate force of throw and apply to projectile
            Vector3 force = forceDirection * playerStats.ForwardThrowForce + transform.up * playerStats.UpwardThrowForce;
            projectileRb.AddForce(force, ForceMode.Impulse);

            playerStats.ThrowablesLeft--;
        }
    }

}