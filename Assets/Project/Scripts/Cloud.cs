using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Transform _Transform;
    [SerializeField] private float _Speed;
    private Vector2 _variable_vector2;

    private void Update()
    {
        _Transform.Translate(Vector3.left * CTJ.TimeSystem._DeltaTime() * _Speed);
        if (_Transform.localPosition.x <= -15.0f)
        {
            _variable_vector2.x = 15.0f;
            _variable_vector2.y = _Transform.localPosition.y;
            _Transform.localPosition = _variable_vector2;
        }
    }
}
