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
        public Status _Status;
        private Vector3 _Scale;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Collider2D[] _Array_Collider2D;
        private Transform _Target;
        private float _Center() { return (CameraControl._Instance._Origin().y + CameraControl._Instance._Vertex().y) / 2.0f; }
        private Vector3 _variable_vector3;
        private int _variable_r;

        public void Initialization()
        {
            _variable_r = Random.Range(0, 2);
            switch (_variable_r)
            {
                case 0:
                    _variable_vector3.x = CameraControl._Instance._Origin().x;
                    _variable_vector3.y = _Target.position.y;
                    _variable_vector3.z = 0.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.y = _SpriteRenderer.bounds.center.y;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.min.x;
                    transform.position = _variable_vector3;

                    _variable_vector3.x = -_Scale.x;
                    _variable_vector3.y = _Scale.y;
                    _variable_vector3.z = _Scale.z;
                    transform.localScale = _variable_vector3;
                    break;
                case 1:
                    _variable_vector3.x = CameraControl._Instance._Vertex().x;
                    _variable_vector3.y = _Target.position.y;
                    _variable_vector3.z = 0.0f;
                    transform.position = _variable_vector3;
                    _variable_vector3.y = _SpriteRenderer.bounds.center.y;
                    transform.position = _variable_vector3;
                    _variable_vector3.x = _SpriteRenderer.bounds.max.x;
                    transform.position = _variable_vector3;

                    transform.localScale = _Scale;
                    break;
            }
        }

        private void Awake()
        {
            _Target = GameObject.FindGameObjectWithTag("Player").transform;
            _Scale = transform.localScale;
            //gameObject.SetActive(false);
        }

        private void Start()
        {
            Initialization();
        }

        private void Update()
        {
            StatusUpdate();
        }

        private void StatusUpdate()
        {
            switch (_Status)
            {
                case Status.Idle:
                    switch (_variable_r)
                    {
                        case 0:
                            transform.Translate(Vector3.right * TimeSystem._DeltaTime() * 2.0f, Space.World);
                            break;
                        case 1:
                            transform.Translate(Vector3.left * TimeSystem._DeltaTime() * 2.0f, Space.World);
                            break;
                    }
                    break;
                case Status.Attack:
                    break;
                case Status.Loop:
                    switch (_variable_r)
                    {
                        case 0:
                            transform.Translate(Vector3.right * TimeSystem._DeltaTime() * 5.0f, Space.World);
                            break;
                        case 1:
                            transform.Translate(Vector3.left * TimeSystem._DeltaTime() * 5.0f, Space.World);
                            break;
                    }
                    break;
            }
        }

        public void Attack()
        {
            _Animator.SetTrigger("Attack");
            _Animator.SetBool("Loop", true);
        }

        private void OnBecameInvisible()
        {
            _Animator.SetBool("Loop", false);
        }
    }
}
