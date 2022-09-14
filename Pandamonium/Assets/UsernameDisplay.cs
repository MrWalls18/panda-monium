using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] private PhotonView view;
    [SerializeField] private Text usernameText;
    Camera cam;

    private void Awake()
    {
        usernameText.text = view.Owner.NickName;
        if(view.IsMine)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        FaceCamera();        
    }

    void FaceCamera()
    {
        if (cam == null)
            cam = FindObjectOfType<Camera>();
        if (cam == null)
            return;

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
