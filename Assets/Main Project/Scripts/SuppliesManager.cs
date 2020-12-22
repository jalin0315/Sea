using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class SuppliesManager : MonoBehaviour
    {
        public static SuppliesManager _Instance;
        [SerializeField] private Camera _Camera;
        public Vector2 _Origin()
        {
            Vector2 _result = _Camera.ScreenToWorldPoint(Vector2.zero);
            return _result;
        }
        public Vector2 _Vertex()
        {
            Vector2 _result = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
            return _result;
        }
        [SerializeField] private Transform _Parent;
        [SerializeField] private GameObject _Prefab_SuppliesRed;
        [SerializeField] private GameObject _Prefab_SuppliesYellow;
        [SerializeField] private GameObject _Prefab_SuppliesAd;
        [SerializeField] private List<GameObject> _Prefab_Props = new List<GameObject>();
        private Queue<GameObject> _Queue_Pool_SuppliesRed = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_SuppliesYellow = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_SuppliesAd = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_00 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_01 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_02 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_03 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_04 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_05 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_06 = new Queue<GameObject>();
        private Queue<GameObject> _Queue_Pool_Props_07 = new Queue<GameObject>();
        [SerializeField] private int _InitialSize_SuppliesRed;
        [SerializeField] private int _InitialSize_SuppliesYellow;
        [SerializeField] private int _InitialSize_SuppliesAd;
        [SerializeField] private int _InitialSize_Props;

        private void Awake()
        {
            _Instance = this;
            for (int _i = 0; _i < _InitialSize_SuppliesRed; _i++)
            {
                GameObject _go = Instantiate(_Prefab_SuppliesRed, _Parent);
                _Queue_Pool_SuppliesRed.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitialSize_SuppliesYellow; _i++)
            {
                GameObject _go = Instantiate(_Prefab_SuppliesYellow, _Parent);
                _Queue_Pool_SuppliesYellow.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitialSize_SuppliesAd; _i++)
            {
                GameObject _go = Instantiate(_Prefab_SuppliesAd, _Parent);
                _Queue_Pool_SuppliesAd.Enqueue(_go);
                _go.SetActive(false);
            }
            for (int _i = 0; _i < _InitialSize_Props; _i++)
            {
                {
                    GameObject _go = Instantiate(_Prefab_Props[0], _Parent);
                    _Queue_Pool_Props_00.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[1], _Parent);
                    _Queue_Pool_Props_01.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[2], _Parent);
                    _Queue_Pool_Props_02.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[3], _Parent);
                    _Queue_Pool_Props_03.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[4], _Parent);
                    _Queue_Pool_Props_04.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[5], _Parent);
                    _Queue_Pool_Props_05.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[6], _Parent);
                    _Queue_Pool_Props_06.Enqueue(_go);
                    _go.SetActive(false);
                }
                {
                    GameObject _go = Instantiate(_Prefab_Props[7], _Parent);
                    _Queue_Pool_Props_07.Enqueue(_go);
                    _go.SetActive(false);
                }
            }
        }

        private void ReUse(Queue<GameObject> _queue, Vector2 _position, int _direction)
        {
            SuppliesControl._RecoveryAll = false;
            if (_queue.Count <= 0) return;
            GameObject _go = _queue.Dequeue();
            _go.SetActive(true);
            _go.transform.position = new Vector3(_position.x, _position.y, 0.8f);
            SuppliesControl _s_c = _go.GetComponent<SuppliesControl>();
            _s_c._Queue = _queue;
            _go.transform.localScale = _s_c._Scale;
            if (_queue == _Queue_Pool_SuppliesRed)
            {
                QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesRed, _go.transform);
                return;
            }
            else if (_queue == _Queue_Pool_SuppliesYellow)
            {
                QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesYellow, _go.transform);
                return;
            }
            else if (_queue == _Queue_Pool_SuppliesAd)
            {
                _s_c._Direction = _direction;
                _s_c._Time = 15.0f;
                _s_c._Timer = _s_c._Time;
                if (_direction == 0) _go.transform.localScale = _s_c._Scale;
                else if (_direction == 1) _go.transform.localScale = new Vector3(-_s_c._Scale.x, _s_c._Scale.y, _s_c._Scale.z);
                QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesAd, _go.transform);
                return;
            }
            else QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesProps, _go.transform);
        }

        public void Recovery(Queue<GameObject> _queue, GameObject _go)
        {
            if (_queue == _Queue_Pool_SuppliesRed) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesRed, _go.transform);
            else if (_queue == _Queue_Pool_SuppliesYellow) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesYellow, _go.transform);
            else if (_queue == _Queue_Pool_SuppliesAd) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesAd, _go.transform);
            else QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesProps, _go.transform);
            _queue.Enqueue(_go);
            _go.SetActive(false);
        }

        private IEnumerator _CallSupplies_Singleton;
        private IEnumerator _CallSupplies_Logic(float _time)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(_time);
                /*
                int _i = Random.Range(1, 101);
                if (_i > 0 && _i <= 50)
                {
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    Vector2 _result = new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + 5.0f);
                    ReUse(_Queue_Pool_SuppliesRed, _result, -1);
                    yield return new WaitForSeconds(_time);
                    continue;
                }
                if (_i > 50 && _i <= 100)
                {
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    Vector2 _result = new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + 5.0f);
                    ReUse(_Queue_Pool_SuppliesYellow, _result, -1);
                    yield return new WaitForSeconds(_time);
                    continue;
                }
                */
                int _i = Random.Range(0, 8);
                //int _i = 1;
                Vector2 _result = new Vector2(Random.Range(_Origin().x, _Vertex().x), _Vertex().y + 2.5f);
                switch (_i)
                {
                    case 0:
                        ReUse(_Queue_Pool_Props_00, _result, -1);
                        break;
                    case 1:
                        ReUse(_Queue_Pool_Props_01, _result, -1);
                        break;
                    case 2:
                        ReUse(_Queue_Pool_Props_02, _result, -1);
                        break;
                    case 3:
                        ReUse(_Queue_Pool_Props_03, _result, -1);
                        break;
                    case 4:
                        ReUse(_Queue_Pool_Props_04, _result, -1);
                        break;
                    case 5:
                        ReUse(_Queue_Pool_Props_05, _result, -1);
                        break;
                    case 6:
                        ReUse(_Queue_Pool_Props_06, _result, -1);
                        break;
                    case 7:
                        ReUse(_Queue_Pool_Props_07, _result, -1);
                        break;
                    default:
                        Debug.LogErrorFormat("CallSupplies Error! Array: {0}", _i);
                        break;
                }
                continue;
            }
        }
        public void IEnumeratorCallSupplies(bool _enable)
        {
            if (_enable)
            {
                if (_CallSupplies_Singleton != null) StopCoroutine(_CallSupplies_Singleton);
                _CallSupplies_Singleton = _CallSupplies_Logic(30.0f); // 30.0f
                StartCoroutine(_CallSupplies_Singleton);
            }
            else
            {
                if (_CallSupplies_Singleton != null) StopCoroutine(_CallSupplies_Singleton);
            }
        }

        private IEnumerator _CallSuppliesAd_Singleton;
        private IEnumerator _CallSuppliesAd_Logic(float _time)
        {
            while (true)
            {
                yield return new WaitForSeconds(_time);
                if (Player._Instance._Slider_Health.value > 20) continue;
                if (Player._Instance._Slider_Health.value <= 20)
                {
                    Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                    Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                    ReUse(_Queue_Pool_SuppliesAd, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + 5.0f), 0);
                    continue;
                }
            }
        }
        public void IEnumeratorCallSuppliesAd(bool _enable)
        {
            if (_enable)
            {
                if (_CallSuppliesAd_Singleton != null) StopCoroutine(_CallSuppliesAd_Singleton);
                _CallSuppliesAd_Singleton = _CallSuppliesAd_Logic(30.0f);
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
