using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentChildren : MonoBehaviour
{
    private void Awake()
    {
        this.transform.DetachChildren();
    }
}
