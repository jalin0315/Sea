using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player _Instance;
    [SerializeField] private Animator _Animator;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private Slider _Slider_MaxHealth;
    [SerializeField] private Slider _Slider_Health;
    [SerializeField] private Slider _Slider_Power;
    [SerializeField] private Image _Image_Health;
    [SerializeField] private Color _HighHealthColor;
    [SerializeField] private Color _LowHealthColor;
    private bool _Invincible;
    [HideInInspector] public int _SkillOptions;
    [SerializeField] private List<float> _List_SkillPay = new List<float>();
    public List<float> _List_SkillTime = new List<float>();
    private bool _EnableSkill;

    private void Awake() => _Instance = this;

    private void Start()
    {
        _Slider_MaxHealth.value = 100.0f;
        _Slider_Health.value = _Slider_MaxHealth.value;
        _Slider_Power.value = _Slider_Power.maxValue;
    }

    private void Update()
    {
        if (!GameManager._Instance._InGame) return;
        SkillUpdate();
    }

    public void InvincibleEnable() => _Invincible = true;
    public void InvincibleDisable() => _Invincible = false;

    private void HealthBarColorChange()
    {
        Color _color_lerp = Color.Lerp(_LowHealthColor, _HighHealthColor, _Slider_Health.value / _Slider_MaxHealth.value);
        _Image_Health.color = _color_lerp;
    }

    public void SkillTrigger()
    {
        _EnableSkill = true;
        _Slider_Power.value -= _List_SkillPay[_SkillOptions];
        switch (_SkillOptions)
        {
            case 0:
                break;
            case 1:
                BaitManager._Instance.Bait();
                break;
            case 2:
                break;
            default:
                break;
        }
        StartCoroutine(Delay(_List_SkillTime[_SkillOptions]));
        IEnumerator Delay(float _time)
        {
            yield return new WaitForSeconds(_time);
            _EnableSkill = false;
        }
    }
    public void SkillUpdate()
    {
        if (_EnableSkill) MenuSystem._Instance._Button_InGameSkill.interactable = false;
        else
        {
            if (_Slider_Power.value < _List_SkillPay[_SkillOptions]) MenuSystem._Instance._Button_InGameSkill.interactable = false;
            else MenuSystem._Instance._Button_InGameSkill.interactable = true;
        }
        switch (_SkillOptions)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public void Supplies(string _tag)
    {
        if (_tag == "SuppliesRed")
        {
            if (_Slider_Health.value < _Slider_MaxHealth.value)
            {
                _Slider_Health.value = _Slider_MaxHealth.value;
                //if (_Slider_Health.value >= _Slider_MaxHealth.value) _Slider_Health.value = _Slider_MaxHealth.value;
                HealthBarColorChange();
            }
            return;
        }
        if (_tag == "SuppliesYellow") _Slider_Power.value = _Slider_Power.maxValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager._Instance._InGame) return;
        if (_Invincible) return;
        if (collision.tag == "Enemy")
        {
            _Slider_Health.value -= 12.5f;
            if (_Slider_Health.value <= 0.0f) Debug.Log("You are dead.");
            HealthBarColorChange();
            _Animator.SetTrigger("Injured");
        }
    }
}