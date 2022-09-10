using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteractions : MonoBehaviour
{
    private Player playerStats;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;

    private void Awake() 
    {
        playerStats = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Throw();
    }

    

    void Throw()
    {
        if(Input.GetButtonDown("Fire1") && playerStats.ThrowablesLeft > 0)
        {
            //Spawn projectile and grab Rigidbody
            GameObject projectile = PhotonNetwork.Instantiate(objectToThrow.name, attackPoint.position, camera.rotation);            
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            //Calculate direction
            Vector3 forceDirection = camera.transform.forward;
            RaycastHit hit;

            if(Physics.Raycast(camera.position, camera.forward, out hit, Mathf.Infinity))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            //Calculate force of throw and apply to projectile
            Vector3 force = forceDirection * playerStats.ForwardThrowForce + transform.up * playerStats.UpwardThrowForce * Time.deltaTime;
            projectileRb.AddForce(force, ForceMode.Impulse);

            playerStats.ThrowablesLeft--;
        }
    }

}
