using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Capsule casts
public static partial class DebugPhysics
{
  public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction,
    float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction))
    {
      // DebugDraw.DrawCapsule(point1, point2, radius, HitColor, DrawLineTime, DepthTest);
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, HitColor, DrawLineTime, DepthTest);
      // DebugDraw.DrawCapsule(point1 + direction * maxDistance, point2 + direction * maxDistance, radius, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      // DebugDraw.DrawCapsule(point1, point2, radius, NoHitColor, DrawLineTime, DepthTest);
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      // DebugDraw.DrawCapsule(point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
      DebugDraw.DrawCapsuleCast(point1, point2, point1 + direction * maxDistance, point2 + direction * maxDistance, radius, NoHitColor, DrawLineTime, DepthTest);
    }

    return false;
  }

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

    }
    return false;
  }
}
