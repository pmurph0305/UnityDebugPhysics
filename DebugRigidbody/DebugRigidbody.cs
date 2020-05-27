using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugRigidbody
{

  public static bool DrawExplosionForceSphere = true;

  public static bool DepthTest = false;

  public static float DrawVectorArrowScale = 0.1f;
  public static float MinTorqueDrawScale = 1.0f;

  public static float DrawLineTime = 0.02f;
  public static bool DrawWithFixedDeltaTime = true;
  public static bool ScaleForceUsingMass = true;
  public static Color DrawExplosionColor = Color.red;
  public static Color DrawVelocityColor = Color.magenta;
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

  public static void DrawVelocity(Rigidbody rigidbody)
  {
    DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + rigidbody.velocity, DrawVelocityColor, DrawVectorArrowScale, GetDrawTime(), DepthTest);
  }

  public static void DrawForce(Rigidbody rigidbody, Vector3 origin, Vector3 force, ForceMode mode)
  {
    if (ScaleForceUsingMass && (mode == ForceMode.Force || mode == ForceMode.Impulse)) // these modes use it's math so it makes sense to scale with mass if specified
    {
      DebugDraw.DrawVector(origin, origin + force.normalized * force.magnitude / rigidbody.mass, GetForceModeColor(mode), DrawVectorArrowScale, GetDrawTime(), DepthTest);
    }
    else
    {
      DebugDraw.DrawVector(origin, origin + force, GetForceModeColor(mode), DrawVectorArrowScale, GetDrawTime(), DepthTest);
    }
  }

  private static float GetDrawTime()
  {
    return DrawWithFixedDeltaTime ? Time.fixedDeltaTime : DrawLineTime;
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
    }
    else
    {
      scale = torque.magnitude;
    }
    scale = scale > MinTorqueDrawScale ? scale : MinTorqueDrawScale;
    // DebugDraw.DrawPoint(origin, Color.red, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.right * scale, look * Vector3.up * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.up * scale, look * Vector3.left * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.left * scale, look * Vector3.down * scale, GetForceModeColor(mode), true, DrawVectorArrowScale, DrawLineTime, DepthTest);
  }

  public static void DrawExplosionForce(Rigidbody rigidbody, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier, ForceMode mode)
  {
    Color color = GetForceModeColor(mode);
    Vector3 pos = explosionPosition;
    pos.y -= upwardsModifier;
    if (DrawExplosionForceSphere)
    {
      if (explosionRadius > 0)
      {
        DebugDraw.DrawSphere(pos, explosionRadius, Vector3.up, DrawExplosionColor, DrawLineTime, DepthTest);
      }
      else
      {
        DebugDraw.DrawPoint(pos, DrawExplosionColor, DrawLineTime, DepthTest);
      }
    }
    Debug.DrawLine(pos, rigidbody.worldCenterOfMass, DrawExplosionColor, DrawLineTime, DepthTest);
    DrawForce(rigidbody, rigidbody.worldCenterOfMass, (rigidbody.worldCenterOfMass - pos).normalized * explosionForce, mode);
  }

}
