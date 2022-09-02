using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        
        Destroy(this.gameObject);
    }
}
