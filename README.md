# UnityDebugPhysics

Import DebugPhysics folder into your unity project, replace Physics.MethodName calls with DebugPhysics.MethodName and the result will be visualized in play mode.

Usage Example:

Replace

`Physics.BoxCast(center, halfExtents, direction) { ... }`

with

`DebugPhysics.BoxCast(center, halfExtents, direction) { ... }`

Most static `Physics.MethodName` can be replaced with `DebugPhysics.MethodName` for visualization when those calls are made.

The only methods not supported with visualization are: Physics.GetIgnoreLayerCollsion,  Physics.IgnoreLayerCollision, and Physics.Simulate

Options for adjusting visualization are described in the documentation of the static variables in DebugPhysics.cs
