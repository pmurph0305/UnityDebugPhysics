using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugRigidbody
{
  public static bool DepthTest = false;
  public static float DrawVectorArrowScale = 0.1f;
  public static float MinTorqueDrawScale = 1.0f;
  public static float DrawLineTime = 0.00001f;
  public static bool ScaleForceUsingMass = true;
  public static Color ForceModeForceColor = Color.green;
  public static Color ForceModeAccelerationColor = Color.yellow;
  public static Color ForceModeImpulseColor = Color.blue;
  public static Color ForceModeVelocityChangeColor = Color.red;

  public static Color GetForceModeColor(ForceMode forceMode)
  {
    switch (forceMode)
    {
      case ForceMode.Force:
        return ForceModeForceColor;
      case ForceMode.Acceleration:
        return ForceModeAccelerationColor;
      case ForceMode.Impulse:
        return ForceModeImpulseColor;
      case ForceMode.VelocityChange:
        return ForceModeVelocityChangeColor;
      default:
        return ForceModeForceColor;
    }
  }

  public static void DrawForce(Rigidbody rigidbody, Vector3 origin, Vector3 force, ForceMode mode)
  {
    if (ScaleForceUsingMass)
    {
      DebugDraw.DrawVector(origin, origin + force.normalized * force.magnitude / rigidbody.mass, GetForceModeColor(mode), DrawVectorArrowScale, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawVector(origin, origin + force, GetForceModeColor(mode), DrawVectorArrowScale, DrawLineTime, DepthTest);
    }
  }


  public static void DrawTorque(Rigidbody rigidbody, Vector3 origin, Vector3 torque, ForceMode mode)
  {
    if (torque == Vector3.zero) return;
    Vector3 c1 = Vector3.Cross(torque, Vector3.up);
    Quaternion look = Quaternion.LookRotation(torque, c1);
    float scale = 1.0f;
    if (ScaleForceUsingMass)
    {
      scale = torque.magnitude / rigidbody.mass;
      scale = scale > MinTorqueDrawScale ? scale : MinTorqueDrawScale;
    }
    DebugDraw.DrawPoint(origin, Color.red, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.right * scale, look * Vector3.up * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.up * scale, look * Vector3.left * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.left * scale, look * Vector3.down * scale, GetForceModeColor(mode), true, DrawVectorArrowScale, DrawLineTime, DepthTest);
  }
}
