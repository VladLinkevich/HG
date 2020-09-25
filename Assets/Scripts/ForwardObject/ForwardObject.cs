using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]

public class ForwardObject : MonoBehaviour
{
    [SerializeField] private string changeAlphaChanelTag;

    private SpriteRenderer _sprite;
    private string _id;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == changeAlphaChanelTag)
        {
            _sprite.DOColor(new Color(1f, 1f, 1f, 0.5f), 1f);
            _id = collision.gameObject.name;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_id == collision.gameObject.name)
        {
            _sprite.DOColor(new Color(1f, 1f, 1f, 1f), 1f);
        }
    }

}
