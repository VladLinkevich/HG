using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static FollowCam instance;

    [SerializeField] private float smoothTime = 0.2f; 

    private Transform target;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position,
            targetPosition, ref _velocity, smoothTime);
    }

    public void SetTargetTransform(Transform target)
    {
        this.target = target;
    }
}
