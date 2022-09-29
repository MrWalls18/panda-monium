using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LookAround : MonoBehaviour
{
    private static string mouseSensPrefKey = "Sensitivity"; 

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    PhotonView view;

    void Start()
    {
        if(PlayerPrefs.HasKey(mouseSensPrefKey))
        {
            mouseSensitivity = PlayerPrefs.GetFloat(mouseSensPrefKey);
            sensitivitySlider.value = mouseSensitivity;
        }

        Cursor.lockState = CursorLockMode.Locked;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            Look();
            MouseSensitivitySlider();
        }
    }

    void MouseSensitivitySlider()
    {
        sensitivitySlider.onValueChanged.AddListener((v) =>
        {
            mouseSensitivity = v;
            PlayerPrefs.SetFloat(mouseSensPrefKey, v);
        });
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 30f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
