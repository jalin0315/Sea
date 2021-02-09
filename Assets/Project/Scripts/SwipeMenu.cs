using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class SwipeMenu : MonoBehaviour
    {
        [SerializeField] private Transform _Transform_Parent;
        [SerializeField] private Scrollbar _Scrollbar;
        [SerializeField] private List<Transform> _List_Child;
        [SerializeField] private List<float> _List_Pos;
        private float _ScrollPos;
        private float _Distance;

        private void Start()
        {
            _Distance = 1f / (_Transform_Parent.childCount - 1f);
            for (int _i = 0; _i < _Transform_Parent.childCount; _i++)
            {
                _List_Child.Add(_Transform_Parent.GetChild(_i));
                _List_Pos.Add(_Distance * _i);
            }
        }

        private void Update()
        {
            if (GameManager._InGame) return;
            if (MenuSystem._Instance._Status != MenuSystem.Status.HowToPlayMenu) return;
            if (Input.GetMouseButton(0)) _ScrollPos = _Scrollbar.value;
            else
            {
                for (int _i = 0; _i < _List_Pos.Count; _i++)
                {
                    if (_ScrollPos < _List_Pos[_i] + (_Distance / 2) && _ScrollPos > _List_Pos[_i] - (_Distance / 2))
                        _Scrollbar.value = Mathf.Lerp(_Scrollbar.value, _List_Pos[_i], 0.1f);
                }
            }
            for (int _i = 0; _i < _List_Pos.Count; _i++)
            {
                if (_ScrollPos < _List_Pos[_i] + (_Distance / 2) && _ScrollPos > _List_Pos[_i] - (_Distance / 2))
                {
                    _List_Child[_i].localScale = Vector2.Lerp(_List_Child[_i].localScale, new Vector2(1.0f, 1.0f), TimeSystem._DeltaTime() * 10.0f);
                    for (int _j = 0; _j < _List_Pos.Count; _j++)
                    {
                        if (_j != _i)
                        {
                            _List_Child[_j].localScale = Vector2.Lerp(_List_Child[_j].localScale, new Vector2(0.5f, 0.5f), TimeSystem._DeltaTime() * 10.0f);
                        }
                    }
                }
            }
        }
    }
}
