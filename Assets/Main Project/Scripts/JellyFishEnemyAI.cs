using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class JellyFishEnemyAI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Rigidbody2D _Rigidbody2D;
        [SerializeField] private Light _Light;
        private float _Speed;
        private bool _Visible;
        [SerializeField] private float _Time;
        private float _Timer;
        public static bool _Recycle;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private float _scale;
        private Vector2 _position;
        private Vector2 _new_position;
        private int _r;
        private void OnEnable()
        {
            _scale = Random.Range(0.05f, 0.1f);
            transform.localScale = new Vector3(_scale, _scale, 1.0f);
            if (MovementSystem._Instance._VelocityX() > 2.0f)
            {
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        // 下方螢幕外生成
                        _position.x = Random.Range(CameraControl._Instance._Origin().x, CameraControl._Instance._Vertex().x);
                        _position.y = CameraControl._Instance._Origin().y;
                        transform.position = _position;
                        // 重新計算位置
                        _new_position = transform.position;
                        _new_position.y = _SpriteRenderer.bounds.min.y;
                        transform.position = _new_position;
                        break;
                    case 1:
                        // 右螢幕外生成
                        _position.x = CameraControl._Instance._Vertex().x;
                        _position.y = Random.Range(CameraControl._Instance._Origin().y, CameraControl._Instance._Vertex().y);
                        transform.position = _position;
                        // 重新計算位置
                        _new_position = transform.position;
                        _new_position.x = Random.Range(_SpriteRenderer.bounds.max.x, _SpriteRenderer.bounds.max.x + 2.5f);
                        transform.position = _new_position;
                        break;
                }
            }
            else if (MovementSystem._Instance._VelocityX() < -2.0f)
            {
                _r = Random.Range(0, 2);
                switch (_r)
                {
                    case 0:
                        // 下方螢幕外生成
                        _position.x = Random.Range(CameraControl._Instance._Origin().x, CameraControl._Instance._Vertex().x);
                        _position.y = CameraControl._Instance._Origin().y;
                        transform.position = _position;
                        // 重新計算位置
                        _new_position = transform.position;
                        _new_position.y = _SpriteRenderer.bounds.min.y;
                        transform.position = _new_position;
                        break;
                    case 1:
                        // 左螢幕外生成
                        _position.x = CameraControl._Instance._Origin().x;
                        _position.y = Random.Range(CameraControl._Instance._Origin().y, CameraControl._Instance._Vertex().y);
                        transform.position = _position;
                        // 重新計算位置
                        _new_position = transform.position;
                        _new_position.x = Random.Range(_SpriteRenderer.bounds.min.x, _SpriteRenderer.bounds.min.x + -2.5f);
                        transform.position = _new_position;
                        break;
                }
            }
            else
            {
                // 下方螢幕外生成
                _position.x = Random.Range(CameraControl._Instance._Origin().x + -5.0f, CameraControl._Instance._Vertex().x + 5.0f);
                _position.y = CameraControl._Instance._Origin().y;
                transform.position = _position;
                // 重新計算位置
                _new_position = transform.position;
                _new_position.y = _SpriteRenderer.bounds.min.y;
                transform.position = _new_position;
            }
            _Animator.speed = Random.Range(0.5f, 2.0f);
            _Speed = Random.Range(50.0f, 200.0f);
            _Timer = _Time;
            _Light.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        }

        private void Update()
        {
            if (_Recycle) { EnemyManager._Instance.RecycleJellyFish(gameObject); return; }
            if (!_Visible) _Timer -= TimeSystem._DeltaTime();
            if (_Timer <= 0.0f)
            {
                _Timer = 0.0f;
                EnemyManager._Instance.RecycleJellyFish(gameObject);
                return;
            }
        }

        public void Movement()
        {
            _Rigidbody2D.AddForce(Vector2.up * TimeSystem._FixedDeltaTime() * _Speed, ForceMode2D.Impulse);
        }

        private void OnBecameVisible()
        {
            _Visible = true;
            _Timer = _Time;
        }
        private void OnBecameInvisible()
        {
            _Visible = false;
        }
    }
}
