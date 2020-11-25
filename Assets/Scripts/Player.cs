using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player _Instance;
    public Transform _Transform_Player;
    public Animator _Animator;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public Slider _Slider_MaxHealth;
    public Slider _Slider_Health;
    [SerializeField] private Slider _Slider_Power;
    [SerializeField] private Image _Image_Health;
    [SerializeField] private Color _HighHealthColor;
    [SerializeField] private Color _LowHealthColor;
    private bool _Invincible;
    [SerializeField] private ParticleSystem _ParticleSystem_Invincible;
    [HideInInspector] public int _SkillOptions;
    [SerializeField] private List<float> _List_SkillPay = new List<float>();
    public List<float> _List_SkillTime = new List<float>();
    public bool _EnableSkill;
    [SerializeField] private ParticleSystem _ParticleSystem_Death;
    [SerializeField] private ParticleSystem _ParticleSystem_Light;

    private void Awake() => _Instance = this;

    public void InitializeStart()
    {
        _Slider_MaxHealth.value = 100.0f - (GameManager._Instance._Meter * 0.005f);
        _Slider_Health.value = _Slider_MaxHealth.value;
        _Slider_Power.value = _Slider_Power.maxValue;
        HealthBarColorChange();
    }
    private void Start() => InitializeStart();

    private void Update()
    {
        if (!GameManager._Instance._InGame) return;
        SkillUpdate();
    }

    public void InvincibleEnable() => _Invincible = true;
    public void InvincibleDisable() => _Invincible = false;
    public void InvincibleEffectEnable() => _ParticleSystem_Invincible.Play();
    public void InvincibleEffectDisable() => _ParticleSystem_Invincible.Stop();

    public void VerifyHealth(float _meter) => _Slider_MaxHealth.value = 100.0f - (_meter * 0.005f);
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
                _ParticleSystem_Light.Play();
                break;
            case 1:
                BaitManager._Instance.Bait();
                break;
            case 2:
                _Animator.SetTrigger("Invincible");
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

    public void Supplies(string _tag, int _number)
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
        if (_tag == "SuppliesYellow")
        {
            _Slider_Power.value = _Slider_Power.maxValue;
            return;
        }
        if (_tag == "SuppliesAd")
        {
            GoogleAdMob._Instance.MaxHealthPower(true);
            return;
        }
        if (_tag == "SuppliesProps")
        {
            switch (_number)
            {
                case 0:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 1:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 2:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 3:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 4:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 5:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 6:
                    _Animator.SetTrigger("Invincible");
                    break;
                case 7:
                    _Animator.SetTrigger("Invincible");
                    break;
                default:
                    Debug.LogErrorFormat("SuppliesNumber Error! Number: {0}", _number);
                    break;
            }
            return;
        }
    }
    public void MaxHealthPower()
    {
        _Slider_Health.value = _Slider_MaxHealth.value;
        _Slider_Power.value = _Slider_Power.maxValue;
        HealthBarColorChange();
        _Animator.SetTrigger("Invincible");
    }

    public void DeathEnable()
    {
        GameManager._Instance._InGame = false;
        MovementSystem._Instance._FloatingJoystick.Initialize();
        MenuSystem._Instance.StateChange(MenuSystem.Status.Animation);
    }
    public void DeathDisable()
    {
        Time.timeScale = 1.0f;
        GameManager._Instance._InGame = true;
        MovementSystem._Instance._FloatingJoystick.Initialize();
        MenuSystem._Instance.StateChange(MenuSystem.Status.InGame);
        _Slider_Health.value = _Slider_MaxHealth.value;
        _Slider_Power.value = _Slider_Power.maxValue;
        HealthBarColorChange();
        _Animator.SetBool("Death", false);
    }
    public void DeathMenu()
    {
        Time.timeScale = 0.0f;
        MenuSystem._Instance.StateChange(MenuSystem.Status.DeathMenu);
    }
    public void DeathEffectEnable() => _ParticleSystem_Death.Play();
    public void DeathEffectDisable() => _ParticleSystem_Death.Stop();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager._Instance._InGame) return;
        if (_Invincible) return;
        if (collision.tag == "Enemy")
        {
            _Slider_Health.value -= 12.5f;
            if (_Slider_Health.value <= 0.0f) _Animator.SetBool("Death", true);
            else _Animator.SetTrigger("Injured");
            HealthBarColorChange();
            if (GameManager._Instance._Enable_Vibrate) Handheld.Vibrate();
        }
    }
}