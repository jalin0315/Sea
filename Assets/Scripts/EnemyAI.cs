using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum Status
    {
        Null = -1,
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
    public SpriteRenderer _SpriteRenderer;
    public Queue<GameObject> _Pool = new Queue<GameObject>();
    private Vector3 _Scale;
    [HideInInspector] public float _ScaleMagnification;
    [HideInInspector] public float _Speed;
    [SerializeField] private float _RotateSpeed;
    private GameObject _WaypointTarget;
    private float _PatrolTime;
    private float _P_Timer;
    private float _PatrolIntervalTime;
    private float _P_I_Timer;
    private Vector3 _OutOfDistance_Position_Offset;
    private Vector2 _OutOfDistance_Rotation_Offset;
    private Vector2 _TranslateOffset;
    private Transform _Player;
    public static Transform _Bait;
    private Vector3 _CurrentPlayerPosition;
    private float _DistanceUpdate;
    private float _TargetLockTime;
    private float _T_L_Timer;
    private bool _Visible;
    [HideInInspector] public float _TimeOut;
    private float _Visible_Timer;
    [HideInInspector] public bool _FadeDisappear;
    public float _FadeDisappear_Time;
    public static bool _RecoveryAll;

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Scale = transform.localScale;
    }

    private void Start()
    {
        _Visible = false;
    }

    private void Update()
    {
        if (_RecoveryAll)
            EnemyManager._Instance.Recovery(_Pool, gameObject);
    }

    private void FixedUpdate()
    {
        UpdateStatus();
    }

    public void StateChange(Status _status)
    {
        _Status = _status;
        _Visible = false;
        _Visible_Timer = _TimeOut;
        if (_Status == Status.Patrol)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            int _index = Random.Range(0, EnemyManager._Instance._Waypoints.Count);
            _WaypointTarget = EnemyManager._Instance._Waypoints[_index];
            _PatrolTime = Random.Range(10.0f, 20.0f);
            _P_Timer = _PatrolTime;
            _PatrolIntervalTime = 5.0f;
            _P_I_Timer = _PatrolIntervalTime;
            return;
        }
        if (_Status == Status.SwimUp)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            //_Speed = Random.Range(0.5f, 2.0f);
            return;
        }
        if (_Status == Status.SwimDown)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            //_Speed = Random.Range(0.5f, 2.0f);
            return;
        }
        if (_Status == Status.SwimLeft)
        {
            transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _ScaleMagnification;
            _TranslateOffset = new Vector2(0.0f, 0.0f);
            return;
        }
        if (_Status == Status.SwimRight)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            _TranslateOffset = new Vector2(0.0f, 0.0f);
            return;
        }
        if (_Status == Status.SwimLeftStyle)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            return;
        }
        if (_Status == Status.SwimRightStyle)
        {
            transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _ScaleMagnification;
            return;
        }
        if (_Status == Status.Target)
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
            return;
        }
        if (_Status == Status.TargetLock)
        {
            transform.localScale = _Scale * _ScaleMagnification;
            //_Speed = Random.Range(0.5f, 1.0f);
            _DistanceUpdate = Random.Range(1.0f, 5.0f);
            _TargetLockTime = Random.Range(5.0f, 10.0f);
            _T_L_Timer = _TargetLockTime;
            if (_Player == null)
            {
                _Player = GameObject.FindGameObjectWithTag("Player").transform;
                _CurrentPlayerPosition = _Player.position;
            }
            else _CurrentPlayerPosition = _Player.position;
            return;
        }
    }

    private void UpdateStatus()
    {
        if (_Status == Status.Null) return;
        if (_Status == Status.Patrol)
        {
            /*
            if (_WaypointTarget.transform.position.x > transform.position.x)
                transform.localScale = _Scale * _ScaleMagnification;
            if (_WaypointTarget.transform.position.x < transform.position.x)
                transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _ScaleMagnification;
            */
            if (_P_Timer > 0.0f)
            {
                _P_Timer -= Time.deltaTime;
                if (_P_I_Timer > 0.0f)
                {
                    _P_I_Timer -= Time.deltaTime;
                    if (Vector2.Distance(transform.position, _WaypointTarget.transform.position) > 1.0f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, _WaypointTarget.transform.position, Time.deltaTime * _Speed);
                        transform.right = Vector2.Lerp(transform.right, (_WaypointTarget.transform.position - transform.position).normalized, Time.deltaTime * _RotateSpeed);
                    }
                    else
                    {
                        transform.position = Vector2.Lerp(transform.position, _WaypointTarget.transform.position + _OutOfDistance_Position_Offset, Time.deltaTime * 0.5f);
                        //transform.right = Vector2.Lerp(transform.right, _OutOfDistance_Rotation_Offset, Time.deltaTime * 0.01f);
                    }
                }
                else if (_P_I_Timer < 0.0f)
                {
                    int _index = Random.Range(0, EnemyManager._Instance._Waypoints.Count);
                    _WaypointTarget = EnemyManager._Instance._Waypoints[_index];
                    _PatrolIntervalTime = 5.0f;
                    _P_I_Timer = _PatrolIntervalTime;
                    _OutOfDistance_Position_Offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                    //_OutOfDistance_Rotation_Offset = new Vector2(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
                }
            }
            else if (_P_Timer < 0.0f)
            {
                _Speed = 4.0f;
                transform.Translate(Vector2.right * Time.deltaTime * _Speed, Space.Self);
            }
            float _delta(float _value)
            {
                float _result = Mathf.DeltaAngle(0, _value);
                return _result;
            }
            if (_delta(transform.eulerAngles.z) > 90.0f || _delta(transform.eulerAngles.z) < -90.0f)
                transform.localScale = new Vector3(transform.localScale.x, -_Scale.y * _ScaleMagnification, transform.localScale.z);
            else
                transform.localScale = new Vector3(transform.localScale.x, _Scale.y * _ScaleMagnification, transform.localScale.z);
            Disappear();
            return;
        }
        if (_Status == Status.SwimUp)
        {
            transform.position = new Vector2(Camera.main.transform.position.x, transform.position.y);
            transform.Translate(Vector2.up * Time.deltaTime * _Speed, Space.World);
            transform.right = Vector2.Lerp(transform.right, Vector2.up, Time.deltaTime * _RotateSpeed);
            Disappear();
            return;
        }
        if (_Status == Status.SwimDown)
        {
            transform.position = new Vector2(Camera.main.transform.position.x, transform.position.y);
            transform.Translate(Vector2.down * Time.deltaTime * _Speed, Space.World);
            transform.right = Vector2.Lerp(transform.right, Vector2.down, Time.deltaTime * _RotateSpeed);
            Disappear();
            return;
        }
        if (_Status == Status.SwimLeft)
        {
            transform.Translate((Vector2.left + _TranslateOffset) * Time.deltaTime * _Speed, Space.World);
            transform.right = Vector2.Lerp(transform.right, -(Vector2.left + _TranslateOffset), Time.deltaTime * _RotateSpeed);
            Disappear();
            return;
        }
        if (_Status == Status.SwimRight)
        {
            transform.Translate((Vector2.right + _TranslateOffset) * Time.deltaTime * _Speed, Space.World);
            transform.right = Vector2.Lerp(transform.right, Vector2.right + _TranslateOffset, Time.deltaTime * _RotateSpeed);
            Disappear();
            return;
        }
        if (_Status == Status.SwimLeftStyle)
        {
            transform.Translate(new Vector3(1.0f, Mathf.Sin(Time.time * 2.5f)) * Time.deltaTime * _Speed, Space.World);
            Disappear();
            return;
        }
        if (_Status == Status.SwimRightStyle)
        {
            transform.Translate(new Vector3(-1.0f, Mathf.Sin(Time.time * 2.5f)) * Time.deltaTime * _Speed, Space.World);
            Disappear();
            return;
        }
        if (_Status == Status.Target)
        {
            transform.Translate(Vector2.right * Time.deltaTime * _Speed, Space.Self);
            Disappear();
            return;
        }
        if (_Status == Status.TargetLock)
        {
            if (_T_L_Timer > 0.0f)
            {
                _T_L_Timer -= Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, _CurrentPlayerPosition, Time.deltaTime * _Speed);
                transform.right = Vector2.Lerp(transform.right, (_CurrentPlayerPosition - transform.position).normalized, Time.deltaTime * _RotateSpeed);
                if (Vector2.Distance(transform.position, _CurrentPlayerPosition) < _DistanceUpdate)
                {
                    _CurrentPlayerPosition = _Player.position;
                    _DistanceUpdate = Random.Range(1.0f, 5.0f);
                }
            }
            else if (_T_L_Timer < 0.0f) transform.Translate(Vector2.right * Time.deltaTime * _Speed, Space.Self);
            float _delta(float _value)
            {
                float _result = Mathf.DeltaAngle(0, _value);
                return _result;
            }
            if (_delta(transform.eulerAngles.z) > 90.0f || _delta(transform.eulerAngles.z) < -90.0f) transform.localScale = new Vector3(transform.localScale.x, -_Scale.y * _ScaleMagnification, transform.localScale.z);
            else transform.localScale = new Vector3(transform.localScale.x, _Scale.y * _ScaleMagnification, transform.localScale.z);
            Disappear();
            return;
        }
    }

    private void Disappear()
    {
        if (!_FadeDisappear)
        {
            if (!_Visible)
                _Visible_Timer -= Time.deltaTime;
            if (_Visible_Timer < 0.0f)
                EnemyManager._Instance.Recovery(_Pool, gameObject);
        }
        else if (_FadeDisappear)
        {
            _SpriteRenderer.color = new Color(_SpriteRenderer.color.r, _SpriteRenderer.color.g, _SpriteRenderer.color.b, _SpriteRenderer.color.a - (Time.deltaTime * _FadeDisappear_Time));
            if (_SpriteRenderer.color.a <= 0.0f)
                EnemyManager._Instance.Recovery(_Pool, gameObject);
        }
    }
    private void OnBecameVisible()
    {
        _Visible = true;
        //_Visible_Timer = _TimeOut;
    }
    private void OnBecameInvisible() => _Visible = false;
}
