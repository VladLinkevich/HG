using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float checkGroundRadius = 0.05f;
    [SerializeField] private float rememberGroundedFor = 0.08f;
    [SerializeField] private Transform isGroundedChecker;
    [SerializeField] private LayerMask groundLayer;

    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
    private Rigidbody2D _body;
    private PlayerDirection _direction = PlayerDirection.Stop;
    private Vector2 _force;
    private Vector2 _movement;
    private float lastTimeGrounded;

    private bool _isGrounded;
    private bool _rightMove;
    private bool _leftMove;    

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SHOOTPLAYER, Shooting);
        Messenger.AddListener(GameEvent.STARTRELOAD, Reload);

        Messenger.AddListener(GameEvent.RUNRIGHT, RunRight);
        Messenger.AddListener(GameEvent.RUNLEFT, RunLeft);
        Messenger.AddListener(GameEvent.STOPRIGHT, StopRight);
        Messenger.AddListener(GameEvent.STOPLEFT, StopLeft);
        Messenger.AddListener(GameEvent.FALL, Fall);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isGrounded = true;
        }
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _collider.isTrigger = false;
    }

    private void Move()
    {

        if (_movement.x < 0)
        {
            if (_isGrounded) { _movement.x -= 1; }
            else { _movement.x -= 0.4f; }
        }

        switch (_direction)
        {
            case PlayerDirection.Stop: _movement = Vector2.zero; break;
            case PlayerDirection.Right: _movement = Vector2.right; break;
            case PlayerDirection.Left: _movement = Vector2.left; break;
            default: _movement = Vector2.zero; break;
        }

        _animator.SetFloat("Speed", Mathf.Abs(_movement.x));

        transform.Translate((_force + _movement) * speed * Time.deltaTime);



    }

    private void FixedUpdate()
    {
        if (_force.x < 0)
        {
            if (_isGrounded) { _force.x += ((_movement.x / 10) + 1f); }
            else { _force.x += ((_movement.x / 10) + 1f) / 2; }
        }
        else
        {
            _force = Vector2.zero;
        }
    }

    private void Jump()
    {
        CheckIfGrounded();
        if (Input.GetButtonDown("Jump") && (_isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            _body.velocity = Vector2.up * jumpSpeed;
            _animator.SetTrigger("Jump");
        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null) { _isGrounded = true; }
        else
        {
            if (_isGrounded) { lastTimeGrounded = Time.time; }
            _isGrounded = false;

        }
    }

    private void RunRight()
    {
        _direction = PlayerDirection.Right;
        if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 0f)) { transform.Rotate(0, 180f, 0); }
        _rightMove = true;
    }
    private void RunLeft()
    {
        _direction = PlayerDirection.Right;
        if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 180f)) { transform.Rotate(0, -180f, 0); }
        _leftMove = true;
    }
    private void StopRight()
    {
        if (_leftMove) { if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 180f)) { transform.Rotate(0, -180f, 0); } }
        else { _direction = PlayerDirection.Stop; }
        _rightMove = false;
    }
    private void StopLeft()
    {
        if (_rightMove) { if (!Mathf.Approximately(transform.rotation.eulerAngles.y, 0f)) { transform.Rotate(0, 180f, 0); } }
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
        _force += Vector2.left * 5;
        ShootingAnimation();
        Reload();
    }

    private void ShootingAnimation()
    {
        _animator.SetTrigger("Shoot");
    }

    private IEnumerator movingDown()
    {
        _collider.isTrigger = true;
        yield return new WaitForSeconds(0.25f);
        _collider.isTrigger = false;

    }
}