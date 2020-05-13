using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spherecast, spherecast all, spherecast non alloc.

public static partial class DebugPhysics
{

  public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
    if (hits.Length > 0)
    {
      Vector3 origin = ray.origin;
      // draw first origin sphere
      bool drawOriginSphere = true;
      for (int i = 0; i < hits.Length; i++)
      {
        // but don't draw other origin spheres, just where they hit.
        DebugDraw.DrawSphereCast(origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, drawOriginSphere, DepthTest, hits[i]);
        if (DrawHitPoints)
        {
          DebugDraw.DrawPoint(hits[i].point, HitColor, DrawLineTime, DepthTest);
        }
        if (DrawHitNormals)
        {
          Debug.DrawLine(hits[i].point, hits[i].point + hits[i].normal, HitNormalColor, DrawLineTime, DepthTest);
        }
        origin = hits[i].point + hits[i].normal * radius;
        drawOriginSphere = false;
      }
      if (Vector3.Distance(ray.origin, origin) < maxDistance)
      {
        maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
        maxDistance = maxDistance - Vector3.Distance(ray.origin, origin); // subtract already drawn distance.
        DebugDraw.DrawSphereCast(origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, false, DepthTest);
      }
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance; // cap max distance to max draw length
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, true, DepthTest);
    }
    return hits;
  }

  public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
    if (val > 0) // have hits
    {
      Vector3 origin = ray.origin;
      bool drawOriginSphere = true;
      for (int i = 0; i < val; i++)
      {
        DebugDraw.DrawSphereCast(origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, drawOriginSphere, DepthTest, results[i]);
        if (DrawHitPoints)
        {
          DebugDraw.DrawPoint(results[i].point, HitColor, DrawLineTime, DepthTest);
        }
        if (DrawHitNormals)
        {
          Debug.DrawLine(results[i].point, results[i].point + results[i].normal, HitNormalColor, DrawLineTime, DepthTest);
        }
        origin = results[i].point + results[i].normal * radius;
        drawOriginSphere = false;
      }
      if (Vector3.Distance(ray.origin, origin) < maxDistance)
      {
        maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
        maxDistance = maxDistance - Vector3.Distance(ray.origin, origin); // subtract already drawn distance.
        DebugDraw.DrawSphereCast(origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, drawOriginSphere, DepthTest);
      }
    }
    else
    {
      maxDistance = maxDistance > MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, true, DepthTest);
    }
    return val;
  }

  public static bool SphereCast(Ray ray, float radius, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, true, DepthTest);
      return true;
    }
    else
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, true, DepthTest);
      return false;
    }
  }

  public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, HitColor, DrawLineTime, true, DepthTest, hitInfo);
      if (DrawHitPoints)
      {
        DebugDraw.DrawPoint(hitInfo.point, HitColor, DrawLineTime, DepthTest);
      }
      if (DrawHitNormals)
      {
        Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal, HitNormalColor, DrawLineTime, DepthTest);
      }
      return true;
    }
    else
    {
      maxDistance = maxDistance >= MaxDrawLength ? MaxDrawLength : maxDistance;
      DebugDraw.DrawSphereCast(ray.origin, radius, ray.direction, maxDistance, NoHitColor, DrawLineTime, true, DepthTest, hitInfo);
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
