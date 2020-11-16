﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _Instance;
    public enum EnemyType
    {
        Null = -1,
        PatrolFish = 0,
        SwimUpFish = -1,
        SwimDownFish = -1,
        SwimLeftFish = 1,
        SwimRightFish = 2,
        SwimLeftStyleFish = 3,
        SwimRightStyleFish = 4,
        TargetFish = 5,
        TargetLockFish = -1
    }
    public EnemyType _EnemyType;
    [SerializeField] private Camera _Camera;
    private Vector2 _Origin;
    private Vector2 _Vertex;
    private Vector2 _Center;
    [SerializeField] private Transform _Parent;
    [SerializeField] private GameObject _Waypoint;
    [SerializeField] private int _NumberOfWaypoints;
    [HideInInspector] public List<GameObject> _Waypoints = new List<GameObject>();
    [SerializeField] private int _Quantity_Size;
    [SerializeField] private List<GameObject> _List_Prefab_Fish = new List<GameObject>();
    public Queue<GameObject> _Fish_Pool = new Queue<GameObject>();
    private int _CurrentCount;
    public int _MaxCount;
    [SerializeField] private float _SpawnOffset;
    [HideInInspector] public bool _BreakSpawnNpcLoop;

    private void Awake()
    {
        _Instance = this;
        for (int _i = 0; _i < _Quantity_Size; _i++)
        {
            for (int _j = 0; _j < _List_Prefab_Fish.Count; _j++)
            {
                GameObject _go = Instantiate(_List_Prefab_Fish[_j], Vector2.zero, Quaternion.identity, transform);
                _Fish_Pool.Enqueue(_go);
                _go.SetActive(false);
            }
        }
    }

    private void Start()
    {
        RandomWaypointsInitialize();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SpawnNpc((EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length), 0.0f, 1));
            StartCoroutine(SpawnNpc((EnemyType)Random.Range(0, 5), 0.0f, 1, true, 1.0f));
            StartCoroutine(SpawnNpc(EnemyType.TargetLockFish, 0.0f, 1));
        }
        */
        //if (Input.GetKeyDown(KeyCode.R)) ReRandomWaypoints();
        //if (Input.GetKeyDown(KeyCode.B)) _BreakSpawnNpcLoop = true;
    }

    private void ReUse(Queue<GameObject> _queue, EnemyAI.Status _status, Vector3 _position, Quaternion _rotation)
    {
        if (_queue.Count > 0)
        {
            GameObject _go = _queue.Dequeue();
            _go.transform.position = _position;
            _go.transform.rotation = _rotation;
            _go.SetActive(true);
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Pool = _queue;
            _CurrentCount++;
            if (GameManager._Instance._Meter >= 8000.0f) { _enemy_ai._ScaleMagnification = Random.Range(20.0f, 26.0f); _MaxCount = 1; _enemy_ai.StateChange(_status); return; }
            if (GameManager._Instance._Meter >= 6000.0f) { _enemy_ai._ScaleMagnification = Random.Range(10.0f, 16.0f); _MaxCount = 2; _enemy_ai.StateChange(_status); return; }
            if (GameManager._Instance._Meter >= 4000.0f) { _enemy_ai._ScaleMagnification = Random.Range(5.0f, 10.1f); _MaxCount = 3; _enemy_ai.StateChange(_status); return; }
            if (GameManager._Instance._Meter >= 2000.0f) { _enemy_ai._ScaleMagnification = Random.Range(2.0f, 4.1f); _MaxCount = 10; _enemy_ai.StateChange(_status); return; }
            if (GameManager._Instance._Meter >= 1000.0f) { _enemy_ai._ScaleMagnification = Random.Range(1.6f, 2.1f); _MaxCount = 20; _enemy_ai.StateChange(_status); return; }
            if (GameManager._Instance._Meter >= 0.0f) { _enemy_ai._ScaleMagnification = Random.Range(1.0f, 1.6f); _MaxCount = 20; _enemy_ai.StateChange(_status); return; }
            return;
        }
        else Debug.LogErrorFormat("{0} is out of range!");
    }
    public void Recovery(Queue<GameObject> _queue, GameObject _go)
    {
        _queue.Enqueue(_go);
        _go.SetActive(false);
        _CurrentCount--;
    }

    private void RandomWaypointsInitialize()
    {
        Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
        for (int _i = 0; _i < _NumberOfWaypoints; _i++)
        {
            float _random_x = Random.Range(_origin.x, _vertex.x);
            float _random_y = Random.Range(_origin.y, _vertex.y);
            GameObject _go = Instantiate(_Waypoint, new Vector2(_random_x, _random_y), Quaternion.identity, _Parent);
            _Waypoints.Add(_go);
        }
    }
    private void ReRandomWaypoints()
    {
        Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
        foreach (GameObject _go in _Waypoints)
        {
            float _random_x = Random.Range(_origin.x, _vertex.x);
            float _random_y = Random.Range(_origin.y, _vertex.y);
            _go.transform.position = new Vector2(_random_x, _random_y);
        }
    }

    public IEnumerator SpawnNpc(Queue<GameObject> _pool, bool _random_status, EnemyType _enemy_type, bool _loop, float _loop_time, int _count, float _time)
    {
        EnemyAI._RecoveryAll = false;
        _BreakSpawnNpcLoop = false;
        while (true)
        {
            if (_BreakSpawnNpcLoop) break;
            yield return new WaitForSeconds(Random.Range(0.0f, _loop_time));
            if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
            if (_pool.Count <= 0) continue;
            if (_random_status) _EnemyType = (EnemyType)Random.Range(0, 6);
            else _EnemyType = _enemy_type;
            if (_EnemyType == EnemyType.Null) yield break;
            if (_EnemyType == EnemyType.PatrolFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    int _index = Random.Range(1, 9);
                    switch (_index)
                    {
                        case 1:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(((_origin.x + _vertex.x) / 2), _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2((_origin.x + _vertex.x) / 2, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimUpFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimUp, new Vector2((_origin.x + _vertex.x) / 2, _origin.y + -_SpawnOffset), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimDownFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimDown, new Vector2(((_origin.x + _vertex.x) / 2), _vertex.y + _SpawnOffset), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimLeftFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimLeft, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimRightFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimRight, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimLeftStyleFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimLeftStyle, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.SwimRightStyleFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_pool, EnemyAI.Status.SwimRightStyle, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.TargetFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    int _index = Random.Range(1, 9);
                    switch (_index)
                    {
                        case 1:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
                if (!_loop) break;
                else continue;
            }
            if (_EnemyType == EnemyType.TargetLockFish)
            {
                for (int _i = 0; _i < _count; _i++)
                {
                    yield return new WaitForSeconds(_time);
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    int _index = Random.Range(1, 9);
                    switch (_index)
                    {
                        case 1:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
                if (!_loop) break;
                else continue;
            }
        }
    }

    private IEnumerator NewSpawnNpc(Queue<GameObject> _pool)
    {
        yield return StartCoroutine(Duration());
        // Command
        //int _index = Random.Range(0, 9);
        int _index = 0;
        switch (_index)
        {
            case 0:
                _EnemyType = EnemyType.PatrolFish;
                break;
            case 1:
                // Command
                // Left and Right
                break;
            case 2:
                // Command
                // Left Style and Right Style
                break;
            case 3:
                // Command
                // Left Up to Down
                break;
            case 4:
                // Command
                // Left Down to Up
                break;
            case 5:
                // Command
                // Right Up to Down
                break;
            case 6:
                // Command
                // Right Down to Up
                break;
            case 7:
                // Command
                // Target Fast Speed Large Quantity
                break;
            case 8:
                // Command
                // Target Slow Speed Small Quantity
                break;
        }
        if (_EnemyType == EnemyType.PatrolFish)
        {
            _MaxCount = 5;
            while (true)
            {
                if (_pool.Count <= 0)
                    continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0)
                    continue;
            }
        }
    }
    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(Random.Range(30.0f, 61.0f));
        // Command
        // 斷開魚池
        yield return new WaitForSeconds(10.0f);
        // Command
        // 重複隨機抽取 AI 模式
        //yield return StartCoroutine(NewSpawnNpc());
    }

    private void OnDrawGizmos()
    {
        // 1
        Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
        // 2
        Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
        Vector2 _center = new Vector2((_origin.x + _vertex.x) / 2, (_origin.y + _vertex.y) / 2);
        // 3
        Vector2 _upper_center = new Vector2((_origin.x + _vertex.x) / 2, _vertex.y);
        // 4
        Vector2 _lower_center = new Vector2((_origin.x + _vertex.x) / 2, _origin.y);
        // 5
        Vector2 _center_left = new Vector2(_origin.x, (_origin.y + _vertex.y) / 2);
        // 6
        Vector2 _center_right = new Vector2(_vertex.x, (_origin.y + _vertex.y) / 2);
        // 7
        Vector2 _upper_left_corner = new Vector2(_origin.x, _vertex.y);
        // 8
        Vector2 _lower_right_corner = new Vector2(_vertex.x, _origin.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_origin, 0.5f);
        Gizmos.DrawWireSphere(_vertex, 0.5f);
        Gizmos.DrawWireSphere(_center, 0.5f);
        Gizmos.DrawWireSphere(_upper_center, 0.5f);
        Gizmos.DrawWireSphere(_lower_center, 0.5f);
        Gizmos.DrawWireSphere(_center_right, 0.5f);
        Gizmos.DrawWireSphere(_center_left, 0.5f);
        Gizmos.DrawWireSphere(_upper_left_corner, 0.5f);
        Gizmos.DrawWireSphere(_lower_right_corner, 0.5f);
    }
}