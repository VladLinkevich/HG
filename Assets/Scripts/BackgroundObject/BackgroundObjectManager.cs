using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectManager : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private PlayerDirection _direction = PlayerDirection.Stop;
    private Vector2 _movement;
    private bool _rightMove;
    private bool _leftMove;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.RUNRIGHT, RightMove);
        Messenger.AddListener(GameEvent.RUNLEFT, LeftMove);
        Messenger.AddListener(GameEvent.STOPRIGHT, StopRightMove);
        Messenger.AddListener(GameEvent.STOPLEFT, StopLeftMove);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RUNRIGHT, RightMove);
        Messenger.RemoveListener(GameEvent.RUNLEFT, LeftMove);
        Messenger.RemoveListener(GameEvent.STOPRIGHT, StopRightMove);
        Messenger.RemoveListener(GameEvent.STOPLEFT, StopLeftMove);
    }

    private void Update()
    {
        switch (_direction)
        {
            case PlayerDirection.Stop: _movement = Vector2.zero; break;
            case PlayerDirection.Right: _movement = Vector2.right; break;
            case PlayerDirection.Left: _movement = Vector2.left; break;
            default: _movement = Vector2.zero; break;
        }

        transform.Translate(_movement * speed * Time.deltaTime);
    }

    private void RightMove()
    {
        _direction = PlayerDirection.Right;
        _rightMove = true;
    }
    private void LeftMove()
    {
        _direction = PlayerDirection.Left;
        _leftMove = true;
    }
    private void StopRightMove()
    {
        if (_leftMove)
        {
            _direction = PlayerDirection.Left;
        }
        else { _direction = PlayerDirection.Stop; }
        _rightMove = false;
    }
    private void StopLeftMove()
    {
        if (_rightMove)
        {
            _direction = PlayerDirection.Right;
        }
        else { _direction = PlayerDirection.Stop; }
        _leftMove = false;
    }
}
