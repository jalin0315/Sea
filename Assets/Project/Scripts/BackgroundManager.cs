using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CTJ
{
    public class BackgroundManager : MonoBehaviour
    {
        public static BackgroundManager _Instance;
        [SerializeField] private Transform _ParentTarget;
        public Transform _Transform_Camera;
        public float _UpperLimit;
        [SerializeField] private GameObject _Prefab_00;
        [SerializeField] private GameObject _Prefab_01;
        [SerializeField] private GameObject _Prefab_02;
        [SerializeField] private GameObject _Prefab_03;
        [SerializeField] private GameObject _Prefab_04;
        [SerializeField] private GameObject _Prefab_05;
        [SerializeField] private GameObject _Prefab_06;
        [SerializeField] private GameObject _Prefab_07;
        [SerializeField] private int _InitailSize;
        public Queue<GameObject> _Pool_Background_00 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_01 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_02 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_03 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_04 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_05 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_06 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_07 = new Queue<GameObject>();

        private void Awake()
        {
            _Instance = this;
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_00, _ParentTarget);
                _Pool_Background_00.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_01, _ParentTarget);
                _Pool_Background_01.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_02, _ParentTarget);
                _Pool_Background_02.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_03, _ParentTarget);
                _Pool_Background_03.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_04, _ParentTarget);
                _Pool_Background_04.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_05, _ParentTarget);
                _Pool_Background_05.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_06, _ParentTarget);
                _Pool_Background_06.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitailSize; _i++)
            {
                GameObject _go = Instantiate(_Prefab_07, _ParentTarget);
                _Pool_Background_07.Enqueue(_go);
                _go.SetActive(false);
            }
        }

        public void ReUse(Queue<GameObject> _queue_gameobject)
        {
            if (_queue_gameobject.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            BackgroundControl._RecoveryAll = false;
            GameObject _reuse = _queue_gameobject.Dequeue();
            BackgroundControl _b_c = _reuse.GetComponent<BackgroundControl>();
            _b_c._Queue_GameObject = _queue_gameobject;
            _reuse.SetActive(true);
        }

        public void Recycle(Queue<GameObject> _queue_gameobject, GameObject _go)
        {
            _queue_gameobject.Enqueue(_go);
            _go.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 _upper_boundary = new Vector2(_Transform_Camera.position.x, _UpperLimit);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_upper_boundary, 1.0f);
        }
    }
}
