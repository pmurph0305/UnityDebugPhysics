using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDraw
{
  public static float DefaultVectorArrowScale = 0.1f;
  public static float DefaultPointScale = 0.05f;

  private static int sphereQuarterSegments = 4;
  public static int SphereQuarterSegments
  {
    get
    {
      return sphereQuarterSegments < 2 ? 2 : sphereQuarterSegments;
    }
    set
    {
      if (value < 2)
      {
        Debug.LogWarning("Cant draw a sphere with less than 2 quarter segments, defaulting to 2.");
        sphereQuarterSegments = 2;
      }
      else
      {
        sphereQuarterSegments = value;
      }
    }
  }

  public static void DrawPoint(Vector3 point, Color color, float time, bool depthTest = false)
  {
    Debug.DrawLine(point - Vector3.up * DefaultPointScale, point + Vector3.up * DefaultPointScale, color, time, depthTest);
    Debug.DrawLine(point - Vector3.left * DefaultPointScale, point + Vector3.left * DefaultPointScale, color, time, depthTest);
    Debug.DrawLine(point - Vector3.forward * DefaultPointScale, point + Vector3.forward * DefaultPointScale, color, time, depthTest);
  }

  public static void DrawLine(Vector3 start, Vector3 end, Color color, float time, bool depthTest = false)
  {
    Debug.DrawLine(start, end, color, time, depthTest);
  }

  public static void DrawVector(Vector3 start, Vector3 end, Color color, float time)
  {
    DrawVector(start, end, color, time, DefaultVectorArrowScale);
  }

  public static void DrawVector(Vector3 start, Vector3 direction, float distance, Color color, float time)
  {
    DrawVector(start, start + direction * distance, color, time);
  }

  public static void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction,
    Quaternion orientation, float distance, Color color, float duration, bool depthTest, bool drawOrigin = true)
  {
    if (drawOrigin)
    {
      DrawBox(center, halfExtents, orientation, color, duration, depthTest);
    }
    Vector3 end = center + (direction * distance);
    // DrawBox((end + center) / 2, new Vector3(halfExtents.x, halfExtents.y, distance / 2 - halfExtents.z), orientation, color, duration, depthTest);
    DrawBoxLinesBetween(center, end, halfExtents, direction, orientation, color, duration, depthTest);
    DrawBox(end, halfExtents, orientation, color, duration, depthTest);
  }

  public static void DrawBoxLinesBetween(Vector3 start, Vector3 end, Vector3 halfExtents, Vector3 direction, Quaternion orientation, Color color, float duration, bool depthTest)
  {
    Vector3[] points = new Vector3[] {
      orientation * (new Vector3(halfExtents.x, halfExtents.y, 0)) + start,
      orientation * (new Vector3(-halfExtents.x, -halfExtents.y, 0)) + start,
      orientation * (new Vector3(-halfExtents.x, +halfExtents.y, 0)) + start,
      orientation * (new Vector3(+halfExtents.x, -halfExtents.y, 0)) + start,
      orientation * (new Vector3(+halfExtents.x, +halfExtents.y, -halfExtents.z)) + end,
      orientation * (new Vector3(+halfExtents.x, -halfExtents.y, -halfExtents.z)) + end,
      orientation * (new Vector3(-halfExtents.x, +halfExtents.y, -halfExtents.z)) + end,
      orientation * (-halfExtents) + end
    };
    Debug.DrawLine(points[0], points[4], color, duration, depthTest);
    Debug.DrawLine(points[1], points[7], color, duration, depthTest);
    Debug.DrawLine(points[2], points[6], color, duration, depthTest);
    Debug.DrawLine(points[3], points[5], color, duration, depthTest);
  }


  public static void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction,
    Quaternion orientation, Color color, float duration, bool depthTest, RaycastHit hit, bool drawOrigin = true)
  {
    float endDistance = Vector3.Dot(hit.point - center, direction);
    // actual end point
    Vector3 end = (center + direction * endDistance) - direction * halfExtents.z;
    // subtract z as that's the direction.
    DrawBoxCast(center, halfExtents, direction, orientation, Vector3.Distance(center, end), color, duration, depthTest, drawOrigin);
  }


  private static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color, float duration, bool depthTest)
  {
    Vector3[] points = new Vector3[8] {
      orientation * (- halfExtents) + center,
      orientation * (new Vector3(- halfExtents.x, - halfExtents.y,  + halfExtents.z)) + center,
      orientation * (new Vector3( - halfExtents.x,  + halfExtents.y,  - halfExtents.z)) + center,
      orientation * (new Vector3( + halfExtents.x,  - halfExtents.y,  - halfExtents.z)) + center,
      orientation * (new Vector3( + halfExtents.x,  + halfExtents.y,  - halfExtents.z)) + center,
      orientation * (new Vector3( + halfExtents.x,  - halfExtents.y,  + halfExtents.z)) + center,
      orientation * (new Vector3( - halfExtents.x,  + halfExtents.y,  + halfExtents.z)) + center,
      orientation * (halfExtents) + center
    };
    Debug.DrawLine(points[0], points[1], color, duration, depthTest);
    Debug.DrawLine(points[0], points[2], color, duration, depthTest);
    Debug.DrawLine(points[0], points[3], color, duration, depthTest);
    Debug.DrawLine(points[1], points[6], color, duration, depthTest);
    Debug.DrawLine(points[1], points[5], color, duration, depthTest);
    Debug.DrawLine(points[2], points[6], color, duration, depthTest);
    Debug.DrawLine(points[2], points[4], color, duration, depthTest);
    Debug.DrawLine(points[3], points[5], color, duration, depthTest);
    Debug.DrawLine(points[3], points[4], color, duration, depthTest);
    Debug.DrawLine(points[7], points[4], color, duration, depthTest);
    Debug.DrawLine(points[7], points[6], color, duration, depthTest);
    Debug.DrawLine(points[7], points[5], color, duration, depthTest);
  }

  public static void DrawVector(Vector3 start, Vector3 end, Color color, float duration, float scale)
  {
    Vector3 direction = (end - start).normalized;
    Vector3 right = Vector3.zero;
    Vector3 up = Vector3.zero;
    if (direction != Vector3.up)
    {
      right = Vector3.Cross(direction, Vector3.up).normalized;
      up = Vector3.Cross(direction, right).normalized;
    }
    else
    {
      up = Vector3.Cross(direction, Vector3.right).normalized;
      right = Vector3.Cross(direction, up).normalized;
    }
    right = right * scale;
    up = up * scale;
    direction = direction * scale;
    Debug.DrawLine(start, end, color, duration);
    Debug.DrawLine(end, end - direction + up, color, duration);
    Debug.DrawLine(end, end - direction - up, color, duration);
    Debug.DrawLine(end, end - direction + right, color, duration);
    Debug.DrawLine(end, end - direction - right, color, duration);
  }

  public static void DrawRaycast(Vector3 start, Vector3 end, Color color, float duration, bool depthTest = false)
  {
    Debug.DrawLine(start, end, color, duration, depthTest);
  }

  public static void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, Color color, float duration, bool drawOriginSphere = true, bool depthTest = false, RaycastHit hitInfo = new RaycastHit())
  {
    Vector3 end = Vector3.zero;
    if (hitInfo.transform != null)
    {
      end = hitInfo.point + hitInfo.normal * radius;
    }
    else
    {
      end = origin + direction * maxDistance;
    }
    DebugDraw.DrawSphere(end, radius, direction, color, duration, depthTest);
    if (drawOriginSphere)
    {
      DebugDraw.DrawSphere(origin, radius, direction, color, duration, depthTest);
    }
    DrawSphereLines(origin, direction, end, radius, color, duration, depthTest);
  }

  private static void DrawSphereLines(Vector3 origin, Vector3 direction, Vector3 end, float radius, Color color, float duration, bool depthTest)
  {
    Vector3 right = Vector3.zero;
    Vector3 up = Vector3.zero;
    if (direction != Vector3.up)
    {
      right = Vector3.Cross(direction, Vector3.up).normalized;
      up = Vector3.Cross(direction, right).normalized;
    }
    else
    {
      up = Vector3.Cross(direction, Vector3.right).normalized;
      right = Vector3.Cross(direction, up).normalized;
    }
    Debug.DrawLine(origin + up * radius, end + up * radius, color, duration, depthTest);
    Debug.DrawLine(origin + right * radius, end + right * radius, color, duration, depthTest);
    Debug.DrawLine(origin - up * radius, end - up * radius, color, duration, depthTest);
    Debug.DrawLine(origin - right * radius, end - right * radius, color, duration, depthTest);
  }

  public static void DrawSphere(Vector3 position, float radius, Vector3 direction, Color color, float duration, bool depthTest)
  {
    if (radius < 0) radius *= -1;
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
    direction = direction.normalized;
    Quaternion r = Quaternion.LookRotation(direction, up);
    float x1 = 0;
    float y1 = radius;
    for (int i = 1; i < SphereQuarterSegments; i++)
    {
      float x2 = radius * Mathf.Sin(((float)i / (SphereQuarterSegments - 1)) * (Mathf.PI / 2));
      float y2 = Mathf.Sqrt((radius * radius) - (x2 * x2));
      // Draw XY
      Vector3 v1 = r * new Vector3(x1, y1, 0);
      Vector3 v2 = r * new Vector3(x2, y2, 0);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(-x1, y1, 0);
      v2 = r * new Vector3(-x2, y2, 0);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(-x1, -y1, 0);
      v2 = r * new Vector3(-x2, -y2, 0);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(x1, -y1, 0);
      v2 = r * new Vector3(x2, -y2, 0);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      // Draw XZ
      v1 = r * new Vector3(x1, 0, y1);
      v2 = r * new Vector3(x2, 0, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(-x1, 0, y1);
      v2 = r * new Vector3(-x2, 0, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(-x1, 0, -y1);
      v2 = r * new Vector3(-x2, 0, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(x1, 0, -y1);
      v2 = r * new Vector3(x2, 0, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      // Draw YZ
      v1 = r * new Vector3(0, x1, y1);
      v2 = r * new Vector3(0, x2, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(0, -x1, y1);
      v2 = r * new Vector3(0, -x2, y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(0, -x1, -y1);
      v2 = r * new Vector3(0, -x2, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(0, x1, -y1);
      v2 = r * new Vector3(0, x2, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      x1 = x2;
      y1 = y2;
    }
  }
}
