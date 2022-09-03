using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int _health = 100;
    public int Health
    {
        get {
            return _health;
        }
        set {
            _health = value;
        }
    }

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

    private float _jumpHeight = 3f;
    public float JumpHeight
    {
        get {
            return _jumpHeight;
        }
    }

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

    float forwardThrowForce, upwardThrowForce;

}
