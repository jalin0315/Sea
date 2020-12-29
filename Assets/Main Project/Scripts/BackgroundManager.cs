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
        [SerializeField] private int _InitailSize;
        public Queue<GameObject> _Pool_Background_00 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_01 = new Queue<GameObject>();
        public Queue<GameObject> _Pool_Background_02 = new Queue<GameObject>();

        private void Start()
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
        }

        public void ReUse(Queue<GameObject> _queue_gameobject)
        {
            if (_queue_gameobject.Count <= 0) { Debug.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            BackgroundControl._RecoveryAll = false;
            GameObject _reuse = _queue_gameobject.Dequeue();
            BackgroundControl _b_c = _reuse.GetComponent<BackgroundControl>();
            _b_c._Queue_GameObject = _queue_gameobject;
            _b_c.ReInitialize();
            _reuse.SetActive(true);
        }

        public void Recovery(Queue<GameObject> _queue_gameobject, GameObject _go)
        {
            _queue_gameobject.Enqueue(_go);
            _go.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 _upper_boundary = new Vector2(_Transform_Camera.position.x, _UpperLimit);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_upper_boundary, 10.0f);
        }
    }
}
