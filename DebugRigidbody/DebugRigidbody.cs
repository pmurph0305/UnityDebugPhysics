using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugRigidbody
{
  public static bool DepthTest = false;
  public static float DrawVectorArrowScale = 0.1f;
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

  public static void DrawForce(Rigidbody rigidbody, Vector3 force, ForceMode mode)
  {
    if (ScaleForceUsingMass)
    {

      DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + force.normalized * force.magnitude / rigidbody.mass, GetForceModeColor(mode), DrawVectorArrowScale, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + force, GetForceModeColor(mode), DrawVectorArrowScale, DrawLineTime, DepthTest);
    }
  }

}
