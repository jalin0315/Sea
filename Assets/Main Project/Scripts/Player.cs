using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class Player : MonoBehaviour
    {
        public static Player _Instance;
        public Transform _Transform;
        public Animator _Animator;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        public Sprite _Sprite_Player;
        [SerializeField] private List<Sprite> _List_Sprite_Fishes = new List<Sprite>();
        public Slider _Slider_MaxHealth;
        public Slider _Slider_Health;
        [SerializeField] private Image _Image_Health;
        [SerializeField] private Color _HighHealthColor;
        [SerializeField] private Color _LowHealthColor;
        private bool _Invincible;
        private bool _Invincible_Guise;
        private bool _Attack;
        [SerializeField] private ParticleSystem _ParticleSystem_Invincible;
        [HideInInspector] public int _SkillOptions;
        [SerializeField] private List<float> _List_SkillPay = new List<float>();
        public List<float> _List_SkillTime = new List<float>();
        public bool _EnableSkill;
        [SerializeField] private GameObject _Object_PropTime;
        [SerializeField] private Image _Image_PropTimeBackground;
        [SerializeField] private Image _Image_PropTime;
        [SerializeField] private float _PropTimer;
        [SerializeField] private ParticleSystem _ParticleSystem_Death;
        [SerializeField] private ParticleSystem _ParticleSystem_Light;
        [SerializeField] private ParticleSystem _ParticleSystem_Yellow;
        [SerializeField] private ParticleSystem _ParticleSystem_Attack;

        private void Awake()
        {
            _Instance = this;
        }

        public void Initialization()
        {
            if (_Sprite_Player == null) _Sprite_Player = _SpriteRenderer.sprite;
            _Slider_MaxHealth.value = 100.0f - (GameManager._Meter * 0.005f);
            //_Slider_MaxHealth.value = 10.0f;
            _Slider_Health.maxValue = _Slider_MaxHealth.value;
            _Slider_Health.value = _Slider_Health.maxValue;
            HealthBarColorChange();
            _Animator.SetBool("Invincible2", false);
            _Image_PropTime.fillAmount = 1.0f;
            _PropTimer = 0.0f;
        }
        private void Start() => Initialization();

        private void Update()
        {
            if (!GameManager._InGame) return;
            PropTime();
        }

        public void InvincibleEnable() => _Invincible = true;
        public void InvincibleDisable() => _Invincible = false;
        public void InvincibleEffectEnable() => _ParticleSystem_Invincible.Play();
        public void InvincibleEffectDisable() => _ParticleSystem_Invincible.Stop();

        public void VerifyHealth(float _meter) => _Slider_MaxHealth.value = 100.0f - (_meter * 0.005f);
        //public void VerifyHealth(float _meter) => _Slider_MaxHealth.value = 10.0f;
        private void HealthBarColorChange()
        {
            Color _color_lerp = Color.Lerp(_LowHealthColor, _HighHealthColor, _Slider_Health.value / _Slider_MaxHealth.value);
            _Image_Health.color = _color_lerp;
        }

        private void PropTime()
        {
            if (_PropTimer <= 0.0f) return;
            _Image_PropTime.fillAmount -= 1.0f / _PropTimer * TimeSystem._DeltaTime();
            if (_Image_PropTime.fillAmount <= 0.0f)
            {
                _Object_PropTime.SetActive(false);
                _Image_PropTime.fillAmount = 1.0f;
                _PropTimer = 0.0f;
            }
        }
        public void Supplies(string _tag, int _id, Sprite _sprite)
        {
            if (_tag == "SuppliesProps")
            {
                switch (_id)
                {
                    case 0:
                        {
                            // 護盾
                            _Animator.SetBool("Invincible2", true);
                            _ParticleSystem_Invincible.Play();
                            break;
                        }
                    case 1:
                        {
                            // 炸彈
                            EnemyAI._Recycle = true;
                            JellyFishEnemyAI._Recycle = true;
                            _ParticleSystem_Light.Play();
                            _ParticleSystem_Death.Play();
                            break;
                        }
                    case 2:
                        {
                            // 玩家加速
                            MovementSystem._Instance._Magnification = 2.5f;
                            _ParticleSystem_Yellow.Play();
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = 5.0f;
                            _Object_PropTime.SetActive(true);
                            StartCoroutine(Delay(5.0f));
                            IEnumerator Delay(float _time)
                            {
                                yield return new WaitForSeconds(_time);
                                MovementSystem._Instance._Magnification = 1.0f;
                                _ParticleSystem_Yellow.Stop();
                            }
                            break;
                        }
                    case 3:
                        {
                            // 偽裝
                            MovementSystem._Instance._Scale = Vector3.one * 0.25f;
                            int _i = Random.Range(0, _List_Sprite_Fishes.Count);
                            _SpriteRenderer.sprite = _List_Sprite_Fishes[_i];
                            _Invincible_Guise = true;
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = 5.0f;
                            _Object_PropTime.SetActive(true);
                            StartCoroutine(Delay(5.0f));
                            IEnumerator Delay(float _time)
                            {
                                yield return new WaitForSeconds(_time);
                                MovementSystem._Instance._Scale = MovementSystem._Instance._Scale_Original;
                                _SpriteRenderer.sprite = _Sprite_Player;
                                _Invincible_Guise = false;
                            }
                            break;
                        }
                    case 4:
                        {
                            // 慢動作
                            TimeSystem.TimeScale(0.5f);
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = 2.0f;
                            _Object_PropTime.SetActive(true);
                            StartCoroutine(Delay(2.0f));
                            IEnumerator Delay(float _time)
                            {
                                yield return new WaitForSeconds(_time);
                                TimeSystem.TimeScale(1.0f);
                            }
                        }
                        break;
                    case 5:
                        {
                            // 撞魚
                            _Attack = true;
                            _ParticleSystem_Attack.Play();
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = 5.0f;
                            _Object_PropTime.SetActive(true);
                            StartCoroutine(Delay(5.0f));
                            IEnumerator Delay(float _time)
                            {
                                yield return new WaitForSeconds(_time);
                                _Attack = false;
                                _ParticleSystem_Attack.Stop();
                            }
                        }
                        break;
                    case 6:
                        {
                            // 玩家縮小
                            MovementSystem._Instance._Scale_Magnification = 0.5f;
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = 5.0f;
                            _Object_PropTime.SetActive(true);
                            StartCoroutine(Delay(5.0f));
                            IEnumerator Delay(float _time)
                            {
                                yield return new WaitForSeconds(_time);
                                MovementSystem._Instance._Scale_Magnification = 1.0f;
                            }
                        }
                        break;
                    case 7:
                        {
                            // 增加復活次數
                            GameManager._Instance.ResurrectControl(1);
                        }
                        break;
                }
                return;
            }
            if (_tag == "SuppliesAd")
            {
                if (Advertising.IsRewardedAdReady())
                {
                    Advertising.ShowRewardedAd();
                    AdvertisingEvent._Reward_MaxHealth = true;
                }
            }
        }
        public void MaxHealth()
        {
            _Slider_Health.value = _Slider_MaxHealth.value;
            HealthBarColorChange();
            InvincibleEnable();
            _Animator.SetTrigger("Invincible");
        }

        public void DeathEnable()
        {
            GameManager._InGame = false;
            MenuSystem._Instance.StateChange(MenuSystem.Status.Animation);
            MovementSystem._Instance._FloatingJoystick.Initialize();
            MovementSystem._Instance._DynamicJoystick.Initialize();
            _ParticleSystem_Death.Play();
        }
        public void DeathDisable()
        {
            TimeSystem.TimeScale(1.0f);
            GameManager._InGame = true;
            MenuSystem._Instance.StateChange(MenuSystem.Status.InGame);
            _Slider_Health.value = _Slider_MaxHealth.value;
            HealthBarColorChange();
            InvincibleEnable();
            _Animator.SetBool("Death", false);
            _ParticleSystem_Death.Stop();
        }
        public void DeathMenu()
        {
            MenuSystem._Instance.StateChange(MenuSystem.Status.DeathMenu);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_Attack) return;
            if (collision.tag == "Enemy")
            {
                Attack _attack = collision.GetComponent<Attack>();
                if (_attack == null) return;
                _attack.AttackEnemy();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!GameManager._InGame) return;
            if (_Invincible) return;
            if (_Attack) return;
            if (_Invincible_Guise) return;
            if (collision.tag == "Enemy")
            {
                if (_Animator.GetBool("Invincible2"))
                {
                    _Animator.SetBool("Invincible2", false);
                    InvincibleEnable();
                    _Animator.SetTrigger("Invincible");
                    _ParticleSystem_Invincible.Stop();
                    _ParticleSystem_Death.Play();
                    Vibration.Vibrate(2);
                    return;
                }
                _Slider_Health.value -= 10.0f; // 12.5f
                if (_Slider_Health.value <= 0.0f) _Animator.SetBool("Death", true);
                else
                {
                    InvincibleEnable();
                    _Animator.SetTrigger("Injured");
                }
                HealthBarColorChange();
                Vibration.Vibrate(2);
            }
        }
    }
}
