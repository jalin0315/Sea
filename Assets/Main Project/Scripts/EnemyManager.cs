using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _Instance;
    [SerializeField] private Camera _Camera;
    private Vector2 _Origin() { return _Camera.ScreenToWorldPoint(Vector2.zero); }
    private Vector2 _Vertex() { return _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight)); }
    [SerializeField] private Transform _Parent;
    [SerializeField] private GameObject _Waypoint;
    [SerializeField] private int _NumberOfWaypoints;
    [HideInInspector] public List<GameObject> _Waypoints = new List<GameObject>();
    [SerializeField] private int _Quantity_Size;
    [SerializeField] private List<GameObject> _List_Prefab_Fish = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_SingleFish = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_TargetFish = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_BackgroundFish = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_ObstacleFishZoneTwo = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_TargetFishZoneTwo = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_BackgroundFishZoneTwo = new List<GameObject>();
    [SerializeField] private List<GameObject> _List_Prefab_JellyFish = new List<GameObject>();
    private Queue<GameObject> _Fish_Pool = new Queue<GameObject>();
    private Queue<GameObject> _SingleFish_Pool = new Queue<GameObject>();
    private Queue<GameObject> _TargetFish_Pool = new Queue<GameObject>();
    private Queue<GameObject> _BackgroundFish_Pool = new Queue<GameObject>();
    private Queue<GameObject> _ObstacleFishZoneTwo_Pool = new Queue<GameObject>();
    private Queue<GameObject> _TargetFishZoneTwo_Pool = new Queue<GameObject>();
    private Queue<GameObject> _BackgroundFishZoneTwo_Pool = new Queue<GameObject>();
    private Queue<GameObject> _JellyFish_Pool = new Queue<GameObject>();
    [SerializeField] private int _CurrentCount;
    [SerializeField] private int _CurrentCount_BackgroundFish;
    private int _MaxCount;
    [HideInInspector] public int _MaxCount_BackgroundFish;
    [SerializeField] private float _SpawnOffset;
    [SerializeField] private float _SpawnOffsetMedium;
    [SerializeField] private float _SpawnOffsetLarge;

    private void Awake()
    {
        _Instance = this;
        for (int _i = 0; _i < _Quantity_Size; _i++)
        {
            List<int> _t = new List<int>();
            for (int _x = 0; _x < _List_Prefab_Fish.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_Fish.Count); _t.Count < _List_Prefab_Fish.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_Fish[_j], Vector2.zero, Quaternion.identity, transform);
                    _Fish_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_Fish.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_SingleFish.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_SingleFish.Count); _t.Count < _List_Prefab_SingleFish.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_SingleFish[_j], Vector2.zero, Quaternion.identity, transform);
                    _SingleFish_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_SingleFish.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_TargetFish.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_TargetFish.Count); _t.Count < _List_Prefab_TargetFish.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_TargetFish[_j], Vector2.zero, Quaternion.identity, transform);
                    _TargetFish_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_TargetFish.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_BackgroundFish.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_BackgroundFish.Count); _t.Count < _List_Prefab_BackgroundFish.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_BackgroundFish[_j], Vector2.zero, Quaternion.identity, transform);
                    _BackgroundFish_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_BackgroundFish.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_ObstacleFishZoneTwo.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_ObstacleFishZoneTwo.Count); _t.Count < _List_Prefab_ObstacleFishZoneTwo.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_ObstacleFishZoneTwo[_j], Vector2.zero, Quaternion.identity, transform);
                    _ObstacleFishZoneTwo_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_ObstacleFishZoneTwo.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_TargetFishZoneTwo.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_TargetFishZoneTwo.Count); _t.Count < _List_Prefab_TargetFishZoneTwo.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_TargetFishZoneTwo[_j], Vector2.zero, Quaternion.identity, transform);
                    _TargetFishZoneTwo_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_TargetFishZoneTwo.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_BackgroundFishZoneTwo.Count;)
            {
                for (int _j = Random.Range(0, _List_Prefab_BackgroundFishZoneTwo.Count); _t.Count < _List_Prefab_BackgroundFishZoneTwo.Count;)
                {
                    if (_t.Contains(_j))
                        break;
                    GameObject _go = Instantiate(_List_Prefab_BackgroundFishZoneTwo[_j], Vector2.zero, Quaternion.identity, transform);
                    _BackgroundFishZoneTwo_Pool.Enqueue(_go);
                    _go.SetActive(false);
                    _t.Add(_j);
                    _x++;
                }
                if (_t.Count >= _List_Prefab_BackgroundFishZoneTwo.Count)
                    _t.Clear();
            }
            for (int _x = 0; _x < _List_Prefab_JellyFish.Count; _x++)
            {
                GameObject _go = Instantiate(_List_Prefab_JellyFish[_x], Vector2.zero, Quaternion.identity, transform);
                _JellyFish_Pool.Enqueue(_go);
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
        }
        */
    }

    private void ReUse(Queue<GameObject> _queue, EnemyAI.Status _status, Vector3 _position, Quaternion _rotation, float _speed, float _time_out, bool _disappear_status)
    {
        EnemyAI._RecoveryAll = false;
        if (_queue.Count > 0)
        {
            GameObject _go = _queue.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _position;
            _go.transform.rotation = _rotation;
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Pool = _queue;
            _enemy_ai._Speed = _speed;
            _enemy_ai._TimeOut = _time_out;
            _enemy_ai._FadeDisappear = _disappear_status;
            if (_queue == _TargetFishZoneTwo_Pool)
            {
                _enemy_ai._SpriteRenderer.color = Color.white;
                _enemy_ai._FadeDisappear_Time = 0.1f;
            }
            if (_queue == _ObstacleFishZoneTwo_Pool)
            {
                _enemy_ai._ScaleMagnification = 1.5f;
            }
            if (_queue == _BackgroundFish_Pool)
            {
                float _alpha = Random.Range(0.1f, 0.8f);
                if (_alpha < 0.3f) _enemy_ai._ScaleMagnification = 0.2f;
                else if (_alpha < 0.55f) _enemy_ai._ScaleMagnification = 0.6f;
                else if (_alpha < 0.8f) _enemy_ai._ScaleMagnification = 1.0f;
                _enemy_ai._SpriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, _alpha);
                _enemy_ai._FadeDisappear_Time = 0.025f;
                _CurrentCount_BackgroundFish++;
            }
            if (_queue == _BackgroundFishZoneTwo_Pool)
            {
                float _alpha = Random.Range(0.1f, 0.8f);
                if (_alpha < 0.3f) _enemy_ai._ScaleMagnification = 0.8f;
                else if (_alpha < 0.55f) _enemy_ai._ScaleMagnification = 0.9f;
                else if (_alpha < 0.8f) _enemy_ai._ScaleMagnification = 1.0f;
                _enemy_ai._SpriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, _alpha);
                _enemy_ai._FadeDisappear_Time = 0.025f;
                _CurrentCount_BackgroundFish++;
            }
            if (_queue != _BackgroundFish_Pool && _queue != _BackgroundFishZoneTwo_Pool)
            {
                if (GameManager._Instance._Meter >= 6000.0f) { _enemy_ai._ScaleMagnification = 2.0f; }
                else if (GameManager._Instance._Meter >= 4000.0f) { _enemy_ai._ScaleMagnification = 1.0f; }
                else if (GameManager._Instance._Meter >= 2000.0f) { _enemy_ai._ScaleMagnification = 2.0f; }
                else if (GameManager._Instance._Meter >= 1000.0f) { _enemy_ai._ScaleMagnification = 1.5f; }
                else if (GameManager._Instance._Meter >= 0.0f) { _enemy_ai._ScaleMagnification = 1.0f; }
                _CurrentCount++;
            }
            _enemy_ai.StateChange(_status);
        }
    }
    public void Recovery(Queue<GameObject> _queue, GameObject _go)
    {
        _queue.Enqueue(_go);
        _go.SetActive(false);
        if (_queue == _BackgroundFish_Pool || _queue == _BackgroundFishZoneTwo_Pool) _CurrentCount_BackgroundFish--;
        else _CurrentCount--;
    }

    private void ReUseJellyFish()
    {
        if (_JellyFish_Pool.Count <= 0) { Debug.LogWarningFormat("_JellyFish_Pool count: {0}", _JellyFish_Pool.Count); return; }
        JellyFishEnemyAI._RecoveryAll = false;
        GameObject _go = _JellyFish_Pool.Dequeue();
        _go.SetActive(true);
    }
    public void RecoveryJellyFish(GameObject _go)
    {
        _JellyFish_Pool.Enqueue(_go);
        _go.SetActive(false);
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

    /*
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
            float _speed = 1.5f;
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
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(((_origin.x + _vertex.x) / 2), _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2((_origin.x + _vertex.x) / 2, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimUp, new Vector2((_origin.x + _vertex.x) / 2, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimDown, new Vector2(((_origin.x + _vertex.x) / 2), _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimLeft, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimRight, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimLeftStyle, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
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
                    ReUse(_pool, EnemyAI.Status.SwimRightStyle, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
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
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
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
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 2:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 3:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 4:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 5:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 6:
                            ReUse(_pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed);
                            break;
                        case 7:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed);
                            break;
                        case 8:
                            ReUse(_pool, EnemyAI.Status.TargetLock, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed);
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
    */

    private IEnumerator _SpawnNpc_Background_Singleton;
    private IEnumerator _SpawnNpc_Logic_Background()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (_BackgroundFish_Pool.Count <= 0) continue;
            if (_CurrentCount_BackgroundFish >= _MaxCount_BackgroundFish) continue;
            float _speed = Random.Range(0.25f, 0.85f);
            int _index = Random.Range(0, 2);
            switch (_index)
            {
                case 0:
                    if (GameManager._Instance._Meter >= 4000.0f)
                        ReUse(_BackgroundFishZoneTwo_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    else if (GameManager._Instance._Meter >= 0.0f)
                        ReUse(_BackgroundFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    break;
                case 1:
                    if (GameManager._Instance._Meter >= 4000.0f)
                        ReUse(_BackgroundFishZoneTwo_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    else if (GameManager._Instance._Meter >= 0.0f)
                        ReUse(_BackgroundFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }
    public void IEnumeratorSpawnNpcBackground(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_Background_Singleton != null)
                StopCoroutine(_SpawnNpc_Background_Singleton);
            _SpawnNpc_Background_Singleton = _SpawnNpc_Logic_Background();
            StartCoroutine(_SpawnNpc_Background_Singleton);
        }
        else
        {
            if (_SpawnNpc_Background_Singleton != null)
                StartCoroutine(_SpawnNpc_Background_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_00_Singleton;
    private IEnumerator _SpawnNpc_Logic_00()
    {
        _MaxCount = 5; // 10
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (_Fish_Pool.Count <= 0) continue;
            if (_CurrentCount >= _MaxCount) continue;
            float _speed = Random.Range(1.5f, 2.5f); // 2.5f 3.5f
            float _time_out = 2.0f;
            int _i = Random.Range(0, 2);
            switch (_i)
            {
                case 0:
                    ReUse(_Fish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                    break;
                case 1:
                    ReUse(_Fish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
    public void IEnumeratorSpawnNpc00(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_00_Singleton != null)
                StopCoroutine(_SpawnNpc_00_Singleton);
            _SpawnNpc_00_Singleton = _SpawnNpc_Logic_00();
            StartCoroutine(_SpawnNpc_00_Singleton);
        }
        else
        {
            if (_SpawnNpc_00_Singleton != null)
                StopCoroutine(_SpawnNpc_00_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_01_Singleton;
    private IEnumerator _SpawnNpc_Logic_01()
    {
        IEnumeratorDuration(true);
        int _index = Random.Range(0, 3);
        //int _index = 0;
        /*
        if (_index == 0)
        {
            _MaxCount = 25;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_SingleFish_Pool.Count <= 0)
                    continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0)
                    continue;
                Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                int _i = Random.Range(1, 9);
                float _speed = Random.Range(1.0f, 1.5f);
                float _time_out = 3.0f;
                switch (_i)
                {
                    case 1:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 2:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 3:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(((_origin.x + _vertex.x) / 2), _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 4:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2((_origin.x + _vertex.x) / 2, _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 5:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out);
                        break;
                    case 6:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out);
                        break;
                    case 7:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 8:
                        ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        */
        /*
        if (_index == 0)
        {
            _MaxCount = 20;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_Fish_Pool.Count <= 0)
                    continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0)
                    continue;
                Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                int _i = Random.Range(0, 2);
                float _speed = Random.Range(2.5f, 3.5f);
                float _time_out = 2.0f;
                switch (_i)
                {
                    case 0:
                        ReUse(_Fish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    case 1:
                        ReUse(_Fish_Pool, EnemyAI.Status.SwimRight, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
        }
        */
        if (_index == 0)
        {
            _MaxCount = 10;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_Fish_Pool.Count <= 0) continue;
                if (_CurrentCount >= _MaxCount) continue;
                float _speed = Random.Range(1.5f, 2.5f); // 2.5f 3.5f
                float _time_out = 2.0f;
                int _i = Random.Range(0, 2);
                switch (_i)
                {
                    case 0:
                        ReUse(_Fish_Pool, EnemyAI.Status.SwimLeftStyle, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    case 1:
                        ReUse(_Fish_Pool, EnemyAI.Status.SwimRightStyle, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));
            }
        }
        if (_index == 1)
        {
            int _index_i = Random.Range(0, 4);
            if (_index_i == 0)
            {
                bool _initialize = false;
                float[] _points = new float[8]; // 10
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_SingleFish_Pool.Count <= 0) continue;
                    if (!_initialize)
                    {
                        float _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                        for (int _i = 0; _i < _points.Length; _i++) _points[_i] = _Vertex().y + (_result * (_i + 1));
                        _initialize = true;
                    }
                    for (int _i = 0; _i < _points.Length; _i++)
                    {
                        float _speed = Random.Range(2.5f, 3.5f); // 4.0f 4.5f
                        float _time_out = 1.5f;
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffset, _points[_i]), Quaternion.identity, _speed, _time_out, false);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            if (_index_i == 1)
            {
                bool _initialize = false;
                float[] _points = new float[8]; // 10
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_SingleFish_Pool.Count <= 0) continue;
                    if (!_initialize)
                    {
                        float _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                        for (int _i = 0; _i < _points.Length; _i++) _points[_i] = _Vertex().y + (_result * (_i + 1));
                        _initialize = true;
                    }
                    for (int _i = _points.Length - 1; _i >= 0; _i--)
                    {
                        float _speed = Random.Range(2.5f, 3.5f); // 4.0f 4.5f
                        float _time_out = 1.5f;
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffset, _points[_i]), Quaternion.identity, _speed, _time_out, false);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            if (_index_i == 2)
            {
                bool _initialize = false;
                float[] _points = new float[8]; // 10
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_SingleFish_Pool.Count <= 0) continue;
                    if (!_initialize)
                    {
                        float _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                        for (int _i = 0; _i < _points.Length; _i++) _points[_i] = _Vertex().y + (_result * (_i + 1));
                        _initialize = true;
                    }
                    for (int _i = 0; _i < _points.Length; _i++)
                    {
                        float _speed = Random.Range(2.5f, 3.5f); // 4.0f 4.5f
                        float _time_out = 1.5f;
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffset, _points[_i]), Quaternion.identity, _speed, _time_out, false);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            if (_index_i == 3)
            {
                bool _initialize = false;
                float[] _points = new float[8];  // 10
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_SingleFish_Pool.Count <= 0) continue;
                    if (!_initialize)
                    {
                        float _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                        for (int _i = 0; _i < _points.Length; _i++) _points[_i] = _Vertex().y + (_result * (_i + 1));
                        _initialize = true;
                    }
                    for (int _i = _points.Length - 1; _i >= 0; _i--)
                    {
                        float _speed = Random.Range(2.5f, 3.5f); // 4.0f 4.5f
                        float _time_out = 1.5f;
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffset, _points[_i]), Quaternion.identity, _speed, _time_out, false);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
        }
        /*
        if (_index == 4)
        {
            _MaxCount = 5;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_SpecialFish_Pool.Count <= 0)
                    continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0)
                    continue;
                Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                int _i = Random.Range(1, 9);
                float _speed = Random.Range(5.0f, 6.0f);
                float _time_out = 1.0f;
                switch (_i)
                {
                    case 1:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 2:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 3:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 4:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(Random.Range(_origin.x, _vertex.x), _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 5:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out);
                        break;
                    case 6:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, Random.Range(_origin.y, _vertex.y)), Quaternion.identity, _speed, _time_out);
                        break;
                    case 7:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_origin.x + -_SpawnOffset, _vertex.y + _SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    case 8:
                        ReUse(_SpecialFish_Pool, EnemyAI.Status.Target, new Vector2(_vertex.x + _SpawnOffset, _origin.y + -_SpawnOffset), Quaternion.identity, _speed, _time_out);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));
            }
        }
        */
        if (_index == 2)
        {
            _MaxCount = 15; // 30
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_TargetFish_Pool.Count <= 0) continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
                int _i = Random.Range(1, 3);
                float _speed = Random.Range(2.0f, 3.0f); // 3.0f 4.0f
                float _time_out = 1.0f;
                switch (_i)
                {
                    case 1:
                        ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    case 2:
                        ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            }
        }
    }
    public void IEnumeratorSpawnNpc01(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_01_Singleton != null)
                StopCoroutine(_SpawnNpc_01_Singleton);
            _SpawnNpc_01_Singleton = _SpawnNpc_Logic_01();
            StartCoroutine(_SpawnNpc_01_Singleton);
        }
        else
        {
            if (_SpawnNpc_01_Singleton != null)
                StopCoroutine(_SpawnNpc_01_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_02_Singleton;
    private IEnumerator _SpawnNpc_Logic_02()
    {
        IEnumeratorDuration(true);
        int _index = Random.Range(0, 4);
        //int _index = 3;
        if (_index == 0)
        {
            bool _initialize = false;
            float[] _points_00 = new float[8];
            float[] _points_01 = new float[5];
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_SingleFish_Pool.Count <= 0) continue;
                if (!_initialize)
                {
                    float _result_00 = (_Origin().y - _Vertex().y) / (_points_00.Length - 1);
                    float _result_01 = (_Origin().y - _Vertex().y) / (_points_01.Length + 1);
                    for (int _i = 0; _i < _points_00.Length - 2; _i++) _points_00[_i] = _Vertex().y + (_result_00 * (_i + 1));
                    for (int _i = 0; _i < _points_01.Length; _i++) _points_01[_i] = _Vertex().y + (_result_01 * (_i + 1));
                    _initialize = true;
                }
                int _j = -1;
                int _k = _points_01.Length - 1;
                while (true)
                {
                    if (_j >= _points_00.Length - 2) break;
                    float _speed = 3.0f;
                    float _time_out = 2.5f;
                    if (_j < 0)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _Vertex().y), Quaternion.identity, _speed, _time_out, false);
                        _j++;
                    }
                    if (_j >= 0 && _j < _points_00.Length - 2)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _points_00[_j]), Quaternion.identity, _speed, _time_out, false);
                        _j++;
                    }
                    if (_j >= _points_00.Length - 2)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _Origin().y), Quaternion.identity, _speed, _time_out, false);
                    }
                    if (_k >= 0)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _points_01[_k]), Quaternion.identity, _speed, _time_out, false);
                        _k--;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        if (_index == 1)
        {
            bool _initialize = false;
            float[] _points_00 = new float[8];
            float[] _points_01 = new float[5];
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_SingleFish_Pool.Count <= 0) continue;
                if (!_initialize)
                {
                    float _result_00 = (_Origin().y - _Vertex().y) / (_points_00.Length - 1);
                    float _result_01 = (_Origin().y - _Vertex().y) / (_points_01.Length + 1);
                    for (int _i = 0; _i < _points_00.Length - 2; _i++) _points_00[_i] = _Vertex().y + (_result_00 * (_i + 1));
                    for (int _i = 0; _i < _points_01.Length; _i++) _points_01[_i] = _Vertex().y + (_result_01 * (_i + 1));
                    _initialize = true;
                }
                int _j = -1;
                int _k = _points_01.Length - 1;
                while (true)
                {
                    if (_j >= _points_00.Length - 2) break;
                    float _speed = 3.0f;
                    float _time_out = 2.5f;
                    if (_j < 0)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _Vertex().y), Quaternion.identity, _speed, _time_out, false);
                        _j++;
                    }
                    if (_j >= 0 && _j < _points_00.Length - 2)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _points_00[_j]), Quaternion.identity, _speed, _time_out, false);
                        _j++;
                    }
                    if (_j >= _points_00.Length - 2)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _Origin().y), Quaternion.identity, _speed, _time_out, false);
                    }
                    if (_k >= 0)
                    {
                        ReUse(_SingleFish_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _points_01[_k]), Quaternion.identity, _speed, _time_out, false);
                        _k--;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        if (_index == 2)
        {
            _MaxCount = 20;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_TargetFish_Pool.Count <= 0) continue;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
                float _speed = Random.Range(1.5f, 3.5f);
                float _time_out = 2.0f;
                int _i = Random.Range(1, 3);
                switch (_i)
                {
                    case 1:
                        ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    case 2:
                        ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
            }
        }
        if (_index == 3)
        {
            _MaxCount = 5;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
                float _i_speed = Random.Range(2.5f, 4.5f);
                float _i_time_out = 2.5f;
                int _i = Random.Range(1, 3);
                switch (_i)
                {
                    case 1:
                        if (_TargetFish_Pool.Count > 0)
                            ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _i_speed, _i_time_out, false);
                        break;
                    case 2:
                        if (_TargetFish_Pool.Count > 0)
                            ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _i_speed, _i_time_out, false);
                        break;
                    default:
                        break;
                }
                int _j = Random.Range(1, 3);
                float _j_speed = Random.Range(1.0f, 1.5f);
                float _j_time_out = 2.5f;
                switch (_j)
                {
                    case 1:
                        if (_SingleFish_Pool.Count > 0)
                            ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _j_speed, _j_time_out, false);
                        break;
                    case 2:
                        if (_SingleFish_Pool.Count > 0)
                            ReUse(_SingleFish_Pool, EnemyAI.Status.Patrol, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _j_speed, _j_time_out, false);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            }
        }
    }
    public void IEnumeratorSpawnNpc02(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_02_Singleton != null)
                StopCoroutine(_SpawnNpc_02_Singleton);
            _SpawnNpc_02_Singleton = _SpawnNpc_Logic_02();
            StartCoroutine(_SpawnNpc_02_Singleton);
        }
        else
        {
            if (_SpawnNpc_02_Singleton != null)
                StopCoroutine(_SpawnNpc_02_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_03_Singleton;
    private IEnumerator _SpawnNpc_Logic_03()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (GameManager._Instance._Result < 6000)
            {
                _MaxCount = 5;
                if (_CurrentCount >= _MaxCount) continue;
                if (_TargetFishZoneTwo_Pool.Count > 0)
                {
                    if (MovementSystem._Instance._VelocityX() > 2.0f)
                    {
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                    }
                    else if (MovementSystem._Instance._VelocityX() < -2.0f)
                    {
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                    }
                    else
                    {
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                    }
                }
                if (_ObstacleFishZoneTwo_Pool.Count > 0)
                {
                    if (MovementSystem._Instance._VelocityX() > 2.0f)
                        ReUse(_ObstacleFishZoneTwo_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 1.0f, 2.0f, false);
                    else if (MovementSystem._Instance._VelocityX() < -2.0f)
                        ReUse(_ObstacleFishZoneTwo_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 1.0f, 2.0f, false);
                    else
                    {
                        int _index = Random.Range(0, 2);
                        switch (_index)
                        {
                            case 0:
                                ReUse(_ObstacleFishZoneTwo_Pool, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 1.0f, 2.0f, false);
                                break;
                            case 1:
                                ReUse(_ObstacleFishZoneTwo_Pool, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 1.0f, 2.0f, false);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (GameManager._Instance._Result >= 6000)
            {
                _MaxCount = 4;
                if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
                if (_TargetFishZoneTwo_Pool.Count > 0)
                {
                    if (MovementSystem._Instance._VelocityX() > 2.0f)
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetLarge, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                    else if (MovementSystem._Instance._VelocityX() < -2.0f)
                        ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetLarge, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                    else
                    {
                        int _index = Random.Range(0, 2);
                        switch (_index)
                        {
                            case 0:
                                ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetLarge, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                                break;
                            case 1:
                                ReUse(_TargetFishZoneTwo_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetLarge, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, 3.0f, -1.0f, true);
                                break;
                            default:
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(3.0f);
            }
        }
    }
    public void IEnumeratorSpawnNpc03(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_03_Singleton != null)
                StopCoroutine(_SpawnNpc_03_Singleton);
            _SpawnNpc_03_Singleton = _SpawnNpc_Logic_03();
            StartCoroutine(_SpawnNpc_03_Singleton);
        }
        else
        {
            if (_SpawnNpc_03_Singleton != null)
                StopCoroutine(_SpawnNpc_03_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_04_Singleton;
    private IEnumerator _SpawnNpc_Logic_04()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _MaxCount = 1;
            if (_CurrentCount >= _MaxCount && _MaxCount != 0)
                continue;
            float _speed = Random.Range(3.0f, 4.0f);
            int _index = Random.Range(0, 2);
            switch (_index)
            {
                case 0:
                    ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    break;
                case 1:
                    ReUse(_TargetFish_Pool, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                    break;
                default:
                    break;
            }
        }
    }
    public void IEnumeratorSpawnNpc04(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_04_Singleton != null)
                StopCoroutine(_SpawnNpc_04_Singleton);
            _SpawnNpc_04_Singleton = _SpawnNpc_Logic_04();
            StartCoroutine(_SpawnNpc_04_Singleton);
        }
        else
        {
            if (_SpawnNpc_04_Singleton != null)
                StopCoroutine(_SpawnNpc_04_Singleton);
        }
    }

    private IEnumerator _SpawnNpc_JellyFish_Singleton;
    private IEnumerator _SpawnNpc_Logic_JellyFish()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            ReUseJellyFish();
            yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        }
    }
    public void IEnumeratorSpawnNpcJellyFish(bool _enable)
    {
        if (_enable)
        {
            if (_SpawnNpc_JellyFish_Singleton != null) StopCoroutine(_SpawnNpc_JellyFish_Singleton);
            _SpawnNpc_JellyFish_Singleton = _SpawnNpc_Logic_JellyFish();
            StartCoroutine(_SpawnNpc_JellyFish_Singleton);
        }
        else
        {
            if (_SpawnNpc_JellyFish_Singleton != null) StopCoroutine(_SpawnNpc_JellyFish_Singleton);
        }
    }

    private IEnumerator _Duration_Singleton;
    private IEnumerator _Duration_Logic()
    {
        yield return new WaitForSeconds(Random.Range(15.0f, 20.0f));
        IEnumeratorSpawnNpc00(false);
        IEnumeratorSpawnNpc01(false);
        IEnumeratorSpawnNpc02(false);
        IEnumeratorSpawnNpc03(false);
        IEnumeratorSpawnNpc04(false);
        yield return new WaitForSeconds(5.0f);
        if (GameManager._Instance._Result >= 600 && GameManager._Instance._Result < 2000)
            IEnumeratorSpawnNpc01(true);
        if (GameManager._Instance._Result >= 2000 && GameManager._Instance._Result < 4000)
            IEnumeratorSpawnNpc02(true);
        if (GameManager._Instance._Result >= 4000 && GameManager._Instance._Result < 8000)
            IEnumeratorSpawnNpc03(true);
        if (GameManager._Instance._Result >= 8000)
            IEnumeratorSpawnNpc04(true);
    }
    public void IEnumeratorDuration(bool _enable)
    {
        if (_enable)
        {
            if (_Duration_Singleton != null)
                StopCoroutine(_Duration_Singleton);
            _Duration_Singleton = _Duration_Logic();
            StartCoroutine(_Duration_Singleton);
        }
        else
        {
            if (_Duration_Singleton != null)
                StopCoroutine(_Duration_Singleton);
        }
    }

    public void IEnumeratorStopAllCoroutines() => StopAllCoroutines();

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