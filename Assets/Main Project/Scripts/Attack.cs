using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _Rigidbody2D;
        private Vector2 _variable_vector2;

        public void AttackEnemy()
        {
            if (_Rigidbody2D == null) { Logger.LogWarningFormat("{0} is Null.", nameof(_Rigidbody2D)); return; }
            _variable_vector2.x = Random.Range(-1.0f, 1.0f);
            _variable_vector2.y = Random.Range(-1.0f, 1.0f);
            _Rigidbody2D.AddForce(_variable_vector2 * TimeSystem._FixedDeltaTime() * 1000.0f, ForceMode2D.Impulse);
            _Rigidbody2D.AddTorque(TimeSystem._FixedDeltaTime() * 1000.0f, ForceMode2D.Impulse);
        }
    }
}
