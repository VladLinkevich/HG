using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private ShootgunBullet bullet;
    [SerializeField] private Transform spawnPos;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            Messenger.AddListener(GameEvent.SHOOTMESSENGER, Shoot);
        }
    }

    private void OnDestroy()
    {
        if (_photonView.IsMine)
        {
            Messenger.RemoveListener(GameEvent.SHOOTMESSENGER, Shoot);
        }
    }

    private void Shoot()
    {
        Messenger<Transform>.Broadcast(GameEvent.SHOOT, spawnPos);
        Messenger.Broadcast(GameEvent.SHOOTPLAYER);
    }
}
