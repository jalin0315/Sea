using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class BackgroundControl : MonoBehaviour
    {
        [SerializeField] private List<Transform> _List_Background;
        [SerializeField] private List<SpriteRenderer> _List_SpriteRenderer;
        [SerializeField] private List<Vector2> _List_StartPos;
        [SerializeField] private List<Vector2> _List_Length;
        [SerializeField] private List<float> _List_ParallaxEffect;
        [SerializeField] private List<float> _List_Original_Y;
        [SerializeField] private List<float> _List_Y;
        [SerializeField] private List<float> _List_Speed;
        [SerializeField] private List<float> _List_SpeedRate;
        [SerializeField] private int _RecoveryLastLayer;
        public Queue<GameObject> _Pool = new Queue<GameObject>();
        public static bool _RecoveryAll;

        private void Awake()
        {
            for (int _i = 0; _i < _List_Background.Count; _i++)
            {
                if (_List_Background[_i] == null) continue;
                Initialize(_i);
            }
        }
        private void Start()
        {
            for (int _i = 0; _i < _List_Background.Count; _i++)
            {
                if (_List_Background[_i] == null) continue;
                // X = 現速 / 原速
                _List_SpeedRate[_i] = GameManager._Instance._Time / 5;
            }
        }

        private void Update()
        {
            if (_RecoveryAll)
                BackgroundManager._Instance.Recovery(_Pool, gameObject);
        }

        private void FixedUpdate()
        {
            if (GameManager._Instance._InGame)
            {
                for (int _i = 0; _i < _List_Background.Count; _i++)
                {
                    if (_List_Background[_i] == null) continue;
                    Control(_i);
                }
            }
        }

        private void Initialize(int _i)
        {
            if (_List_Background[_i] == null) return;
            _List_StartPos[_i] = _List_Background[_i].position;
            _List_Length[_i] = _List_SpriteRenderer[_i].bounds.size;
            _List_Original_Y[_i] = _List_Background[_i].position.y;
            _List_Y[_i] = _List_Original_Y[_i];
        }
        public void ReInitialize()
        {
            for (int _i = 0; _i < _List_Background.Count; _i++)
            {
                if (_List_Background[_i] == null) continue;
                _List_Y[_i] = _List_Original_Y[_i];
                _List_Background[_i].position = new Vector3(_List_Background[_i].position.x, _List_Y[_i], _List_Background[_i].position.z);
            }
        }
        private void Control(int _i)
        {
            if (_List_Background[_i] == null) return;
            if (_List_Background[_i].position.y > BackgroundManager._Instance._UpperLimit)
            {
                if (_i == _RecoveryLastLayer) BackgroundManager._Instance.Recovery(_Pool, gameObject);
                return;
            }
            Vector2 _i_temp = BackgroundManager._Instance._Transform_Camera.position * (1.0f - _List_ParallaxEffect[_i]);
            Vector2 _i_dist = BackgroundManager._Instance._Transform_Camera.position * _List_ParallaxEffect[_i];
            //_List_Y[_i] += Time.deltaTime * _List_Speed[_i];
            _List_Y[_i] += TimeSystem._DeltaTime() * _List_Speed[_i] * _List_SpeedRate[_i];
            _List_Background[_i].position = new Vector3(_List_StartPos[_i].x + _i_dist.x, _List_Y[_i], _List_Background[_i].position.z);
            if (_i_temp.x > _List_StartPos[_i].x + _List_Length[_i].x)
            {
                var _start_pos_x = _List_StartPos[_i].x;
                _start_pos_x += _List_Length[_i].x;
                _List_StartPos[_i] = new Vector2(_start_pos_x, _List_StartPos[_i].y);
            }
            else if (_i_temp.x < _List_StartPos[_i].x - _List_Length[_i].x)
            {
                var _start_pos_x = _List_StartPos[_i].x;
                _start_pos_x -= _List_Length[_i].x;
                _List_StartPos[_i] = new Vector2(_start_pos_x, _List_StartPos[_i].y);
            }
        }

        /*
        private Vector2 _Length, _Startpos;
        private float _Y;
        private float _ParallaxEffect;
        private float _Y_Speed;
        */
        private void StartNull()
        {
            /*
            _Startpos = transform.position;
            _Length = GetComponent<SpriteRenderer>().bounds.size;
            _Y = transform.position.y;
            */
        }
        private void UpdateNull()
        {
            /*
            Vector2 _temp = (Camera.main.transform.position * (1 - _ParallaxEffect));
            Vector2 _dist = (Camera.main.transform.position * _ParallaxEffect);
            _Y += Time.deltaTime * _Y_Speed;
            //transform.position = new Vector3(_Startpos.x + _dist.x, _Startpos.y + _dist.y - 10, transform.position.z);
            transform.position = new Vector3(_Startpos.x + _dist.x, _Y, transform.position.z);
            if (_temp.x > _Startpos.x + _Length.x) _Startpos.x += _Length.x;
            else if (_temp.x < _Startpos.x - _Length.x) _Startpos.x -= _Length.x;
            if (transform.position.y > _UpperBoundary.y)
            {
                transform.position = new Vector3(transform.position.x, _LowerBoundary.y, transform.position.z);
                _Y = transform.position.y;
            }
            //if (_temp.y > _Startpos.y + _Length.y) _Startpos.y += _Length.y;
            //if (_temp.y < _Startpos.y - _Length.y) _Startpos.y -= _Length.y;
            */
        }
    }
}
