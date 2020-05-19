using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycasts, RaycastAll, and RaycastNonAlloc

public static partial class DebugPhysics
{

  /// <summary>
  /// Draws a single Raycast using RaycastHit data.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="hit">RaycastHit data from a raycast</param>
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


  /// <summary>
  /// Draws the raycast and hit information in the hits array.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="hits">Array of RaycastHit data from a raycast</param>
  /// <param name="maxDistance">Max length of the raycast</param>
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

  /// <summary>
  /// Casts a ray through the scene and stores the hits info the buffer.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="results">Buffer to store hits in</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the buffer</returns>
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


  /// <summary>
  /// Casts a ray through the scene.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if raycast intersects with a collider, false otherwise</returns>
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

  /// <summary>
  /// Casts a ray through the scene.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if raycast intersects with a collider, false otherwise</returns>
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

  /// <summary>
  /// Casts a ray through the scene and returns an array of all the hits.
  /// </summary>
  /// <param name="ray">Origin and direction of the raycast</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>RaycastHit array containing information on all colliders that were hit</returns>
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

  /// <summary>
  /// Casts a ray through the scene.
  /// </summary>
  /// <param name="origin">Starting point of the raycast</param>
  /// <param name="direction">Direction of the raycast</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if raycast intersects with a collider, false otherwise</returns>
  public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return Raycast(new Ray(origin, direction), maxDistance, layerMask, queryTriggerInteraction);
  }

  /// <summary>
  /// Casts a ray through the scene, stores information about a hit if something is hit.
  /// </summary>
  /// <param name="origin">Starting point of the raycast</param>
  /// <param name="direction">Direction of the raycast</param>
  /// <param name="hitInfo">If true is returned, hitInfo will contain more information about where the collider was hit</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>True if raycast intersects with a collider, false otherwise</returns>
  public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return Raycast(new Ray(origin, direction), out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
  }

  /// <summary>
  /// Casts a ray through the scene and returns an array of all the hits.
  /// </summary>
  /// <param name="origin">Starting point of the raycast</param>
  /// <param name="direction">Direction of the raycast</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>RaycastHit array containing information on all colliders that were hit</returns>
  public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return RaycastAll(new Ray(origin, direction), maxDistance, layerMask, queryTriggerInteraction);
  }

  /// <summary>
  /// Casts a ray through the scene and stores the hits info the buffer.
  /// </summary>
  /// <param name="origin">Starting point of the raycast</param>
  /// <param name="direction">Direction of the raycast</param>
  /// <param name="results">Buffer to store hits in</param>
  /// <param name="maxDistance">Max length of the raycast</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of hits stored in the buffer</returns>
  public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity,
    int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    return RaycastNonAlloc(new Ray(origin, direction), results, maxDistance, layerMask, queryTriggerInteraction);
  }
}
