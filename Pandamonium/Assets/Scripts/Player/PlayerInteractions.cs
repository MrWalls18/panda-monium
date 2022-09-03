using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int throwablesLeft = 10;

    [SerializeField] private float forwardThrowForce = 10f;
    [SerializeField] private float upwardThrowForce = 2f;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;

    // Update is called once per frame
    void Update()
    {
        Throw();
    }

    

    void Throw()
    {
        if(Input.GetButtonDown("Fire1") && throwablesLeft > 0)
        {
            //Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
            //objectToThrow.transform.rotation = q * attackPoint.transform.rotation;

            //Spawn projectile and grab Rigidbody
            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, camera.rotation);            
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            //Calculate direction
            Vector3 forceDirection = camera.transform.forward;
            RaycastHit hit;

            if(Physics.Raycast(camera.position, camera.forward, out hit, Mathf.Infinity))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            //Calculate force of throw and apply to projectile
            Vector3 force = forceDirection * forwardThrowForce + transform.up * upwardThrowForce * Time.deltaTime;
            projectileRb.AddForce(force, ForceMode.Impulse);

            throwablesLeft--;
        }
    }

}
