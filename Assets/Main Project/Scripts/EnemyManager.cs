using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace CTJ
{
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
        [SerializeField] private List<GameObject> _List_Prefab_ObstacleFishZoneTwo = new List<GameObject>();
        [SerializeField] private List<GameObject> _List_Prefab_TargetFishZoneTwo = new List<GameObject>();
        [SerializeField] private List<GameObject> _List_Prefab_JellyFish = new List<GameObject>();
        [SerializeField] private List<GameObject> _List_Prefab_NPC_ZoneOne = new List<GameObject>();
        [SerializeField] private List<GameObject> _List_Prefab_NPC_ZoneTwo = new List<GameObject>();
        private Queue<GameObject> _Pool_Fish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_SingleFish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_TargetFish = new Queue<GameObject>();
        private Queue<GameObject> _ObstacleFishZoneTwo_Pool = new Queue<GameObject>();
        private Queue<GameObject> _TargetFishZoneTwo_Pool = new Queue<GameObject>();
        private Queue<GameObject> _JellyFish_Pool = new Queue<GameObject>();
        private Queue<GameObject> _Pool_NPC_ZoneOne = new Queue<GameObject>();
        private Queue<GameObject> _Pool_NPC_ZoneTwo = new Queue<GameObject>();
        private int _CurrentCount;
        private int _CurrentCount_NPC;
        private int _MaxCount;
        private int _MaxCount_NPC;
        [SerializeField] private float _SpawnOffset;
        [SerializeField] private float _SpawnOffsetMedium;
        [SerializeField] private float _SpawnOffsetLarge;

        private void Awake()
        {
            _Instance = this;
            List<int> _t = new List<int>();
            for (int _i = 0; _i < _Quantity_Size; _i++)
            {
                for (int _x = 0; _x < _List_Prefab_Fish.Count;)
                {
                    for (int _j = Random.Range(0, _List_Prefab_Fish.Count); _t.Count < _List_Prefab_Fish.Count;)
                    {
                        if (_t.Contains(_j))
                            break;
                        GameObject _go = Instantiate(_List_Prefab_Fish[_j], Vector2.zero, Quaternion.identity, transform);
                        _Pool_Fish.Enqueue(_go);
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
                        _Pool_SingleFish.Enqueue(_go);
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
                        _Pool_TargetFish.Enqueue(_go);
                        _go.SetActive(false);
                        _t.Add(_j);
                        _x++;
                    }
                    if (_t.Count >= _List_Prefab_TargetFish.Count)
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
                for (int _x = 0; _x < _List_Prefab_JellyFish.Count; _x++)
                {
                    GameObject _go = Instantiate(_List_Prefab_JellyFish[_x], Vector2.zero, Quaternion.identity, transform);
                    _JellyFish_Pool.Enqueue(_go);
                    _go.SetActive(false);
                }
                for (int _x = 0; _x < _List_Prefab_NPC_ZoneOne.Count;)
                {
                    for (int _j = Random.Range(0, _List_Prefab_NPC_ZoneOne.Count); _t.Count < _List_Prefab_NPC_ZoneOne.Count;)
                    {
                        if (_t.Contains(_j))
                            break;
                        GameObject _go = Instantiate(_List_Prefab_NPC_ZoneOne[_j], Vector2.zero, Quaternion.identity, transform);
                        _Pool_NPC_ZoneOne.Enqueue(_go);
                        _go.SetActive(false);
                        _t.Add(_j);
                        _x++;
                    }
                    if (_t.Count >= _List_Prefab_NPC_ZoneOne.Count)
                        _t.Clear();
                }
                for (int _x = 0; _x < _List_Prefab_NPC_ZoneTwo.Count;)
                {
                    for (int _j = Random.Range(0, _List_Prefab_NPC_ZoneTwo.Count); _t.Count < _List_Prefab_NPC_ZoneTwo.Count;)
                    {
                        if (_t.Contains(_j))
                            break;
                        GameObject _go = Instantiate(_List_Prefab_NPC_ZoneTwo[_j], Vector2.zero, Quaternion.identity, transform);
                        _Pool_NPC_ZoneTwo.Enqueue(_go);
                        _go.SetActive(false);
                        _t.Add(_j);
                        _x++;
                    }
                    if (_t.Count >= _List_Prefab_NPC_ZoneTwo.Count)
                        _t.Clear();
                }
            }
        }

        private void Start()
        {
            RandomWaypointsInitialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                IEnumeratorSpawnNpc01(true);
            }
        }

        private void ReUseAI(Queue<GameObject> _queue_gameobject, EnemyAI.Screen _screen, EnemyAI.Status _status, Vector2 _position, float _scale_magnification, float _speed, float _activity_time, bool _fade_disappear)
        {
            if (_queue_gameobject.Count <= 0) { Debug.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            EnemyAI._Recycle = false;
            GameObject _go = _queue_gameobject.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _position;
            _go.transform.rotation = Quaternion.identity;
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Queue_GameObject = _queue_gameobject;
            _enemy_ai._ScaleMagnification = _scale_magnification;
            _enemy_ai._Speed = _speed;
            _enemy_ai._ActivityTime = _activity_time;
            _enemy_ai._FadeDisappear = _fade_disappear;
            _enemy_ai.ScreenChange(_screen);
            _enemy_ai.StateChange(_status);
            _CurrentCount++;
        }
        private void ReUseAI(Queue<GameObject> _queue_gameobject, EnemyAI.Screen _screen, EnemyAI.Status _status, Vector2 _position, float _speed, float _activity_time, bool _fade_disappear)
        {
            if (_queue_gameobject.Count <= 0) { Debug.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            EnemyAI._Recycle = false;
            GameObject _go = _queue_gameobject.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _position;
            _go.transform.rotation = Quaternion.identity;
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Queue_GameObject = _queue_gameobject;
            _enemy_ai._ScaleMagnification = 1.0f;
            _enemy_ai._Speed = _speed;
            _enemy_ai._ActivityTime = _activity_time;
            _enemy_ai._FadeDisappear = _fade_disappear;
            _enemy_ai.ScreenChange(_screen);
            _enemy_ai.StateChange(_status);
            _CurrentCount++;
        }
        private void ReUseNPC(Queue<GameObject> _queue_gameobject)
        {
            if (_queue_gameobject.Count <= 0) { Debug.LogWarningFormat("Queue index out of range. Count: {1}.", _queue_gameobject.Count); return; }
            NPC._Recycle = false;
            GameObject _go = _queue_gameobject.Dequeue();
            _go.SetActive(true);
            NPC _npc = _go.GetComponent<NPC>();
            _npc._Queue_GameObject = _queue_gameobject;
            _CurrentCount_NPC++;
        }
        public void RecycleAI(Queue<GameObject> _queue_gameobject, GameObject _go)
        {
            _queue_gameobject.Enqueue(_go);
            _go.SetActive(false);
            _CurrentCount--;
        }
        public void RecycleNPC(Queue<GameObject> _queue_gameobject, GameObject _go)
        {
            _queue_gameobject.Enqueue(_go);
            _go.SetActive(false);
            _CurrentCount_NPC--;
        }

        private void ReUse(Queue<GameObject> _queue, EnemyAI.Status _status, Vector3 _position, Quaternion _rotation, float _speed, float _time_out, bool _disappear_status)
        {
            EnemyAI._Recycle = false;
            if (_queue.Count > 0)
            {
                GameObject _go = _queue.Dequeue();
                _go.SetActive(true);
                _go.transform.position = _position;
                _go.transform.rotation = _rotation;
                EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
                _enemy_ai._Queue_GameObject = _queue;
                _enemy_ai._Speed = _speed;
                //_enemy_ai._TimeOut = _time_out;
                _enemy_ai._FadeDisappear = _disappear_status;
                if (_queue == _TargetFishZoneTwo_Pool)
                {
                    //_enemy_ai._SpriteRenderer.color = Color.white;
                    //_enemy_ai._FadeDisappear_Time = 0.1f;
                }
                if (_queue == _ObstacleFishZoneTwo_Pool)
                {
                    _enemy_ai._ScaleMagnification = 1.5f;
                }
                if (_queue == _Pool_NPC_ZoneOne)
                {
                    float _alpha = Random.Range(0.1f, 0.8f);
                    if (_alpha < 0.3f) _enemy_ai._ScaleMagnification = 0.2f;
                    else if (_alpha < 0.55f) _enemy_ai._ScaleMagnification = 0.6f;
                    else if (_alpha < 0.8f) _enemy_ai._ScaleMagnification = 1.0f;
                    //_enemy_ai._SpriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, _alpha);
                    //_enemy_ai._FadeDisappear_Time = 0.025f;
                    //_CurrentCount_BackgroundFish++;
                }
                if (_queue == _Pool_NPC_ZoneTwo)
                {
                    float _alpha = Random.Range(0.1f, 0.8f);
                    if (_alpha < 0.3f) _enemy_ai._ScaleMagnification = 0.8f;
                    else if (_alpha < 0.55f) _enemy_ai._ScaleMagnification = 0.9f;
                    else if (_alpha < 0.8f) _enemy_ai._ScaleMagnification = 1.0f;
                    //_enemy_ai._SpriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, _alpha);
                    //_enemy_ai._FadeDisappear_Time = 0.025f;
                    //_CurrentCount_BackgroundFish++;
                }
                if (_queue != _Pool_NPC_ZoneOne && _queue != _Pool_NPC_ZoneTwo)
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
        // 螢幕外倒數消失
        private void ReUse(Queue<GameObject> _queue, EnemyAI.Status _status, Vector3 _position, float _speed, float _time_out)
        {
            EnemyAI._Recycle = false;
            if (_queue.Count <= 0) { Debug.LogWarningFormat("{0} pool count: {1}", nameof(_queue), _queue.Count); return; }
            GameObject _go = _queue.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _position;
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Queue_GameObject = _queue;
            _enemy_ai._ScaleMagnification = 1.0f;
            _enemy_ai._Speed = _speed;
            //_enemy_ai._TimeOut = _time_out;
            _enemy_ai._FadeDisappear = false;
            //_enemy_ai._SpriteRenderer.color = Color.white;
        }
        // 逐漸消失
        private void ReUse(Queue<GameObject> _queue, EnemyAI.Status _status, Vector3 _position, float _speed, bool _enable_disappear, float _disappear_time)
        {
            EnemyAI._Recycle = false;
            if (_queue.Count <= 0) { Debug.LogWarningFormat("{0} pool count: {1}", nameof(_queue), _queue.Count); return; }
        }
        public void Recovery(Queue<GameObject> _queue, GameObject _go)
        {
            _queue.Enqueue(_go);
            _go.SetActive(false);
            _CurrentCount--;
        }
        private void ReUseJellyFish()
        {
            if (_JellyFish_Pool.Count <= 0) { Debug.LogWarningFormat("{0} current count: {1}.", nameof(_JellyFish_Pool), _JellyFish_Pool.Count); return; }
            JellyFishEnemyAI._Recycle = false;
            GameObject _go = _JellyFish_Pool.Dequeue();
            _go.SetActive(true);
        }
        public void RecycleJellyFish(GameObject _go)
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
            foreach (GameObject _go in _Waypoints)
            {
                //float _random_x = Random.Range(_origin.x, _vertex.x);
                //float _random_y = Random.Range(_origin.y, _vertex.y);
                //_go.transform.position = new Vector2(_random_x, _random_y);
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

        private IEnumerator _SpawnNpc_NPC_Singleton;
        private IEnumerator _SpawnNpc_Logic_NPC()
        {
            float _interval;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _interval = Random.Range(1.0f, 2.0f);
                if (GameManager._Instance._Meter >= 4000.0f)
                {
                    _MaxCount_NPC = 3;
                    ReUseNPC(_Pool_NPC_ZoneTwo);
                    yield return new WaitForSeconds(_interval);
                    continue;
                }
                if (GameManager._Instance._Meter >= 0.0f)
                {
                    _MaxCount_NPC = 10;
                    ReUseNPC(_Pool_NPC_ZoneOne);
                    yield return new WaitForSeconds(_interval);
                    continue;
                }
            }
        }
        public void IEnumeratorSpawnNpcNPC(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_NPC_Singleton != null)
                    StopCoroutine(_SpawnNpc_NPC_Singleton);
                _SpawnNpc_NPC_Singleton = _SpawnNpc_Logic_NPC();
                StartCoroutine(_SpawnNpc_NPC_Singleton);
            }
            else
            {
                if (_SpawnNpc_NPC_Singleton != null)
                    StartCoroutine(_SpawnNpc_NPC_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_00_Singleton;
        private IEnumerator _SpawnNpc_Logic_00()
        {
            _MaxCount = 10;
            Vector2 _position;
            float _speed;
            float _activity_time = 1.0f;
            int _r;
            float _interval;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (_CurrentCount >= _MaxCount) { Debug.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                _speed = Random.Range(1.5f, 2.5f);
                _interval = Random.Range(0.1f, 0.5f);
                if (MovementSystem._Instance._VelocityX() > 1.0f)
                {
                    _position.x = _Vertex().x;
                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                    yield return new WaitForSeconds(_interval);
                    continue;
                }
                if (MovementSystem._Instance._VelocityX() < -1.0f)
                {
                    _position.x = _Origin().x;
                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                    yield return new WaitForSeconds(_interval);
                    continue;
                }
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        _position.x = _Origin().x;
                        _position.y = Random.Range(_Origin().y, _Vertex().y);
                        ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                        break;
                    case 1:
                        _position.x = _Vertex().x;
                        _position.y = Random.Range(_Origin().y, _Vertex().y);
                        ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                        break;
                }
                yield return new WaitForSeconds(_interval);
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
            switch (_index)
            {
                case 0:
                    {
                        _MaxCount = 10;
                        Vector2 _position;
                        float _speed;
                        float _activity_time = 1.0f;
                        int _r;
                        float _interval;
                        while (true)
                        {
                            yield return new WaitForEndOfFrame();
                            if (_CurrentCount >= _MaxCount) { Debug.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _speed = Random.Range(1.5f, 2.5f);
                            _r = Random.Range(0, 2);
                            _interval = Random.Range(0.1f, 0.5f);
                            if (MovementSystem._Instance._VelocityX() > 1.0f)
                            {
                                _position.x = _Vertex().x;
                                _position.y = Random.Range(_Origin().y, _Vertex().y);
                                ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimRightStyle, _position, _speed, _activity_time, false);
                                yield return new WaitForSeconds(_interval);
                                continue;
                            }
                            if (MovementSystem._Instance._VelocityX() < -1.0f)
                            {
                                _position.x = _Origin().x;
                                _position.y = Random.Range(_Origin().y, _Vertex().y);
                                ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimLeftStyle, _position, _speed, _activity_time, false);
                                yield return new WaitForSeconds(_interval);
                                continue;
                            }
                            switch (_r)
                            {
                                case 0:
                                    _position.x = _Origin().x;
                                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimLeftStyle, _position, _speed, _activity_time, false);
                                    break;
                                case 1:
                                    _position.x = _Vertex().x;
                                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimRightStyle, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return new WaitForSeconds(_interval);
                        }
                    }
                    break;
                case 1:
                    {
                        int _i = Random.Range(0, 4);
                        switch (_i)
                        {
                            case 0:
                                {
                                    Vector2 _position;
                                    float _speed;
                                    float _activity_time = 1.0f;
                                    float _interval = 0.5f;
                                    bool _initialize = false;
                                    float[] _points = new float[8];
                                    float _result;
                                    while (true)
                                    {
                                        yield return new WaitForEndOfFrame();
                                        if (!_initialize)
                                        {
                                            _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = _Vertex().y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = 0; _x < _points.Length; _x++)
                                        {
                                            _position.x = _Origin().x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                            yield return new WaitForSeconds(_interval);
                                        }
                                    }
                                }
                                break;
                            case 1:
                                {
                                    Vector2 _position;
                                    float _speed;
                                    float _activity_time = 1.0f;
                                    float _interval = 0.5f;
                                    bool _initialize = false;
                                    float[] _points = new float[8];
                                    float _result;
                                    while (true)
                                    {
                                        yield return new WaitForEndOfFrame();
                                        if (!_initialize)
                                        {
                                            _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = _Vertex().y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = _points.Length - 1; _x >= 0; _x--)
                                        {
                                            _position.x = _Origin().x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                            yield return new WaitForSeconds(_interval);
                                        }
                                    }
                                }
                                break;
                            case 2:
                                {
                                    Vector2 _position;
                                    float _speed;
                                    float _activity_time = 1.0f;
                                    float _interval = 0.5f;
                                    bool _initialize = false;
                                    float[] _points = new float[8];
                                    float _result;
                                    while (true)
                                    {
                                        yield return new WaitForEndOfFrame();
                                        if (!_initialize)
                                        {
                                            _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = _Vertex().y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = 0; _x < _points.Length; _x++)
                                        {
                                            _position.x = _Vertex().x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                            yield return new WaitForSeconds(_interval);
                                        }
                                    }
                                }
                                break;
                            case 3:
                                {
                                    Vector2 _position;
                                    float _speed;
                                    float _activity_time = 1.0f;
                                    float _interval = 0.5f;
                                    bool _initialize = false;
                                    float[] _points = new float[8];
                                    float _result;
                                    while (true)
                                    {
                                        yield return new WaitForEndOfFrame();
                                        if (!_initialize)
                                        {
                                            _result = (_Origin().y - _Vertex().y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = _Vertex().y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = _points.Length - 1; _x >= 0; _x--)
                                        {
                                            _position.x = _Vertex().x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                            yield return new WaitForSeconds(_interval);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        _MaxCount = 15;
                        Vector2 _position;
                        float _speed;
                        float _activity_time = 2.0f;
                        int _r;
                        float _interval;
                        while (true)
                        {
                            yield return new WaitForEndOfFrame();
                            if (_CurrentCount >= _MaxCount) { Debug.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _r = Random.Range(1, 3);
                            _speed = Random.Range(2.0f, 3.0f);
                            _interval = Random.Range(0.5f, 1.0f);
                            switch (_r)
                            {
                                case 1:
                                    _position.x = _Origin().x;
                                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                                case 2:
                                    _position.x = _Vertex().x;
                                    _position.y = Random.Range(_Origin().y, _Vertex().y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return new WaitForSeconds(_interval);
                        }
                    }
                    break;
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
            if (_index == 0)
            {
                bool _initialize = false;
                float[] _points_00 = new float[8];
                float[] _points_01 = new float[5];
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_Pool_SingleFish.Count <= 0) continue;
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
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _Vertex().y), Quaternion.identity, _speed, _time_out, false);
                            _j++;
                        }
                        if (_j >= 0 && _j < _points_00.Length - 2)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _points_00[_j]), Quaternion.identity, _speed, _time_out, false);
                            _j++;
                        }
                        if (_j >= _points_00.Length - 2)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _Origin().y), Quaternion.identity, _speed, _time_out, false);
                        }
                        if (_k >= 0)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _points_01[_k]), Quaternion.identity, _speed, _time_out, false);
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
                    if (_Pool_SingleFish.Count <= 0) continue;
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
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _Vertex().y), Quaternion.identity, _speed, _time_out, false);
                            _j++;
                        }
                        if (_j >= 0 && _j < _points_00.Length - 2)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _points_00[_j]), Quaternion.identity, _speed, _time_out, false);
                            _j++;
                        }
                        if (_j >= _points_00.Length - 2)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimLeft, new Vector2(_Vertex().x + _SpawnOffsetMedium, _Origin().y), Quaternion.identity, _speed, _time_out, false);
                        }
                        if (_k >= 0)
                        {
                            ReUse(_Pool_SingleFish, EnemyAI.Status.SwimRight, new Vector2(_Origin().x + -_SpawnOffsetMedium, _points_01[_k]), Quaternion.identity, _speed, _time_out, false);
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
                    if (_Pool_TargetFish.Count <= 0) continue;
                    if (_CurrentCount >= _MaxCount && _MaxCount != 0) continue;
                    float _speed = Random.Range(1.5f, 3.5f);
                    float _time_out = 2.0f;
                    int _i = Random.Range(1, 3);
                    switch (_i)
                    {
                        case 1:
                            ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
                            break;
                        case 2:
                            ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, _time_out, false);
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
                            if (_Pool_TargetFish.Count > 0)
                                ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _i_speed, _i_time_out, false);
                            break;
                        case 2:
                            if (_Pool_TargetFish.Count > 0)
                                ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _i_speed, _i_time_out, false);
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
                            if (_Pool_SingleFish.Count > 0)
                                ReUse(_Pool_SingleFish, EnemyAI.Status.Patrol, new Vector2(_Origin().x + -_SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _j_speed, _j_time_out, false);
                            break;
                        case 2:
                            if (_Pool_SingleFish.Count > 0)
                                ReUse(_Pool_SingleFish, EnemyAI.Status.Patrol, new Vector2(_Vertex().x + _SpawnOffsetMedium, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _j_speed, _j_time_out, false);
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
                        ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Origin().x + -_SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
                        break;
                    case 1:
                        ReUse(_Pool_TargetFish, EnemyAI.Status.Target, new Vector2(_Vertex().x + _SpawnOffset, Random.Range(_Origin().y, _Vertex().y)), Quaternion.identity, _speed, -1.0f, true);
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
            if (GameManager._Instance._Result >= 600 && GameManager._Instance._Result < 2000) IEnumeratorSpawnNpc01(true);
            if (GameManager._Instance._Result >= 2000 && GameManager._Instance._Result < 4000) IEnumeratorSpawnNpc02(true);
            if (GameManager._Instance._Result >= 4000 && GameManager._Instance._Result < 8000) IEnumeratorSpawnNpc03(true);
            if (GameManager._Instance._Result >= 8000) IEnumeratorSpawnNpc04(true);
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
}
