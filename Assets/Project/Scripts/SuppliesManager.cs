using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class SuppliesManager : MonoBehaviour
    {
        public static SuppliesManager _Instance;
        [SerializeField] private Transform _Parent;
        [SerializeField] private List<GameObject> _Prefab_Props;
        [SerializeField] private GameObject _Prefab_SuppliesAd;
        private Queue<GameObject> _Pool_Props_00 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_01 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_02 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_03 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_04 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_05 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_06 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_Props_07 = new Queue<GameObject>();
        private Queue<GameObject> _Pool_SuppliesAd = new Queue<GameObject>();
        [SerializeField] private int _InitialSize_SuppliesAd;
        [SerializeField] private int _InitialSize_Props;
        private Vector2 _variable_vector2;

        private void Awake()
        {
            _Instance = this;
            for (int _i = 0; _i < _InitialSize_Props; _i++)
            {
                {
                    GameObject _go = Instantiate(_Prefab_Props[0], _Parent);
                    _Pool_Props_00.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[1], _Parent);
                    _Pool_Props_01.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[2], _Parent);
                    _Pool_Props_02.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[3], _Parent);
                    _Pool_Props_03.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[4], _Parent);
                    _Pool_Props_04.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[5], _Parent);
                    _Pool_Props_05.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[6], _Parent);
                    _Pool_Props_06.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[7], _Parent);
                    _Pool_Props_07.Enqueue(_go);
                    _go.SetActive(false);
                }
            }
            for (int _i = 0; _i < _InitialSize_SuppliesAd; _i++)
            {
                GameObject _go = Instantiate(_Prefab_SuppliesAd, _Parent);
                _Pool_SuppliesAd.Enqueue(_go);
                _go.SetActive(false);
            }
        }

        private void ReUseProp(Queue<GameObject> _queue_gameobject)
        {
            if (_queue_gameobject.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _queue_gameobject.Count); return; }
            SuppliesControl._Recycle = false;
            GameObject _go = _queue_gameobject.Dequeue();
            _variable_vector2.x = Random.Range(CameraControl._Origin.x + 1.0f, CameraControl._Vertex.x + -1.0f);
            _variable_vector2.y = CameraControl._Vertex.y + 1.0f;
            _go.transform.position = _variable_vector2;
            _go.SetActive(true);
            SuppliesControl _s_c = _go.GetComponent<SuppliesControl>();
            _s_c._Queue_GameObject = _queue_gameobject;
            QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Pool_SuppliesProps, _go.transform);
        }
        private void ReUseAd()
        {
            if (_Pool_SuppliesAd.Count <= 0) { Logger.LogWarningFormat("Queue index out of range. Count: {0}.", _Pool_SuppliesAd.Count); return; }
            SuppliesControl._Recycle = false;
            GameObject _go = _Pool_SuppliesAd.Dequeue();
            _variable_vector2.x = Random.Range(CameraControl._Origin.x + 1.0f, CameraControl._Vertex.x + -1.0f);
            _variable_vector2.y = CameraControl._Vertex.y + 1.0f;
            _go.transform.position = _variable_vector2;
            _go.SetActive(true);
            SuppliesControl _s_c = _go.GetComponent<SuppliesControl>();
            _s_c._AdTime = 20.0f;
            QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Pool_SuppliesAd, _go.transform);
        }
        public void RecycleProp(Queue<GameObject> _queue, GameObject _go)
        {
            _queue.Enqueue(_go);
            _go.SetActive(false);
            QuestArrowPointerSystem._Instance.Recycle(QuestArrowPointerSystem._Instance._Pool_SuppliesProps, _go.transform);
        }
        public void RecycleAd(GameObject _go)
        {
            _Pool_SuppliesAd.Enqueue(_go);
            _go.SetActive(false);
            QuestArrowPointerSystem._Instance.Recycle(QuestArrowPointerSystem._Instance._Pool_SuppliesAd, _go.transform);
        }

        private IEnumerator _CallSupplies_Singleton;
        private IEnumerator _CallSupplies_Logic()
        {
            int _r;
            float _interval = 25.0f;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                yield return OPT._WaitForSeconds(_interval);
                _r = Random.Range(0, 8);
                switch (_r)
                {
                    case 0:
                        ReUseProp(_Pool_Props_00);
                        break;
                    case 1:
                        ReUseProp(_Pool_Props_01);
                        break;
                    case 2:
                        ReUseProp(_Pool_Props_02);
                        break;
                    case 3:
                        ReUseProp(_Pool_Props_03);
                        break;
                    case 4:
                        ReUseProp(_Pool_Props_04);
                        break;
                    case 5:
                        ReUseProp(_Pool_Props_05);
                        break;
                    case 6:
                        ReUseProp(_Pool_Props_06);
                        break;
                    case 7:
                        ReUseProp(_Pool_Props_07);
                        break;
                }
            }
        }
        public void IEnumeratorCallSupplies(bool _enable)
        {
            if (_enable)
            {
                if (_CallSupplies_Singleton != null) StopCoroutine(_CallSupplies_Singleton);
                _CallSupplies_Singleton = _CallSupplies_Logic();
                StartCoroutine(_CallSupplies_Singleton);
            }
            else
            {
                if (_CallSupplies_Singleton != null) StopCoroutine(_CallSupplies_Singleton);
            }
        }

        private IEnumerator _CallSuppliesAd_Singleton;
        private IEnumerator _CallSuppliesAd_Logic()
        {
            float _interval = 60.0f;
            while (true)
            {
                yield return OPT._WaitForEndOfFrame;
                yield return OPT._WaitForSeconds(_interval);
                ReUseAd();
            }
        }
        public void IEnumeratorCallSuppliesAd(bool _enable)
        {
            if (_enable)
            {
                if (_CallSuppliesAd_Singleton != null) StopCoroutine(_CallSuppliesAd_Singleton);
                _CallSuppliesAd_Singleton = _CallSuppliesAd_Logic();
                StartCoroutine(_CallSuppliesAd_Singleton);
            }
            else
            {
                if (_CallSuppliesAd_Singleton != null) StopCoroutine(_CallSuppliesAd_Singleton);
            }
        }

        public void IEnumeratorStopAllCoroutines() => StopAllCoroutines();
    }
}
