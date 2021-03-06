﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace CTJ
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager _Instance;
        [SerializeField] private Transform _Parent;
        [SerializeField] private GameObject _Waypoint;
        [SerializeField] private int _NumberOfWaypoints;
        [HideInInspector] public List<GameObject> _Waypoints = new List<GameObject>();
        [SerializeField] private int _Quantity_Size;
        [SerializeField] private List<GameObject> _List_Prefab_Fish;
        [SerializeField] private List<GameObject> _List_Prefab_SingleFish;
        [SerializeField] private List<GameObject> _List_Prefab_TargetFish;
        [SerializeField] private List<GameObject> _List_Prefab_ObstacleFishZoneTwo;
        [SerializeField] private List<GameObject> _List_Prefab_TargetFishZoneTwo;
        [SerializeField] private List<GameObject> _List_Prefab_JellyFish;
        [SerializeField] private List<GameObject> _List_Prefab_ClioneLimacina;
        [SerializeField] private List<GameObject> _List_Prefab_Human;
        [SerializeField] private List<GameObject> _List_Prefab_Guard;
        [SerializeField] private List<GameObject> _List_Prefab_Boss;
        [SerializeField] private List<GameObject> _List_Prefab_NPC_ZoneOne;
        [SerializeField] private List<GameObject> _List_Prefab_NPC_ZoneTwo;
        private Queue<GameObject> _Pool_Fish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_SingleFish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_TargetFish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_ObstacleFish_ZoneTwo = new Queue<GameObject>();
        private Queue<GameObject> _Pool_TargetFish_ZoneTwo = new Queue<GameObject>();
        private Queue<GameObject> _Pool_JellyFish = new Queue<GameObject>();
        private Queue<GameObject> _Pool_ClioneLimacina = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Human = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Guard = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Boss = new Queue<GameObject>();
        private Queue<GameObject> _Pool_NPC_ZoneOne = new Queue<GameObject>();
        private Queue<GameObject> _Pool_NPC_ZoneTwo = new Queue<GameObject>();
        private int _CurrentCount;
        private int _CurrentCount_NPC;
        private int _MaxCount;
        private int _MaxCount_NPC;

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
                        _Pool_ObstacleFish_ZoneTwo.Enqueue(_go);
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
                        _Pool_TargetFish_ZoneTwo.Enqueue(_go);
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
                    _Pool_JellyFish.Enqueue(_go);
                    _go.SetActive(false);
                }
                for (int _x = 0; _x < _List_Prefab_Guard.Count; _x++)
                {
                    GameObject _go = Instantiate(_List_Prefab_Guard[_x], Vector2.zero, Quaternion.identity, transform);
                    _Pool_Guard.Enqueue(_go);
                    _go.SetActive(false);
                }
                for (int _x = 0; _x < _List_Prefab_ClioneLimacina.Count; _x++)
                {
                    GameObject _go = Instantiate(_List_Prefab_ClioneLimacina[_x], Vector2.zero, Quaternion.identity, transform);
                    _Pool_ClioneLimacina.Enqueue(_go);
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
            for (int _x = 0; _x < _List_Prefab_Human.Count; _x++)
            {
                GameObject _go = Instantiate(_List_Prefab_Human[_x], Vector2.zero, Quaternion.identity, transform);
                _Pool_Human.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _x = 0; _x < _List_Prefab_Boss.Count; _x++)
            {
                GameObject _go = Instantiate(_List_Prefab_Boss[_x], _List_Prefab_Boss[_x].transform.position, Quaternion.identity, transform);
                _Pool_Boss.Enqueue(_go);
                _go.SetActive(false);
            }
        }

        private void Start()
        {
            RandomWaypointsInitialize();
        }

        private void ReUseAI(Queue<GameObject> _queue_gameobject, EnemyAI.Screen _screen, EnemyAI.Status _status, Vector2 _position, float _scale_magnification, float _speed, float _activity_time, bool _fade_disappear)
        {
            if (_queue_gameobject.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
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
            if (_queue_gameobject.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            EnemyAI._Recycle = false;
            GameObject _go = _queue_gameobject.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _position;
            _go.transform.rotation = Quaternion.identity;
            EnemyAI _enemy_ai = _go.GetComponent<EnemyAI>();
            _enemy_ai._Queue_GameObject = _queue_gameobject;
            if (GameManager._Meter >= 6000.0f) _enemy_ai._ScaleMagnification = 1.8f;
            else if (GameManager._Meter >= 4000.0f) _enemy_ai._ScaleMagnification = 1.6f;
            else if (GameManager._Meter >= 2000.0f) _enemy_ai._ScaleMagnification = 1.4f;
            else if (GameManager._Meter >= 1000.0f) _enemy_ai._ScaleMagnification = 1.2f;
            else if (GameManager._Meter >= 0.0f) _enemy_ai._ScaleMagnification = 1.0f;
            _enemy_ai._Speed = _speed;
            _enemy_ai._ActivityTime = _activity_time;
            _enemy_ai._FadeDisappear = _fade_disappear;
            _enemy_ai.ScreenChange(_screen);
            _enemy_ai.StateChange(_status);
            _CurrentCount++;
        }
        private void ReUseJellyFish()
        {
            if (_Pool_JellyFish.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_JellyFish.Count); return; }
            JellyFishEnemyAI._Recycle = false;
            GameObject _go = _Pool_JellyFish.Dequeue();
            _go.SetActive(true);
        }
        private void ReUseClioneLimacina()
        {
            if (_Pool_ClioneLimacina.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_ClioneLimacina.Count); return; }
            ClioneLimacina._Recycle = false;
            GameObject _go = _Pool_ClioneLimacina.Dequeue();
            _go.SetActive(true);
        }
        private void ReUseHuman()
        {
            if (_Pool_Human.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_Human.Count); return; }
            HumanEnemyAI._Recycle = false;
            GameObject _go = _Pool_Human.Dequeue();
            _go.SetActive(true);
        }
        public void ReUseBoss()
        {
            if (_Pool_Boss.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_Boss.Count); return; }
            GameObject _go = _Pool_Boss.Dequeue();
            _go.transform.position = new Vector2((CameraControl._Origin.x + CameraControl._Vertex.x) * 0.5f, _go.transform.position.y);
            _go.SetActive(true);
        }
        private void ReUseNPC(Queue<GameObject> _queue_gameobject)
        {
            if (_queue_gameobject.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {1}.", _queue_gameobject.Count); return; }
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
        public void RecycleJellyFish(GameObject _go)
        {
            _Pool_JellyFish.Enqueue(_go);
            _go.SetActive(false);
        }
        public void RecycleClioneLimacina(GameObject _go)
        {
            _Pool_ClioneLimacina.Enqueue(_go);
            _go.SetActive(false);
        }
        public void RecycleHuman(GameObject _go)
        {
            _Pool_Human.Enqueue(_go);
            _go.SetActive(false);
        }
        public void RecycleBoss(GameObject _go)
        {
            _Pool_Boss.Enqueue(_go);
            _go.SetActive(false);
        }
        public void RecycleNPC(Queue<GameObject> _queue_gameobject, GameObject _go)
        {
            _queue_gameobject.Enqueue(_go);
            _go.SetActive(false);
            _CurrentCount_NPC--;
        }
        private void RandomWaypointsInitialize()
        {
            Vector2 _random;
            for (int _i = 0; _i < _NumberOfWaypoints; _i++)
            {
                _random.x = Random.Range(CameraControl._Origin.x, CameraControl._Vertex.x);
                _random.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                GameObject _go = Instantiate(_Waypoint, _random, Quaternion.identity, _Parent);
                _Waypoints.Add(_go);
            }
        }
        public void ReRandomWaypoints()
        {
            Vector2 _random;
            for (int _i = 0; _i < _Waypoints.Count; _i++)
            {
                _random.x = Random.Range(CameraControl._Origin.x, CameraControl._Vertex.x);
                _random.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                _Waypoints[_i].transform.position = _random;
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
                yield return OPT._WaitForEndOfFrame;
                if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                _speed = Random.Range(1.5f, 2.5f);
                _interval = Random.Range(0.1f, 0.5f);
                if (MovementSystem._Instance._VelocityX() > 1.0f)
                {
                    _position.x = CameraControl._Vertex.x;
                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                    yield return OPT._WaitForSeconds(_interval);
                    continue;
                }
                if (MovementSystem._Instance._VelocityX() < -1.0f)
                {
                    _position.x = CameraControl._Origin.x;
                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                    yield return OPT._WaitForSeconds(_interval);
                    continue;
                }
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                        break;
                    case 1:
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                        break;
                }
                yield return OPT._WaitForSeconds(_interval);
            }
        }
        public void IEnumeratorSpawnNpc00(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_00_Singleton != null) StopCoroutine(_SpawnNpc_00_Singleton);
                _SpawnNpc_00_Singleton = _SpawnNpc_Logic_00();
                StartCoroutine(_SpawnNpc_00_Singleton);
            }
            else
            {
                if (_SpawnNpc_00_Singleton != null) StopCoroutine(_SpawnNpc_00_Singleton);
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
                            yield return OPT._WaitForEndOfFrame;
                            if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _speed = Random.Range(1.5f, 2.5f);
                            _r = Random.Range(0, 2);
                            _interval = Random.Range(0.1f, 0.5f);
                            if (MovementSystem._Instance._VelocityX() > 1.0f)
                            {
                                _position.x = CameraControl._Vertex.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimRightStyle, _position, _speed, _activity_time, false);
                                yield return OPT._WaitForSeconds(_interval);
                                continue;
                            }
                            if (MovementSystem._Instance._VelocityX() < -1.0f)
                            {
                                _position.x = CameraControl._Origin.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimLeftStyle, _position, _speed, _activity_time, false);
                                yield return OPT._WaitForSeconds(_interval);
                                continue;
                            }
                            switch (_r)
                            {
                                case 0:
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimLeftStyle, _position, _speed, _activity_time, false);
                                    break;
                                case 1:
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimRightStyle, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return OPT._WaitForSeconds(_interval);
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
                                        yield return OPT._WaitForEndOfFrame;
                                        if (!_initialize)
                                        {
                                            _result = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = CameraControl._Vertex.y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = 0; _x < _points.Length; _x++)
                                        {
                                            _position.x = CameraControl._Origin.x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                            yield return OPT._WaitForSeconds(_interval);
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
                                        yield return OPT._WaitForEndOfFrame;
                                        if (!_initialize)
                                        {
                                            _result = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = CameraControl._Vertex.y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = _points.Length - 1; _x >= 0; _x--)
                                        {
                                            _position.x = CameraControl._Origin.x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                            yield return OPT._WaitForSeconds(_interval);
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
                                        yield return OPT._WaitForEndOfFrame;
                                        if (!_initialize)
                                        {
                                            _result = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = CameraControl._Vertex.y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = 0; _x < _points.Length; _x++)
                                        {
                                            _position.x = CameraControl._Vertex.x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                            yield return OPT._WaitForSeconds(_interval);
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
                                        yield return OPT._WaitForEndOfFrame;
                                        if (!_initialize)
                                        {
                                            _result = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points.Length + 1);
                                            for (int _x = 0; _x < _points.Length; _x++) _points[_x] = CameraControl._Vertex.y + (_result * (_x + 1));
                                            _initialize = true;
                                        }
                                        for (int _x = _points.Length - 1; _x >= 0; _x--)
                                        {
                                            _position.x = CameraControl._Vertex.x;
                                            _position.y = _points[_x];
                                            _speed = Random.Range(3.0f, 5.0f);
                                            ReUseAI(_Pool_Fish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                            yield return OPT._WaitForSeconds(_interval);
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
                            yield return OPT._WaitForEndOfFrame;
                            if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _r = Random.Range(1, 3);
                            _speed = Random.Range(2.0f, 3.0f);
                            _interval = Random.Range(0.5f, 1.0f);
                            switch (_r)
                            {
                                case 1:
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                                case 2:
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return OPT._WaitForSeconds(_interval);
                        }
                    }
                    break;
            }
        }
        public void IEnumeratorSpawnNpc01(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_01_Singleton != null) StopCoroutine(_SpawnNpc_01_Singleton);
                _SpawnNpc_01_Singleton = _SpawnNpc_Logic_01();
                StartCoroutine(_SpawnNpc_01_Singleton);
            }
            else
            {
                if (_SpawnNpc_01_Singleton != null) StopCoroutine(_SpawnNpc_01_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_02_Singleton;
        private IEnumerator _SpawnNpc_Logic_02()
        {
            IEnumeratorDuration(true);
            int _index = Random.Range(0, 4);
            switch (_index)
            {
                case 0:
                    {
                        Vector2 _position;
                        float _speed = 4.0f;
                        float _activity_time = 1.0f;
                        float _interval = 0.5f;
                        bool _initialize = false;
                        float[] _points_00 = new float[8];
                        float[] _points_01 = new float[5];
                        float _result_00;
                        float _result_01;
                        int _y;
                        int _z;
                        while (true)
                        {
                            yield return OPT._WaitForEndOfFrame;
                            if (!_initialize)
                            {
                                _result_00 = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points_00.Length - 1);
                                _result_01 = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points_01.Length + 1);
                                for (int _x = 0; _x < _points_00.Length - 2; _x++) _points_00[_x] = CameraControl._Vertex.y + (_result_00 * (_x + 1));
                                for (int _x = 0; _x < _points_01.Length; _x++) _points_01[_x] = CameraControl._Vertex.y + (_result_01 * (_x + 1));
                                _initialize = true;
                            }
                            _y = -1;
                            _z = _points_01.Length - 1;
                            while (true)
                            {
                                if (_y >= _points_00.Length - 2) break;
                                if (_y < 0)
                                {
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = CameraControl._Vertex.y;
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                    _y++;
                                }
                                if (_y >= 0 && _y < _points_00.Length - 2)
                                {
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = _points_00[_y];
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                    _y++;
                                }
                                if (_y >= _points_00.Length - 2)
                                {
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = CameraControl._Origin.y;
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                }
                                if (_z >= 0)
                                {
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = _points_01[_z];
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                    _z--;
                                }
                                yield return OPT._WaitForSeconds(_interval);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        Vector2 _position;
                        float _speed = 4.0f;
                        float _activity_time = 1.0f;
                        float _interval = 0.5f;
                        bool _initialize = false;
                        float[] _points_00 = new float[8];
                        float[] _points_01 = new float[5];
                        float _result_00;
                        float _result_01;
                        int _y;
                        int _z;
                        while (true)
                        {
                            yield return OPT._WaitForEndOfFrame;
                            if (!_initialize)
                            {
                                _result_00 = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points_00.Length - 1);
                                _result_01 = (CameraControl._Origin.y - CameraControl._Vertex.y) / (_points_01.Length + 1);
                                for (int _x = 0; _x < _points_00.Length - 2; _x++) _points_00[_x] = CameraControl._Vertex.y + (_result_00 * (_x + 1));
                                for (int _x = 0; _x < _points_01.Length; _x++) _points_01[_x] = CameraControl._Vertex.y + (_result_01 * (_x + 1));
                                _initialize = true;
                            }
                            _y = -1;
                            _z = _points_01.Length - 1;
                            while (true)
                            {
                                if (_y >= _points_00.Length - 2) break;
                                if (_y < 0)
                                {
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = CameraControl._Vertex.y;
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                    _y++;
                                }
                                if (_y >= 0 && _y < _points_00.Length - 2)
                                {
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = _points_00[_y];
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                    _y++;
                                }
                                if (_y >= _points_00.Length - 2)
                                {
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = CameraControl._Origin.y;
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, false);
                                }
                                if (_z >= 0)
                                {
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = _points_01[_z];
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, false);
                                    _z--;
                                }
                                yield return OPT._WaitForSeconds(_interval);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        _MaxCount = 5;
                        Vector2 _position;
                        float _speed;
                        float _activity_time = 1.0f;
                        int _r;
                        float _interval = 0.5f;
                        while (true)
                        {
                            yield return OPT._WaitForEndOfFrame;
                            if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _speed = Random.Range(2.0f, 4.0f);
                            if (MovementSystem._Instance._VelocityX() > 1.0f)
                            {
                                _position.x = CameraControl._Vertex.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                yield return OPT._WaitForSeconds(_interval);
                                continue;
                            }
                            if (MovementSystem._Instance._VelocityX() < -1.0f)
                            {
                                _position.x = CameraControl._Origin.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                yield return OPT._WaitForSeconds(_interval);
                                continue;
                            }
                            _r = Random.Range(0, 2);
                            switch (_r)
                            {
                                case 0:
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                                case 1:
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return OPT._WaitForSeconds(_interval);
                        }
                    }
                    break;
                case 3:
                    {
                        _MaxCount = 10;
                        Vector2 _position;
                        float _speed;
                        float _activity_time = 1.0f;
                        int _r;
                        float _interval;
                        while (true)
                        {
                            yield return OPT._WaitForEndOfFrame;
                            if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                            _speed = 2.5f;
                            _r = Random.Range(0, 2);
                            _interval = Random.Range(0.5f, 1.0f);
                            switch (_r)
                            {
                                case 0:
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                                case 1:
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_TargetFish, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, _speed, _activity_time, false);
                                    break;
                            }
                            _speed = Random.Range(2.0f, 3.0f);
                            _r = Random.Range(0, 2);
                            switch (_r)
                            {
                                case 0:
                                    _position.x = CameraControl._Origin.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Left, EnemyAI.Status.Patrol, _position, _speed, _activity_time, false);
                                    break;
                                case 1:
                                    _position.x = CameraControl._Vertex.x;
                                    _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                    ReUseAI(_Pool_SingleFish, EnemyAI.Screen.Right, EnemyAI.Status.Patrol, _position, _speed, _activity_time, false);
                                    break;
                            }
                            yield return OPT._WaitForSeconds(_interval);
                        }
                    }
                    break;
            }
        }
        public void IEnumeratorSpawnNpc02(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_02_Singleton != null) StopCoroutine(_SpawnNpc_02_Singleton);
                _SpawnNpc_02_Singleton = _SpawnNpc_Logic_02();
                StartCoroutine(_SpawnNpc_02_Singleton);
            }
            else
            {
                if (_SpawnNpc_02_Singleton != null) StopCoroutine(_SpawnNpc_02_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_03_Singleton;
        private IEnumerator _SpawnNpc_Logic_03()
        {
            Vector2 _position;
            float _speed;
            float _activity_time = 8.0f;
            int _r;
            float _interval;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                if (GameManager._Meter < 6000.0f)
                {
                    _MaxCount = 5;
                    if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                    if (MovementSystem._Instance._VelocityX() > 1.0f)
                    {
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                        _interval = Random.Range(0.5f, 2.0f);
                        yield return new WaitForSeconds(_interval);
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else if (MovementSystem._Instance._VelocityX() < -1.0f)
                    {
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                        _interval = Random.Range(0.5f, 2.0f);
                        yield return new WaitForSeconds(_interval);
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else
                    {
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                        _interval = Random.Range(1.0f, 3.0f);
                        yield return new WaitForSeconds(_interval);
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                    }
                    _interval = Random.Range(0.5f, 2.0f);
                    yield return OPT._WaitForSeconds(_interval);
                    if (MovementSystem._Instance._VelocityX() > 1.0f)
                    {
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else if (MovementSystem._Instance._VelocityX() < -1.0f)
                    {
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else
                    {
                        _r = Random.Range(0, 2);
                        switch (_r)
                        {
                            case 0:
                                _position.x = CameraControl._Origin.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                _speed = Random.Range(2.0f, 6.0f);
                                ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, 1.0f, _speed, _activity_time, true);
                                break;
                            case 1:
                                _position.x = CameraControl._Vertex.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                _speed = Random.Range(2.0f, 6.0f);
                                ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, 1.0f, _speed, _activity_time, true);
                                break;
                        }
                    }
                    _interval = Random.Range(0.5f, 2.0f);
                    yield return OPT._WaitForSeconds(_interval);
                }
                else if (GameManager._Meter >= 6000.0f)
                {
                    _MaxCount = 4;
                    if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                    if (MovementSystem._Instance._VelocityX() > 2.0f)
                    {
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else if (MovementSystem._Instance._VelocityX() < -2.0f)
                    {
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _speed = Random.Range(2.0f, 6.0f);
                        ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                    }
                    else
                    {
                        _r = Random.Range(0, 2);
                        switch (_r)
                        {
                            case 0:
                                _position.x = CameraControl._Origin.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                _speed = Random.Range(2.0f, 6.0f);
                                ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                                break;
                            case 1:
                                _position.x = CameraControl._Vertex.x;
                                _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                                _speed = Random.Range(2.0f, 6.0f);
                                ReUseAI(_Pool_TargetFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.Target, _position, 1.0f, _speed, _activity_time, true);
                                break;
                        }
                    }
                    _interval = 3.0f;
                    yield return OPT._WaitForSeconds(_interval);
                }
            }
        }
        public void IEnumeratorSpawnNpc03(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_03_Singleton != null) StopCoroutine(_SpawnNpc_03_Singleton);
                _SpawnNpc_03_Singleton = _SpawnNpc_Logic_03();
                StartCoroutine(_SpawnNpc_03_Singleton);
            }
            else
            {
                if (_SpawnNpc_03_Singleton != null) StopCoroutine(_SpawnNpc_03_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_04_Singleton;
        private IEnumerator _SpawnNpc_Logic_04()
        {
            Vector2 _position;
            float _speed;
            float _activity_time = 8.0f;
            int _r;
            float _interval;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                _MaxCount = 5;
                if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                _speed = Random.Range(3.0f, 5.0f);
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Left, EnemyAI.Status.SwimRight, _position, _speed, _activity_time, true);
                        break;
                    case 1:
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_ObstacleFish_ZoneTwo, EnemyAI.Screen.Right, EnemyAI.Status.SwimLeft, _position, _speed, _activity_time, true);
                        break;
                }
                _interval = Random.Range(1.0f, 4.0f);
                yield return OPT._WaitForSeconds(_interval);
            }
        }
        public void IEnumeratorSpawnNpc04(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_04_Singleton != null) StopCoroutine(_SpawnNpc_04_Singleton);
                _SpawnNpc_04_Singleton = _SpawnNpc_Logic_04();
                StartCoroutine(_SpawnNpc_04_Singleton);
            }
            else
            {
                if (_SpawnNpc_04_Singleton != null) StopCoroutine(_SpawnNpc_04_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_JellyFish_Singleton;
        private IEnumerator _SpawnNpc_Logic_JellyFish()
        {
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                ReUseJellyFish();
                yield return OPT._WaitForSeconds(Random.Range(0.0f, 0.5f));
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

        private IEnumerator _SpawnNpc_ClioneLimacina_Singleton;
        private IEnumerator _SpawnNpc_Logic_ClioneLimacina()
        {
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                ReUseClioneLimacina();
                yield return OPT._WaitForSeconds(Random.Range(0.0f, 0.5f));
            }
        }
        public void IEnumeratorSpawnNpcClioneLimacina(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_ClioneLimacina_Singleton != null) StopCoroutine(_SpawnNpc_ClioneLimacina_Singleton);
                _SpawnNpc_ClioneLimacina_Singleton = _SpawnNpc_Logic_ClioneLimacina();
                StartCoroutine(_SpawnNpc_ClioneLimacina_Singleton);
            }
            else
            {
                if (_SpawnNpc_ClioneLimacina_Singleton != null) StopCoroutine(_SpawnNpc_ClioneLimacina_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_Human_Singleton;
        private IEnumerator _SpawnNpc_Logic_Human()
        {
            Vector2 _position;
            float _speed;
            float _activity_time = 1.0f;
            int _r;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                ReUseHuman();
                _MaxCount = 6;
                if (_CurrentCount >= _MaxCount) { Logger.LogWarningFormat("{0} current count: {1}. Max count: {2}.", nameof(_CurrentCount), _CurrentCount, _MaxCount); continue; }
                _speed = Random.Range(1.0f, 3.0f);
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        _position.x = CameraControl._Origin.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_Guard, EnemyAI.Screen.Left, EnemyAI.Status.Patrol, _position, 2.5f, _speed, _activity_time, false);
                        break;
                    case 1:
                        _position.x = CameraControl._Vertex.x;
                        _position.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        ReUseAI(_Pool_Guard, EnemyAI.Screen.Right, EnemyAI.Status.Patrol, _position, 2.5f, _speed, _activity_time, false);
                        break;
                }
                ReUseJellyFish();
                yield return OPT._WaitForSeconds(Random.Range(1.0f, 3.0f));
            }
        }
        public void IEnumeratorSpawnNpcHuman(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_Human_Singleton != null) StopCoroutine(_SpawnNpc_Human_Singleton);
                _SpawnNpc_Human_Singleton = _SpawnNpc_Logic_Human();
                StartCoroutine(_SpawnNpc_Human_Singleton);
            }
            else
            {
                if (_SpawnNpc_Human_Singleton != null) StopCoroutine(_SpawnNpc_Human_Singleton);
            }
        }

        private IEnumerator _SpawnNpc_NPC_Singleton;
        private IEnumerator _SpawnNpc_Logic_NPC()
        {
            float _interval;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                _interval = Random.Range(1.0f, 2.0f);
                if (GameManager._Meter >= 4000.0f)
                {
                    _MaxCount_NPC = 3;
                    ReUseNPC(_Pool_NPC_ZoneTwo);
                    yield return OPT._WaitForSeconds(_interval);
                    continue;
                }
                if (GameManager._Meter >= 0.0f)
                {
                    _MaxCount_NPC = 10;
                    ReUseNPC(_Pool_NPC_ZoneOne);
                    yield return OPT._WaitForSeconds(_interval);
                    continue;
                }
            }
        }
        public void IEnumeratorSpawnNpcNPC(bool _enable)
        {
            if (_enable)
            {
                if (_SpawnNpc_NPC_Singleton != null) StopCoroutine(_SpawnNpc_NPC_Singleton);
                _SpawnNpc_NPC_Singleton = _SpawnNpc_Logic_NPC();
                StartCoroutine(_SpawnNpc_NPC_Singleton);
            }
            else
            {
                if (_SpawnNpc_NPC_Singleton != null) StartCoroutine(_SpawnNpc_NPC_Singleton);
            }
        }

        private IEnumerator _Duration_Singleton;
        private IEnumerator _Duration_Logic()
        {
            yield return OPT._WaitForSeconds(10.0f);
            IEnumeratorSpawnNpc00(false);
            IEnumeratorSpawnNpc01(false);
            IEnumeratorSpawnNpc02(false);
            IEnumeratorSpawnNpc03(false);
            IEnumeratorSpawnNpc04(false);
            yield return OPT._WaitForSeconds(2.0f);
            if (GameManager._Meter >= 8000.0f) IEnumeratorSpawnNpc04(true);
            else if (GameManager._Meter >= 4000.0f) IEnumeratorSpawnNpc03(true);
            else if (GameManager._Meter >= 2000.0f) IEnumeratorSpawnNpc02(true);
            else if (GameManager._Meter >= 600.0f) IEnumeratorSpawnNpc01(true);
        }
        public void IEnumeratorDuration(bool _enable)
        {
            if (_enable)
            {
                if (_Duration_Singleton != null) StopCoroutine(_Duration_Singleton);
                _Duration_Singleton = _Duration_Logic();
                StartCoroutine(_Duration_Singleton);
            }
            else
            {
                if (_Duration_Singleton != null) StopCoroutine(_Duration_Singleton);
            }
        }

        public void IEnumeratorStopAllCoroutines() => StopAllCoroutines();
    }
}
