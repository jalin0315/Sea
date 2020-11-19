using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesControl : MonoBehaviour
{
    public Vector3 _Scale;
    [SerializeField] private float _Speed;
    public Queue<GameObject> _Queue = new Queue<GameObject>();
    public static bool _RecoveryAll;
    public int _Direction;
    public float _Time;
    public float _Timer;
    [SerializeField] private List<GameObject> _List_LightGroup = new List<GameObject>();

    private void FixedUpdate()
    {
        if (_RecoveryAll)
        {
            SuppliesManager._Instance.Recovery(_Queue, gameObject);
            return;
        }
        if (tag == "SuppliesRed" || tag == "SuppliesYellow")
        {
            transform.Translate(Vector2.down * _Speed * Time.deltaTime, Space.Self);
            Vector2 _origin = SuppliesManager._Instance._Camera.ScreenToWorldPoint(Vector2.zero);
            if (transform.position.y < _origin.y) SuppliesManager._Instance.Recovery(_Queue, gameObject);
            return;
        }
        if (tag == "SuppliesAd")
        {
            // 螢幕內自動轉向
            /*
            Vector2 _origin = SuppliesManager._Instance._Camera.ScreenToWorldPoint(Vector2.zero);
            Vector2 _vertex = SuppliesManager._Instance._Camera.ScreenToWorldPoint(new Vector2(SuppliesManager._Instance._Camera.pixelWidth, SuppliesManager._Instance._Camera.pixelHeight));
            if (transform.position.x > _vertex.x)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180.0f);
                transform.localScale = new Vector3(_Scale.x, -_Scale.y, _Scale.z);
            }
            else if (transform.position.x < _origin.x)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                transform.localScale = _Scale;
            }
            transform.Translate(Vector2.right * _Speed * Time.deltaTime, Space.Self);
            */
            /*
            if (_Direction == 0)
            {
                transform.Translate(Vector2.right * _Speed * Time.deltaTime, Space.Self);
                return;
            }
            if (_Direction == 1)
            {
                transform.Translate(Vector2.left * _Speed * Time.deltaTime, Space.Self);
                return;
            }
            */
            if (_Timer > 0)
            {
                _Timer -= Time.deltaTime;
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, 0.0f), Time.deltaTime * _Speed);
            }
            else if (_Timer <= 0)
            {
                Vector2 _vertex = SuppliesManager._Instance._Camera.ScreenToWorldPoint(new Vector2(SuppliesManager._Instance._Camera.pixelWidth, SuppliesManager._Instance._Camera.pixelHeight));
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, _vertex.y + 10.0f), Time.deltaTime * _Speed);
                if (transform.position.y > _vertex.y + 5.0f)
                    SuppliesManager._Instance.Recovery(_Queue, gameObject);
                _Timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player._Instance.Supplies(tag);
            SuppliesManager._Instance.Recovery(_Queue, gameObject);
        }
    }
}
