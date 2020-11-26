using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineSwipeMenu : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _AnimationPlayer;
    [SerializeField] private SpriteRenderer _AnimationPlayer_WaterReflect;
    [SerializeField] private SpriteRenderer _Player;
    [SerializeField] private Transform _Transform_Parent;
    [SerializeField] private Scrollbar _Scrollbar;
    [SerializeField] private List<Transform> _List_Child = new List<Transform>();
    [SerializeField] private List<float> _List_Pos = new List<float>();
    [SerializeField] private List<Image> _List_Image = new List<Image>();
    [SerializeField] private List<Toggle> _List_Toggle = new List<Toggle>();
    [SerializeField] private List<Button> _List_Button = new List<Button>();
    private float _ScrollPos;
    private float _Distance;

    private void Start()
    {
        _Distance = 1f / (_Transform_Parent.childCount - 1f);
        for (int _i = 0; _i < _Transform_Parent.childCount; _i++)
        {
            _List_Child.Add(_Transform_Parent.GetChild(_i));
            _List_Pos.Add(_Distance * _i);
            _List_Image.Add(_Transform_Parent.GetChild(_i).GetComponent<Image>());
            _List_Toggle.Add(_Transform_Parent.GetChild(_i).GetComponent<Toggle>());
            _List_Button.Add(_Transform_Parent.GetChild(_i).GetChild(1).GetComponent<Button>());
            // Closure Problem
            int _j = _i;
            _List_Button[_i].onClick.AddListener(() => ChangeSubmarine(_j));
        }
    }

    private void Update()
    {
        if (MenuSystem._Instance._Status != MenuSystem.Status.Submarine)
            return;
        if (Input.GetMouseButton(0))
            _ScrollPos = _Scrollbar.value;
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
                if (_List_Toggle[_i].isOn)
                {
                    _List_Image[_i].color = Color.white;
                    _List_Button[_i].interactable = true;
                }
                else
                {
                    _List_Image[_i].color = Color.black;
                    _List_Button[_i].interactable = false;
                }
                _List_Child[_i].localScale = Vector2.Lerp(_List_Child[_i].localScale, new Vector2(1.0f, 1.0f), Time.deltaTime * 10.0f);
                for (int _j = 0; _j < _List_Pos.Count; _j++)
                {
                    if (_j != _i)
                    {
                        _List_Button[_j].interactable = false;
                        if (_List_Toggle[_j].isOn)
                            _List_Image[_j].color = Color.gray;
                        else
                            _List_Image[_j].color = Color.black;
                        _List_Child[_j].localScale = Vector2.Lerp(_List_Child[_j].localScale, new Vector2(0.5f, 0.5f), Time.deltaTime * 10.0f);
                    }
                }
            }
        }
    }

    private void ChangeSubmarine(int _index)
    {
        _AnimationPlayer.sprite = _List_Image[_index].sprite;
        _AnimationPlayer_WaterReflect.sprite = _List_Image[_index].sprite;
        _Player.sprite = _List_Image[_index].sprite;
        Player._Instance._Sprite_Player = _List_Image[_index].sprite;
        MenuSystem._Instance.StateChange(MenuSystem.Status.MainMenu);
    }
}
