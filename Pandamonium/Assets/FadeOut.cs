
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FadeOut : MonoBehaviour
{
    private Color alphaColor;
    private float timeBetweenFade = 1f;
    private PhotonView view;

    private void Awake()
    {
        alphaColor = GetComponent<Text>().color;
        view = GetComponent<PhotonView>();
    }

    void Start()
    {
        //Fade(timeBetweenFade);
    }

    void Fade(float timer)
    {
        if(timer > 0f)
        {
            Fade(timer - Time.deltaTime);
        }
        alphaColor.a -= 0.1f;
        if (alphaColor.a <= 0f)
        {
            if(view.IsMine)
                PhotonNetwork.Destroy(this.gameObject);

        }
        else
            Fade(timeBetweenFade);
    }
}
