using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private float speed = 2f;

    private bool _left;
    private bool _right;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.RUNRIGHT, RightMove);
        Messenger.AddListener(GameEvent.RUNLEFT, LeftMove);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RUNRIGHT, RightMove);
        Messenger.RemoveListener(GameEvent.RUNLEFT, LeftMove);
    }

    private void Update()
    {
        Move()
    }

    private void RightMove()
    {
        background.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    private void LeftMove()
    {
        background.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
