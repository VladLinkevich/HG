﻿using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody2D))]
public class StickmanController : MonoBehaviour
{
    #region SerializeField private
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float rocketJump = 1f;
    [SerializeField] private float checkGroundRadius = 0.05f;
    [SerializeField] private float rememberGroundedFor = 0.08f;
    [SerializeField] private float forseForGun = 50f;
    [SerializeField] private Transform isGroundedChecker;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    #region Private value
    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
    private Rigidbody2D _body;
    private PhotonView _photonView;
    private PlayerDirection _direction = PlayerDirection.Stop;
    private Vector2 _force;
    private Vector2 _movement;
    private Vector2 _velocity;
    private float lastTimeGrounded;

    private bool _isGrounded;
    private bool _rightMove;
    private bool _leftMove;
    #endregion

    #region MonoBehavior Callbacks
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            Messenger.AddListener(GameEvent.SHOOTPLAYER, Shooting);
            Messenger.AddListener(GameEvent.STARTRELOAD, Reload);

            Messenger.AddListener(GameEvent.RUNRIGHT, RunRight);
            Messenger.AddListener(GameEvent.RUNLEFT, RunLeft);
            Messenger.AddListener(GameEvent.STOPRIGHT, StopRight);
            Messenger.AddListener(GameEvent.STOPLEFT, StopLeft);
            Messenger.AddListener(GameEvent.FALL, Fall);
            Messenger.AddListener(GameEvent.JUMP, Jump);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine) {
            FollowCam.instance.SetTargetTransform(this.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border") {
            _collider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _collider.isTrigger = false;
        }

        if (collision.tag == "Border")
        {
            _collider.isTrigger = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        CheckIfGrounded();
    }

    #endregion

    #region Private methods
    private void Move()
    {

        switch (_direction)
        {
            case PlayerDirection.Stop: _body.velocity = new Vector2(0, _body.velocity.y); break;
            case PlayerDirection.Right: _body.velocity = new Vector2(speed, _body.velocity.y); break;
            case PlayerDirection.Left: _body.velocity = new Vector2(-speed, _body.velocity.y); break;
            default: _movement = Vector2.zero; break;
        }

        _animator.SetFloat("Speed", Mathf.Abs(_body.velocity.x));

    }

    private void Jump()
    {
        if ((_isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            _body.velocity = Vector2.up * jumpSpeed;
            _animator.SetTrigger("Jump");
        }
       
    }



    private void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null) { _isGrounded = true; }
        else
        {
            if (_isGrounded) { lastTimeGrounded = Time.time; }
            _isGrounded = false;

        }
    }

    private void ShootingAnimation()
    {
        _animator.SetTrigger("Shoot");
    }

    #endregion

    #region Messenger methods
    private void RunRight()
    {
        _direction = PlayerDirection.Right;
        if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 0f)) { transform.Rotate(0, 180f, 0); }
        _rightMove = true;
    }
    private void RunLeft()
    {
        _direction = PlayerDirection.Left;
        if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 180f)) { transform.Rotate(0, -180f, 0); }
        _leftMove = true;
    }
    private void StopRight()
    {
        if (_leftMove) 
        {
            if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 180f)) { transform.Rotate(0, -180f, 0); }
            _direction = PlayerDirection.Left;
        } 
        else { _direction = PlayerDirection.Stop; }
        _rightMove = false;
    }
    private void StopLeft()
    {
        if (_rightMove) 
        {
            if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 0f)) { transform.Rotate(0, 180f, 0); }
            _direction = PlayerDirection.Right;
        }
        else { _direction = PlayerDirection.Stop; }
        _leftMove = false;
    }
    private void Fall()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            if (collider.tag == "Platform")
                _collider.isTrigger = true;
        }
    }

    private void CompleteReload()
    {
        _animator.SetBool("Reload", false);

    }

    private void Reload()
    {
        _animator.SetBool("Reload", true);
    }
    private void Shooting()
    {
        if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 0f))
        {
            _body.AddForce(new Vector2(forseForGun * _body.mass, 0));
        }
        else { _body.AddForce(new Vector2(-forseForGun * _body.mass, 0)); }
        ShootingAnimation();
        Reload();
    }

    #endregion

    #region IEnumerator
    private IEnumerator movingDown()
    {
        _collider.isTrigger = true;
        yield return new WaitForSeconds(0.25f);
        _collider.isTrigger = false;

    }
    #endregion

}