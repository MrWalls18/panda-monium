using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int _health;
    public int Health
    {
        get {
            return _health;
        }
        set {
            _health = value;
        }
    }

    [SerializeField]
    private float _speed;
    public float Speed
    {
        get {
            return _speed;
        }
        set {
            _speed = value;
        }
    }

    [SerializeField]
    private float _jumpHeight;
    public float JumpHeight
    {
        get {
            return _jumpHeight;
        }
    }

    [SerializeField]
    private bool _hasDoubleJump;
    public bool HasDoubleJump
    {
        get {
            return _hasDoubleJump;
        }
        set {
            _hasDoubleJump = value;
        }
    }

    [SerializeField]
    private bool _isPlayerGrounded;
    public bool IsPlayerGrounded
    {
        get {
            return _isPlayerGrounded;
        }
        set {
            _isPlayerGrounded = value;
        }
    }

    [SerializeField] private int _maxBullets;

    [SerializeField]
    private int _bulletsLeft;
    public int BulletsLeft
    {
        get {
            return _bulletsLeft;
        }
        set {
            _bulletsLeft = value;
            if (_bulletsLeft > _maxBullets)
            {
                _bulletsLeft = _maxBullets;
            }
        }
    }

    [SerializeField]
    private bool _hasSpeedBoost;
    public bool HasSpeedBoost
    {
        get {
            return _hasSpeedBoost;
        }
        set {
            _hasSpeedBoost = value;
        }
    }
}