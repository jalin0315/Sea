using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class MovementSystem : MonoBehaviour
    {
        public static MovementSystem _Instance;
        public FloatingJoystick _FloatingJoystick;
        public DynamicJoystick _DynamicJoystick;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Rigidbody2D _Rigidbody2D;
        [HideInInspector] public float _VelocityX() { return _Rigidbody2D.velocity.x; }
        [SerializeField] private float _Speed;
        public float _Magnification;
        [SerializeField] private float _MaxVelocitySpeed;
        public Vector3 _Scale_Original;
        public Vector3 _Scale;
        public float _Scale_Magnification;
        [SerializeField] private Vector3 _Slope;
        [SerializeField] private float _SmoothSlopeSpeed;

        private void Awake() => _Instance = this;

        private void Start()
        {
            _Scale_Original = transform.localScale;
            _Scale = _Scale_Original;
        }

        private void FixedUpdate()
        {
            if (GameManager._Instance._Enable_Joystick) if (GameManager._InGame) JoystickMovement();
        }

        //private Vector2 _movement() { return new Vector2(_FloatingJoystick.Horizontal, _FloatingJoystick.Vertical); }
        private Vector2 _movement() { return new Vector2(_DynamicJoystick.Horizontal, _DynamicJoystick.Vertical); }
        private void JoystickMovement()
        {
            _Rigidbody2D.AddForce(_movement() * (_Speed * _Magnification));
            _Rigidbody2D.velocity = Vector2.ClampMagnitude(_Rigidbody2D.velocity, _MaxVelocitySpeed);
            PlayerObjectRotate();
        }

        private void PlayerObjectRotate()
        {
            Quaternion _rotation = Quaternion.Euler(_Slope);
            if (_Rigidbody2D.velocity.x > 0)
            {
                transform.localScale = _Scale * _Scale_Magnification;
                if (_Rigidbody2D.velocity.y > 0) _rotation.z = +_rotation.z;
                else if (_Rigidbody2D.velocity.y < 0) _rotation.z = -_rotation.z;
            }
            else if (_Rigidbody2D.velocity.x < 0)
            {
                transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z) * _Scale_Magnification;
                if (_Rigidbody2D.velocity.y > 0) _rotation.z = -_rotation.z;
                else if (_Rigidbody2D.velocity.y < 0) _rotation.z = +_rotation.z;
            }
            float _result_z = Mathf.Lerp(0.0f, _rotation.z, _Rigidbody2D.velocity.magnitude);
            Quaternion _rotation_result = new Quaternion(_rotation.x, _rotation.y, _result_z, _rotation.w);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation_result, TimeSystem._DeltaTime() * _SmoothSlopeSpeed);
        }
    }
}
