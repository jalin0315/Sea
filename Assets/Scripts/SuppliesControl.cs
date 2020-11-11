using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesControl : MonoBehaviour
{
    public Vector3 _Scale;
    [SerializeField] private float _Speed;
    public Queue<GameObject> _Queue = new Queue<GameObject>();
    public int _Direction;
    public static bool _RecoveryAll;

    private void Update()
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
