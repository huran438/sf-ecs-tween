using UnityEngine;

namespace SFramework.ECS.Runtime.Tween
{
    public struct ColorTo : ISFComponent
    {
        public float Cooldown;
        public float Duration;
        public TweenLoopType LoopType;
        public TweenAnimationCurve AnimationCurve;
        public Color StartValue;
        public Color EndValue;
        public bool UnscaledTime;
        public float ElapsedTime;
    }
}