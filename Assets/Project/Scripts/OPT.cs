using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class OPT : MonoBehaviour
    {
        public static WaitForEndOfFrame _WaitForEndOfFrame = new WaitForEndOfFrame();
        public static WaitForSeconds _WaitForSeconds(float _delay) { return new WaitForSeconds(_delay); }
    }
}
