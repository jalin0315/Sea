using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesManager : MonoBehaviour
{
    public static SuppliesManager _Instance;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Transform _Parent;
    [SerializeField] private GameObject _Prefab_SuppliesRed;
    [SerializeField] private GameObject _Prefab_SuppliesYellow;
    private Queue<GameObject> _Queue_Pool_SuppliesRed = new Queue<GameObject>();
    private Queue<GameObject> _Queue_Pool_SuppliesYellow = new Queue<GameObject>();
    [SerializeField] private int _InitialSize_SuppliesRed;
    [SerializeField] private int _InitialSize_SuppliesYellow;

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
    }

    private void ReUse(Queue<GameObject> _queue)
    {
        if (_queue.Count <= 0) return;
        Vector2 _origin = _Camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 _vertex = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelWidth, _Camera.pixelHeight));
        Vector2 _result = new Vector2(Random.Range(_origin.x, _vertex.x), _vertex.y);
        GameObject _go = _queue.Dequeue();
        _go.SetActive(true);
        _go.transform.position = _result;
        SuppliesControl _s_c = _go.GetComponent<SuppliesControl>();
        _s_c._Queue = _queue;
    }

    public void Recovery(Queue<GameObject> _queue, GameObject _go)
    {
        _queue.Enqueue(_go);
        _go.SetActive(false);
    }

    public IEnumerator CallSupplies(float _time)
    {
        while (true)
        {
            if (SuppliesControl._RecoveryAll) break;
            int _i = Random.Range(1, 101);
            if (_i > 0 && _i <= 50)
            {
                ReUse(_Queue_Pool_SuppliesRed);
                yield return new WaitForSeconds(_time);
                continue;
            }
            if (_i > 50 && _i <= 100)
            {
                ReUse(_Queue_Pool_SuppliesYellow);
                yield return new WaitForSeconds(_time);
                continue;
            }
        }
    }
}
