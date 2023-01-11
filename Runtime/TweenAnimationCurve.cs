using System;

namespace SFramework.ECS.Runtime.Tween
{
    [Serializable]
    public enum TweenAnimationCurve
    {
        EaseInOut,
        EaseOut,
        EaseIn,
        Berp,
        Bounce,
        Lerp,
        Clerp
    }
}