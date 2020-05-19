using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Line casts.

public static partial class DebugPhysics
{

  /// <summary>
  /// Checks if there is any collider intersecting the line between start and end
  /// </summary>
  /// <param name="start">Start point of the line</param>
  /// <param name="end">End point of the line</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if there is any collider intersecting between start and end</returns>
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

  /// <summary>
  /// Checks if there is any collider intersecting the line between start and end
  /// </summary>
  /// <param name="start">Start point of the line</param>
  /// <param name="end">End point of the line</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if there is any collider intersecting between start and end</returns>
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
