using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ImagePacker : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _SpriteAtlas;
    [SerializeField] private Image _Image;
    [SerializeField] private string _SpriteName;

    private void Awake()
    {
        _Image.sprite = _SpriteAtlas.GetSprite(_SpriteName);
    }
}
