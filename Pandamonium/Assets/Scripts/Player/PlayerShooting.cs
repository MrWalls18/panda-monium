using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField] private Transform cameraPositionRef;

    [Header("Shooting Variables")]
    [SerializeField] private int MAX_SPREAD;
    private GameObject bulletImpactClone;
    private int currentSpread;
    private int shotOriginX, shotOriginY;
    private float timerForContShooting;
    private float timeBetweenShots;
    private float timePassed;
    private Vector3 offsetSpread;

    PhotonView view;

    private void Awake() 
    {
        playerStats = GetComponent<PlayerStats>();
        view = GetComponent<PhotonView>();
        timerForContShooting = 0;
        timeBetweenShots = 0.10f;
        timePassed = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(Input.GetButton("Fire1") && playerStats.BulletsLeft > 0)
        {
            if (timePassed < timeBetweenShots)
            {
                timePassed += Time.deltaTime;
                return;
            }

            //Calculate the spread of the bullets, dependent on  how long the player has been holding down the shoot button
            currentSpread = (int)(timerForContShooting / 10 * MAX_SPREAD);
            if (currentSpread > MAX_SPREAD)
                currentSpread = MAX_SPREAD;

            //Assigns new random origin point based on the current spread
            shotOriginX = Random.Range(-currentSpread, currentSpread);
            shotOriginY = Random.Range(-currentSpread, currentSpread);
            offsetSpread = new Vector3(shotOriginX, shotOriginY);

            RaycastHit hit;
            if(Physics.Raycast(cameraPositionRef.position + offsetSpread, cameraPositionRef.forward, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit " + hit.collider.name);

                if(hit.collider.gameObject.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(18, view.Owner);
                }
                else
                {
                    //PhotonNetwork spawn GO BulletImpact Prefab and point of impact
                    bulletImpactClone = GameManager.Instance.SpawnBulletImpact();
                    bulletImpactClone.transform.position = hit.point;
                    bulletImpactClone.transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);

                }
            }


            timerForContShooting += Time.deltaTime;
            playerStats.BulletsLeft--;
            timePassed = 0f;
        }

        else
        {
            timerForContShooting -= Time.deltaTime * 2;

            if (timerForContShooting < 0)
                timerForContShooting = 0;
        }

        
    }


}