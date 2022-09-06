using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Damage Health
            other.gameObject.GetComponent<Player>().Health -= 10;
        }
        
        Destroy(this.gameObject);
    }
}
