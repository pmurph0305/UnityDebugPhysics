using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugPhysics
{
  /// <summary>
  /// Calculates the point on the given collider that is closest to the given point.
  /// </summary>
  /// <param name="point">World location you want to find the closest point to</param>
  /// <param name="collider">Collider you want to find the closest point on</param>
  /// <param name="position">Position of the collider</param>
  /// <param name="rotation">Rotation of the collider</param>
  /// <returns>The closest point on the collider to the given point</returns>
  public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
  {
    Vector3 closestPoint = Physics.ClosestPoint(point, collider, position, rotation);
    DebugDraw.DrawPoint(closestPoint, HitColor, DrawLineTime, DepthTest);
    if (DrawClosestPointCollider)
    {
      DebugDraw.DrawCollider(collider, HitColor, DrawLineTime, DepthTest);
    }
    return closestPoint;
  }

  /// <summary>
  /// Makes the collision system ignore (or unignore) all collisions between collider1 and collider2
  /// </summary>
  /// <param name="collider1">Any collider</param>
  /// <param name="collider2">Another collider you want to have collider 1 ignore or unignore collisions with</param>
  /// <param name="ignore">Should collisions between the two colliders be ignored</param>
  public static void IgnoreCollision(Collider collider1, Collider collider2, bool ignore = true)
  {
    Physics.IgnoreCollision(collider1, collider2, ignore);
    if (ignore)
    {
      DebugDraw.DrawCollider(collider1, NoHitColor, IgnoreCollisionDrawTime, DepthTest);
      DebugDraw.DrawCollider(collider2, NoHitColor, IgnoreCollisionDrawTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawCollider(collider1, HitColor, IgnoreCollisionDrawTime, DepthTest);
      DebugDraw.DrawCollider(collider2, HitColor, IgnoreCollisionDrawTime, DepthTest);
    }
  }

  /// <summary>
  /// Computes the minimimal translation in direction for collider A to seperate the given colliders at the specified poses.
  /// </summary>
  /// <param name="colliderA">First collider</param>
  /// <param name="positionA">First collider's position</param>
  /// <param name="rotationA">First collider's rotation</param>
  /// <param name="colliderB">Second collider</param>
  /// <param name="positionB">Second collider's position</param>
  /// <param name="rotationB">Second collider's rotation</param>
  /// <param name="direction">Direction along which the translation is required to seperate</param>
  /// <param name="distance">Distance along direction required to seperate colliders</param>
  /// <returns>True if the given colliders overlap at the provided positions</returns>
  public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
  {
    bool canSeperate = Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction, out distance);
    if (canSeperate)
    {
      if (DrawPenetrationColliders)
      {
        DebugDraw.DrawColliderAtPositionAndRotation(colliderA, positionA, rotationA, HitColor, DrawLineTime, DepthTest);
        DebugDraw.DrawColliderAtPositionAndRotation(colliderB, positionB, rotationB, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawLine(positionA, positionA + direction * distance, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      if (DrawPenetrationColliders)
      {
        DebugDraw.DrawColliderAtPositionAndRotation(colliderA, positionA, rotationA, NoHitColor, DrawLineTime, DepthTest);
        DebugDraw.DrawColliderAtPositionAndRotation(colliderB, positionB, rotationB, NoHitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawLine(positionA, positionB, NoHitColor, DrawLineTime, DepthTest);
    }
    return canSeperate;
  }
}
