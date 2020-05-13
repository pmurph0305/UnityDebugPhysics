using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Line casts.

public static partial class DebugPhysics
{
  public static bool Linecast(Vector3 start, Vector3 end,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Linecast(start, end, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawRaycast(start, end, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawRaycast(start, end, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Linecast(start, end, out hitInfo, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawRaycast(start, hitInfo.point, HitColor, DrawLineTime, DepthTest);
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
      DebugDraw.DrawRaycast(start, end, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }
}
