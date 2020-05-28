using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugRigidbody
{
  /// <summary>
  /// Use to set a minimum force vector length
  /// </summary>
  public static float MinForceVectorLength = 0.0f;
  public static bool DrawTorqueFromPosition = true;
  /// <summary>
  /// Draw the sphere of radius (or a point if radius is 0) at the explosion force's position?
  /// </summary>
  public static bool DrawExplosionForceSphere = true;
  /// <summary>
  /// Should lines drawn be obscured by objects in the scene?
  /// </summary>
  public static bool DepthTest = false;
  /// <summary>
  /// Scale of end arrows on vectors
  /// </summary>
  public static float DrawVectorArrowScale = 0.1f;
  /// <summary>
  /// Minimum scale to draw torque ring with
  /// </summary>
  public static float MinTorqueDrawScale = 1.0f;

  /// <summary>
  /// Duration to draw lines for if DrawWithFixedDeltaTime is false.
  /// </summary>
  public static float DrawLineTime = 0.02f;
  /// <summary>
  /// Should we always draw for Time.fixedDeltaTime?
  /// </summary>
  public static bool DrawWithFixedDeltaTime = true;
  /// <summary>
  /// Should forces drawn for ForceMode.Force and ForceMode.Impulse be scaled by the rigidbodies mass?
  /// </summary>
  public static bool ScaleForceUsingMass = true;
  /// <summary>
  /// Color to draw explosion force's sphere and line with
  /// </summary>
  public static Color DrawExplosionColor = Color.red;
  /// <summary>
  /// Color to draw velocity vector with when using DebugVelocity()
  /// </summary>
  public static Color DrawVelocityColor = Color.magenta;
  /// <summary>
  /// Color to draw with when using ForceMode.Force
  /// </summary>
  public static Color ForceModeForceColor = Color.green;
  /// <summary>
  /// Color to draw with when using ForceMode.Acceleration
  /// </summary>
  public static Color ForceModeAccelerationColor = Color.yellow;
  /// <summary>
  /// Color to draw with when using ForceMode.Impulse
  /// </summary>
  public static Color ForceModeImpulseColor = Color.blue;
  /// <summary>
  /// Color to draw with when using ForceMode.VelocityChange
  /// </summary>
  public static Color ForceModeVelocityChangeColor = Color.red;

  /// <summary>
  /// Gets the color to draw with force the force mode.
  /// </summary>
  /// <param name="forceMode">Force mode we are drawing for</param>
  /// <returns>Color to draw with for the given ForceMode</returns>
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

  /// <summary>
  /// Draws the velocity vector of the rigidbody.
  /// </summary>
  /// <param name="rigidbody">Rigidbody to draw velocity vector for.</param>
  public static void DrawVelocity(Rigidbody rigidbody)
  {
    DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + rigidbody.velocity, DrawVelocityColor, DrawVectorArrowScale, GetDrawTime(), DepthTest);
  }

  /// <summary>
  /// Draws a force vector for a given rigidbody and force.
  /// </summary>
  /// <param name="rigidbody">Rigidbody force is being applied to</param>
  /// <param name="origin">Origin point to draw force vector from</param>
  /// <param name="force">Force vector being applied to the rigidbody</param>
  /// <param name="mode">ForceMode of the force being applied</param>
  public static void DrawForce(Rigidbody rigidbody, Vector3 origin, Vector3 force, ForceMode mode)
  {
    if (ScaleForceUsingMass && (mode == ForceMode.Force || mode == ForceMode.Impulse)) // these modes use it's math so it makes sense to scale with mass if specified
    {
      Vector3 f = force.magnitude / rigidbody.mass > MinForceVectorLength ? force.normalized * force.magnitude / rigidbody.mass : force.normalized * MinForceVectorLength;
      DebugDraw.DrawVector(origin, origin + f, GetForceModeColor(mode), DrawVectorArrowScale, GetDrawTime(), DepthTest);
    }
    else
    {
      DebugDraw.DrawVector(origin, origin + force, GetForceModeColor(mode), DrawVectorArrowScale, GetDrawTime(), DepthTest);
    }
  }

  /// <summary>
  /// Gets the time to draw lines with
  /// </summary>
  /// <returns>Length of time to draw lines</returns>
  private static float GetDrawTime()
  {
    return DrawWithFixedDeltaTime ? Time.fixedDeltaTime : DrawLineTime;
  }


  /// <summary>
  /// Draws a torque applied to a rigidbody using a 3/4 circular arrow around the origin in the direction of the torque vector.
  /// </summary>
  /// <param name="rigidbody">Rigidbody torque is being added to</param>
  /// <param name="origin">Origin point of the drawing</param>
  /// <param name="torque">Torque vector</param>
  /// <param name="mode">Force mode of the torque</param>
  public static void DrawTorque(Rigidbody rigidbody, Vector3 origin, Vector3 torque, ForceMode mode)
  {
    if (torque == Vector3.zero) return;
    // Create a rotation to align with the torque vector
    Quaternion look = Quaternion.LookRotation(torque, Vector3.Cross(torque, Vector3.up));
    float scale = 1.0f;
    if (ScaleForceUsingMass && (mode == ForceMode.Impulse || mode == ForceMode.Force))
    {
      scale = torque.magnitude / rigidbody.mass;
    }
    else
    {
      scale = torque.magnitude;
    }
    scale = scale > MinTorqueDrawScale ? scale : MinTorqueDrawScale;
    DebugDraw.DrawAngleBetween(origin, look * Vector3.right * scale, look * Vector3.up * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.up * scale, look * Vector3.left * scale, GetForceModeColor(mode), false, DrawVectorArrowScale, DrawLineTime, DepthTest);
    DebugDraw.DrawAngleBetween(origin, look * Vector3.left * scale, look * Vector3.down * scale, GetForceModeColor(mode), true, DrawVectorArrowScale, DrawLineTime, DepthTest);
  }

  /// <summary>
  /// Draws an explosion force being added to a rigidbody
  /// </summary>
  /// <param name="rigidbody">Rigidbody explosion force is being applied to</param>
  /// <param name="explosionForce">Magnitude of explosion force</param>
  /// <param name="explosionPosition">Position of explosion force</param>
  /// <param name="explosionRadius">Radius of explosion force</param>
  /// <param name="upwardsModifier">Upwards modifier of explosion force</param>
  /// <param name="mode">Forcemode of explosion force</param>
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

  /// <summary>
  /// Draws a force at position, drawing torque if enabled
  /// </summary>
  /// <param name="rigidbody"></param>
  /// <param name="force"></param>
  /// <param name="position"></param>
  /// <param name="mode"></param>
  public static void DrawForceAtPostion(Rigidbody rigidbody, Vector3 force, Vector3 position, ForceMode mode)
  {
    DrawForce(rigidbody, position, force, mode);
    if (DrawTorqueFromPosition)
    {
      DrawTorque(rigidbody, rigidbody.worldCenterOfMass, Vector3.Cross(position - rigidbody.worldCenterOfMass, force), mode);
    }
  }

}
