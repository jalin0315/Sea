using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _Speed;

    private void Update()
    {
        transform.Translate(Vector3.right * CTJ.TimeSystem._DeltaTime() * _Speed);
        if (transform.position.x >= 15.0f) transform.position = new Vector2(-15.0f, transform.position.y);
    }
}
