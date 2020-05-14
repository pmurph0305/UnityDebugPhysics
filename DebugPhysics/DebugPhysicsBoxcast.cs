using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Box cast methods

public static partial class DebugPhysics
{
  // Quaternion needs to be a compile time constant to be a default parameter.
  // Although unity's dogs specify Quaternion orientation = Quaternion.identity, that's not possible, so we have to overload all the other methods..

  public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation,
    float maxDistance = Mathf.Infinity, int layermask = Physics.DefaultRaycastLayers,
     QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layermask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      for (int i = 0; i < hits.Length; i++)
      {
        DebugDraw.DrawBoxCast(center, halfExtents, direction, orientation, HitColor, DrawLineTime, DepthTest, hits[i], true);
        if (DrawHitPoints)
        {
          DebugDraw.DrawPoint(hits[i].point, HitColor, DrawLineTime, DepthTest);
        }
        if (DrawHitNormals)
        {
          DebugDraw.DrawLine(hits[i].point, hits[i].point + hits[i].normal, HitNormalColor, DrawLineTime, DepthTest);
        }
      }
      float angle = Vector3.Angle(direction, (hits[hits.Length - 1].point - center)) * Mathf.Deg2Rad;
      // distance to end
      float endDistance = (hits[hits.Length - 1].point - center).magnitude * Mathf.Cos(angle);
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      if (maxDistance > endDistance)
      {
        DebugDraw.DrawBoxCast((center + direction * endDistance) - direction * halfExtents.z, halfExtents, direction, orientation, maxDistance - endDistance, NoHitColor, DrawLineTime, DepthTest, false);
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
