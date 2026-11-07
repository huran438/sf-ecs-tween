using NUnit.Framework;
using SFramework.ECS.Runtime.Tween;
using UnityEngine;

namespace SFramework.ECS.Tween.Tests
{
    public sealed class SFMathFXTests
    {
        private const float Tolerance = 0.00001f;

        [TestCase(TweenAnimationCurve.EaseInOut, 0.25f, 3.25f)]
        [TestCase(TweenAnimationCurve.EaseInOut, 0.75f, 8.75f)]
        [TestCase(TweenAnimationCurve.EaseOut, 0.5f, 7.656854f)]
        [TestCase(TweenAnimationCurve.EaseIn, 0.5f, 4.343146f)]
        [TestCase(TweenAnimationCurve.Lerp, 0.25f, 4f)]
        public void CurveDispatchProducesTheExpectedInteriorSample(TweenAnimationCurve curve, float time, float expected)
        {
            Assert.That(SFMathFXHelper.CurvedValue(curve, 2f, 10f, time), Is.EqualTo(expected).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValueECS(curve, 2f, 10f, time), Is.EqualTo(expected).Within(Tolerance));
        }

        [TestCase(TweenAnimationCurve.EaseInOut)]
        [TestCase(TweenAnimationCurve.EaseOut)]
        [TestCase(TweenAnimationCurve.EaseIn)]
        [TestCase(TweenAnimationCurve.Berp)]
        [TestCase(TweenAnimationCurve.Lerp)]
        public void TransitionCurvesPreserveBothEndpointsForDescendingRanges(TweenAnimationCurve curve)
        {
            Assert.That(SFMathFXHelper.CurvedValue(curve, 9f, -3f, 0f), Is.EqualTo(9f).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValue(curve, 9f, -3f, 1f), Is.EqualTo(-3f).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValueECS(curve, 9f, -3f, 0f), Is.EqualTo(9f).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValueECS(curve, 9f, -3f, 1f), Is.EqualTo(-3f).Within(Tolerance));
        }

        [TestCase(-0.25f, 0f)]
        [TestCase(1.25f, 12f)]
        public void LinearInterpolationExtrapolatesOutsideTheUnitInterval(float time, float expected)
        {
            Assert.That(SFMathFX.Lerp(2f, 10f, time), Is.EqualTo(expected).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValue(TweenAnimationCurve.Lerp, 2f, 10f, time), Is.EqualTo(expected).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Lerp, 2f, 10f, time), Is.EqualTo(expected).Within(Tolerance));
        }

