using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour
{
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float rocketJump = 0.4f;
    [SerializeField] float eatFuelInFrame = 0.5f;

    private Rigidbody2D _body;
    private float _fuel = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.STARTFILLUP, StartFillUp);
    }

    private void OnDestroy()
    {
        
    }
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RocketJump();
    }

    private void StartFillUp()
    {
        _fuel = maxFuel;
    }

    private void RocketJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_fuel > 0)
            {
                if (_body.velocity.y > 0)
                {
                    _body.velocity += Vector2.up * rocketJump;
                }
                else
                {
                    _body.velocity += Vector2.up * rocketJump * 4;
                }
                _fuel -= eatFuelInFrame;
            }
        }
    }
}
