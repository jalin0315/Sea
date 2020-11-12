using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Diagnostics;

public class QuestArrowPointerSystem : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    [SerializeField] private List<Transform> _TargetPosition;
    [SerializeField] private List<RectTransform> _PointerRectTransform;

    private void FixedUpdate()
    {
        for(int _i = 0; _i < _PointerRectTransform.Count; _i++)
        {
            if (_PointerRectTransform[_i] == null) continue;
            Vector3 _to_position = _TargetPosition[_i].position;
            Vector3 _from_position = _Camera.transform.position;
            _from_position.z = 0;
            Vector3 _dir = (_to_position - _from_position).normalized;
            float _angle = (Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg) % 360;
            _PointerRectTransform[_i].localEulerAngles = new Vector3(_PointerRectTransform[_i].localEulerAngles.x, _PointerRectTransform[_i].localEulerAngles.y, _angle);

            float _border_size = 100.0f;
            Vector3 _target_position_screen_point = _Camera.WorldToScreenPoint(_TargetPosition[_i].position);
            bool _is_off_screen = _target_position_screen_point.x <= _border_size || _target_position_screen_point.x >= Screen.width - _border_size || _target_position_screen_point.y <= _border_size || _target_position_screen_point.y >= Screen.height - _border_size;

            if (_is_off_screen)
            {
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
                Vector3 _pointer_world_position = _Camera.ScreenToWorldPoint(_target_position_screen_point);
                _PointerRectTransform[_i].position = _pointer_world_position;
                _PointerRectTransform[_i].localPosition = new Vector3(_PointerRectTransform[_i].localPosition.x, _PointerRectTransform[_i].localPosition.y, 0f);

                _PointerRectTransform[_i].localEulerAngles = Vector3.zero;
            }
        }
    }
}
