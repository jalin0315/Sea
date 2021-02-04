using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class EnemyAI : MonoBehaviour
    {
        // --- 位置 ---
        public enum Screen
        {
            Up,
            Down,
            Left,
            Right
        }
        private Screen _Screen;
        // --- 位置 ---
        // --- 狀態 ---
        public enum Status
        {
            Patrol,
            SwimUp,
            SwimDown,
            SwimLeft,
            SwimRight,
            SwimLeftStyle,
            SwimRightStyle,
            Target,
            TargetLock
        }
        [HideInInspector] public Status _Status;
        // --- 狀態 ---
        [SerializeField] private Transform _Transform;
        private Vector3 _Scale;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        private Color _Color;
        [SerializeField] private List<Collider2D> _Collider2D;
        [HideInInspector] public Queue<GameObject> _Queue_GameObject = new Queue<GameObject>();
        [HideInInspector] public float _ScaleMagnification;
        [HideInInspector] public float _Speed;
        private float _RotateSpeed;
        // --- 巡邏 ---
        private GameObject _WaypointTarget;
        private float _PatrolTime;
        private float _PatrolIntervalTime;
        // --- 巡邏 ---
        private Transform _Player;
        public static Transform _Bait;
        private Vector3 _CurrentPlayerPosition;
        private float _DistanceUpdate;
        private float _TargetLockTime;
        private float _T_L_Timer;
        // --- 回收機制 ---
        [HideInInspector] public float _ActivityTime;
        private bool _Visible;
        [HideInInspector] public bool _FadeDisappear;
        // --- 回收機制 ---
        public static bool _Recycle;
        private int _variable_int;
        private Vector3 _variable_vector3;

        private void Awake()
        {
            _Scale = _Transform.localScale;
            _RotateSpeed = 10.0f;
        }

        private void OnEnable()
        {
            _Color = Color.white;
            _SpriteRenderer.color = _Color;
            for (int _i = 0; _i < _Collider2D.Count; _i++) _Collider2D[_i].enabled = true;
        }

        private void Update()
        {
            if (_Recycle) EnemyManager._Instance.RecycleAI(_Queue_GameObject, gameObject);
            Disappear();
        }

        private void FixedUpdate() => UpdateStatus();

        public void ScreenChange(Screen _screen)
        {
            _Transform.localScale = _Scale * _ScaleMagnification;
            _variable_vector3 = _Transform.localPosition;
            _Screen = _screen;
            switch (_Screen)
            {
                case Screen.Up:
                    break;
                case Screen.Down:
                    break;
                case Screen.Left:
                    _variable_vector3.x = _SpriteRenderer.bounds.min.x;
                    break;
                case Screen.Right:
                    _variable_vector3.x = _SpriteRenderer.bounds.max.x;
                    break;
            }
            _Transform.localPosition = _variable_vector3;
        }
        public void StateChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Patrol:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                        _variable_int = Random.Range(0, 2);
                        switch (_variable_int)
                        {
                            case 0:
                                EnemyManager._Instance.ReRandomWaypoints();
                                break;
                        }
                        _WaypointTarget = EnemyManager._Instance._Waypoints[Random.Range(0, EnemyManager._Instance._Waypoints.Count)];
                        _PatrolTime = Random.Range(10.0f, 20.0f);
                        _PatrolIntervalTime = 2.5f;
                    }
                    break;
                case Status.SwimUp:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimDown:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimLeft:
                    {
                        _variable_vector3.x = -_Scale.x;
                        _variable_vector3.y = _Scale.y;
                        _variable_vector3.z = 1.0f;
                        _Transform.localScale = _variable_vector3 * _ScaleMagnification;
                    }
                    break;
                case Status.SwimRight:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimLeftStyle:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimRightStyle:
                    {
                        _variable_vector3.x = -_Scale.x;
                        _variable_vector3.y = _Scale.y;
                        _variable_vector3.z = 1.0f;
                        _Transform.localScale = _variable_vector3 * _ScaleMagnification;
                    }
                    break;
                case Status.Target:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                        if (_Bait != null) _Transform.right = (_Bait.localPosition - _Transform.localPosition).normalized;
                        else
                        {
                            if (_Player == null) _Player = GameObject.FindGameObjectWithTag("Player").transform;
                            _Transform.right = (_Player.localPosition - _Transform.localPosition).normalized;
                        }
                        float _delta(float _value)
                        {
                            float _result = Mathf.DeltaAngle(0, _value);
                            return _result;
                        }
                        if (_delta(_Transform.eulerAngles.z) > 90.0f || _delta(_Transform.eulerAngles.z) < -90.0f) _Transform.localScale = new Vector3(_Transform.localScale.x, -_Scale.y * _ScaleMagnification, _Transform.localScale.z);
                        else _Transform.localScale = new Vector3(_Transform.localScale.x, _Scale.y * _ScaleMagnification, _Transform.localScale.z);
                    }
                    break;
                case Status.TargetLock:
                    {
                        _Transform.localScale = _Scale * _ScaleMagnification;
                        _DistanceUpdate = Random.Range(1.0f, 5.0f);
                        _TargetLockTime = Random.Range(5.0f, 10.0f);
                        _T_L_Timer = _TargetLockTime;
                        if (_Player == null)
                        {
                            _Player = GameObject.FindGameObjectWithTag("Player").transform;
                            _CurrentPlayerPosition = _Player.localPosition;
                        }
                        else _CurrentPlayerPosition = _Player.localPosition;
                    }
                    break;
            }
        }

        private float _delta(float _value) { return Mathf.DeltaAngle(0, _value); }
        private void UpdateStatus()
        {
            switch (_Status)
            {
                case Status.Patrol:
                    {
                        if (_PatrolTime > 0.0f)
                        {
                            _PatrolTime -= TimeSystem._FixedDeltaTime();
                            if (_PatrolIntervalTime > 0.0f)
                            {
                                _PatrolIntervalTime -= TimeSystem._FixedDeltaTime();
                                if (Vector2.Distance(_Transform.localPosition, _WaypointTarget.transform.position) > 1.0f)
                                {
                                    _Transform.localPosition = Vector2.MoveTowards(_Transform.localPosition, _WaypointTarget.transform.position, TimeSystem._FixedDeltaTime() * _Speed);
                                    _Transform.right = Vector2.Lerp(_Transform.right, (_WaypointTarget.transform.position - _Transform.localPosition).normalized, TimeSystem._FixedDeltaTime() * _RotateSpeed);
                                }
                                else _Transform.localPosition = Vector2.Lerp(_Transform.localPosition, _WaypointTarget.transform.position, TimeSystem._FixedDeltaTime() * 0.5f);
                            }
                            else if (_PatrolIntervalTime < 0.0f)
                            {
                                _WaypointTarget = EnemyManager._Instance._Waypoints[Random.Range(0, EnemyManager._Instance._Waypoints.Count)];
                                _PatrolIntervalTime = 5.0f;
                            }
                        }
                        else if (_PatrolTime < 0.0f)
                        {
                            _FadeDisappear = true;
                            _Transform.Translate(Vector2.right * TimeSystem._FixedDeltaTime() * (_Speed * 2.0f), Space.Self);
                        }
                        if (_delta(_Transform.eulerAngles.z) > 90.0f || _delta(_Transform.eulerAngles.z) < -90.0f)
                        {
                            _variable_vector3.x = _Transform.localScale.x;
                            _variable_vector3.y = -_Scale.y * _ScaleMagnification;
                            _variable_vector3.z = 1.0f;
                            _Transform.localScale = _variable_vector3;
                        }
                        else
                        {
                            _variable_vector3.x = _Transform.localScale.x;
                            _variable_vector3.y = _Scale.y * _ScaleMagnification;
                            _variable_vector3.z = 1.0f;
                            _Transform.localScale = _variable_vector3;
                        }
                    }
                    break;
                case Status.SwimUp:
                    {
                        _Transform.localPosition = new Vector2(Camera.main.transform.localPosition.x, _Transform.localPosition.y);
                        _Transform.Translate(Vector2.up * TimeSystem._DeltaTime() * _Speed, Space.World);
                        _Transform.right = Vector2.Lerp(_Transform.right, Vector2.up, TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimDown:
                    {
                        _Transform.localPosition = new Vector2(Camera.main.transform.localPosition.x, _Transform.localPosition.y);
                        _Transform.Translate(Vector2.down * TimeSystem._DeltaTime() * _Speed, Space.World);
                        _Transform.right = Vector2.Lerp(_Transform.right, Vector2.down, TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimLeft:
                    {
                        _Transform.Translate(Vector2.left * TimeSystem._FixedDeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.SwimRight:
                    {
                        _Transform.Translate(Vector2.right * TimeSystem._FixedDeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.SwimLeftStyle:
                    {
                        _variable_vector3.x = 1.0f;
                        _variable_vector3.y = Mathf.Sin(TimeSystem._Time() * 2.5f);
                        _variable_vector3.z = 0.0f;
                        _Transform.Translate(_variable_vector3 * TimeSystem._DeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.SwimRightStyle:
                    {
                        _variable_vector3.x = -1.0f;
                        _variable_vector3.y = Mathf.Sin(TimeSystem._Time() * 2.5f);
                        _variable_vector3.z = 0.0f;
                        _Transform.Translate(_variable_vector3 * TimeSystem._DeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.Target:
                    {
                        _Transform.Translate(Vector2.right * TimeSystem._DeltaTime() * _Speed, Space.Self);
                    }
                    break;
                case Status.TargetLock:
                    {
                        if (_T_L_Timer > 0.0f)
                        {
                            _T_L_Timer -= TimeSystem._DeltaTime();
                            _Transform.localPosition = Vector2.MoveTowards(_Transform.localPosition, _CurrentPlayerPosition, TimeSystem._DeltaTime() * _Speed);
                            _Transform.right = Vector2.Lerp(_Transform.right, (_CurrentPlayerPosition - _Transform.localPosition).normalized, TimeSystem._DeltaTime() * _RotateSpeed);
                            if (Vector2.Distance(_Transform.localPosition, _CurrentPlayerPosition) < _DistanceUpdate)
                            {
                                _CurrentPlayerPosition = _Player.localPosition;
                                _DistanceUpdate = Random.Range(1.0f, 5.0f);
                            }
                        }
                        else if (_T_L_Timer < 0.0f) _Transform.Translate(Vector2.right * TimeSystem._DeltaTime() * _Speed, Space.Self);
                        if (_delta(_Transform.eulerAngles.z) > 90.0f || _delta(_Transform.eulerAngles.z) < -90.0f) _Transform.localScale = new Vector3(_Transform.localScale.x, -_Scale.y * _ScaleMagnification, _Transform.localScale.z);
                        else _Transform.localScale = new Vector3(_Transform.localScale.x, _Scale.y * _ScaleMagnification, _Transform.localScale.z);
                    }
                    break;
            }
        }

        private bool _disable_once;
        private void Disappear()
        {
            if (!_FadeDisappear)
            {
                if (!_Visible) _ActivityTime -= TimeSystem._DeltaTime();
                if (_ActivityTime <= 0.0f) { EnemyManager._Instance.RecycleAI(_Queue_GameObject, gameObject); return; }
            }
            else
            {
                _ActivityTime -= TimeSystem._DeltaTime();
                if (_ActivityTime <= 0.0f)
                {
                    if (!_disable_once)
                    {
                        for (int _i = 0; _i < _Collider2D.Count; _i++) _Collider2D[_i].enabled = false;
                        _disable_once = true;
                    }
                    _Color.r -= TimeSystem._DeltaTime() * 2.0f;
                    _Color.g -= TimeSystem._DeltaTime() * 2.0f;
                    _Color.b -= TimeSystem._DeltaTime() * 2.0f;
                    _Color.a -= TimeSystem._DeltaTime() * 0.8f;
                    _SpriteRenderer.color = _Color;
                    if (_Color.a <= 0.0f) { _disable_once = false; EnemyManager._Instance.RecycleAI(_Queue_GameObject, gameObject); return; }
                }
            }
        }
        private void OnBecameVisible() { if (_FadeDisappear) return; _Visible = true; }
        private void OnBecameInvisible() { if (_FadeDisappear) return; _Visible = false; }
    }
}
