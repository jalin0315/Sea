using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class BossAI : MonoBehaviour
    {
        public static BossAI _Instance;
        public Animator _Animator;
        [HideInInspector] public Transform _Player;
        [SerializeField] private GameObject[] _Array_Prefab_BossLeg;
        private Queue<GameObject> _Pool_BossLeg = new Queue<GameObject>();
        [SerializeField] private int _InitialQuantity;

        private void Awake()
        {
            _Instance = this;
            _Player = GameObject.FindGameObjectWithTag("Player").transform;
            for (int _i = 0; _i < _InitialQuantity; _i++)
            {
                for (int _j = 0; _j < _Array_Prefab_BossLeg.Length; _j++)
                {
                    GameObject _go = Instantiate(_Array_Prefab_BossLeg[_j], Vector3.zero, Quaternion.identity, transform);
                    _Pool_BossLeg.Enqueue(_go);
                    _go.SetActive(false);
                }
            }
        }

        public void ReUse()
        {
            if (_Pool_BossLeg.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_BossLeg.Count); return; }
            BossLegAI._Recycle = false;
            GameObject _go = _Pool_BossLeg.Dequeue();
            _go.SetActive(true);
        }
        public void Recycle(GameObject _go)
        {
            _Pool_BossLeg.Enqueue(_go);
            _go.SetActive(false);
        }

        public void RecycleThis()
        {
            BossLegAI._Recycle = true;
            EnemyManager._Instance.RecycleBoss(gameObject);
        }
    }
}
