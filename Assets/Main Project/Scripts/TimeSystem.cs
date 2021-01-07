using UnityEngine;

namespace CTJ
{
    public class TimeSystem
    {
        public static readonly float _FixedTimeStep = 0.02f;
        public static float _Time() { return Time.time; }
        public static void TimeScale(float _time_scale)
        {
            Time.timeScale = _time_scale;
            if (_time_scale <= 0.0f) { Time.fixedDeltaTime = 1.0f; return; }
            Time.fixedDeltaTime = Time.timeScale * _FixedTimeStep;
        }
        public static float _DeltaTime() { return Time.deltaTime; }
        public static float _FixedDeltaTime() { return Time.fixedDeltaTime; }
    }
}
