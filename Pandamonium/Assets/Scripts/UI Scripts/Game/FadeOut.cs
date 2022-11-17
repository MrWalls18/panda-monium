
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FadeOut : MonoBehaviourPunCallbacks
{
    private PhotonView view;


    private void Awake()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            StartCoroutine(DestroyAfterSeconds());
        }
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(5f);

        PhotonNetwork.Destroy(gameObject);
    }

}
