using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishEnemyAI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _Rigidbody2D;
    [SerializeField] private Light _Light;
    [SerializeField] private float _Speed;
    private bool _Visible;
    [SerializeField] private float _Time;
    private float _Timer;
    public static bool _RecoveryAll;

    private void OnEnable()
    {
        float _position_x;
        float _position_y;
        if (MovementSystem._Instance._VelocityX() > 2.0f)
        {
            _position_x = Random.Range(CameraControl._Instance._Vertex().x, CameraControl._Instance._Vertex().x + 5.0f);
            _position_y = Random.Range(CameraControl._Instance._Origin().y, CameraControl._Instance._Vertex().y);
        }
        else if (MovementSystem._Instance._VelocityX() < -2.0f)
        {
            _position_x = Random.Range(CameraControl._Instance._Origin().x, CameraControl._Instance._Origin().x + -5.0f);
            _position_y = Random.Range(CameraControl._Instance._Origin().y, CameraControl._Instance._Vertex().y);
        }
        else
        {
            _position_x = Random.Range(CameraControl._Instance._Origin().x + -10.0f, CameraControl._Instance._Vertex().x + 10.0f);
            _position_y = CameraControl._Instance._Origin().y + -0.5f;
        }
        Vector2 _position = new Vector2(_position_x, _position_y);
        transform.position = _position;

        float _scale = Random.Range(0.05f, 0.1f);
        transform.localScale = new Vector3(_scale, _scale, 1.0f);

        _Timer = _Time;

        _Light.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void Update()
    {
        if (_RecoveryAll) EnemyManager._Instance.RecoveryJellyFish(gameObject);
        if (!_Visible) _Timer -= CTJ.TimeSystem._DeltaTime();
        if (_Timer <= 0.0f)
        {
            _Timer = 0.0f;
            EnemyManager._Instance.RecoveryJellyFish(gameObject);
        }
    }

    public void Movement()
    {
        _Rigidbody2D.AddForce(Vector2.up * CTJ.TimeSystem._FixedDeltaTime() * _Speed, ForceMode2D.Impulse);
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
