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
        public DynamicJoystick _DynamicJoystick;
        [SerializeField] private Rigidbody2D _Rigidbody2D;
        [HideInInspector] public float _VelocityX() { return _Rigidbody2D.velocity.x; }
        [SerializeField] private float _Speed;
        public float _SpeedAddition;
        [SerializeField] private float _MaxVelocitySpeed;
        private Vector3 _Scale;
        public float _Scale_Magnification;
        [SerializeField] private Vector3 _Slope;
        // Variable
        private float _variable_float;
        private Vector3 _variable_vector3;
        private Quaternion _variable_quaternion;

        public void Initialization()
        {
            _Rigidbody2D.velocity = Vector2.zero;
        }

        private void Awake() => _Instance = this;

        private void Start()
        {
            _Scale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (GameManager._Instance._Enable_Joystick) if (GameManager._InGame) JoystickMovement();
        }

        private Vector2 _movement() { return new Vector2(_DynamicJoystick.Horizontal, _DynamicJoystick.Vertical); }
        private void JoystickMovement()
        {
            _Rigidbody2D.AddForce(_movement() * (_Speed * _SpeedAddition));
            _Rigidbody2D.velocity = Vector2.ClampMagnitude(_Rigidbody2D.velocity, _MaxVelocitySpeed);
            PlayerObjectRotate();
        }

        private void PlayerObjectRotate()
        {
            _variable_quaternion = Quaternion.Euler(_Slope);
            if (_Rigidbody2D.velocity.x >= 0)
            {
                transform.localScale = _Scale * _Scale_Magnification;
                if (_Rigidbody2D.velocity.y > 0) _variable_quaternion.z = +_variable_quaternion.z;
                else if (_Rigidbody2D.velocity.y < 0) _variable_quaternion.z = -_variable_quaternion.z;
            }
            else if (_Rigidbody2D.velocity.x < 0)
            {
                _variable_vector3.x = -_Scale.x;
                _variable_vector3.y = _Scale.y;
                _variable_vector3.z = 1.0f;
                transform.localScale = _variable_vector3 * _Scale_Magnification;
                if (_Rigidbody2D.velocity.y > 0) _variable_quaternion.z = -_variable_quaternion.z;
                else if (_Rigidbody2D.velocity.y < 0) _variable_quaternion.z = +_variable_quaternion.z;
            }
            _variable_float = Mathf.Lerp(0.0f, _variable_quaternion.z, _Rigidbody2D.velocity.magnitude);
            _variable_quaternion.z = _variable_float;
            transform.rotation = Quaternion.Slerp(transform.rotation, _variable_quaternion, TimeSystem._DeltaTime() * 5.0f);
        }
    }
}
