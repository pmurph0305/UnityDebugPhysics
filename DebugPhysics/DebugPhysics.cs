using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugPhysics
{
  public static Color HitColor = Color.green;
  public static Color NoHitColor = Color.red;


  private static float drawLineTime = 0.0001f;
  public static float DrawLineTime
  {
    get
    {
      return drawLineTime < 0 ? 0.0001f : drawLineTime;

    }
    set { drawLineTime = value; }
  }



  public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask)
  {
    if (Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask))
    {
      DebugDraw.DrawSphereCast(origin, radius, direction, maxDistance, HitColor, DrawLineTime, hitInfo);
      return true;
    }
    else
    {
      DebugDraw.DrawSphereCast(origin, radius, direction, maxDistance, NoHitColor, DrawLineTime, hitInfo);
      return false;
    }
  }

  public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
  {
    if (Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance))
    {
      DebugDraw.DrawSphereCast(origin, radius, direction, maxDistance, HitColor, DrawLineTime, hitInfo);
      return true;
    }
    else
    {
      DebugDraw.DrawSphereCast(origin, radius, direction, maxDistance, NoHitColor, DrawLineTime, hitInfo);
      return false;
    }
  }
}
