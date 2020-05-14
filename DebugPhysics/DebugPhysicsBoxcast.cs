using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Box cast methods

public static partial class DebugPhysics
{
  // Quaternion needs to be a compile time constant to be a default parameter.
  // Although unity's dogs specify Quaternion orientation = Quaternion.identity, that's not possible, so we have to overload all the other methods..

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


  public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layermask, queryTriggerInteraction);
    if (val > 0)
    {
      float maxHitDistance = -Mathf.Infinity;
      for (int i = 0; i < val; i++)
      {
        DrawBoxCastHit(center, halfExtents, direction, results[i], orientation, maxDistance);
        // distance to end
        float endDistance = Vector3.Dot(results[i].point - center, direction);
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
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
     QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layermask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      float maxHitDistance = -Mathf.Infinity;
      for (int i = 0; i < hits.Length; i++)
      {
        DrawBoxCastHit(center, halfExtents, direction, hits[i], orientation, maxDistance);
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
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return hits;
  }

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

  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, HitColor, DrawLineTime, DepthTest, hitInfo);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(hitInfo.point, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
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

  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
  {
    return BoxCast(center, halfExtents, direction, Quaternion.identity);
  }

  public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
  {
    return BoxCast(center, halfExtents, direction, out hitInfo, Quaternion.identity);
  }

}
