using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player _Instance;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public int _Health;
    public Text _Text_Health;
    private bool _Invincible;
    [SerializeField] private float _InvincibleTime;
    private float _I_Timer;
    [SerializeField] private bool _ChangeColor;
    private bool _Break;

    private void Awake() => _Instance = this;

    private void Update()
    {
        if (GameManager._Instance._InGame) _Text_Health.text = "Health: " + _Health.ToString();
        Injured();
    }

    private void Injured()
    {
        if (!_Invincible) return;
        _I_Timer -= Time.deltaTime;
        if(_ChangeColor) _SpriteRenderer.color = Color.Lerp(_SpriteRenderer.color, Color.red, Time.deltaTime * 5.0f);
        else _SpriteRenderer.color = Color.Lerp(_SpriteRenderer.color, Color.white, Time.deltaTime * 5.0f);
        if (_I_Timer < 0)
        {
            _SpriteRenderer.color = Color.white;
            _I_Timer = _InvincibleTime;
            _Break = true;
            _Invincible = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_Invincible)
        {
            if (collision.tag == "Enemy")
            {
                _Health -= 1;
                StartCoroutine(Test(0.2f));
                IEnumerator Test(float _time)
                {
                    _Break = false;
                    while (true)
                    {
                        if (_Break) break;
                        _ChangeColor = true;
                        yield return new WaitForSeconds(_time);
                        _ChangeColor = false;
                        yield return new WaitForSeconds(_time);
                    }
                }
                _Invincible = true;
            }
        }
    }
}
