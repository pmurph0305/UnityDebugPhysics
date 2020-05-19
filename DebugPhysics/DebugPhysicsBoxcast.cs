using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Box cast methods

public static partial class DebugPhysics
{
  // Quaternion needs to be a compile time constant to be a default parameter.
  // Although unity's dogs specify Quaternion orientation = Quaternion.identity, that's not possible, so we have to overload all the other methods..

  /// <summary>
  /// Draw a single boxcast using hit data
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="hits">Git data from a boxcast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  public static void DrawBoxCastHit(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit hit, Quaternion orientation, float maxDistance)
  {
    if (hit.normal == -direction && hit.distance == 0.0f && hit.point == Vector3.zero)
    {
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, 0.0f, HitColor, DrawLineTime, DepthTest, true);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(center, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine(center, center + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, HitColor, DrawLineTime, DepthTest, hit, true);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(hit.point, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine(hit.point, hit.point + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
  }

  /// <summary>
  /// Draws a boxcast using an array of hits
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="hits">Array of hits from a boxcast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  private static void DrawBoxCastHits(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] hits, Quaternion orientation, float maxDistance)
  {
    //draw last box first
    float maxHitDistance = -Mathf.Infinity;
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      float endDistance = Vector3.Dot(hits[i].point - center, direction);
      if (endDistance > maxHitDistance)
      {
        maxHitDistance = endDistance;
      }
    }
    maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
    if (maxDistance + halfExtents.z > maxHitDistance)
    {
      DebugDraw.DrawBoxCast((center + direction * maxHitDistance), halfExtents, direction, orientation, maxDistance - maxHitDistance, NoHitColor, DrawLineTime, DepthTest, false);
    }
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      DrawBoxCastHit(center, halfExtents, direction, hits[i], orientation, maxDistance);
    }
  }

  /// <summary>
  /// Casts a box along the direction and store the results in the buffer
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="results">Buffer to store hits in</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the results buffer</returns>
  public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layermask, queryTriggerInteraction);
    if (val > 0)
    {
      DrawBoxCastHits(center, halfExtents, direction, results, orientation, maxDistance);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  /// <summary>
  /// Casts a box through the scene and returns an array of all the hits.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>RaycastHit array containing information on all colliders that were hit</returns>
  public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
     QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layermask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      DrawBoxCastHits(center, halfExtents, direction, hits, orientation, maxDistance);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return hits;
  }

  /// <summary>
  /// Casts a box in a direction.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True when the boxcast intersects any collider, otherwise false.</returns>
  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask))
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  /// <summary>
  /// Casts a box in a direction and returns detailed information on what was hit.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="maxDistance">Max length of the boxcast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True when the boxcast intersects any collider, otherwise false.</returns>
  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction))
    {
      DrawBoxCastHit(center, halfExtents, direction, hitInfo, orientation, maxDistance);
      return true;
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return false;
  }

  // Covers the rest of the cases for orientation not being passed.

  /// <summary>
  /// Casts a box in a direction.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <returns>True when the boxcast intersects any collider, otherwise false.</returns>
  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
  {
    return BoxCast(center, halfExtents, direction, Quaternion.identity);
  }

  /// <summary>
  /// Casts a box in a direction and returns detailed information on what was hit.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each direction</param>
  /// <param name="direction">Direction of the boxcast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <returns>True when the boxcast intersects any collider, otherwise false.</returns>
  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
  {
    return BoxCast(center, halfExtents, direction, out hitInfo, Quaternion.identity);
  }

}
