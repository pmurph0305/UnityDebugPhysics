using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Overlap box, sphere, capsule.
public static partial class DebugPhysics
{
  public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
    if (colliders.Length > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(colliders, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawBox(center, halfExtents, orientation, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, NoHitColor, DrawLineTime, DepthTest);
    }
    return colliders;
  }


  public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
  {
    return OverlapBox(center, halfExtents, Quaternion.identity);
  }

  public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
    if (val > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(results, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawBox(center, halfExtents, orientation, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
  {
    return OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity);
  }


  public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);
    if (colliders.Length > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(colliders, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawCapsule(point0, point1, radius, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawCapsule(point0, point1, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    return colliders;
  }

  public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, queryTriggerInteraction);
    if (val > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(results, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawCapsule(point0, point1, radius, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawCapsule(point0, point1, radius, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

  public static Collider[] OverlapSphere(Vector3 position, float radius,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);
    if (colliders.Length > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(colliders, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawSphere(position, radius, Vector3.up, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, NoHitColor, DrawLineTime, DepthTest);
    }
    return colliders;
  }

  public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    int val = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask, queryTriggerInteraction);
    if (val > 0)
    {
      if (DrawOverlapColliders)
      {
        DebugDraw.DrawColliders(results, HitColor, DrawLineTime, DepthTest);
      }
      DebugDraw.DrawSphere(position, radius, Vector3.up, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

}
