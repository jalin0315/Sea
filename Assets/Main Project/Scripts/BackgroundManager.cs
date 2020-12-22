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
        [SerializeField] private GameObject _Prefab;
        [SerializeField] private int _InitailSize = 5;
        public Queue<GameObject> _Background_00_Pool = new Queue<GameObject>();

        private void Start()
        {
            _Instance = this;
            for (int _cnt = 0; _cnt < _InitailSize; _cnt++)
            {
                GameObject _go = Instantiate(_Prefab, _ParentTarget);
                _Background_00_Pool.Enqueue(_go);
                _go.SetActive(false);
            }
        }

        public void ReUse(Queue<GameObject> _pool)
        {
            BackgroundControl._RecoveryAll = false;
            if (_pool.Count > 0)
            {
                GameObject _reuse = _pool.Dequeue();
                BackgroundControl _b_c = _reuse.GetComponent<BackgroundControl>();
                _b_c._Pool = _pool;
                _b_c.ReInitialize();
                _reuse.SetActive(true);
            }
            else Debug.LogErrorFormat("{0} is out of range!");
        }

        public void Recovery(Queue<GameObject> _pool, GameObject _recovery)
        {
            _pool.Enqueue(_recovery);
            _recovery.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 _upper_boundary = new Vector2(_Transform_Camera.position.x, _UpperLimit);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_upper_boundary, 10.0f);
        }
    }
}
