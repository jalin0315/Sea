using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrowPointerSystem : MonoBehaviour
{
    public static QuestArrowPointerSystem _Instance;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Transform _Parent;
    [SerializeField] private GameObject _Prefab_SuppliesRed;
    [SerializeField] private GameObject _Prefab_SuppliesYellow;
    [SerializeField] private GameObject _Prefab_SuppliesAd;
    public Queue<GameObject> _Queue_Pool_SuppliesRed = new Queue<GameObject>();
    public Queue<GameObject> _Queue_Pool_SuppliesYellow = new Queue<GameObject>();
    public Queue<GameObject> _Queue_Pool_SuppliesAd = new Queue<GameObject>();
    [SerializeField] private int _Initialize;
    [SerializeField] private List<Transform> _TargetPosition = new List<Transform>();
    [SerializeField] private List<RectTransform> _PointerRectTransform = new List<RectTransform>();

    private void Awake()
    {
        _Instance = this;
        for (int _i = 0; _i < _Initialize; _i++)
        {
            GameObject _go = Instantiate(_Prefab_SuppliesRed, Vector2.zero, Quaternion.identity, _Parent);
            _Queue_Pool_SuppliesRed.Enqueue(_go);
            _go.SetActive(false);
        }
        for (int _i = 0; _i < _Initialize; _i++)
        {
            GameObject _go = Instantiate(_Prefab_SuppliesYellow, Vector2.zero, Quaternion.identity, _Parent);
            _Queue_Pool_SuppliesYellow.Enqueue(_go);
            _go.SetActive(false);
        }
        for (int _i = 0; _i < _Initialize; _i++)
        {
            GameObject _go = Instantiate(_Prefab_SuppliesAd, Vector2.zero, Quaternion.identity, _Parent);
            _Queue_Pool_SuppliesAd.Enqueue(_go);
            _go.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        for (int _i = 0; _i < _PointerRectTransform.Count; _i++)
        {
            if (_PointerRectTransform[_i] == null) continue;
            Vector3 _to_position = _TargetPosition[_i].position;
            Vector3 _from_position = _Camera.transform.position;
            _from_position.z = 0;
            Vector3 _dir = (_to_position - _from_position).normalized;
            float _angle = (Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg) % 360;
            _PointerRectTransform[_i].localEulerAngles = new Vector3(_PointerRectTransform[_i].localEulerAngles.x, _PointerRectTransform[_i].localEulerAngles.y, _angle);

            float _border_size = 60.0f;
            Vector3 _target_position_screen_point = _Camera.WorldToScreenPoint(_TargetPosition[_i].position);
            bool _is_off_screen = _target_position_screen_point.x <= _border_size || _target_position_screen_point.x >= Screen.width - _border_size || _target_position_screen_point.y <= _border_size || _target_position_screen_point.y >= Screen.height - _border_size;

            if (_is_off_screen)
            {
                _PointerRectTransform[_i].gameObject.SetActive(true);

                Vector3 _capped_target_screen_position = _target_position_screen_point;
                if (_capped_target_screen_position.x <= _border_size) _capped_target_screen_position.x = _border_size;
                if (_capped_target_screen_position.x >= Screen.width - _border_size) _capped_target_screen_position.x = Screen.width - _border_size;
                if (_capped_target_screen_position.y <= _border_size) _capped_target_screen_position.y = _border_size;
                if (_capped_target_screen_position.y >= Screen.height - _border_size) _capped_target_screen_position.y = Screen.height - _border_size;

                Vector3 _pointer_world_position = _Camera.ScreenToWorldPoint(_capped_target_screen_position);
                _PointerRectTransform[_i].position = _pointer_world_position;
                _PointerRectTransform[_i].localPosition = new Vector3(_PointerRectTransform[_i].localPosition.x, _PointerRectTransform[_i].localPosition.y, 0.0f);
            }
            else
            {
                _PointerRectTransform[_i].gameObject.SetActive(false);

                Vector3 _pointer_world_position = _Camera.ScreenToWorldPoint(_target_position_screen_point);
                _PointerRectTransform[_i].position = _pointer_world_position;
                _PointerRectTransform[_i].localPosition = new Vector3(_PointerRectTransform[_i].localPosition.x, _PointerRectTransform[_i].localPosition.y, 0f);

                _PointerRectTransform[_i].localEulerAngles = Vector3.zero;
            }
        }
    }

    public void ReUse(Queue<GameObject> _queue, Transform _target)
    {
        if (_queue.Count <= 0) return;
        GameObject _go = _queue.Dequeue();
        _go.SetActive(true);
        _PointerRectTransform.Add(_go.GetComponent<RectTransform>());
        _TargetPosition.Add(_target);
    }
    public void Recovery(Queue<GameObject> _queue, Transform _target)
    {
        int _index = _TargetPosition.FindIndex(x => x == _target);
        if (_index == -1) return;
        _TargetPosition.Remove(_target);
        _queue.Enqueue(_PointerRectTransform[_index].gameObject);
        _PointerRectTransform[_index].gameObject.SetActive(false);
        _PointerRectTransform.RemoveAt(_index);
    }
}
