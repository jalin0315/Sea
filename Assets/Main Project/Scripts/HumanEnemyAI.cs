using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class HumanEnemyAI : MonoBehaviour
    {
        public enum Status
        {
            Idle,
            Attack,
            Loop
        }
        private Status _Status;
        private Vector3 _Scale;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Collider2D[] _Array_Collider2D;
        private Transform _Target;
        private float _Speed;
        public static bool _Recycle;
        private Vector3 _variable_vector3;
        private int _variable_r;
        private int _variable_int;
        private float _variable_float;

        public void Initialization()
        {
            _variable_r = Random.Range(0, 2);
            switch (_variable_r)
            {
                case 0:
                    _variable_vector3.x = CameraControl._Instance._Origin().x;
                    if (_Target.position.y > 1.5f) _variable_vector3.y = 1.5f;
                    else if (_Target.position.y < -5.5f) _variable_vector3.y = -5.5f;
                    else _variable_vector3.y = _Target.position.y;
                    _variable_vector3.z = 0.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.min.x;
                    _variable_vector3.y = _SpriteRenderer.bounds.center.y;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = -_Scale.x;
                    _variable_vector3.y = _Scale.y;
                    _variable_vector3.z = _Scale.z;
                    transform.localScale = _variable_vector3;
                    break;
                case 1:
                    _variable_vector3.x = CameraControl._Instance._Vertex().x;
                    if (_Target.position.y > 1.5f) _variable_vector3.y = 1.5f;
                    else if (_Target.position.y < -5.5f) _variable_vector3.y = -5.5f;
                    else _variable_vector3.y = _Target.position.y;
                    _variable_vector3.z = 0.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.max.x;
                    _variable_vector3.y = _SpriteRenderer.bounds.center.y;
                    transform.position = _variable_vector3;
                    transform.localScale = _Scale;
                    break;
            }
        }

        private void Awake()
        {
            _Target = GameObject.FindGameObjectWithTag("Player").transform;
            _Scale = transform.localScale;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Initialization();
        }

        private void Update()
        {
            if (_Recycle) EnemyManager._Instance.RecycleHuman(gameObject);
            StatusUpdate();
        }

        public void StatusChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Idle:
                    _variable_float = 1.0f;
                    break;
                case Status.Attack:
                    break;
                case Status.Loop:
                    _variable_float = 0.0f;
                    break;
            }
        }
        private void StatusUpdate()
        {
            switch (_Status)
            {
                case Status.Idle:
                    _variable_float -= TimeSystem._DeltaTime();
                    _Speed = Mathf.Lerp(0.0f, 5.0f, _variable_float);
                    switch (_variable_r)
                    {
                        case 0:
                            transform.Translate(Vector3.right * TimeSystem._DeltaTime() * _Speed, Space.World);
                            break;
                        case 1:
                            transform.Translate(Vector3.left * TimeSystem._DeltaTime() * _Speed, Space.World);
                            break;
                    }
                    break;
                case Status.Attack:
                    break;
                case Status.Loop:
                    _variable_float += TimeSystem._DeltaTime();
                    _Speed = Mathf.Lerp(0.0f, 15.0f, _variable_float);
                    switch (_variable_r)
                    {
                        case 0:
                            transform.Translate(Vector3.right * TimeSystem._DeltaTime() * _Speed, Space.World);
                            break;
                        case 1:
                            transform.Translate(Vector3.left * TimeSystem._DeltaTime() * _Speed, Space.World);
                            break;
                    }
                    break;
            }
        }

        public void Attack()
        {
            _variable_int = Random.Range(0, 2);
            switch (_variable_int)
            {
                case 0:
                    _Animator.SetTrigger("Attack");
                    _Animator.SetBool("Loop", true);
                    break;
                case 1:
                    _Animator.SetTrigger("Attack2");
                    _Animator.SetBool("Loop2", true);
                    break;
            }
        }

        public void Collider2D(int _enable)
        {
            for (int _i = 0; _i < _Array_Collider2D.Length; _i++)
            {
                _Array_Collider2D[_i].enabled = _enable == 1 ? true : false;
            }
        }

        private void OnBecameInvisible()
        {
            _Animator.SetBool("Loop", false);
            _Animator.SetBool("Loop2", false);
            Collider2D(0);
            EnemyManager._Instance.RecycleHuman(gameObject);
        }
    }
}
