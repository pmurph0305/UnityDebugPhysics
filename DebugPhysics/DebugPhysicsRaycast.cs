using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycasts, RaycastAll, and RaycastNonAlloc

public static partial class DebugPhysics
{
  public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
    if (val > 0)
    {
      Vector3 start = ray.origin;
      for (int i = 0; i < val; i++)
      {
        DebugDraw.DrawRaycast(start, results[i].point, HitColor, DrawLineTime, DepthTest);
        if (DrawHitPoints)
        {
          DebugDraw.DrawPoint(results[i].point, HitColor, DrawLineTime, DepthTest);
        }
        if (DrawHitNormals)
        {
          DebugDraw.DrawLine(results[i].point, results[i].point + results[i].normal, HitNormalColor, DrawLineTime, DepthTest);
        }
        start = results[i].point;
      }
      if (Vector3.Distance(ray.origin, start) < maxDistance)
      {
        maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
        maxDistance = maxDistance - Vector3.Distance(ray.origin, start); // subtract already drawn distance.
        DebugDraw.DrawRaycast(start, start + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }


  public static bool Raycast(Ray ray, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, hitInfo.point, HitColor, DrawLineTime, DepthTest);
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
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static RaycastHit[] RaycastAll(Ray ray, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      Vector3 start = ray.origin;
      foreach (RaycastHit hit in hits)
      {
        DebugDraw.DrawRaycast(start, hit.point, HitColor, DrawLineTime, DepthTest);
        start = hit.point;
      }
      if (Vector3.Distance(ray.origin, start) < maxDistance)
      {
        maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
        maxDistance = maxDistance - Vector3.Distance(ray.origin, start); // subtract already drawn distance.
        DebugDraw.DrawRaycast(start, start + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * MaxDrawLength, NoHitColor, DrawLineTime, DepthTest);
    }
    return hits;
  }





  // Origin + direction casts just do use the ray method as it's easier to just maintain one method.
  public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return Raycast(new Ray(origin, direction), maxDistance, layerMask, queryTriggerInteraction);
  }
  public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return Raycast(new Ray(origin, direction), out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
  }
  public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return RaycastAll(new Ray(origin, direction), maxDistance, layerMask, queryTriggerInteraction);
  }
  public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return RaycastNonAlloc(new Ray(origin, direction), results, maxDistance, layerMask, queryTriggerInteraction);
  }
}
