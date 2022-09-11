using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;        
    private PlayerStats playerStats;

    [Header("Physics of Player")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 velocity;

    private PhotonView view;

    private void Awake() {
        
        playerStats = GetComponent<PlayerStats>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            CheckForGround();
            MovePlayer();
            Jump();
            AddGravity();
        }
        
    }

    void CheckForGround()
    {
        //Cast physics sphere towards bottom of play to check for ground
        playerStats.IsPlayerGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Resets falling velocity if player is grounded
        if(playerStats.IsPlayerGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;        
        controller.Move(move * playerStats.Speed * Time.deltaTime);
    }

    void Jump()
    {
        //Checks if player can jump
        if(Input.GetButtonDown("Jump") && (playerStats.IsPlayerGrounded || playerStats.HasDoubleJump))
        {
            velocity.y = Mathf.Sqrt(playerStats.JumpHeight * -2 * gravity);

            //Resets double jump
            if (!playerStats.IsPlayerGrounded)
            {
                playerStats.HasDoubleJump = false;
            }

        }
    }

    void AddGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
