using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesManager : MonoBehaviour
{
    public static SuppliesManager _Instance;
    public Camera _Camera;
    [SerializeField] private Transform _Parent;
    [SerializeField] private GameObject _Prefab_SuppliesRed;
    [SerializeField] private GameObject _Prefab_SuppliesYellow;
    [SerializeField] private GameObject _Prefab_SuppliesAd;
    private Queue<GameObject> _Queue_Pool_SuppliesRed = new Queue<GameObject>();
    private Queue<GameObject> _Queue_Pool_SuppliesYellow = new Queue<GameObject>();
    private Queue<GameObject> _Queue_Pool_SuppliesAd = new Queue<GameObject>();
    [SerializeField] private int _InitialSize_SuppliesRed;
    [SerializeField] private int _InitialSize_SuppliesYellow;
    [SerializeField] private int _InitialSize_SuppliesAd;

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
    }

    private void Start()
    {
        _CallSupplies_Singleton = CallSupplies(60.0f); // 60.0f
        _CallSuppliesAd_Singleton = CallSuppliesAd(30.0f); // 30.0f
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
        if (_queue == _Queue_Pool_SuppliesYellow)
        {
            QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesYellow, _go.transform);
            return;
        }
        if (_queue == _Queue_Pool_SuppliesAd)
        {
            _s_c._Direction = _direction;
            _s_c._Time = 30.0f;
            _s_c._Timer = _s_c._Time;
            if (_direction == 0) _go.transform.localScale = _s_c._Scale;
            else if (_direction == 1) _go.transform.localScale = new Vector3(-_s_c._Scale.x, _s_c._Scale.y, _s_c._Scale.z);
            QuestArrowPointerSystem._Instance.ReUse(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesAd, _go.transform);
            return;
        }
    }

    public void Recovery(Queue<GameObject> _queue, GameObject _go)
    {
        if (_queue == _Queue_Pool_SuppliesRed) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesRed, _go.transform);
        else if (_queue == _Queue_Pool_SuppliesYellow) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesYellow, _go.transform);
        else if (_queue == _Queue_Pool_SuppliesAd) QuestArrowPointerSystem._Instance.Recovery(QuestArrowPointerSystem._Instance._Queue_Pool_SuppliesAd, _go.transform);
        _queue.Enqueue(_go);
        _go.SetActive(false);
    }

    public IEnumerator _CallSupplies_Singleton;
    private IEnumerator CallSupplies(float _time)
    {
        while (true)
        {
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
        }
    }
    public IEnumerator _CallSuppliesAd_Singleton;
    private IEnumerator CallSuppliesAd(float _time)
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            if (Player._Instance._Slider_Health.value > 40) continue;
            if (Player._Instance._Slider_Health.value <= 40)
            {
                Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
                Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
                ReUse(_Queue_Pool_SuppliesAd, new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y + 5.0f), 0);
                continue;
            }
        }
    }
}
