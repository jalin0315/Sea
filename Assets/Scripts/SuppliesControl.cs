using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesControl : MonoBehaviour
{
    [SerializeField] private float _Speed;
    public Queue<GameObject> _Queue = new Queue<GameObject>();
    public static bool _RecoveryAll;

    private void Update()
    {
        if (_RecoveryAll)
        {
            SuppliesManager._Instance.Recovery(_Queue, gameObject);
            return;
        }
        transform.Translate(Vector2.down * _Speed * Time.deltaTime, Space.Self);
        Vector2 _origin = Camera.main.ScreenToWorldPoint(Vector2.zero);
        if (transform.position.y < _origin.y) SuppliesManager._Instance.Recovery(_Queue, gameObject);
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
