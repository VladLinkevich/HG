using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{

    [SerializeField] private float leftBorder = -1800;
    [SerializeField] private float rightBorder = 1800;
    [SerializeField] private float speed = 3;
    [SerializeField] private float maxSpeed = 0.03f;
    [SerializeField] private float minSpeed = 0.002f;
    [SerializeField] private float rangeSpeed = 0.001f;
    [SerializeField] private bool isRight = true;

    private void FixedUpdate()
    {
        speed += Random.Range(-rangeSpeed, rangeSpeed);
        if (speed < minSpeed) { speed = minSpeed; }
        if (speed > maxSpeed) { speed = maxSpeed; }
        transform.Translate(isRight ? speed : -speed, 0, 0);
        if (transform.position.x < leftBorder) { isRight = true; }
        if (transform.position.x > rightBorder) { isRight = false; }
    }
}
