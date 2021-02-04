using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class ClioneLimacina : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Rigidbody2D _Rigidbody2D;
        [SerializeField] private Light _Light;
        private float _Scale;
        private float _Speed;
        private float _Time;
        private bool _Visible;
        public static bool _Recycle;
        // Variable
        private int _variable_int;
        private Vector3 _variable_vector3;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            transform.rotation = Quaternion.identity;
            _Scale = Random.Range(0.04f, 0.08f);
            _variable_vector3.x = _Scale;
            _variable_vector3.y = _Scale;
            _variable_vector3.z = 1.0f;
            transform.localScale = _variable_vector3;
            if (MovementSystem._Instance._VelocityX() > 1.0f)
            {
                _variable_int = Random.Range(0, 2);
                switch (_variable_int)
                {
                    case 0:
                        // 下方螢幕外生成
                        _variable_vector3.x = Random.Range(CameraControl._Origin.x, CameraControl._Vertex.x);
                        _variable_vector3.y = CameraControl._Origin.y;
                        _variable_vector3.z = 0.0f;
                        transform.position = _variable_vector3;
                        // 重新計算位置
                        _variable_vector3.y = _SpriteRenderer.bounds.min.y;
                        transform.position = _variable_vector3;
                        break;
                    case 1:
                        // 右方螢幕外生成
                        _variable_vector3.x = CameraControl._Vertex.x;
                        _variable_vector3.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _variable_vector3.z = 0.0f;
                        transform.position = _variable_vector3;
                        // 重新計算位置
                        _variable_vector3.x = Random.Range(_SpriteRenderer.bounds.max.x, _SpriteRenderer.bounds.max.x + 5.0f);
                        transform.position = _variable_vector3;
                        break;
                }
            }
            else if (MovementSystem._Instance._VelocityX() < -1.0f)
            {
                _variable_int = Random.Range(0, 2);
                switch (_variable_int)
                {
                    case 0:
                        // 下方螢幕外生成
                        _variable_vector3.x = Random.Range(CameraControl._Origin.x, CameraControl._Vertex.x);
                        _variable_vector3.y = CameraControl._Origin.y;
                        _variable_vector3.z = 0.0f;
                        transform.position = _variable_vector3;
                        // 重新計算位置
                        _variable_vector3.y = _SpriteRenderer.bounds.min.y;
                        transform.position = _variable_vector3;
                        break;
                    case 1:
                        // 左方螢幕外生成
                        _variable_vector3.x = CameraControl._Origin.x;
                        _variable_vector3.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                        _variable_vector3.z = 0.0f;
                        transform.position = _variable_vector3;
                        // 重新計算位置
                        _variable_vector3.x = Random.Range(_SpriteRenderer.bounds.min.x, _SpriteRenderer.bounds.min.x + -5.0f);
                        transform.position = _variable_vector3;
                        break;
                }
            }
            else
            {
                // 下方螢幕外生成
                _variable_vector3.x = Random.Range(CameraControl._Origin.x + -5.0f, CameraControl._Vertex.x + 5.0f);
                _variable_vector3.y = CameraControl._Origin.y;
                _variable_vector3.z = 0.0f;
                transform.position = _variable_vector3;
                // 重新計算位置
                _variable_vector3.y = _SpriteRenderer.bounds.min.y;
                transform.position = _variable_vector3;
            }
            _Animator.speed = Random.Range(0.5f, 2.5f);
            _Light.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            _Speed = Random.Range(80.0f, 250.0f);
            _Time = 3.0f;
        }

        private void Update()
        {
            if (_Recycle) EnemyManager._Instance.RecycleClioneLimacina(gameObject);
            if (!_Visible) _Time -= TimeSystem._DeltaTime();
            if (_Time <= 0.0f) EnemyManager._Instance.RecycleClioneLimacina(gameObject);
        }

        public void Movement()
        {
            _Rigidbody2D.AddRelativeForce(Vector2.up * TimeSystem._FixedDeltaTime() * _Speed, ForceMode2D.Impulse);
        }

        private void OnBecameVisible() { _Visible = true; }
        private void OnBecameInvisible() { _Visible = false; }
    }
}
