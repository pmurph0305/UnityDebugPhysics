using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Overlap box, sphere, capsule.
public static partial class DebugPhysics
{

  /// <summary>
  /// Compute and returns an array of colliders touching or within the box
  /// </summary>
  /// <param name="center">Cetner of the box</param>
  /// <param name="halfExtents">Half of the size of the box in each dimension</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>An array with all colliders touching or within the box</returns>
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

  /// <summary>
  /// Compute and returns an array of colliders touching or within the box
  /// </summary>
  /// <param name="center">Cetner of the box</param>
  /// <param name="halfExtents">Half of the size of the box in each dimension</param>
  /// <returns>An array with all colliders touching or within the box</returns>
  public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
  {
    return OverlapBox(center, halfExtents, Quaternion.identity);
  }

  /// <summary>
  /// Compute and stores an array of colliders touching or within the box
  /// </summary>
  /// <param name="center">Cetner of the box</param>
  /// <param name="halfExtents">Half of the size of the box in each dimension</param>
  /// <param name="results">The buffer to store results in</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of colliders stored in the buffer</returns>
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

  /// <summary>
  /// Compute and stores an array of colliders touching or within the box
  /// </summary>
  /// <param name="center">Cetner of the box</param>
  /// <param name="halfExtents">Half of the size of the box in each dimension</param>
  /// <param name="results">The buffer to store results in</param>
  /// <returns>The amount of colliders stored in the buffer</returns>
  public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
  {
    return OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity);
  }

  /// <summary>
  ///  Computes and returns an array of colliders touching or within the capsule.
  /// </summary>
  /// <param name="point0">Center of the sphere at the start of the capsule</param>
  /// <param name="point1">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>An array with all colliders touching or within the capsule</returns>
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

  /// <summary>
  /// Computes and stores an array of colliders touching or within the capsule.
  /// </summary>
  /// <param name="point0">Center of the sphere at the start of the capsule</param>
  /// <param name="point1">Center of the sphere at the end of the capsule</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="results">Buffer to store results into</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of colliders stored in the buffer</returns>
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

  /// <summary>
  /// Computes and returns an array of colliders touching or within the sphere.
  /// </summary>
  /// <param name="position">Center of the sphere</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>An array with all colliders touching or within the sphere</returns>
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

  /// <summary>
  /// Computes and stores the amount of colliders touching or within the sphere.
  /// </summary>
  /// <param name="position">Center of the sphere</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="results">Buffer to store results into</param>
  /// <param name="layerMask">A layer mask used to selectively ignore colliders</param>
  /// <param name="queryTriggerInteraction">Specifies whether this query should hit triggers</param>
  /// <returns>The amount of colliders stored in the buffer</returns>
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
