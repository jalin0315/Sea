using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpritePacker : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _SpriteAtlas;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private string _SpriteName;

    private void Awake()
    {
        _SpriteRenderer.sprite = _SpriteAtlas.GetSprite(_SpriteName);
    }
}
