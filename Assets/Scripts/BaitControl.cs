using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitControl : MonoBehaviour
{
    public Rigidbody2D _Rigidbody2D;
    public Queue<GameObject> _Queue = new Queue<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") return;
    }
}