        [Test]
        public void SpringInterpolationOvershootsInsideTheTransitionAndClampsItsTimeAtTheEndpoints()
        {
            Assert.That(SFMathFX.Berp(2f, 10f, -1f), Is.EqualTo(2f).Within(Tolerance));
            Assert.That(SFMathFX.Berp(2f, 10f, 2f), Is.EqualTo(10f).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValue(TweenAnimationCurve.Berp, 2f, 10f, 0.5f), Is.GreaterThan(10f));
            Assert.That(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Berp, 10f, 2f, 0.5f), Is.LessThan(2f));
        }

        [Test]
        public void BounceIsADecayingExcursionThatReturnsToTheStartValue()
        {
            Assert.That(SFMathFXHelper.CurvedValue(TweenAnimationCurve.Bounce, 3f, 9f, 0.5f), Is.InRange(5.9f, 6.1f));
            Assert.That(SFMathFXHelper.CurvedValue(TweenAnimationCurve.Bounce, 3f, 9f, 1f), Is.EqualTo(3f).Within(Tolerance));
            Assert.That(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Bounce, 3f, 9f, 0.5f), Is.InRange(5.9f, 6.1f));
            Assert.That(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Bounce, 3f, 9f, 1f), Is.EqualTo(3f).Within(Tolerance));
        }

        [TestCase(350f, 10f, 0.5f, 0f)]
        [TestCase(10f, 350f, 0.5f, 0f)]
        [TestCase(350f, 10f, 1f, 10f)]
        [TestCase(10f, 350f, 1f, 350f)]
        [TestCase(45f, 135f, 0.5f, 90f)]
        public void CircularInterpolationTakesTheShortArcAcrossTheAngleBoundary(float start, float end, float time, float expectedAngle)
        {
            AssertAngle(SFMathFX.CLerp(start, end, time), expectedAngle);
            AssertAngle(SFMathFXHelper.CurvedValue(TweenAnimationCurve.Clerp, start, end, time), expectedAngle);
            AssertAngle(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Clerp, start, end, time), expectedAngle);
        }

        [Test]
        public void VectorOverloadsInterpolateEveryChannelIncludingDescendingChannels()
        {
            var start2 = new Vector2(-4f, 10f);
            var end2 = new Vector2(12f, -6f);
            var start3 = new Vector3(-4f, 10f, 2f);
            var end3 = new Vector3(12f, -6f, 10f);

            AssertVector2(SFMathFX.Hermite(start2, end2, 0.25f), -1.5f, 7.5f);
            AssertVector2(SFMathFXHelper.CurvedValue(TweenAnimationCurve.EaseInOut, start2, end2, 0.25f), -1.5f, 7.5f);
            AssertVector2(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.EaseInOut, start2, end2, 0.25f), -1.5f, 7.5f);
            AssertVector3(SFMathFX.Hermite(start3, end3, 0.25f), -1.5f, 7.5f, 3.25f);
            AssertVector3(SFMathFXHelper.CurvedValue(TweenAnimationCurve.EaseInOut, start3, end3, 0.25f), -1.5f, 7.5f, 3.25f);
            AssertVector3(SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.EaseInOut, start3, end3, 0.25f), -1.5f, 7.5f, 3.25f);
        }

        [Test]
        public void ColorInterpolationIncludesAlphaAndDoesNotSwapChannels()
        {
            var start = new Color(0f, 0.4f, 0.8f, 0.2f);
            var end = new Color(1f, 0.8f, 0f, 1f);

            var result = SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.EaseInOut, start, end, 0.25f);

            Assert.That(result.r, Is.EqualTo(0.15625f).Within(Tolerance));
            Assert.That(result.g, Is.EqualTo(0.4625f).Within(Tolerance));
            Assert.That(result.b, Is.EqualTo(0.675f).Within(Tolerance));
            Assert.That(result.a, Is.EqualTo(0.325f).Within(Tolerance));
        }

        [Test]
        public void VectorAndColorLinearInterpolationCanExtrapolateBeyondTheirEndValues()
        {
            AssertVector2(SFMathFX.Lerp(new Vector2(2f, 10f), new Vector2(10f, 2f), 1.25f), 12f, 0f);
            AssertVector3(SFMathFX.Lerp(new Vector3(2f, 10f, -2f), new Vector3(10f, 2f, 2f), 1.25f), 12f, 0f, 3f);
            var color = SFMathFXHelper.CurvedValueECS(TweenAnimationCurve.Lerp, Color.clear, Color.white, 1.25f);
            Assert.That(color.r, Is.EqualTo(1.25f).Within(Tolerance));
            Assert.That(color.g, Is.EqualTo(1.25f).Within(Tolerance));
            Assert.That(color.b, Is.EqualTo(1.25f).Within(Tolerance));
            Assert.That(color.a, Is.EqualTo(1.25f).Within(Tolerance));
        }

        [TestCase(-100f, 0f)]
        [TestCase(2f, 0f)]
        [TestCase(3f, 0.15625f)]
        [TestCase(4f, 0.5f)]
        [TestCase(6f, 1f)]
        [TestCase(100f, 1f)]
        public void SmoothStepNormalizesAndClampsANonUnitInputRange(float input, float expected)
        {
            Assert.That(SFMathFX.SmoothStep(input, 2f, 6f), Is.EqualTo(expected).Within(Tolerance));
        }

        [TestCase(1f, 0f)]
        [TestCase(2f, 1f)]
        [TestCase(3f, 1f)]
        public void SmoothStepWithEqualBoundsReturnsAStepAtTheBound(float input, float expected)
        {
            Assert.That(SFMathFX.SmoothStep(input, 2f, 2f), Is.EqualTo(expected));
        }

        [Test]
        public void UnknownCurveReturnsZeroInBothScalarHelpers()
        {
            var curve = (TweenAnimationCurve)int.MaxValue;

            Assert.That(SFMathFXHelper.CurvedValue(curve, 2f, 10f, 0.5f), Is.Zero);
            Assert.That(SFMathFXHelper.CurvedValueECS(curve, 2f, 10f, 0.5f), Is.Zero);
        }

        [TestCase(0f, 2f)]
        [TestCase(4f, 4f)]
        [TestCase(10f, 6f)]
        public void SegmentProjectionClampsToItsEndpointsWhileLineProjectionRemainsUnbounded(float pointX, float segmentX)
        {
            var start = new Vector3(2f, 3f, 4f);
            var end = new Vector3(6f, 3f, 4f);
            var point = new Vector3(pointX, 9f, -2f);

            AssertVector3(SFMathFX.NearestPointStrict(start, end, point), segmentX, 3f, 4f);
            AssertVector3(SFMathFX.NearestPoint(start, end, point), pointX, 3f, 4f);
        }

        [Test]
        public void DegenerateSegmentProjectsToItsSingleEndpoint()
        {
            var endpoint = new Vector3(2f, 3f, 4f);
            AssertVector3(SFMathFX.NearestPointStrict(endpoint, endpoint, new Vector3(100f, -20f, 8f)), 2f, 3f, 4f);
        }

        private static void AssertAngle(float actual, float expected)
        {
            Assert.That(Mathf.Abs(Mathf.DeltaAngle(actual, expected)), Is.LessThan(Tolerance));
        }

        private static void AssertVector2(Vector2 actual, float x, float y)
        {
            Assert.That(actual.x, Is.EqualTo(x).Within(Tolerance));
            Assert.That(actual.y, Is.EqualTo(y).Within(Tolerance));
        }

        private static void AssertVector3(Vector3 actual, float x, float y, float z)
        {
            Assert.That(actual.x, Is.EqualTo(x).Within(Tolerance));
            Assert.That(actual.y, Is.EqualTo(y).Within(Tolerance));
            Assert.That(actual.z, Is.EqualTo(z).Within(Tolerance));
        }
    }
}
