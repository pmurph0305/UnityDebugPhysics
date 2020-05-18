using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDraw
{
  public static float DefaultVectorArrowScale = 0.1f;
  public static float DefaultPointScale = 0.05f;

  private static int sphereQuarterSegments = 8;
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

  public static void DrawRaycast(Vector3 start, Vector3 end, Color color, float duration, bool depthTest = false)
  {
    Debug.DrawLine(start, end, color, duration, depthTest);
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

  public static void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction,
    Quaternion orientation, Color color, float duration, bool depthTest, RaycastHit hit, bool drawOrigin = true)
  {
    float endDistance = Vector3.Dot(hit.point - center, direction);
    // actual end point
    Vector3 end = (center + direction * endDistance) - direction * halfExtents.z;
    // subtract z as that's the direction.
    DrawBoxCast(center, halfExtents, direction, orientation, Vector3.Distance(center, end), color, duration, depthTest, drawOrigin);
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

  public static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color, float duration, bool depthTest)
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

  public static void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, Color color, float duration, bool depthTest = false, RaycastHit hitInfo = new RaycastHit())
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
    DebugDraw.DrawSphere(origin, radius, direction, color, duration, depthTest);
    DrawSphereLines(origin, direction, end, radius, color, duration, depthTest);
  }

  private static void DrawSphereLines(Vector3 origin, Vector3 direction, Vector3 end, float radius, Color color, float duration, bool depthTest)
  {
    Vector3 up = Vector3.Cross(direction, Vector3.right).normalized;
    Vector3 right = Vector3.Cross(direction, up).normalized;
    Debug.DrawLine(origin + up * radius, end + up * radius, color, duration, depthTest);
    Debug.DrawLine(origin + right * radius, end + right * radius, color, duration, depthTest);
    Debug.DrawLine(origin - up * radius, end - up * radius, color, duration, depthTest);
    Debug.DrawLine(origin - right * radius, end - right * radius, color, duration, depthTest);
  }

  public static void DrawSphere(Vector3 position, float radius, Vector3 direction, Color color, float duration, bool depthTest)
  {
    if (radius < 0) radius *= -1;
    Vector3 up = Vector3.Cross(direction, Vector3.right).normalized;
    Vector3 right = Vector3.Cross(direction, up).normalized;
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

  public static void DrawCapsule(Vector3 point1, Vector3 point2, float radius, Color color, float duration, bool depthTest)
  {
    DrawSphereLines(point1, point2 - point1, point2, radius, color, duration, depthTest);
    DrawCapsuleSphere(point1, radius, point2 - point1, color, duration, depthTest);
    DrawCapsuleSphere(point2, radius, point1 - point2, color, duration, depthTest);
  }

  public static void DrawCapsuleCast(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4,
    float radius, Color color, float duration, bool depthTest)
  {
    DrawCapsule(point1, point2, radius, color, duration, depthTest);
    DrawCapsule(point3, point4, radius, color, duration, depthTest);
    DrawCapsuleCastLines(point1, point2, point3, point4, radius, color, duration, depthTest);
  }

  public static void DrawCapsuleCast(Vector3 point1, Vector3 point2,
    float radius, Vector3 direction, Color color, float duration, bool depthTest, RaycastHit hit)
  {
    if (radius < 0) radius *= -1;
    direction = direction.normalized;
    DrawCapsule(point1, point2, radius, color, duration, depthTest);
    // axis of cylinder
    Vector3 dir = (point2 - point1).normalized;
    // height of cylinder
    float h = (point2 - point1).magnitude;
    // point on new axis of cylinder.
    Vector3 dPoint1 = hit.point + hit.normal * radius + dir * h / 2;
    // rotation of new axis with axis as forward, so we get a normal vector.
    Quaternion look = Quaternion.LookRotation(dir, direction);
    Vector3 axisNormal = look * -Vector3.up;
    // dot product of from pt -> axis point and normal gives minimum distance / one side of triangle
    float dotNormal = Vector3.Dot(point1 - dPoint1, axisNormal);
    // hyp = adjacent / cos0.
    float distance = dotNormal / Mathf.Cos(Vector3.Angle(direction, axisNormal) * Mathf.Deg2Rad);
    Vector3 endPoint1 = point1 - direction * distance;
    Vector3 endPoint2 = point2 - direction * distance;
    // draw the capsule.
    DrawCapsule(endPoint1, endPoint2, radius, color, duration, depthTest);
    DrawCapsuleCastLines(point1, point2, endPoint1, endPoint2, radius, color, duration, depthTest);
  }

  private static void DrawCapsuleCastLines(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4,
    float radius, Color color, float duration, bool depthTest)
  {
    if (radius < 0) radius *= -1;
    Vector3 direction = (point2 - point1).normalized;
    Vector3 up = Vector3.Cross(direction, Vector3.right).normalized;
    Quaternion r = Quaternion.LookRotation(direction, up);
    Vector3[] spherePoints = new Vector3[4] {
      r * new Vector3(0.5f, 0, 0),
      r * new Vector3(-0.5f, 0, 0),
      r * new Vector3(0, 0, 0.5f),
      r * new Vector3(0, 0, -0.5f)
    };
    Vector3[] points = new Vector3[6] {
      spherePoints[0] + point1,
      spherePoints[1] + point1,
      spherePoints[3] + point1,
      spherePoints[0] + point2,
      spherePoints[1] + point2,
      spherePoints[2] + point2,
    };
    Vector3 pointDir = point3 - point1;
    Debug.DrawLine(points[0], points[0] + pointDir, color, duration, depthTest);
    Debug.DrawLine(points[1], points[1] + pointDir, color, duration, depthTest);
    Debug.DrawLine(points[2], points[2] + pointDir, color, duration, depthTest);
    Debug.DrawLine(points[3], points[3] + pointDir, color, duration, depthTest);
    Debug.DrawLine(points[4], points[4] + pointDir, color, duration, depthTest);
    Debug.DrawLine(points[5], points[5] + pointDir, color, duration, depthTest);
  }

  private static void DrawCapsuleSphere(Vector3 position, float radius, Vector3 direction, Color color, float duration, bool depthTest)
  {
    if (radius < 0) radius *= -1;
    Vector3 up = Vector3.Cross(direction, Vector3.right).normalized;
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
      v1 = r * new Vector3(-x1, 0, -y1);
      v2 = r * new Vector3(-x2, 0, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      v1 = r * new Vector3(x1, 0, -y1);
      v2 = r * new Vector3(x2, 0, -y2);
      Debug.DrawLine(position + v1, position + v2, color, duration, depthTest);
      // // Draw YZ
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

  public static void DrawColliderAtPositionAndRotation(Collider colliderA, Vector3 positionA, Quaternion rotationA, Color color, float duration, bool depthTest)
  {
    BoxCollider box = colliderA as BoxCollider;
    if (box != null)
    {
      Vector3 size = box.size;
      size.x *= box.transform.lossyScale.x;
      size.y *= box.transform.lossyScale.y;
      size.z *= box.transform.lossyScale.z;
      DebugDraw.DrawBox(positionA, size / 2, rotationA, color, duration, depthTest);
      return;
    }
    SphereCollider sphere = colliderA as SphereCollider;
    if (sphere != null)
    {
      float r = sphere.radius;
      Vector3 scale = sphere.transform.lossyScale;
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
      DebugDraw.DrawSphere(positionA, r, colliderA.transform.position - positionA, color, duration, depthTest);
    }
    CapsuleCollider cap = colliderA as CapsuleCollider;
    if (cap != null)
    {
      Vector3 point1, point2 = point1 = cap.center;
      float heightToAdd = cap.height / 2 - cap.radius;
      Vector3 scale = cap.transform.lossyScale;
      float r = cap.radius;
      if (scale.x >= scale.y && scale.x >= scale.z)
      {
        r *= scale.x;
      }
      else if (scale.y >= scale.x && scale.y >= scale.z)
      {
        r *= scale.y;
      }
      else
      {
        r *= scale.z;
      }
      if (cap.direction == 0)
      {
        point1.x += heightToAdd * scale.x;
        point2.x -= heightToAdd * scale.x;
      }
      else if (cap.direction == 1)
      {
        point1.y += heightToAdd * scale.y;
        point2.y -= heightToAdd * scale.y;
      }
      else
      {
        point1.z += heightToAdd * scale.z;
        point2.z -= heightToAdd * scale.z;
      }
      point1 = rotationA * point1;
      point2 = rotationA * point2;
      DebugDraw.DrawCapsule(point1 + positionA, point2 + positionA, r, color, duration, depthTest);
    }
  }

  public static void DrawColliders(Collider[] colliders, Color color, float duration, bool depthTest)
  {
    foreach (Collider col in colliders)
    {
      DrawCollider(col, color, duration, depthTest);
    }

  }

  public static void DrawCollider(Collider col, Color color, float duration, bool depthTest)
  {
    if (col == null) return;
    BoxCollider box = col as BoxCollider;
    if (box != null) { DrawBoxCollider(box, color, duration, depthTest); return; }
    CapsuleCollider cap = col as CapsuleCollider;
    if (cap != null) { DrawCapsuleCollider(cap, color, duration, depthTest); return; }
    SphereCollider sphere = col as SphereCollider;
    if (sphere != null) { DrawSphereCollider(sphere, color, duration, depthTest); return; }
    MeshCollider mc = col as MeshCollider;
    if (mc != null) { DrawMeshCollider(mc, color); }
  }

  public static void DrawBoxCollider(BoxCollider col, Color color, float duration, bool depthTest)
  {
    Vector3 size = col.size;
    size.x *= col.transform.lossyScale.x;
    size.y *= col.transform.lossyScale.y;
    size.z *= col.transform.lossyScale.z;
    DebugDraw.DrawBox(col.transform.TransformPoint(col.center), size / 2, col.transform.rotation, color, duration, depthTest);
  }
  public static void DrawSphereCollider(SphereCollider col, Color color, float duration, bool depthTest)
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
    DebugDraw.DrawSphere(col.transform.TransformPoint(col.center), r, col.transform.forward, color, duration, depthTest);
  }
  public static void DrawCapsuleCollider(CapsuleCollider col, Color color, float duration, bool depthTest)
  {
    Vector3 point1, point2 = point1 = col.center;
    float r = col.radius;
    Vector3 scale = col.transform.lossyScale;
    float heightToAdd = col.height / 2 - col.radius;
    if (scale.x >= scale.y && scale.x >= scale.z)
    {
      r *= scale.x;
    }
    else if (scale.y >= scale.x && scale.y >= scale.z)
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
    DebugDraw.DrawCapsule(point1 + col.transform.position, point2 + col.transform.position, r, color, duration, depthTest);
  }

  public static void DrawMeshCollider(MeshCollider col, Color color)
  {
    throw new System.NotImplementedException("Draw Mesh Collider not implemented.");
  }
}
