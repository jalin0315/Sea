using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class NPC : MonoBehaviour
    {
        public Queue<GameObject> _Queue_GameObject;
        [SerializeField] private Transform _Transform;
        private Vector3 _Scale;
        private float _ScaleMagnification;
        private Color _Color;
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        private float _Speed;
        public static bool _Recycle;

        private void Awake()
        {
            _Scale = _Transform.localScale;
            gameObject.SetActive(false);
        }

        private int _status;
        private int _direction;
        private Vector2 _new_position;
        private Vector3 _new_scale;
        private void OnEnable()
        {
            _Color = Color.black;
            _status = Random.Range(0, 3);
            switch (_status)
            {
                case 0:
                    _ScaleMagnification = 1.0f;
                    _Color.a = 0.8f;
                    _SpriteRenderer.color = _Color;
                    _Speed = 0.6f;
                    break;
                case 1:
                    _ScaleMagnification = 0.8f;
                    _Color.a = 0.6f;
                    _SpriteRenderer.color = _Color; _Speed = 0.4f;
                    break;
                case 2:
                    _ScaleMagnification = 0.5f;
                    _Color.a = 0.4f;
                    _SpriteRenderer.color = _Color; _Speed = 0.2f;
                    break;
            }
            _direction = Random.Range(0, 2);
            switch (_direction)
            {
                // 從左側螢幕外生成
                case 0:
                    _new_scale = _Scale;
                    _Transform.localScale = _new_scale * _ScaleMagnification;
                    _new_position.x = CameraControl._Origin.x;
                    _new_position.y = Random.Range(CameraControl._Vertex.y, CameraControl._Origin.y);
                    _Transform.localPosition = _new_position;
                    _new_position.x = _SpriteRenderer.bounds.min.x;
                    _Transform.localPosition = _new_position;
                    break;
                // 從右側螢幕外生成
                case 1:
                    _new_scale = _Scale;
                    _new_scale.x = -_new_scale.x;
                    _Transform.localScale = _new_scale * _ScaleMagnification;
                    _new_position.x = CameraControl._Vertex.x;
                    _new_position.y = Random.Range(CameraControl._Vertex.y, CameraControl._Origin.y);
                    _Transform.localPosition = _new_position;
                    _new_position.x = _SpriteRenderer.bounds.max.x;
                    _Transform.localPosition = _new_position;
                    break;
            }
        }

        private void Update()
        {
            if (_Recycle) { EnemyManager._Instance.RecycleNPC(_Queue_GameObject, gameObject); return; }
            _Color.a -= TimeSystem._DeltaTime() * 0.05f;
            _SpriteRenderer.color = _Color;
            if (_Color.a <= 0.0f) { EnemyManager._Instance.RecycleNPC(_Queue_GameObject, gameObject); return; }
        }

        private void FixedUpdate()
        {
            switch (_direction)
            {
                case 0:
                    _Transform.Translate((Vector2.up + Vector2.right) * TimeSystem._FixedDeltaTime() * _Speed);
                    break;
                case 1:
                    _Transform.Translate((Vector2.up + Vector2.left) * TimeSystem._FixedDeltaTime() * _Speed);
                    break;
            }
        }
    }
}
