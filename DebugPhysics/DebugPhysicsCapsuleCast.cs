using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Capsule casts
public static partial class DebugPhysics
{
  /// <summary>
  /// Draws a single capsulecast using raycasthit data
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="hit">RaycastHit data from the capsulecast</param>
  private static void DrawCapsuleCastHit(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit hit)
  {
    // if the values are the ones for when a capsule starts overlapping something..
    if (hit.normal == -direction && hit.distance == 0.0f && hit.point == Vector3.zero)
    {
      DebugDraw.DrawCapsuleCast(point1, point2, point1, point2, radius, HitColor, DrawLineTime, DepthTest);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint((point1 + point2) / 2, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine((point1 + point2) / 2, (point1 + point2) / 2 + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      DebugDraw.DrawCapsuleCast(point1, point2, radius, direction, HitColor, DrawLineTime, DepthTest, hit);
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
  /// Draws a capsulecast using an array of hits
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="maxDistance">Max length of the capsulecast</param>
  /// <param name="hits">Array of hits from a capsulecast</param>
  private static void DrawCapsuleCastHits(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, RaycastHit[] hits)
  {
    float maxHitDistance = -Mathf.Infinity;
    for (int i = 0; i < hits.Length; i++)
    {
      // rough estimate of max distance.
      float endDistance = Vector3.Dot(hits[i].point - (point1 + point2 / 2), direction);
      if (endDistance > maxHitDistance)
      {
        maxHitDistance = endDistance;
      }
    }
    maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
    if (maxDistance + radius > maxHitDistance)
    {
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      DrawCapsuleCastHit(point1, point2, radius, direction, hits[i]);
    }
  }

  /// <summary>
  /// Casts a capsule along the direction and store the results in the buffer
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="results">Buffer to store hits in</param>
  /// <param name="maxDistance">Max length of the capsulecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the results buffer</returns>
  public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layermask, queryTriggerInteraction);
    if (val > 0)
    {
      DrawCapsuleCastHits(point1, point2, radius, direction, maxDistance, results);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  /// <summary>
  /// Casts a capsule through the scene and returns an array of all the hits.
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="maxDistance">Max length of the capsulecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>RaycastHit array containing information on all colliders that were hit</returns>
  public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layermask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      DrawCapsuleCastHits(point1, point2, radius, direction, maxDistance, hits);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    return hits;
  }

  /// <summary>
  /// Casts a capsule in a direction
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="maxDistance">Max length of the capsulecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True when the capsulecast intersects any collider, otherwise false.</returns>
  public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }

    return false;
  }

  /// <summary>
  /// Casts a capsule in a direction and returns detailed information on what was hit.
  /// </summary>
  /// <param name="point1">Center of the sphere at the start of the capsule</param>
  /// <param name="point2">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsulecast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="maxDistance">Max length of the capsulecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True when the capsulecast intersects any collider, otherwise false.</returns>
  public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawCapsuleCast(point1, point2, radius, direction, HitColor, DrawLineTime, DepthTest, hitInfo);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(hitInfo.point, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    return false;
  }
}
