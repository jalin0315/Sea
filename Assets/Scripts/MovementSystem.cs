﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSystem : MonoBehaviour
{
    public static MovementSystem _Instance;
    public FloatingJoystick _FloatingJoystick;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public Rigidbody2D _Rigidbody2D;
    [SerializeField] private float _Speed;
    [SerializeField] private float _MaxVelocitySpeed;
    private Vector3 _Scale;
    [SerializeField] private Vector3 _Slope;
    [SerializeField] private float _SmoothSlopeSpeed;

    private void Awake() => _Instance = this;

    private void Start()
    {
        _Scale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (GameManager._Instance._EnableJoystick)
        {
            if (GameManager._Instance._InGame) JoystickMovement();
        }
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(100, 90, 100, 100), "X: "+_Rigidbody2D.velocity.x.ToString());
        //GUI.Label(new Rect(100, 100, 100, 100), "Y: " + _Rigidbody2D.velocity.y.ToString());
        //GUI.Label(new Rect(100, 110, 100, 100), "M: " + _Rigidbody2D.velocity.magnitude.ToString());
    }

    private void JoystickMovement()
    {
        Vector2 _movement = new Vector2(_FloatingJoystick.Horizontal, _FloatingJoystick.Vertical);
        _Rigidbody2D.AddForce(_movement * Time.deltaTime * _Speed);
        _Rigidbody2D.velocity = Vector2.ClampMagnitude(_Rigidbody2D.velocity, _MaxVelocitySpeed);
        PlayerObjectRotate();
    }

    private void PlayerObjectRotate()
    {
        Quaternion _rotation = Quaternion.Euler(_Slope);
        if (_Rigidbody2D.velocity.x > 0)
        {
            transform.localScale = _Scale;
            if (_Rigidbody2D.velocity.y > 0) _rotation.z = +_rotation.z;
            else if (_Rigidbody2D.velocity.y < 0) _rotation.z = -_rotation.z;
        }
        else if (_Rigidbody2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-_Scale.x, _Scale.y, _Scale.z);
            if (_Rigidbody2D.velocity.y > 0) _rotation.z = -_rotation.z;
            else if (_Rigidbody2D.velocity.y < 0) _rotation.z = +_rotation.z;
        }
        float _result_z = Mathf.Lerp(0.0f, _rotation.z, _Rigidbody2D.velocity.magnitude);
        Quaternion _rotation_result = new Quaternion(_rotation.x, _rotation.y, _result_z, _rotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation_result, Time.deltaTime * _SmoothSlopeSpeed);
    }
}