using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ForwardObject : MonoBehaviour
{

    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _sprite.DOColor(new Color(1f, 1f, 1f, 0.5f), 1f);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _sprite.DOColor(new Color(1f, 1f, 1f, 1f), 1f);
    }

}
