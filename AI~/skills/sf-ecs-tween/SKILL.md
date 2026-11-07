---
name: sf-ecs-tween
description: Use when implementing SFramework ECS MoveTo, RotateTo, ColorTo, tween loops, or SFMathFX curve interpolation.
---

# sf-ecs-tween

Read [the package guide](references/package-readme.md) and [installed source](SOURCE.md).

Use SF ECS Odin's embedded SFramework.ECS.Runtime contracts and concrete tween structs; external Leopotam ECS and ECS DI references are not required. This package does not provide automatic tween systems, scheduling, generated Unity providers, or an AddTween convenience API.

Implement progress, duration validation, loop handling, component removal and final-value application in application systems. Decide whether progress is clamped; helpers have no shared duration/clamping policy.

Choose a curve with the intended endpoints. Bounce at progress 1 returns the start value; Clerp handles scalar degree wraparound per component and is not quaternion interpolation.

SmoothStep with equal bounds returns zero below the bound and one at or above it. Unknown curves return zero for each interpolated component. Read the move, rotation and color component fields when implementing a runner.

Verify endpoints, zero/negative durations, overshoot, looping and the consuming world's phase order. Use package tests for interpolation changes.
