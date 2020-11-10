using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl _Instance;
    // 原始 Size 6.5 最大 10
    [SerializeField] private Camera _Camera;
    public Transform _Transform_Camera;
    private float _MaxMeter;
    private float _OriginalCameraSize;
    [SerializeField] private Transform _Target;
    [SerializeField] private Transform _UpperBoundary;
    [SerializeField] private Transform _LowerBoundary;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _MaxMeter = 3.5f / GameManager._Instance._MaxMeter;
        _OriginalCameraSize = _Camera.orthographicSize;
    }

    private void Update()
    {
        if (GameManager._Instance._InGame)
        {
            Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
            Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
            Vector2 _upper_center = new Vector2((_origin.x + _vertex.x) / 2, _vertex.y);
            Vector2 _lower_center = new Vector2((_origin.x + _vertex.x) / 2, _origin.y);
            _UpperBoundary.position = _upper_center;
            _LowerBoundary.position = _lower_center;
            if (GameManager._Instance._Result <= GameManager._Instance._MaxMeter)
            {
                _Camera.orthographicSize = GameManager._Instance._Result * _MaxMeter + 6.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager._Instance._InGame) return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(_Target.position.x + (MovementSystem._Instance._Rigidbody2D.velocity.x * 2.5f), transform.position.y, transform.position.z), Time.deltaTime * 2.5f);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(_Target.position.x, transform.position.y, transform.position.z), 1.0f);
    }
}
