using UnityEngine;

namespace SFramework.ECS.Runtime.Tween
{
    public static class SFMathFXHelper
    {
        #region [SFMathFX]

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static float CurvedValue(TweenAnimationCurve tweenAnimationCurve, float start, float end, float t)
        {
            switch (tweenAnimationCurve)
            {
                case TweenAnimationCurve.EaseInOut:
                    return SFMathFX.Hermite(start, end, t);
                case TweenAnimationCurve.EaseOut:
                    return SFMathFX.Sinerp(start, end, t);
                case TweenAnimationCurve.EaseIn:
                    return SFMathFX.Coserp(start, end, t);
                case TweenAnimationCurve.Berp:
                    return SFMathFX.Berp(start, end, t);
                case TweenAnimationCurve.Bounce:
                    return start + ((end - start) * SFMathFX.Bounce(t));
                case TweenAnimationCurve.Lerp:
                    return SFMathFX.Lerp(start, end, t);
                case TweenAnimationCurve.Clerp:
                    return SFMathFX.CLerp(start, end, t);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static Vector2 CurvedValue(TweenAnimationCurve tweenAnimationCurve, Vector2 start, Vector2 end, float t)
        {
            return new Vector2(CurvedValue(tweenAnimationCurve, start.x, end.x, t),
                CurvedValue(tweenAnimationCurve, start.y, end.y, t));
        }

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static Vector3 CurvedValue(TweenAnimationCurve tweenAnimationCurve, Vector3 start, Vector3 end, float t)
        {
            return new Vector3(CurvedValue(tweenAnimationCurve, start.x, end.x, t),
                CurvedValue(tweenAnimationCurve, start.y, end.y, t),
                CurvedValue(tweenAnimationCurve, start.z, end.z, t));
        }

        #endregion

        #region [MathfxECS]

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static float CurvedValueECS(TweenAnimationCurve tweenAnimationCurve, float start, float end, float t)
        {
            switch (tweenAnimationCurve)
            {
                case TweenAnimationCurve.EaseInOut:
                    return SFMathFX.Hermite(start, end, t);
                case TweenAnimationCurve.EaseOut:
                    return SFMathFX.Sinerp(start, end, t);
                case TweenAnimationCurve.EaseIn:
                    return SFMathFX.Coserp(start, end, t);
                case TweenAnimationCurve.Berp:
                    return SFMathFX.Berp(start, end, t);
                case TweenAnimationCurve.Bounce:
                    return start + ((end - start) * SFMathFX.Bounce(t));
                case TweenAnimationCurve.Lerp:
                    return SFMathFX.Lerp(start, end, t);
                case TweenAnimationCurve.Clerp:
                    return SFMathFX.CLerp(start, end, t);
                default:
                    return 0f;
            }
        }

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static Vector2 CurvedValueECS(TweenAnimationCurve tweenAnimationCurve, Vector2 start, Vector2 end,
            float t)
        {
            return new Vector2(CurvedValueECS(tweenAnimationCurve, start.x, end.x, t),
                CurvedValueECS(tweenAnimationCurve, start.y, end.y, t));
        }

        /// <summary>
        /// Return value based on curve from SFMathFX class.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="tweenAnimationCurve">Animation curve.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="t">T.</param>
        public static Vector3 CurvedValueECS(TweenAnimationCurve tweenAnimationCurve, Vector3 start, Vector3 end,
            float t)
        {
            return new Vector3(CurvedValueECS(tweenAnimationCurve, start.x, end.x, t),
                CurvedValueECS(tweenAnimationCurve, start.y, end.y, t),
                CurvedValueECS(tweenAnimationCurve, start.z, end.z, t));
        }

        public static Color CurvedValueECS(TweenAnimationCurve tweenAnimationCurve, Color start, Color end, float t)
        {
            return new Color
            (
                CurvedValueECS(tweenAnimationCurve, start.r, end.r, t),
                CurvedValueECS(tweenAnimationCurve, start.g, end.g, t),
                CurvedValueECS(tweenAnimationCurve, start.b, end.b, t),
                CurvedValueECS(tweenAnimationCurve, start.a, end.a, t)
            );
        }

        #endregion
    }
}