using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDraw
{
  public static float DefaultVectorArrowScale = 0.1f;

  private static float defaultPointScale = 0.05f;
  public static float DefaultPointScale
  {
    get { return defaultPointScale; }
    set
    {
      SmallPointScale = value / 2;
      LargePointScale = value * 2;
      defaultPointScale = value;
    }
  }
  private static float SmallPointScale = 0.025f;
  private static float LargePointScale = 0.1f;


  private static int sphereQuarterSegments = 4;
  public static int SphereQuarterSegments
  {
    get
    {
      return sphereQuarterSegments < 2 ? 2 : sphereQuarterSegments;
    }
    set { sphereQuarterSegments = value; }
  }

  public static void DrawPoint(Vector3 point, Color color, float time)
  {
    Debug.DrawLine(point - Vector3.up * DefaultPointScale, point + Vector3.up * DefaultPointScale, color, time);
    Debug.DrawLine(point - Vector3.left * DefaultPointScale, point + Vector3.left * DefaultPointScale, color, time);
    Debug.DrawLine(point - Vector3.forward * DefaultPointScale, point + Vector3.forward * DefaultPointScale, color, time);
  }
  public static void DrawPoint(Vector3 point, Color color, float time, float scale)
  {
    Debug.DrawLine(point - Vector3.up * scale / 2, point + Vector3.up * scale, color, time);
    Debug.DrawLine(point - Vector3.left * scale / 2, point + Vector3.left * scale, color, time);
    Debug.DrawLine(point - Vector3.forward * scale / 2, point + Vector3.forward * scale, color, time);
  }

  public static void DrawPointLarge(Vector3 point, Color color, float time)
  {
    Debug.DrawLine(point - Vector3.up * LargePointScale, point + Vector3.up * LargePointScale, color, time);
    Debug.DrawLine(point - Vector3.left * LargePointScale, point + Vector3.left * LargePointScale, color, time);
    Debug.DrawLine(point - Vector3.forward * LargePointScale, point + Vector3.forward * LargePointScale, color, time);
  }

  public static void DrawPointSmall(Vector3 point, Color color, float time)
  {
    Debug.DrawLine(point - Vector3.up * SmallPointScale, point + Vector3.up * SmallPointScale, color, time);
    Debug.DrawLine(point - Vector3.left * SmallPointScale, point + Vector3.left * SmallPointScale, color, time);
    Debug.DrawLine(point - Vector3.forward * SmallPointScale, point + Vector3.forward * SmallPointScale, color, time);
  }

  public static void DrawVector(Vector3 start, Vector3 end, Color color, float time)
  {
    DrawVector(start, end, color, time, DefaultVectorArrowScale);
  }

  public static void DrawVector(Vector3 start, Vector3 direction, float distance, Color color, float time)
  {
    DrawVector(start, start + direction * distance, color, time);
  }

  public static void DrawVector(Vector3 start, Vector3 end, Color color, float time, float scale)
  {
    Vector3 forward = (end - start).normalized * scale;
    Vector3 crossUp = Vector3.Cross(forward, Vector3.up).normalized * scale;
    if (crossUp == Vector3.zero)
    {
      crossUp = Vector3.Cross(forward, Vector3.Cross(forward, Vector3.right)).normalized * scale;
    }
    Vector3 crossRight = Vector3.Cross(forward, crossUp).normalized * scale;
    Debug.DrawLine(start, end, color, time);
    Debug.DrawLine(end, end - forward + crossUp, color, time);
    Debug.DrawLine(end, end - forward - crossUp, color, time);
    Debug.DrawLine(end, end - forward + crossRight, color, time);
    Debug.DrawLine(end, end - forward - crossRight, color, time);
  }

  public static void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, Color color, float duration, RaycastHit hitInfo)
  {
    Vector3 end = Vector3.zero;
    if (hitInfo.transform != null)
    {
      end = hitInfo.point + hitInfo.normal * radius;
      DebugDraw.DrawPoint(hitInfo.point, color, duration);
    }
    else
    {
      end = origin + direction * maxDistance;
    }
    DebugDraw.DrawSphere(end, radius, color, duration);
    DebugDraw.DrawSphere(origin, radius, color, duration);
    Vector3 right = Vector3.zero;
    Vector3 up = Vector3.zero;
    if (direction != Vector3.up)
    {
      right = Vector3.Cross(direction, Vector3.up);
      up = Vector3.Cross(direction, right);
    }
    else
    {
      up = Vector3.Cross(direction, Vector3.right);
      right = Vector3.Cross(direction, up);
    }
    Debug.DrawLine(origin + up * radius, end + up * radius, color, duration);
    Debug.DrawLine(origin + right * radius, end + right * radius, color, duration);
    Debug.DrawLine(origin - up * radius, end - up * radius, color, duration);
    Debug.DrawLine(origin - right * radius, end - right * radius, color, duration);
  }



  public static void DrawSphere(Vector3 position, float radius, Color color, float duration)
  {
    float x1 = 0;
    float y1 = radius;
    float r2 = radius * radius;
    float halfPI = Mathf.PI / 2;
    for (int i = 1; i < SphereQuarterSegments; i++)
    {
      float x2 = radius * Mathf.Sin(((float)i / (SphereQuarterSegments - 1)) * halfPI);
      float y2 = Mathf.Sqrt(r2 - (x2 * x2));
      // Draw XY
      Vector3 v1 = new Vector3(x1, y1, 0);
      Vector3 v2 = new Vector3(x2, y2, 0);
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.x = -v1.x;
      v2.x = -v2.x;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.y = -v1.y;
      v2.y = -v2.y;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.x = -v1.x;
      v2.x = -v2.x;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      // Draw XZ
      v1 = new Vector3(x1, 0, y1);
      v2 = new Vector3(x2, 0, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.x = -v1.x;
      v2.x = -v2.x;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.z = -v1.z;
      v2.z = -v2.z;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.x = -v1.x;
      v2.x = -v2.x;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      // Draw YZ
      v1 = new Vector3(0, x1, y1);
      v2 = new Vector3(0, x2, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.y = -v1.y;
      v2.y = -v2.y;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.z = -v1.z;
      v2.z = -v2.z;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      v1.y = -v1.y;
      v2.y = -v2.y;
      Debug.DrawLine(position + v1, position + v2, color, duration);
      x1 = x2;
      y1 = y2;
    }
  }
}
