using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class EnemyAI : MonoBehaviour
    {
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
        public Status _Status;
        private Vector3 _Scale;
        public SpriteRenderer _SpriteRenderer;
        public Queue<GameObject> _Queue_GameObject = new Queue<GameObject>();
        [HideInInspector] public float _ScaleMagnification;
        [HideInInspector] public float _Speed;
        private float _RotateSpeed;
        // --- 巡邏 ---
        private GameObject _WaypointTarget;
        private float _PatrolTime;
        private float _PatrolIntervalTime;
        private Vector3 _OutOfDistance_Position_Offset;
        // --- End ---
        private Vector2 _TranslateOffset;
        private Transform _Player;
        public static Transform _Bait;
        private Vector3 _CurrentPlayerPosition;
        private float _DistanceUpdate;
        private float _TargetLockTime;
        private float _T_L_Timer;
        // --- 回收機制 ---
        private bool _Visible;
        [HideInInspector] public float _Visible_Timer;
        [HideInInspector] public bool _FadeDisappear;
        public float _FadeDisappear_Time;
        // --- End ---
        public static bool _Recycle;

        private void Awake()
        {
            _Scale = transform.localScale;
            _RotateSpeed = 10.0f;
        }

        private void OnEnable()
        {

        }

        private void Update()
        {
            if (_Recycle) EnemyManager._Instance.Recovery(_Queue_GameObject, gameObject);
            Disappear();
        }

        private void FixedUpdate() => UpdateStatus();

        public void StateChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Patrol:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                        _WaypointTarget = EnemyManager._Instance._Waypoints[Random.Range(0, EnemyManager._Instance._Waypoints.Count)];
                        _PatrolTime = Random.Range(10.0f, 20.0f);
                        _PatrolIntervalTime = 5.0f;
                    }
                    break;
                case Status.SwimUp:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimDown:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimLeft:
                    {
                        transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _ScaleMagnification;
                        _TranslateOffset = new Vector2(0.0f, 0.0f);
                    }
                    break;
                case Status.SwimRight:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                        _TranslateOffset = new Vector2(0.0f, 0.0f);
                    }
                    break;
                case Status.SwimLeftStyle:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                    }
                    break;
                case Status.SwimRightStyle:
                    {
                        transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _ScaleMagnification;
                    }
                    break;
                case Status.Target:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                        if (_Bait != null) transform.right = (_Bait.position - transform.position).normalized;
                        else
                        {
                            if (_Player == null) _Player = GameObject.FindGameObjectWithTag("Player").transform;
                            transform.right = (_Player.position - transform.position).normalized;
                        }
                        float _delta(float _value)
                        {
                            float _result = Mathf.DeltaAngle(0, _value);
                            return _result;
                        }
                        if (_delta(transform.eulerAngles.z) > 90.0f || _delta(transform.eulerAngles.z) < -90.0f) transform.localScale = new Vector3(transform.localScale.x, -_Scale.y * _ScaleMagnification, transform.localScale.z);
                        else transform.localScale = new Vector3(transform.localScale.x, _Scale.y * _ScaleMagnification, transform.localScale.z);
                    }
                    break;
                case Status.TargetLock:
                    {
                        transform.localScale = _Scale * _ScaleMagnification;
                        _DistanceUpdate = Random.Range(1.0f, 5.0f);
                        _TargetLockTime = Random.Range(5.0f, 10.0f);
                        _T_L_Timer = _TargetLockTime;
                        if (_Player == null)
                        {
                            _Player = GameObject.FindGameObjectWithTag("Player").transform;
                            _CurrentPlayerPosition = _Player.position;
                        }
                        else _CurrentPlayerPosition = _Player.position;
                    }
                    break;
            }
        }

        private float _delta(float _value)
        {
            return Mathf.DeltaAngle(0, _value);
        }
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
                                if (Vector2.Distance(transform.position, _WaypointTarget.transform.position) > 1.0f)
                                {
                                    transform.position = Vector2.MoveTowards(transform.position, _WaypointTarget.transform.position, TimeSystem._FixedDeltaTime() * _Speed);
                                    transform.right = Vector2.Lerp(transform.right, (_WaypointTarget.transform.position - transform.position).normalized, TimeSystem._FixedDeltaTime() * _RotateSpeed);
                                }
                                else transform.position = Vector2.Lerp(transform.position, _WaypointTarget.transform.position, TimeSystem._FixedDeltaTime() * 0.5f);
                            }
                            else if (_PatrolIntervalTime < 0.0f)
                            {
                                _WaypointTarget = EnemyManager._Instance._Waypoints[Random.Range(0, EnemyManager._Instance._Waypoints.Count)];
                                _PatrolIntervalTime = 5.0f;
                            }
                        }
                        else if (_PatrolTime < 0.0f) transform.Translate(Vector2.right * TimeSystem._FixedDeltaTime() * (_Speed * 2.0f), Space.Self);
                        if (_delta(transform.eulerAngles.z) > 90.0f || _delta(transform.eulerAngles.z) < -90.0f)
                            transform.localScale = new Vector3(transform.localScale.x, -_Scale.y * _ScaleMagnification, transform.localScale.z);
                        else transform.localScale = new Vector3(transform.localScale.x, _Scale.y * _ScaleMagnification, transform.localScale.z);
                    }
                    break;
                case Status.SwimUp:
                    {
                        transform.position = new Vector2(Camera.main.transform.position.x, transform.position.y);
                        transform.Translate(Vector2.up * TimeSystem._DeltaTime() * _Speed, Space.World);
                        transform.right = Vector2.Lerp(transform.right, Vector2.up, TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimDown:
                    {
                        transform.position = new Vector2(Camera.main.transform.position.x, transform.position.y);
                        transform.Translate(Vector2.down * TimeSystem._DeltaTime() * _Speed, Space.World);
                        transform.right = Vector2.Lerp(transform.right, Vector2.down, TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimLeft:
                    {
                        transform.Translate((Vector2.left + _TranslateOffset) * TimeSystem._DeltaTime() * _Speed, Space.World);
                        transform.right = Vector2.Lerp(transform.right, -(Vector2.left + _TranslateOffset), TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimRight:
                    {
                        transform.Translate((Vector2.right + _TranslateOffset) * TimeSystem._DeltaTime() * _Speed, Space.World);
                        transform.right = Vector2.Lerp(transform.right, Vector2.right + _TranslateOffset, TimeSystem._DeltaTime() * _RotateSpeed);
                    }
                    break;
                case Status.SwimLeftStyle:
                    {
                        transform.Translate(new Vector3(1.0f, Mathf.Sin(TimeSystem._Time() * 2.5f)) * TimeSystem._DeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.SwimRightStyle:
                    {
                        transform.Translate(new Vector3(-1.0f, Mathf.Sin(TimeSystem._Time() * 2.5f)) * TimeSystem._DeltaTime() * _Speed, Space.World);
                    }
                    break;
                case Status.Target:
                    {
                        transform.Translate(Vector2.right * TimeSystem._DeltaTime() * _Speed, Space.Self);
                    }
                    break;
                case Status.TargetLock:
                    {
                        if (_T_L_Timer > 0.0f)
                        {
                            _T_L_Timer -= TimeSystem._DeltaTime();
                            transform.position = Vector2.MoveTowards(transform.position, _CurrentPlayerPosition, TimeSystem._DeltaTime() * _Speed);
                            transform.right = Vector2.Lerp(transform.right, (_CurrentPlayerPosition - transform.position).normalized, TimeSystem._DeltaTime() * _RotateSpeed);
                            if (Vector2.Distance(transform.position, _CurrentPlayerPosition) < _DistanceUpdate)
                            {
                                _CurrentPlayerPosition = _Player.position;
                                _DistanceUpdate = Random.Range(1.0f, 5.0f);
                            }
                        }
                        else if (_T_L_Timer < 0.0f) transform.Translate(Vector2.right * TimeSystem._DeltaTime() * _Speed, Space.Self);
                        if (_delta(transform.eulerAngles.z) > 90.0f || _delta(transform.eulerAngles.z) < -90.0f) transform.localScale = new Vector3(transform.localScale.x, -_Scale.y * _ScaleMagnification, transform.localScale.z);
                        else transform.localScale = new Vector3(transform.localScale.x, _Scale.y * _ScaleMagnification, transform.localScale.z);
                    }
                    break;
            }
        }

        private void Disappear()
        {
            if (_FadeDisappear)
            {
                _SpriteRenderer.color = new Color(_SpriteRenderer.color.r, _SpriteRenderer.color.g, _SpriteRenderer.color.b, _SpriteRenderer.color.a - (TimeSystem._DeltaTime() * _FadeDisappear_Time));
                if (_SpriteRenderer.color.a <= 0.0f) EnemyManager._Instance.Recovery(_Queue_GameObject, gameObject);
                return;
            }
            if (!_Visible) _Visible_Timer -= TimeSystem._DeltaTime();
            if (_Visible_Timer < 0.0f) EnemyManager._Instance.Recovery(_Queue_GameObject, gameObject);
        }
        private void OnBecameVisible() => _Visible = true;
        private void OnBecameInvisible() => _Visible = false;
    }
}
