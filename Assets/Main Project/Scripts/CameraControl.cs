using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl _Instance;
    // 原始 Size 6.5 最大 10
    public Camera _Camera;
    public Transform _Transform_Camera;
    [HideInInspector] public Vector2 _Origin() { return _Camera.ScreenToWorldPoint(Vector2.zero); }
    [HideInInspector] public Vector2 _Vertex() { return _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight)); }
    [SerializeField] private Transform _Target;
    [SerializeField] private Transform _UpperBoundary;
    [SerializeField] private Transform _LowerBoundary;
    private Vector2 _UpperCenter() { return new Vector2((_Origin().x + _Vertex().x) / 2, _Vertex().y); }
    private Vector2 _LowerCenter() { return new Vector2((_Origin().x + _Vertex().x) / 2, _Origin().y); }
    private float _MaxMeter;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _MaxMeter = 3.5f / GameManager._Instance._MaxMeter;
    }

    private void Update()
    {
        if (!GameManager._Instance._InGame) return;
        if (GameManager._Instance._Result <= GameManager._Instance._MaxMeter) _Camera.orthographicSize = GameManager._Instance._Result * _MaxMeter + 6.5f;
        _UpperBoundary.position = _UpperCenter();
        _LowerBoundary.position = _LowerCenter();
    }

    private void FixedUpdate()
    {
        if (!GameManager._Instance._InGame) return;
        //transform.position = Vector3.Lerp(transform.position, new Vector3(_Target.position.x + (MovementSystem._Instance._Rigidbody2D.velocity.x * 2.5f), transform.position.y, transform.position.z), Time.deltaTime * 2.5f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_Target.position.x, transform.position.y, transform.position.z), 1.0f);
    }
}
