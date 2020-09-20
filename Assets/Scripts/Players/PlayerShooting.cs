using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private ShootgunBullet bullet;
    [SerializeField] private Transform spawnPos;

    private Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Messenger<Transform>.Broadcast(GameEvent.SHOOT, spawnPos);
            Messenger.Broadcast(GameEvent.SHOOTPLAYER);
        }
    }

}
