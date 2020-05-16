using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check box, check capsule, check sphere.
public static partial class DebugPhysics
{

  public static bool CheckBox(Vector3 center, Vector3 halfExtents,
    Quaternion orientation, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction))
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawBox(center, halfExtents, orientation, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static bool CheckBox(Vector3 center, Vector3 halfExtents)
  {
    return CheckBox(center, halfExtents, Quaternion.identity);
  }


  public static bool CheckSphere(Vector3 position, float radius, int layerMask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction))
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawSphere(position, radius, Vector3.up, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }

  public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask = Physics.DefaultRaycastLayers,
    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
  {
    if (Physics.CheckCapsule(start, end, radius, layermask, queryTriggerInteraction))
    {
      DebugDraw.DrawCapsule(start, end, radius, HitColor, DrawLineTime, DepthTest);
      return true;
    }
    else
    {
      DebugDraw.DrawCapsule(start, end, radius, NoHitColor, DrawLineTime, DepthTest);
      return false;
    }
  }
}
