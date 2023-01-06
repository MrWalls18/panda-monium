using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactFade : MonoBehaviour
{
    [SerializeField] private float timeToFade;
    [SerializeField] private float fadeTimer;
    private void Awake()
    {
        fadeTimer = timeToFade;
    }

    private void OnEnable()
    {
        fadeTimer = timeToFade;
    }

    private void FixedUpdate()
    {
        if (fadeTimer >= 0)
        {
            this.gameObject.SetActive(true);
            fadeTimer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
