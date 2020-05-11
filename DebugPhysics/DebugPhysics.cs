using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugPhysics
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

  public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask)
  {
    if (Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask))
    {
      DrawRaycast(origin, hitInfo.point, HitColor, DrawLineTime);
      DebugDraw.DrawPointSmall(hitInfo.point, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      DrawRaycast(origin, origin + direction.normalized * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }

  public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
  {
    if (Physics.Raycast(origin, direction, out hitInfo, maxDistance))
    {
      DrawRaycast(origin, hitInfo.point, HitColor, DrawLineTime);
      DebugDraw.DrawPointSmall(hitInfo.point, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      DrawRaycast(origin, origin + direction.normalized * maxDistance, NoHitColor, DrawLineTime);
      return false;
    }
  }

  public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask)
  {
    if (Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask))
    {
      Vector3 pos = hitInfo.point + hitInfo.normal * radius;
      DebugDraw.DrawSphere(pos, radius, HitColor, DrawLineTime);
      DebugDraw.DrawPoint(hitInfo.point, HitColor, DrawLineTime);
      return true;
    }
    else
    {
      DebugDraw.DrawSphere(origin + direction * maxDistance, radius, NoHitColor, DrawLineTime);
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

  private static void DrawRaycast(Vector3 start, Vector3 end, Color color, float duration)
  {
    Debug.DrawLine(start, end, color, duration);
  }
}
