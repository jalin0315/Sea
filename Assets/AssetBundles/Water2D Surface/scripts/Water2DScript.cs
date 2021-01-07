// 2016 - Damien Mayance (@Valryon)
// Source: https://github.com/valryon/water2d-unity/
using UnityEngine;

/// <summary>
/// Water surface script (update the shader properties).
/// </summary>
public class Water2DScript : MonoBehaviour
{
    [SerializeField] private Vector2 _Speed;
    [SerializeField] private Renderer _Renderer;
    private Material _Material;
    private Vector2 _Scroll;

    private void Start()
    {
        _Material = _Renderer.material;
    }

    private void Update()
    {
        _Scroll = CTJ.TimeSystem._DeltaTime() * _Speed;
        _Material.mainTextureOffset += _Scroll;
    }
}