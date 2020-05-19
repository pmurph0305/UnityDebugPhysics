using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spherecast, spherecast all, spherecast non alloc.

public static partial class DebugPhysics
{
  /// <summary>
  /// Draws a single spherecast using RaycastHit data.
  /// </summary>
  /// <param name="origin">Origin of the spherecast</param>
  /// <param name="radius">Radius of the spherecast</param>
  /// <param name="direction">Direction of the spherecast</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="hit">RaycastHit data from the spherecast</param>
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

  /// <summary>
  /// Draws all spherecasts in the hits array.
  /// </summary>
  /// <param name="ray">Origin and direction of the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="hits">Array of hits from a spherecast</param>
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

  /// <summary>
  /// Like Spherecast, but this function will return all hits the sphere sweep intersects.
  /// </summary>
  /// <param name="ray">Origin and direction of the ray used to spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>An array of all colliders hit and information in the sweep</returns>
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

  /// <summary>
  /// Cast a sphere along the direction and store the results in the buffer
  /// </summary>
  /// <param name="ray">Origin and direction of the ray used to spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="results">Buffer to save the spherecast results to</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the results buffer</returns>
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

  /// <summary>
  /// Casts a sphere along a ray and returns true if it hit something
  /// </summary>
  /// <param name="ray">Origin and direction of the ray used to spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if spherecast intersects any collider</returns>
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

  /// <summary>
  /// Casts a sphere along a ray and returns true if it hit something, sets RaycastHit data if something is hit.
  /// </summary>
  /// <param name="ray">Origin and direction of the ray used to spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if spherecast intersects any collider</returns>
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

  /// <summary>
  /// Casts a sphere along a ray and returns detailed information on what was hit.
  /// </summary>
  /// <param name="origin">Center of the sphere to start the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">Direction in which to spherecast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True when the spherecast intersects any collider, otherwise false.</returns>
  public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCast(new Ray(origin, direction), radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
  }

  /// <summary>
  ///  Like spherecast, but this function will return all hits the sphere sweep intersects.
  /// </summary>
  /// <param name="origin">Center of the sphere to start the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">Direction in which to spherecast</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>An array of all colliders hit and information in the sweep</returns>
  /// <returns></returns>
  public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCastAll(new Ray(origin, direction), radius, maxDistance, layerMask, queryTriggerInteraction);
  }

  /// <summary>
  /// Cast a sphere along the direction and store the results in the buffer
  /// </summary>
  /// <param name="origin">Center of the sphere to start the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">Direction in which to spherecast</param>
  /// <param name="results">Buffer to save the spherecast results to</param>
  /// <param name="maxDistance">Max length of the spherecast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the results buffer</returns>
  public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return SphereCastNonAlloc(new Ray(origin, direction), radius, results, maxDistance, layerMask, queryTriggerInteraction);
  }
}
