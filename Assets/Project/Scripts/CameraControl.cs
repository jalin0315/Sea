using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class CameraControl : MonoBehaviour
    {
        public static CameraControl _Instance;
        public enum Status
        {
            Free,
            Lock
        }
        [SerializeField] private Status _Status;
        // 原始 Size 6.5 最大 10
        private static Camera _Camera;
        [SerializeField] private Transform _Transform;
        public static Vector2 _Origin => _Camera.ScreenToWorldPoint(Vector2.zero);
        public static Vector2 _Vertex => _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
        [SerializeField] private Transform _Target;
        [SerializeField] private Transform _UpBoundary;
        [SerializeField] private Transform _DownBoundary;
        [SerializeField] private Transform _LeftBoundary;
        [SerializeField] private Transform _RightBoundary;
        private Vector2 _UpCenter => new Vector2((_Origin.x + _Vertex.x) * 0.5f, _Vertex.y);
        private Vector2 _DownCenter => new Vector2((_Origin.x + _Vertex.x) * 0.5f, _Origin.y);
        private Vector2 _LeftCenter => new Vector2(_Origin.x, (_Origin.y + _Vertex.y) * 0.5f);
        private Vector2 _RightCenter => new Vector2(_Vertex.x, (_Origin.y + _Vertex.y) * 0.5f);
        [SerializeField] private Collider2D _Collider2D_Up;
        [SerializeField] private Collider2D _Collider2D_Down;
        [SerializeField] private Collider2D _Collider2D_Left;
        [SerializeField] private Collider2D _Collider2D_Right;
        private float _MaxMeter;
        // Variable
        private Vector3 _variable_Vector3;

        public void Initialization()
        {
            _MaxMeter = 3.5f / GameManager._MaxMeter;
            _Camera.orthographicSize = GameManager._Meter * _MaxMeter + 6.5f;
            _variable_Vector3.x = _Target.position.x;
            _variable_Vector3.y = Vector2.zero.y;
            _variable_Vector3.z = _Transform.position.z;
            _Transform.position = _variable_Vector3;
            StatusChange(Status.Free);
        }

        private void Awake()
        {
            _Instance = this;
            _Camera = GetComponent<Camera>();
        }

        private void Start()
        {
            Initialization();
        }

        private void Update()
        {
            if (GameManager._Meter <= GameManager._MaxMeter) _Camera.orthographicSize = GameManager._Meter * _MaxMeter + 6.5f;
            StatusUpdate();
        }

        private void FixedUpdate()
        {
            StatusFixedUpdate();
        }

        public void StatusChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Free:
                    _UpBoundary.position = _UpCenter;
                    _DownBoundary.position = _DownCenter;
                    _LeftBoundary.position = _LeftCenter;
                    _RightBoundary.position = _RightCenter;
                    _Collider2D_Up.enabled = true;
                    _Collider2D_Down.enabled = true;
                    _Collider2D_Left.enabled = false;
                    _Collider2D_Right.enabled = false;
                    break;
                case Status.Lock:
                    _UpBoundary.position = _UpCenter;
                    _DownBoundary.position = _DownCenter;
                    _LeftBoundary.position = _LeftCenter;
                    _RightBoundary.position = _RightCenter;
                    _Collider2D_Up.enabled = true;
                    _Collider2D_Down.enabled = true;
                    _Collider2D_Left.enabled = true;
                    _Collider2D_Right.enabled = true;
                    break;
            }
        }

        private void StatusUpdate()
        {
            switch (_Status)
            {
                case Status.Free:
                    _UpBoundary.position = _UpCenter;
                    _DownBoundary.position = _DownCenter;
                    break;
            }
        }
        private void StatusFixedUpdate()
        {
            switch (_Status)
            {
                case Status.Free:
                    _variable_Vector3.x = _Target.position.x;
                    _variable_Vector3.y = Vector2.zero.y;
                    _variable_Vector3.z = transform.position.z;
                    _Transform.position = Vector3.Lerp(transform.position, _variable_Vector3, TimeSystem._FixedDeltaTime() * 15.0f);
                    break;
            }
        }
    }
}
