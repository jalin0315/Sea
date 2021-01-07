using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class BaitManager : MonoBehaviour
    {
        public static BaitManager _Instance;
        [SerializeField] private Transform _Parent;
        [SerializeField] private Transform _Player;
        [SerializeField] private GameObject _Prefab_Bait;
        private Queue<GameObject> _Queue_Pool_Bait = new Queue<GameObject>();
        [SerializeField] private int _InitialSize_Bait;
        [SerializeField] private float _ThrowingSpeed;

        private void Awake()
        {
            _Instance = this;
            for (int _i = 0; _i < _InitialSize_Bait; _i++)
            {
                GameObject _go = Instantiate(_Prefab_Bait, _Parent);
                _Queue_Pool_Bait.Enqueue(_go);
                _go.SetActive(false);
            }
        }

        private void ReUse(Queue<GameObject> _queue)
        {
            if (_queue.Count <= 0) return;
            GameObject _go = _queue.Dequeue();
            _go.SetActive(true);
            _go.transform.position = _Player.position;
            EnemyAI._Bait = _go.transform;
            BaitControl _b_c = _go.GetComponent<BaitControl>();
            _b_c._Rigidbody2D.AddForce(Vector2.up * _ThrowingSpeed, ForceMode2D.Impulse);
            _b_c._Queue = _queue;
            //StartCoroutine(Delay(Player._Instance._List_SkillTime[Player._Instance._SkillOptions]));
            IEnumerator Delay(float _time)
            {
                yield return new WaitForSeconds(_time);
                Recovery(_queue, _go);
            }
        }

        public void Bait()
        {
            BaitControl._RecoveryAll = false;
            ReUse(_Queue_Pool_Bait);
        }

        public void Recovery(Queue<GameObject> _queue, GameObject _go)
        {
            EnemyAI._Bait = null;
            _queue.Enqueue(_go);
            _go.SetActive(false);
        }
    }
}
