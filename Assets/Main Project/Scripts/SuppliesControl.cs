using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class SuppliesControl : MonoBehaviour
    {
        [HideInInspector] public Vector3 _Scale;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private int _Number;
        [SerializeField] private float _Speed;
        public Queue<GameObject> _Queue = new Queue<GameObject>();
        public static bool _RecoveryAll;
        public int _Direction;
        public float _Time;
        public float _Timer;
        [SerializeField] private List<GameObject> _List_LightGroup = new List<GameObject>();

        private void Awake()
        {
            _Scale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (_RecoveryAll)
            {
                SuppliesManager._Instance.Recovery(_Queue, gameObject);
                return;
            }
            if (tag == "SuppliesRed" || tag == "SuppliesYellow" || tag == "SuppliesProps")
            {
                transform.Translate(Vector2.down * _Speed * TimeSystem._DeltaTime(), Space.Self);
                if (transform.position.y < SuppliesManager._Instance._Origin().y) SuppliesManager._Instance.Recovery(_Queue, gameObject);
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
                    _Timer -= TimeSystem._DeltaTime();
                    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, 0.0f), TimeSystem._DeltaTime() * _Speed);
                }
                else if (_Timer <= 0)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, SuppliesManager._Instance._Vertex().y + 10.0f), TimeSystem._DeltaTime() * _Speed);
                    if (transform.position.y > SuppliesManager._Instance._Vertex().y + 5.0f)
                        SuppliesManager._Instance.Recovery(_Queue, gameObject);
                    _Timer = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!GameManager._Instance._InGame) return;
            if (collision.tag == "Player")
            {
                Player._Instance.Supplies(tag, _Number, _SpriteRenderer.sprite);
                SuppliesManager._Instance.Recovery(_Queue, gameObject);
            }
        }
    }
}
