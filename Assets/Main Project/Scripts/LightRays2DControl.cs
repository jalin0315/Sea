using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRays2DControl : MonoBehaviour
{
    public static LightRays2DControl _Instance;
    [SerializeField] private bool _Enable;
    [SerializeField] private GameObject _Object_LightRaysCanvas;
    [SerializeField] private RectTransform _RectTransform;
    [SerializeField] private Vector2 _OriginalPos;
    [SerializeField] private float _UpperLimit;
    [SerializeField] private float _Speed;
    private float _SpeedRate;
    private float _Y;

    private void Awake()
    {
        _Instance = this;
    }

    public void Initialization(bool _enable)
    {
        _Enable = _enable;
        if (!_Enable) { _Object_LightRaysCanvas.SetActive(false); return; }
        else _Object_LightRaysCanvas.SetActive(true);
        _SpeedRate = GameManager._Instance._Time / 5;
        _Y = _OriginalPos.y;
        _RectTransform.position = new Vector3(_RectTransform.position.x, _Y, _RectTransform.position.z);
    }
    private void Start()
    {
        Initialization(true);
    }

    private void Update()
    {
        if (!GameManager._Instance._InGame) return;
        if (!_Enable) return;
        if (_RectTransform.position.y > _UpperLimit)
        {
            _Object_LightRaysCanvas.SetActive(false);
            _Enable = false;
            return;
        }
        _Y += CTJ.TimeSystem._DeltaTime() * _Speed * _SpeedRate;
        _RectTransform.position = new Vector3(_RectTransform.position.x, _Y, _RectTransform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector2 _upper_limit = new Vector2(_RectTransform.position.x, _UpperLimit);
        Gizmos.DrawWireSphere(_upper_limit, 10.0f);
    }
}
