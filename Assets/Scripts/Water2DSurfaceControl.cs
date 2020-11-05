using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water2DSurfaceControl : MonoBehaviour
{
    [SerializeField] private Renderer _Renderer;
    private Material _Material;
    [SerializeField] private Vector2 _MainTex;
    [SerializeField] private float _Magnitude;

    private void Start()
    {
        _Material = _Renderer.material;
        _MainTex = _Material.GetTextureScale("_MainTex");
        _Magnitude = _Material.GetFloat("_Magnitude");
    }

    private void Update()
    {
        _Material.SetTextureScale("_MainTex", _MainTex);
        _Material.SetFloat("_Magnitude", _Magnitude);
    }
}
