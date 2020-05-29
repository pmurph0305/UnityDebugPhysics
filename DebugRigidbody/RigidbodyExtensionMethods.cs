using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensionMethods
{
  /// <summary>
  /// Applies a force to a rigidbody that simulates explosion effects
  /// </summary>
  /// <param name="explosionForce">The force of the explosion (this may be modified by distance)</param>
  /// <param name="explosionPosition">Center of the sphere within which the explosion has an effect</param>
  /// <param name="explosionRadius">Radius of the sphere within which the explosion effects</param>
  /// <param name="upwardsModifier">Adjustment to position of the explosion to make it seem like it lifts things</param>
  /// <param name="mode">The type of force to apply</param>
  public static void DebugAddExplosionForce(this Rigidbody rigidbody, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawExplosionForce(rigidbody, explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
    rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);

  }

  /// <summary>
  /// Adds a force to the rigidbody
  /// </summary>
  /// <param name="force">Force vector in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddForce(this Rigidbody rigidbody, Vector3 force, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, force, mode);
    rigidbody.AddForce(force, mode);
  }

  /// <summary>
  /// Adds a force to the rigidbody
  /// </summary>
  /// <param name="x">Size of force along world x-axis</param>
  /// <param name="y">Size of force along world y-axis</param>
  /// <param name="z">Size of force along world z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddForce(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, new Vector3(x, y, z), mode);
    rigidbody.AddForce(x, y, z, mode);
  }

  /// <summary>
  /// Applies force at position.static As a result this will apply a torque and force on the object.
  /// </summary>
  /// <param name="force">Force vector in world coodrinates</param>
  /// <param name="position">Position to apply force at in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddForceAtPosition(this Rigidbody rigidbody, Vector3 force, Vector3 position, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForceAtPostion(rigidbody, force, position, mode);
    rigidbody.AddForceAtPosition(force, position, mode);
  }

  /// <summary>
  /// Adds a force to the rigidbody relative to its local coordinate system
  /// </summary>
  /// <param name="force">Force vector in local coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddRelativeForce(this Rigidbody rigidbody, Vector3 force, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, rigidbody.transform.TransformDirection(force), mode);
    rigidbody.AddRelativeForce(force, mode);
  }
  /// <summary>
  /// Adds a force to the rigidbody relative to its local coordinate system
  /// </summary>
  /// <param name="x">Size of force along local x-axis</param>
  /// <param name="y">Size of force along local y-axis</param>
  /// <param name="z">Size of force along local z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddRelativeForce(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, rigidbody.transform.TransformDirection(new Vector3(x, y, z)), mode);
    rigidbody.AddRelativeForce(x, y, z, mode);
  }

  /// <summary>
  /// Addsa  torque to the rigidbody relative to its local coordinate system
  /// </summary>
  /// <param name="torque">Torque vector in local coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddRelativeTorque(this Rigidbody rigidbody, Vector3 torque, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, rigidbody.transform.TransformDirection(torque), mode);
    rigidbody.AddRelativeTorque(torque, mode);
  }
  /// <summary>
  /// Addsa  torque to the rigidbody relative to its local coordinate system
  /// </summary>
  /// <param name="x">Size of torque along local x-axis</param>
  /// <param name="y">Size of torque along local y-axis</param>
  /// <param name="z">Size of torque along local z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddRelativeTorque(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, rigidbody.transform.TransformDirection(new Vector3(x, y, z)), mode);
    rigidbody.AddRelativeTorque(x, y, z, mode);
  }

  /// <summary>
  /// Adds a torque to the rigidbody
  /// </summary>
  /// <param name="torque">Torque vector in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddTorque(this Rigidbody rigidbody, Vector3 torque, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, torque, mode);
    rigidbody.AddTorque(torque, mode);
  }

  /// <summary>
  /// Adds a torque to the rigidbody
  /// </summary>
  /// <param name="x">Size of torque along world x-axis</param>
  /// <param name="y">Size of torque along world y-axis</param>
  /// <param name="z">Size of torque along world z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddTorque(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, new Vector3(x, y, z), mode);
    rigidbody.AddTorque(x, y, z, mode);
  }

  /// <summary>
  /// Returbns the closest point on the bounding box of the attached colliders.
  /// </summary>
  /// <param name="position">Position in world coordiates</param>
  /// <returns>Closest point on the bounding box of the rigidbody's attached colliders</returns>
  public static Vector3 DebugClosestPointOnBounds(this Rigidbody rigidbody, Vector3 position)
  {
    Vector3 point = rigidbody.ClosestPointOnBounds(position);
    DebugDraw.DrawPoint(position, DebugRigidbody.ForceModeForceColor, DebugRigidbody.DrawLineTime, DebugRigidbody.DepthTest);
    DebugDraw.DrawPoint(point, DebugRigidbody.ForceModeAccelerationColor, DebugRigidbody.DrawLineTime, DebugRigidbody.DepthTest);
    return point;
  }

  /// <summary>
  /// Gets the velocity of the rigidbody at a point in world space
  /// </summary>
  /// <param name="worldPoint">Point in word space coordinates</param>
  /// <returns>Velocity of the rigidbody at point</returns>
  public static Vector3 DebugGetPointVelocity(this Rigidbody rigidbody, Vector3 worldPoint)
  {
    Vector3 velocity = rigidbody.GetPointVelocity(worldPoint);
    DebugRigidbody.DrawForce(rigidbody, worldPoint, velocity, ForceMode.VelocityChange);
    return velocity;
  }

  /// <summary>
  /// Gets the velocity relative to the rigidbodys local coordinates at a point.
  /// </summary>
  /// <param name="relativePoint">Point in local coordinates</param>
  /// <returns>Velocity at a point in local coordinates</returns>
  public static Vector3 DebugGetRelativePointVelocity(this Rigidbody rigidbody, Vector3 relativePoint)
  {
    Vector3 velocity = rigidbody.GetRelativePointVelocity(relativePoint);
    DebugRigidbody.DrawForce(rigidbody, rigidbody.transform.TransformPoint(relativePoint), rigidbody.transform.TransformDirection(velocity), ForceMode.VelocityChange);
    return velocity;
  }

  /// <summary>
  /// Tests if a rigidbody would collider with anything travelling in direction for distance.
  /// </summary>
  /// <param name="direction">Direction in which to sweep the rigidbody</param>
  /// <param name="hitInfo">If true is returned, contains more information about where the hit occured</param>
  /// <param name="maxDistance">The length of the sweep</param>
  /// <param name="queryTriggerInteraction">Should this query hit triggers</param>
  /// <returns>True if the sweep intersects with any collider, otherwise false</returns>
  public static bool DebugSweepTest(this Rigidbody rigidbody, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (rigidbody.SweepTest(direction, out hitInfo, maxDistance, queryTriggerInteraction))
    {
      DebugRigidbody.DrawSweep(rigidbody, direction, maxDistance, hitInfo);
      return true;
    }
    if (DebugRigidbody.DrawSweepDirection)
    {
      maxDistance = maxDistance > DebugRigidbody.MaxDrawSweepDistance ? DebugRigidbody.MaxDrawSweepDistance : maxDistance;
      DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + direction * maxDistance, DebugRigidbody.SweepDirectioNoHitColor, DebugRigidbody.DrawVectorArrowScale, DebugRigidbody.DrawLineTime, DebugRigidbody.DepthTest);
    }
    return false;
  }

  /// <summary>
  /// Tests what a rigidbody would hit if it travels through the scene. Returns multiple hits.
  /// </summary>
  /// <param name="direction">Direction in which to sweep the rigidbody</param>
  /// <param name="maxDistance">The length of the sweep</param>
  /// <param name="queryTriggerInteraction">Should this query hit triggers</param>
  /// <returns>Array of RaycastHit data of the points the rigidbody would hit if it travelled in direction for distance</returns>
  public static RaycastHit[] DebugSweepTestAll(this Rigidbody rigidbody, Vector3 direction, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = rigidbody.SweepTestAll(direction, maxDistance, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      foreach (RaycastHit hit in hits)
      {
        DebugRigidbody.DrawSweep(rigidbody, direction, maxDistance, hit);
      }
    }
    else if (DebugRigidbody.DrawSweepDirection)
    {
      maxDistance = maxDistance > DebugRigidbody.MaxDrawSweepDistance ? DebugRigidbody.MaxDrawSweepDistance : maxDistance;
      DebugDraw.DrawVector(rigidbody.worldCenterOfMass, rigidbody.worldCenterOfMass + direction * maxDistance, DebugRigidbody.SweepDirectioNoHitColor, DebugRigidbody.DrawVectorArrowScale, DebugRigidbody.DrawLineTime, DebugRigidbody.DepthTest);
    }
    return hits;
  }

  /// <summary>
  /// Draws the velocity vector of the rigidbody
  /// </summary>
  public static void DebugVelocity(this Rigidbody rigidbody)
  {
    DebugRigidbody.DrawVelocity(rigidbody);
  }

}
