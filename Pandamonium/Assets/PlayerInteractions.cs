using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public int throwablesLeft = 10;

    public float forceThrow = 10f;
    public float forceThrowUp = 2f;

    public Transform camera;
    public Transform attackPoint;
    public GameObject objectToThrow;

    // Update is called once per frame
    void Update()
    {
        PickUp();
        Throw();
    }

    void PickUp()
    {

    }

    void Throw()
    {
        if(Input.GetButtonDown("Fire1") && throwablesLeft > 0)
        {
            Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
            objectToThrow.transform.rotation = q * attackPoint.transform.rotation;

            //Spawn projectile and grab Rigidbody
            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, q);            
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            //Calculate direction
            Vector3 forceDirection = camera.transform.forward;
            RaycastHit hit;

            if(Physics.Raycast(camera.position, camera.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            //Calculate force of throw and apply to projectile
            Vector3 force = forceDirection * forceThrow + transform.up * forceThrowUp * Time.deltaTime;
            projectileRb.AddForce(force, ForceMode.Impulse);

            throwablesLeft--;
        }
    }

}
