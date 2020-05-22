# DebugPhysics and DebugVector3 in Unity

## Debug Physics

Import DebugPhysics folder into your unity project, replace Physics.MethodName calls with DebugPhysics.MethodName and the result will be visualized in play mode.

Usage Example:

Replace

`Physics.BoxCast(center, halfExtents, direction) { ... }`

with

`DebugPhysics.BoxCast(center, halfExtents, direction) { ... }`

Most static `Physics.MethodName` can be replaced with `DebugPhysics.MethodName` for visualization when those calls are made.

The only methods not supported with visualization are: Physics.GetIgnoreLayerCollsion,  Physics.IgnoreLayerCollision, and Physics.Simulate

Options for adjusting visualization are described in the documentation of the static variables in DebugPhysics.cs

## Debug Vector3

Import DebugVector3 folder into your unity project, replace Vector3.MethodName calls with DebugVector3.MethodName and the results will be visualized in play more. 

Visualization occurs at DebugVector3.origin's value, which by default is the world origin, change this to the transform.position of your gameobject to more easily view the results.

Operators (+ - / \*) are also drawn if enabled, and one of the variables in the operator is a DebugVector3.

Options for adjusting visualization are described in the documentation of the static variables in DebugVector3.cs

Usage Example

Replace

`Vector3.Cross(Vector3.up, Vector3.right);` or `Vector3 vector = Vector3.up + Vector3.right;`

with

`DebugVector3.Cross(Vector3.up, Vector3.right);` or `DebugVector3 vector = DebugVector3.up + Vector3.right;`

