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
            if (_health <= 0)
            {
                GameManager.Instance.RespawnPlayer();
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [SerializeField]
    private float _speed = 12f;
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
    private float _jumpHeight = 3f;
    public float JumpHeight
    {
        get {
            return _jumpHeight;
        }
    }

    [SerializeField]
    private bool _hasDoubleJump = true;
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
    private bool _isPlayerGrounded = false;
    public bool IsPlayerGrounded
    {
        get {
            return _isPlayerGrounded;
        }
        set {
            _isPlayerGrounded = value;
        }
    }

    [SerializeField]
    private float _forwardThrowForce;
    public float ForwardThrowForce
    {
        get {
            return _forwardThrowForce;
        }
    }

    [SerializeField]
    private float _upwardThrowForce;
    public float UpwardThrowForce
    {
        get {
            return _upwardThrowForce;
        }
    }

    [SerializeField] private int _maxThrowables;

    [SerializeField]
    private int _throwablesLeft;
    public int ThrowablesLeft
    {
        get {
            return _throwablesLeft;
        }
        set {
            _throwablesLeft = value;
            if (_throwablesLeft > _maxThrowables)
            {
                _throwablesLeft = _maxThrowables;
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
