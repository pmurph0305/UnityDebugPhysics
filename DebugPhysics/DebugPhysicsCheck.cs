using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check box, check capsule, check sphere.
public static partial class DebugPhysics
{

  /// <summary>
  /// Checks if any colliders overlap a box volume in world space
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if any colliders are touching or within the provided box</returns>
  public static bool CheckBox(Vector3 center, Vector3 halfExtents,
    Quaternion orientation, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction))
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  /// <summary>
  /// Checks if any colliders overlap a box volume in world space
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <returns>True if any colliders are touching or within the provided box</returns>
  public static bool CheckBox(Vector3 center, Vector3 halfExtents)
  {
    return CheckBox(center, halfExtents, Quaternion.identity);
  }


  /// <summary>
  /// Checks if any colliders overlap a sphere volume in world space
  /// </summary>
  /// <param name="position">Center of the sphere in world space</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if any colliders are touching or within the provided sphere</returns>
  public static bool CheckSphere(Vector3 position, float radius, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }



  /// <summary>
  /// Checks if any colliders overlap a capsule volume in world space
  /// </summary>
  /// <param name="start">Center of the sphere at the start of the capsule</param>
  /// <param name="end">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if any colliders are touching or within the provided capsule</returns>
  public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckCapsule(start, end, radius, layermask, queryTriggerInteraction))
    {
      DebugDraw.DrawCapsule(start, end, radius, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawCapsule(start, end, radius, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }
}
