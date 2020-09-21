using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public float durationBullet = 2;
    public float poolSize = 10;
    public float lenghtShooting = 10;
    public Transform _spawnPos;
    public float spread = 0.25f;


    private List<GameObject> _poolBullets;
    private GameObject _bullet;

    public List<GameObject> PoolBullets
    {
        get { return _poolBullets; }
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
}
