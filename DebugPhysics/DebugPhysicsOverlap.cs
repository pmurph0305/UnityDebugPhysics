using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Overlap box, sphere, capsule.
public static partial class DebugPhysics
{

  public static void DrawOverlappedColliders(Collider[] colliders)
  {
    foreach (Collider col in colliders)
    {
      if (col == null) continue;
      BoxCollider box = col as BoxCollider;
      if (box != null) { DrawBoxCollider(box); continue; }
      CapsuleCollider cap = col as CapsuleCollider;
      if (cap != null) { DrawCapsuleCollider(cap); continue; }
      SphereCollider sphere = col as SphereCollider;
      if (sphere != null) { DrawSphereCollider(sphere); continue; }
      MeshCollider mc = col as MeshCollider;
      if (mc != null) { DrawMeshCollider(mc); }
    }
  }

  public static void DrawBoxCollider(BoxCollider col)
  {
    Vector3 size = col.size;
    size.x *= col.transform.lossyScale.x;
    size.y *= col.transform.lossyScale.y;
    size.z *= col.transform.lossyScale.z;
    DebugDraw.DrawBox(col.transform.TransformPoint(col.center), size / 2, col.transform.rotation, HitColor, DrawLineTime, DepthTest);
  }
  public static void DrawSphereCollider(SphereCollider col)
  {
    float r = col.radius;
    Vector3 scale = col.transform.lossyScale;
    if (scale.x > scale.y && scale.x > scale.z)
    {
      r *= scale.x;
    }
    else if (scale.y > scale.x && scale.y > scale.z)
    {
      r *= scale.y;
    }
    else
    {
      r *= scale.z;
    }
    DebugDraw.DrawSphere(col.transform.TransformPoint(col.center), r, col.transform.forward, HitColor, DrawLineTime, DepthTest);
  }
  public static void DrawCapsuleCollider(CapsuleCollider col)
  {
    Vector3 point1, point2 = point1 = col.center;
    float r = col.radius;
    Vector3 scale = col.transform.lossyScale;
    float heightToAdd = col.height / 2 - col.radius;
    if (scale.x > scale.y && scale.x > scale.z)
    {
      r *= scale.x;
    }
    else if (scale.y > scale.x && scale.y > scale.z)
    {
      r *= scale.y;
    }
    else
    {
      r *= scale.z;
    }
    if (col.direction == 0)
    {
      point1.x += heightToAdd;
      point2.x -= heightToAdd;
    }
    else if (col.direction == 1)
    {
      point1.y += heightToAdd;
      point2.y -= heightToAdd;
    }
    else
    {
      point1.z += heightToAdd;
      point2.z -= heightToAdd;
    }
    point1 = col.transform.TransformVector(point1);
    point2 = col.transform.TransformVector(point2);
    DebugDraw.DrawCapsule(point1 + col.transform.position, point2 + col.transform.position, r, HitColor, DrawLineTime, DepthTest);
  }

  public static void DrawMeshCollider(MeshCollider col)
  {

  }
  public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation,
    int layerMask = Physics.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
    if (colliders.Length > 0)
    {
      DrawOverlappedColliders(colliders);
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
      DrawOverlappedColliders(results);
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
      DrawOverlappedColliders(colliders);
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
      DrawOverlappedColliders(results);
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
      DrawOverlappedColliders(colliders);
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
      DrawOverlappedColliders(results);
      DebugDraw.DrawSphere(position, radius, Vector3.up, HitColor, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, NoHitColor, DrawLineTime, DepthTest);
    }
    return val;
  }

}
