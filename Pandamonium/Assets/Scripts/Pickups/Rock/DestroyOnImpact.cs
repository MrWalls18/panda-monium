using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : Player
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Damage Health
            Health -= 10;
        }
        
        Destroy(this.gameObject);
    }
}
