using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class BaitControl : MonoBehaviour
    {
        public Rigidbody2D _Rigidbody2D;
        public Queue<GameObject> _Queue = new Queue<GameObject>();
        public static bool _RecoveryAll;

        private void Update()
        {
            if (_RecoveryAll) BaitManager._Instance.Recovery(_Queue, gameObject);
        }
    }
}
