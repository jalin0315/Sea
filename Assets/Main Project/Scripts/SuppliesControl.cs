using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class SuppliesControl : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private string _Tag;
        [SerializeField] private int _ID;
        [SerializeField] private float _Speed;
        [HideInInspector] public float _AdTime;
        [SerializeField] private List<GameObject> _List_LightGroup;
        [HideInInspector] public Queue<GameObject> _Queue_GameObject = new Queue<GameObject>();
        public static bool _Recycle;
        private Vector2 _variable_vector2;

        private void Update()
        {
            if (_Tag == "SuppliesProps") { if (_Recycle) { SuppliesManager._Instance.RecycleProp(_Queue_GameObject, gameObject); return; } return; }
            if (_Tag == "SuppliesAd") { if (_Recycle) { SuppliesManager._Instance.RecycleAd(gameObject); return; } }
        }

        private void FixedUpdate()
        {
            if (_Tag == "SuppliesProps")
            {
                transform.Translate(Vector2.down * _Speed * TimeSystem._FixedDeltaTime());
                if (transform.position.y < CameraControl._Origin.y + -1.0f) SuppliesManager._Instance.RecycleProp(_Queue_GameObject, gameObject);
                return;
            }
            if (_Tag == "SuppliesAd")
            {
                if (_AdTime > 0)
                {
                    _AdTime -= TimeSystem._FixedDeltaTime();
                    _variable_vector2.x = transform.position.x;
                    _variable_vector2.y = 0.0f;
                    transform.position = Vector2.Lerp(transform.position, _variable_vector2, TimeSystem._FixedDeltaTime() * _Speed);
                    return;
                }
                if (_AdTime <= 0)
                {
                    _variable_vector2.x = transform.position.x;
                    _variable_vector2.y = CameraControl._Vertex.y + 5.0f;
                    transform.position = Vector2.Lerp(transform.position, _variable_vector2, TimeSystem._FixedDeltaTime() * _Speed);
                    if (transform.position.y > CameraControl._Vertex.y + 1.0f) SuppliesManager._Instance.RecycleAd(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!GameManager._InGame) return;
            if (collision.tag == "Player")
            {
                Player._Instance.Supplies(_Tag, _ID, _SpriteRenderer.sprite);
                if (_Tag == "SuppliesProps") SuppliesManager._Instance.RecycleProp(_Queue_GameObject, gameObject);
                if (_Tag == "SuppliesAd") SuppliesManager._Instance.RecycleAd(gameObject);
            }
        }
    }
}
