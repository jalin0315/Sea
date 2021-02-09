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
        public Animator _Animator;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        public Sprite _Sprite_Player;
        [SerializeField] private Animator _Animator_Health;
        [SerializeField] private Image _Image_WaterPressure;
        public Image _Image_Health;
        [SerializeField] private int _Health;
        [SerializeField] private int _WaterPressure;
        [HideInInspector] public bool _Invincible;
        private bool _Spy;
        private bool _Attack;
        [SerializeField] private Image _Image_PropTimeBackground;
        [SerializeField] private Image _Image_PropTime;
        private float _PropTimer;
        [SerializeField] private float _AccelerateTime;
        [SerializeField] private float _SpyTime;
        [SerializeField] private float _SlowMotionTime;
        [HideInInspector] public bool _SlowMotionActivity;
        public float _SlowMotionActivityTime;
        [SerializeField] private float _AttackTime;
        [SerializeField] private float _ShrinkTime;
        [SerializeField] private Sprite[] _Array_Sprite_Enemies;
        [SerializeField] private ParticleSystem _ParticleSystem_Invincible;
        [SerializeField] private ParticleSystem _ParticleSystem_Shield;
        [SerializeField] private ParticleSystem _ParticleSystem_Destroy;
        [SerializeField] private ParticleSystem _ParticleSystem_Accelerate;
        [SerializeField] private ParticleSystem _ParticleSystem_Explosion;
        [SerializeField] private ParticleSystem _ParticleSystem_Attack;
        [SerializeField] private ParticleSystem _ParticleSystem_Kick;
        [SerializeField] private ParticleSystem _ParticleSystem_Damage;
        [SerializeField] private ParticleSystem _ParticleSystem_Death;

        private void Awake()
        {
            _Instance = this;
        }

        public void Initialization()
        {
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
            _Invincible = false;
            _Spy = false;
            _Attack = false;
            _Animator.SetBool("Shield", false);
            _Animator.SetBool("Death", false);
            _SpriteRenderer.sprite = _Sprite_Player;
            InitHealth();
            HealthBarColorChange();
            StopAllCoroutines();
            _Image_PropTime.fillAmount = 1.0f;
            _PropTimer = 0.0f;
            _SlowMotionActivity = false;
            MovementSystem._Instance._Scale_Magnification = 1.0f;
            MovementSystem._Instance._SpeedAddition = 1.0f;
            _ParticleSystem_Invincible.Stop();
            _ParticleSystem_Shield.Stop();
            _ParticleSystem_Destroy.Stop();
            _ParticleSystem_Accelerate.Stop();
            _ParticleSystem_Explosion.Stop();
            _ParticleSystem_Attack.Stop();
            _ParticleSystem_Kick.Stop();
            _ParticleSystem_Damage.Stop();
            _ParticleSystem_Death.Stop();
        }
        private void Start() => Initialization();

        private void Update()
        {
            PropTime();
            if (!GameManager._InGame) return;
        }

        public void InitHealth()
        {
            _Health = 100 - Mathf.FloorToInt(GameManager._Meter * 0.005f);
            _WaterPressure = Mathf.FloorToInt(GameManager._Meter * 0.005f);
            _Image_WaterPressure.fillAmount = _WaterPressure * 0.01f;
            _Image_Health.fillAmount = _Health * 0.01f;
        }
        public void VerifyHealth(float _meter)
        {
            _WaterPressure = Mathf.FloorToInt(_meter * 0.005f);
            _Image_WaterPressure.fillAmount = _WaterPressure * 0.01f;
        }
        private void HealthBarColorChange()
        {
            _Image_Health.fillAmount = _Health * 0.01f;
            if (_Health <= 20)
            {
                AudioSystem._Instance.PlaySoundEffect("HealthAlert");
                _Animator_Health.SetTrigger("Danger");
            }
            else if (_Health <= 40) _Animator_Health.SetTrigger("Warning");
            else _Animator_Health.SetTrigger("Normal");
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
                            AudioSystem._Instance.PlaySoundEffect("Power");
                            Shield(true);
                        }
                        break;
                    case 1:
                        {
                            // 炸彈
                            AudioSystem._Instance.PlaySoundEffect("Bomb");
                            EnemyAI._Recycle = true;
                            JellyFishEnemyAI._Recycle = true;
                            ClioneLimacina._Recycle = true;
                            _ParticleSystem_Explosion.Play();
                        }
                        break;
                    case 2:
                        {
                            // 玩家加速
                            AudioSystem._Instance.PlaySoundEffect("Power");
                            MovementSystem._Instance._SpeedAddition = 5.0f;
                            _ParticleSystem_Accelerate.Play();
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = _AccelerateTime;
                            Accelerate(_AccelerateTime);
                            MenuSystem._Instance._Animator.SetBool("PropTime", true);
                        }
                        break;
                    case 3:
                        {
                            // 偽裝
                            AudioSystem._Instance.PlaySoundEffect("Buff");
                            MovementSystem._Instance._Scale_Magnification = 3.0f;
                            _SpriteRenderer.sprite = _Array_Sprite_Enemies[Random.Range(0, _Array_Sprite_Enemies.Length)];
                            _Spy = true;
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = _SpyTime;
                            Spy(_SpyTime);
                            MenuSystem._Instance._Animator.SetBool("PropTime", true);
                        }
                        break;
                    case 4:
                        {
                            // 慢動作
                            AudioSystem._Instance.PlaySoundEffect("Buff");
                            _SlowMotionActivity = true;
                            TimeSystem.TimeScale(_SlowMotionActivityTime);
                            MovementSystem._Instance._SpeedAddition = 5.0f;
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = _SlowMotionTime;
                            SlowMotion(_SlowMotionTime);
                            MenuSystem._Instance._Animator.SetBool("PropTime", true);
                        }
                        break;
                    case 5:
                        {
                            // 撞魚
                            AudioSystem._Instance.PlaySoundEffect("Power");
                            _Attack = true;
                            _ParticleSystem_Attack.Play();
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = _AttackTime;
                            Attack(_AttackTime);
                            MenuSystem._Instance._Animator.SetBool("PropTime", true);
                        }
                        break;
                    case 6:
                        {
                            // 玩家縮小
                            AudioSystem._Instance.PlaySoundEffect("Buff");
                            MovementSystem._Instance._Scale_Magnification = 0.5f;
                            _Image_PropTimeBackground.sprite = _sprite;
                            _Image_PropTime.sprite = _sprite;
                            _Image_PropTime.fillAmount = 1.0f;
                            _PropTimer = _ShrinkTime;
                            Shrink(_ShrinkTime);
                            MenuSystem._Instance._Animator.SetBool("PropTime", true);
                        }
                        break;
                    case 7:
                        {
                            // 增加復活次數
                            AudioSystem._Instance.PlaySoundEffect("Life");
                            GameManager._Instance.ResurrectControl(1);
                        }
                        break;
                }
                return;
            }
            if (_tag == "SuppliesAd")
            {
                if (Advertising.IsInterstitialAdReady())
                {
                    AdvertisingEvent._Interstitial_MaxHealth = true;
                    Advertising.ShowInterstitialAd();
                    MovementSystem._Instance._DynamicJoystick.Initialization();
                    MovementSystem._Instance.Initialization();
                }
                else
                {
                    MaxHealth();
                    Logger.LogWarning("Interstitial advertising not ready yet.");
                }
            }
        }
        private IEnumerator IEnumeratorSingletonAccelerate;
        private IEnumerator IEnumeratorAccelerate(float _time)
        {
            yield return OPT._WaitForSeconds(_time);
            MovementSystem._Instance._SpeedAddition = 1.0f;
            _ParticleSystem_Accelerate.Stop();
        }
        private void Accelerate(float _time)
        {
            if (IEnumeratorSingletonAccelerate != null) StopCoroutine(IEnumeratorSingletonAccelerate);
            IEnumeratorSingletonAccelerate = IEnumeratorAccelerate(_time);
            StartCoroutine(IEnumeratorSingletonAccelerate);
        }
        private IEnumerator IEnumeratorSingletonSpy;
        private IEnumerator IEnumeratorSpy(float _time)
        {
            yield return OPT._WaitForSeconds(_time);
            MovementSystem._Instance._Scale_Magnification = 1.0f;
            _SpriteRenderer.sprite = _Sprite_Player;
            _Spy = false;
        }
        private void Spy(float _time)
        {
            if (IEnumeratorSingletonSpy != null) StopCoroutine(IEnumeratorSingletonSpy);
            IEnumeratorSingletonSpy = IEnumeratorSpy(_time);
            StartCoroutine(IEnumeratorSingletonSpy);
        }
        private IEnumerator IEnumeratorSingletonSlowMotion;
        private IEnumerator IEnumeratorSlowMotion(float _time)
        {
            yield return OPT._WaitForSeconds(_time);
            _SlowMotionActivity = false;
            TimeSystem.TimeScale(1.0f);
            MovementSystem._Instance._SpeedAddition = 1.0f;
        }
        private void SlowMotion(float _time)
        {
            if (IEnumeratorSingletonSlowMotion != null) StopCoroutine(IEnumeratorSingletonSlowMotion);
            IEnumeratorSingletonSlowMotion = IEnumeratorSlowMotion(_time);
            StartCoroutine(IEnumeratorSingletonSlowMotion);
        }
        private IEnumerator IEnumeratorSingletonAttack;
        private IEnumerator IEnumeratorAttack(float _time)
        {
            yield return OPT._WaitForSeconds(_time);
            _Attack = false;
            _ParticleSystem_Attack.Stop();
        }
        private void Attack(float _time)
        {
            if (IEnumeratorSingletonAttack != null) StopCoroutine(IEnumeratorSingletonAttack);
            IEnumeratorSingletonAttack = IEnumeratorAttack(_time);
            StartCoroutine(IEnumeratorSingletonAttack);
        }
        private IEnumerator IEnumeratorSingletonShrink;
        private IEnumerator IEnumeratorShrink(float _time)
        {
            yield return OPT._WaitForSeconds(_time);
            MovementSystem._Instance._Scale_Magnification = 1.0f;
        }
        private void Shrink(float _time)
        {
            if (IEnumeratorSingletonShrink != null) StopCoroutine(IEnumeratorSingletonShrink);
            IEnumeratorSingletonShrink = IEnumeratorShrink(_time);
            StartCoroutine(IEnumeratorSingletonShrink);
        }
        private void PropTime()
        {
            if (_PropTimer <= 0.0f) return;
            _Image_PropTime.fillAmount -= 1.0f / _PropTimer * TimeSystem._DeltaTime();
            if (_Image_PropTime.fillAmount <= 0.0f)
            {
                _Image_PropTime.fillAmount = 1.0f;
                MenuSystem._Instance._Animator.SetBool("PropTime", false);
                _PropTimer = 0.0f;
            }
        }
        public void MaxHealth()
        {
            Invincible();
            if (_Health < (100 - _WaterPressure)) _Health += 10;
            HealthBarColorChange();
        }

        private void Invincible()
        {
            _Invincible = true;
            _Animator.SetTrigger("Invincible");
            _ParticleSystem_Invincible.Play();
        }
        public void InvincibleDisable() => _Invincible = false;
        public void ParticleSystemInvincibleStop() => _ParticleSystem_Invincible.Stop();
        private void Shield(bool _enable)
        {
            if (_enable)
            {
                _Animator.SetBool("Shield", _enable);
                _ParticleSystem_Shield.Play();
                return;
            }
            else
            {
                _Animator.SetBool("Shield", _enable);
                _ParticleSystem_Shield.Stop();
                _ParticleSystem_Destroy.Play();
            }
        }
        private void Death()
        {
            GameManager._Instance.StopMusic();
            GameManager._InGame = false;
            MenuSystem._Instance.StateChange(MenuSystem.Status.Animation);
            MovementSystem._Instance._DynamicJoystick.Initialization();
            _Animator.SetBool("Death", true);
            _ParticleSystem_Death.Play();
        }
        public void Resurrection()
        {
            GameManager._Instance.PlayMusic();
            GameManager._Instance.GameState(true);
            MenuSystem._Instance.StateChange(MenuSystem.Status.InGameMenu);
            _Health = 100 - _WaterPressure;
            _Image_Health.fillAmount = _Health * 0.01f;
            HealthBarColorChange();
            _Animator.SetBool("Death", false);
            _ParticleSystem_Death.Stop();
            Invincible();
        }
        public void DeathMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Fail");
            GameManager._Instance.GameState(false);
            MenuSystem._Instance.StateChange(MenuSystem.Status.DeathMenu);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_Attack) return;
            if (collision.tag == "Enemy")
            {
                Attack _attack = collision.GetComponent<Attack>();
                if (_attack == null) return;
                AudioSystem._Instance.PlaySoundEffect("Attack");
                _attack.AttackEnemy();
                _ParticleSystem_Kick.Play();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!GameManager._InGame) return;
            if (_Invincible) return;
            if (_Spy) return;
            if (_Attack) return;
            if (collision.tag == "Enemy")
            {
                if (_Animator.GetBool("Shield"))
                {
                    AudioSystem._Instance.PlaySoundEffect("Destroy");
                    Shield(false);
                    Invincible();
                    return;
                }
                AudioSystem._Instance.PlaySoundEffect("Injured");
                _Health -= 10;
                if (_Health <= 0) Death();
                else _Animator.SetTrigger("Injured");
                _ParticleSystem_Damage.Play();
                _Invincible = true;
                HealthBarColorChange();
                Vibration.Vibrate(2);
            }
        }
    }
}
