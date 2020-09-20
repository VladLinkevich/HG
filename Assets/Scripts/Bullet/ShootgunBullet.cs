using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunBullet : Bullet
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float durationBullet = 2;
    [SerializeField] private float poolSize = 10;
    [SerializeField] private float lenghtShooting = 10;
    [SerializeField] private int numberRounds = 7;
    [SerializeField] private float spread = 0.25f;
    [SerializeField] private Ease ease = Ease.Flash;
    [SerializeField] private Transform _spawnPos;

    private List<GameObject> _poolBullets;
    private GameObject _bullet;
    private float _angels;


    private void Awake()
    {
        Messenger<Transform>.AddListener(GameEvent.SHOOT, Shooting);
    }

    private void Start()
    {
        _poolBullets = new List<GameObject>();
        for (int i = 0; i < poolSize; ++i)
        {
            _bullet = Instantiate(bullet);
            _bullet.SetActive(false);
            _poolBullets.Add(_bullet);
        }
    }

    public void Shooting(Transform spawnPos)
    {
        for (int i = 0, end = numberRounds; i < end; ++i)
        {
            _bullet = _poolBullets[i];
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
                    0), durationBullet)
                .SetEase(ease)
                .OnComplete(DeactiveBullet);
        }
    }

    private void DeactiveBullet()
    {
        for (int i = 0, end = numberRounds; i < end; ++i)
        {
            _poolBullets[i].SetActive(false);
        }
    }
}
