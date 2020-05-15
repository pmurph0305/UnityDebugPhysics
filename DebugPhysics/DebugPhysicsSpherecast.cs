using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spherecast, spherecast all, spherecast non alloc.

public static partial class DebugPhysics
{

  private static void DrawSphereCastHit(Vector3 origin, float radius, Vector3 direction, float maxDistance, RaycastHit hit)
  {
    if (hit.normal == -direction && hit.distance == 0.0f && hit.point == Vector3.zero)
    {
      DebugDraw.DrawSphere(origin, radius, direction, HitColor, DrawLineTime, DepthTest);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(origin, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        DebugDraw.DrawLine(origin, origin + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
    else
    {
      DebugDraw.DrawSphereCast(origin, radius, direction, maxDistance, HitColor, DrawLineTime, DepthTest, hit);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(hit.point, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        Debug.DrawLine(hit.point, hit.point + hit.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
    }
  }

  private static void DrawSphereCastHits(Ray ray, float radius, float maxDistance, RaycastHit[] hits)
  {
    float maxHitDistance = -Mathf.Infinity;
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      float distance = Vector3.Distance(ray.origin, hits[i].point + hits[i].normal * radius);
      if (distance > maxHitDistance)
      {
        maxHitDistance = distance;
      }
    }
    maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
    if (maxDistance > maxHitDistance)
    {
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime);
    }
    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].transform == null) continue;
      DrawSphereCastHit(ray.origin, radius, ray.direction, maxDistance, hits[i]);
    }
  }

  public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      DrawSphereCastHits(ray, radius, maxDistance, hits);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, DepthTest);
    }
    return hits;
  }

  public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
    if (val > 0) // have hits
    {
      DrawSphereCastHits(ray, radius, maxDistance, results);
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  public static bool SphereCast(Ray ray, float radius, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      DrawSphereCastHit(ray.origin, radius, ray.direction, maxDistance, hitInfo);
      return true;
    }
    else
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, DepthTest, hitInfo);
      return false;
    }
  }

  // Suprisingly for Physics.SphereCast there is no origin + direction without a RaycastHit.
  public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCast(new Ray(origin, direction), radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
  }

  public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCastAll(new Ray(origin, direction), radius, maxDistance, layerMask, queryTriggerInteraction);
  }

  public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCastNonAlloc(new Ray(origin, direction), radius, results, maxDistance, layerMask, queryTriggerInteraction);
  }
}
