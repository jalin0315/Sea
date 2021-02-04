using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class BossLegAI : MonoBehaviour
    {
        private Vector3 _Scale;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Collider2D[] _Array_Collider2D;
        private float _Delta(float _value) { return Mathf.DeltaAngle(0, _value); }
        [SerializeField] private float _Speed;
        public static bool _Recycle;
        private Vector3 _variable_vector3;
        private int _variable_int;

        private void Awake()
        {
            _Scale = transform.localScale;
        }

        private bool _return = true;
        private void OnEnable()
        {
            if (_return) { _return = false; return; }
            transform.rotation = Quaternion.identity;
            _variable_int = Random.Range(0, 2);
            switch (_variable_int)
            {
                case 0:
                    _variable_vector3.x = CameraControl._Origin.x;
                    _variable_vector3.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                    _variable_vector3.z = -1.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.min.x;
                    transform.position = _variable_vector3;
                    break;
                case 1:
                    _variable_vector3.x = CameraControl._Vertex.x;
                    _variable_vector3.y = Random.Range(CameraControl._Origin.y, CameraControl._Vertex.y);
                    _variable_vector3.z = -1.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.max.x;
                    transform.position = _variable_vector3;
                    break;
            }
            _variable_vector3 = BossAI._Instance._Player.position;
            _variable_vector3.x += Random.Range(-5.0f, 5.0f);
            _variable_vector3.y += Random.Range(-5.0f, 5.0f);
            _variable_vector3.z = -1.0f;
            transform.right = (_variable_vector3 - transform.position).normalized;
            if (_Delta(transform.eulerAngles.z) > 90.0f || _Delta(transform.eulerAngles.z) < -90.0f)
            {
                _variable_vector3.x = transform.localScale.x;
                _variable_vector3.y = -_Scale.y;
                _variable_vector3.z = 1.0f;
                transform.localScale = _variable_vector3;
            }
            else
            {
                _variable_vector3.x = transform.localScale.x;
                _variable_vector3.y = _Scale.y;
                _variable_vector3.z = 1.0f;
                transform.localScale = _variable_vector3;
            }
            _Speed = 10.0f;
            _variable_int = Random.Range(0, 6);
            switch (_variable_int)
            {
                case 0:
                    _Animator.SetTrigger("Attack00");
                    break;
                case 1:
                    _Animator.SetTrigger("Attack01");
                    break;
                case 2:
                    _Animator.SetTrigger("Attack02");
                    break;
                case 3:
                    _Animator.SetTrigger("Attack03");
                    break;
                case 4:
                    _Animator.SetTrigger("Attack04");
                    break;
                case 5:
                    _Animator.SetTrigger("Attack05");
                    break;
            }
            for (int _i = 0; _i < _Array_Collider2D.Length; _i++) _Array_Collider2D[_i].enabled = true;
        }

        private void Update()
        {
            if (_Recycle)
            {
                for (int _i = 0; _i < _Array_Collider2D.Length; _i++) _Array_Collider2D[_i].enabled = false;
                BossAI._Instance.Recycle(gameObject);
                return;
            }
            _Speed -= TimeSystem._DeltaTime() * 4.5f;
            transform.Translate(Vector2.right * TimeSystem._DeltaTime() * _Speed);
            if (_Speed <= -15.0f)
            {
                for (int _i = 0; _i < _Array_Collider2D.Length; _i++) _Array_Collider2D[_i].enabled = false;
                BossAI._Instance.Recycle(gameObject);
                return;
            }
        }
    }
}
