using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycasts, RaycastAll, and RaycastNonAlloc

public static partial class DebugPhysics
{
  public static void DrawRaycastHit(Ray ray, RaycastHit hit)
  {
    DebugDraw.DrawRaycast(ray.origin, hit.point, HitColor, DrawLineTime, DepthTest);
    if (DrawHitPoints)
    {
      DebugDraw.DrawPoint(hit.point, HitColor, DrawLineTime, DepthTest);
    }
    if (DrawHitNormals)
    {
      DebugDraw.DrawLine(hit.point, hit.point + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
    }
  }

  public static void DrawRaycastHits(Ray ray, RaycastHit[] hits, float maxDistance)
  {
    float maxHitDistance = -Mathf.Infinity;
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      float distance = Vector3.Distance(ray.origin, hits[i].point);
      if (distance > maxHitDistance)
      {
        maxHitDistance = distance;
      }
    }
    maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
    if (maxDistance > maxHitDistance)
    {
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      DrawRaycastHit(ray, hits[i]);
    }
  }


  public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
    if (val > 0)
    {
      DrawRaycastHits(ray, results, maxDistance);
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
      DrawRaycastHit(ray, hitInfo);
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
      DrawRaycastHits(ray, hits, maxDistance);
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
