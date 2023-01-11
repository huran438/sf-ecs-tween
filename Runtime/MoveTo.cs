using UnityEngine;

namespace SFramework.ECS.Runtime.Tween
{
    public struct MoveTo : ISFComponent
    {
        public float Cooldown;
        public float Duration;
        public TweenLoopType LoopType;
        public TweenAnimationCurve AnimationCurve;
        public Vector3 StartValue;
        public Vector3 EndValue;
        public bool UnscaledTime;
        public float ElapsedTime;
    }
}