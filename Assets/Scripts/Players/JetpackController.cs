using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleFire;

    [SerializeField] float maxFuel = 100f;
    [SerializeField] float maxSpeedY = 100f;
    [SerializeField] float rocketJump = 0.4f;
    [SerializeField] float eatFuelInFrame = 0.5f;

    private Rigidbody2D _body;
    private float _fuel = 0;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _fuel = maxFuel;
    }

    private void FixedUpdate()
    {
        RocketJump();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _fuel > 0)
        {
            particleFire.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space) || _fuel <= 0)
        {
            particleFire.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Refill"){ _fuel = maxFuel; }
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
                    if (Mathf.Abs(_body.velocity.y) > maxSpeedY)
                    {
                        _body.velocity = new Vector2(_body.velocity.x, maxSpeedY);
                    }
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
