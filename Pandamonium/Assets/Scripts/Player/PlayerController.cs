using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
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
    private PlayerManager playerManager;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        view = GetComponent<PhotonView>();

        //Finds player manager in scene
        playerManager = PhotonView.Find((int)view.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start() 
    {
        if (!view.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
    }

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

#region Movements
    void CheckForGround()
    {
        //Cast physics sphere towards bottom of player to check for ground
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
            //Physics of a jump
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
#endregion

#region Damage/Death RPC
    public void TakeDamage(int damage, Player player)
    {
        view.RPC(nameof(RPC_TakeDamage), view.Owner, damage, player);
    }

    [PunRPC]
    void RPC_TakeDamage(int damage, Player shooter)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            //creates KillFeed item
            GameManager.Instance.KillFeed(shooter, view.Owner.NickName);

            //Give the kill to the shooter
            PlayerManager.Find(shooter).GetKill();

            DisablePlayer();

            GetComponent<PlayerUIManager>().DeathScreen();
        }
    }

    public void DisablePlayer()
    {
        view.RPC(nameof(RPC_DisablePlayer), RpcTarget.All);
    }

    [PunRPC]
    void RPC_DisablePlayer()
    {
        GetComponent<CharacterController>().enabled = false;
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        if (mesh != null)
            mesh.enabled = false;   
    }
#endregion

    //Gets called when player presses the respawn button
    public void Die()
    {
        playerManager.Die();
    }

}
