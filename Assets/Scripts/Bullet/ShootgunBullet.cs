using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunBullet : Bullet
{
    [SerializeField] private int numberRounds = 7;
    [SerializeField] private Ease ease = Ease.Flash;

    private GameObject _bullet;
    private float _angels;

    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.SHOOT, Shooting);
    }

    private void OnDestroy()
    {
        Messenger<Transform>.RemoveListener(GameEvent.SHOOT, Shooting);
    }

    public void Shooting(Transform spawnPos)
    {
        for (int i = 0, end = numberRounds; i < end; ++i)
        {
            _bullet = PoolBullets[i];
            _bullet.SetActive(true);
            _bullet.transform.position = spawnPos.position;
            if (Mathf.Approximately(spawnPos.rotation.eulerAngles.y, 0))
            {
                _angels = spawnPos.rotation.eulerAngles.z + UnityEngine.Random.Range(-spread, spread);
            }
            else
            {
                _angels = (180f - spawnPos.rotation.eulerAngles.z) + UnityEngine.Random.Range(-spread, spread);
            }

            _bullet.transform.DORotate(new Vector3(0, 0, _angels), 0);

            _bullet.transform.DOMove(new Vector3(
                    spawnPos.position.x + (lenghtShooting * Mathf.Cos(_angels * Mathf.Deg2Rad)),
                    spawnPos.position.y + (lenghtShooting * Mathf.Sin(_angels * Mathf.Deg2Rad)),
                    0), durationBullet * UnityEngine.Random.Range(0.95f, 1.05f))
                .SetEase(ease)
                .OnComplete(DeactiveBullet);
        }
    }

    private void DeactiveBullet()
    {
        for (int i = 0, end = numberRounds; i < end; ++i)
        {
            PoolBullets[i].SetActive(false);
        }
    }
}
