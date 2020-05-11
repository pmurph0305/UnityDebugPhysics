using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugPhysics
{
  // Physics.Raycast has 4 overall methods, some have default parameters that are used if not supplied.
  // Replacing Physics.Raycast with DebugPhysics.Raycast will work in 4 methods

  // public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
  // public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
  // public static bool Raycast(Ray ray, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
  // public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);



  public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(origin, origin + direction * maxDistance, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(origin, origin + direction * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }

  public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawRaycast(origin, hitInfo.point, HitColor, DrawLineTime);
      DebugDraw.DrawPointSmall(hitInfo.point, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(origin, origin + direction * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }

  public static bool Raycast(Ray ray, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }

  public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction))
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, HitColor, DrawLineTime);
      DebugDraw.DrawPointSmall(hitInfo.point, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      maxDistance = maxDistance == Mathf.Infinity ? 10000f : maxDistance;
      DebugDraw.DrawRaycast(ray.origin, ray.origin + ray.direction * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }
}
