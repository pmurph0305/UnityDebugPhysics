using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class DebugDraw
{
  public static float DefaultVectorArrowScale = 0.1f;

  /// <summary>
  /// Scale to use to draw points.
  /// </summary>
  public static float DefaultPointScale = 0.05f;

  private static int sphereQuarterSegments = 8;
  /// <summary>
  /// Number of line sections to use per quarter sphere. Must be >= 2.
  /// </summary>
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

  /// <summary>
  /// Draws a point using lines aligned with the world-space axis'
  /// </summary>
  /// <param name="point">Location in world space to draw the point</param>
  /// <param name="color">Color to draw point</param>
  /// <param name="duration">	How long the point should be visible for.</param>
  /// <param name="depthTest">	Should the line be obscured by objects closer to the camera?</param>
  public static void DrawPoint(Vector3 point, Color color, float duration, bool depthTest = false)
  {
    Debug.DrawLine(point - Vector3.up * DefaultPointScale, point + Vector3.up * DefaultPointScale, color, duration, depthTest);
    Debug.DrawLine(point - Vector3.left * DefaultPointScale, point + Vector3.left * DefaultPointScale, color, duration, depthTest);
    Debug.DrawLine(point - Vector3.forward * DefaultPointScale, point + Vector3.forward * DefaultPointScale, color, duration, depthTest);
  }

  /// <summary>
  /// Draws a line between start and end points.
  /// </summary>
  /// <param name="start">Point in world space where the line should start</param>
  /// <param name="end">Point in world space where the line should end.</param>
  /// <param name="color">Color of the line.</param>
  /// <param name="time">How long the line should be visible for.</param>
  /// <param name="depthTest">Should the line be obscured by objects closer to the camera?</param>
  public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest = false)
  {
    Debug.DrawLine(start, end, color, duration, depthTest);
  }

  /// <summary>
  /// Draws a raycast using a line between start and end poitns.
  /// </summary>
  /// <param name="start">Point in world space where the line should start</param>
  /// <param name="end">Point in world space where the line should end.</param>
  /// <param name="color">Color of the line.</param>
  /// <param name="duration">How long the line should be visible for.</param>
  /// <param name="depthTest">Should the line be obscured by objects closer to the camera?</param>
  public static void DrawRaycast(Vector3 start, Vector3 end, Color color, float duration, bool depthTest = false)
  {
    Debug.DrawLine(start, end, color, duration, depthTest);
  }


  public static void DrawVector(Vector3 start, Vector3 end, Color color, float scale, float duration, bool depthTest = false)
  {
    // throw new System.NotImplementedException();
    Vector3 direction = (end - start).normalized;
    Vector3 right = Vector3.zero;
    Vector3 up = Vector3.zero;
    if (direction != Vector3.up && direction != -Vector3.up)
    {
      right = Vector3.Cross(direction, Vector3.up).normalized;
      up = Vector3.Cross(direction, right).normalized;
    }
    else
    {
      up = Vector3.Cross(direction, Vector3.forward).normalized;
      right = Vector3.Cross(direction, up).normalized;
    }
    right = right * scale;
    up = up * scale;
    direction = direction * scale;
    Debug.DrawLine(start, end, color, duration, depthTest);
    // Debug.DrawLine(end, end - direction + up, color, duration, depthTest);
    // Debug.DrawLine(end, end - direction - up, color, duration, depthTest);
    Debug.DrawLine(end, end - direction + right, color, duration, depthTest);
    Debug.DrawLine(end, end - direction - right, color, duration, depthTest);
  }

  /// <summary>
  /// Draws a single boxcast from center to direction using raycasthit data.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <param name="direction">Direction in which the box was cast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="color">Color to draw the boxcast with</param>
  /// <param name="duration">Length of time to draw the boxcast</param>
  /// <param name="depthTest">Should the drawing be obscured by objects in the scene?</param>
  /// <param name="hit">Hit data of the boxcast</param>
  /// <param name="drawOrigin">Should a box be drawn at center?</param>
  public static void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction,
    Quaternion orientation, Color color, float duration, bool depthTest, RaycastHit hit, bool drawOrigin = true)
  {
    float endDistance = Vector3.Dot(hit.point - center, direction);
    // actual end point
    Vector3 end = (center + direction * endDistance) - direction * halfExtents.z;
    // subtract z as that's the direction.
    DrawBoxCast(center, halfExtents, direction, orientation, Vector3.Distance(center, end), color, duration, depthTest, drawOrigin);
  }

  /// <summary>
  /// Draws a single boxcast from center to center + direction * distance.
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <param name="direction">Direction in which the box was cast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="distance">Distance of the boxcast</param>
  /// <param name="color">Color to draw the boxcast with</param>
  /// <param name="duration">Length of time to draw the boxcast</param>
  /// <param name="depthTest">Should the drawing be obscured by objects in the scene?</param>
  /// <param name="drawOrigin">Should a box be drawn at center?</param>
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

  /// <summary>
  /// Draws the lines between the leading corners of the box at start, and the trailing cornerns of the box at end.
  /// </summary>
  /// <param name="start">Center of the box at the start</param>
  /// <param name="end">Center of the box at the end</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <param name="direction">Direction in which the box was cast</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="color">Color to draw the boxcast with</param>
  /// <param name="duration">Length of time to draw the boxcast</param>
  /// <param name="depthTest">Should the drawing be obscured by objects in the scene?</param>
  private static void DrawBoxLinesBetween(Vector3 start, Vector3 end, Vector3 halfExtents, Vector3 direction, Quaternion orientation, Color color, float duration, bool depthTest)
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

  /// <summary>
  /// Draws a box at a position
  /// </summary>
  /// <param name="center">Center of the box</param>
  /// <param name="halfExtents">Half the size of the box in each dimension</param>
  /// <param name="orientation">Rotation of the box</param>
  /// <param name="color">Color to draw the box with</param>
  /// <param name="duration">Length of time to draw the box for</param>
  /// <param name="depthTest">Should the box be obscured by objects in the scene?</param>
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

  /// <summary>
  /// Draws a spherecast using raycasthit data.
  /// </summary>
  /// <param name="origin">Start of the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">Direction of the spherecast</param>
  /// <param name="maxDistance">Distance of the spherecast</param>
  /// <param name="color">Color to draw the spherecast</param>
  /// <param name="duration">Length of time to draw the spherecast</param>
  /// <param name="depthTest">Should the drawing be obscured by objects in the scene?</param>
  /// <param name="hitInfo">Spherecast's raycasthit data</param>
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

  /// <summary>
  /// Draws the lines from the origin spheres up/down/left/right to the end spheres up/down/left/right
  /// </summary>
  /// <param name="origin">World space start of the spherecast</param>
  /// <param name="direction">World space direction of the spherecast</param>
  /// <param name="end">World space end of the spherecast</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="color">Color to draw the sphere with</param>
  /// <param name="duration">Length of time to draw the sphere</param>
  /// <param name="depthTest">Should the sphere be obscured by objects in the scene?</param>
  private static void DrawSphereLines(Vector3 origin, Vector3 direction, Vector3 end, float radius, Color color, float duration, bool depthTest)
  {
    Vector3 up = Vector3.Cross(direction, Vector3.right).normalized;
    Vector3 right = Vector3.Cross(direction, up).normalized;
    Debug.DrawLine(origin + up * radius, end + up * radius, color, duration, depthTest);
    Debug.DrawLine(origin + right * radius, end + right * radius, color, duration, depthTest);
    Debug.DrawLine(origin - up * radius, end - up * radius, color, duration, depthTest);
    Debug.DrawLine(origin - right * radius, end - right * radius, color, duration, depthTest);
  }

  /// <summary>
  /// Draws a sphere at a world position
  /// </summary>
  /// <param name="position">World space position of the sphere</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">World space direction of the sphere's forward</param>
  /// <param name="color">Color to draw the sphere with</param>
  /// <param name="duration">Length of time to draw the sphere</param>
  /// <param name="depthTest">Should the sphere be obscured by objects in the scene?</param>
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

  /// <summary>
  /// Draws a capsule cast
  /// </summary>
  /// <param name="point1">The center of the sphere at the start of the start capsule.</param>
  /// <param name="point2">The center of the sphere at the end of the start capsule.</param>
  /// <param name="point3">The center of the sphere at the start of the end capsule.</param>
  /// <param name="point4">The center of the sphere at the end of the end capsule.</param>
  /// <param name="radius">The radius of the capsule.</param>
  /// <param name="color">The color to draw the capsule with</param>
  /// <param name="duration">The length of time to draw the capsule</param>
  /// <param name="depthTest">Should the capsule be obscured by objects in the scene</param>
  public static void DrawCapsuleCast(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4,
    float radius, Color color, float duration, bool depthTest)
  {
    DrawCapsule(point1, point2, radius, color, duration, depthTest);
    DrawCapsule(point3, point4, radius, color, duration, depthTest);
    DrawCapsuleCastLines(point1, point2, point3, point4, radius, color, duration, depthTest);
  }

  /// <summary>
  /// Draws a single capsule cast in a direction using raycasthit data
  /// </summary>
  /// <param name="point1">The center of the sphere at the start of the capsule.</param>
  /// <param name="point2">The center of the sphere at the end of the capsule.</param>
  /// <param name="radius">Radius of the capsule</param>
  /// <param name="direction">Direction of the capsule cast</param>
  /// <param name="color">The color to draw the capsule with</param>
  /// <param name="duration">The length of time to draw the capsule</param>
  /// <param name="depthTest">Should the capsule be obscured by objects in the scene</param>
  /// <param name="hit">Raycasthit data of the capsule cast</param>
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

  /// <summary>
  /// Draws the lines between the start capsules top and bottom sphere, and the end capsules top and bottom spheres
  /// </summary>
  /// <param name="point1">The center of the sphere at the start of the start capsule.</param>
  /// <param name="point2">The center of the sphere at the end of the start capsule.</param>
  /// <param name="point3">The center of the sphere at the start of the end capsule.</param>
  /// <param name="point4">The center of the sphere at the end of the end capsule.</param>
  /// <param name="radius">The radius of the capsule.</param>
  /// <param name="color">The color to draw the capsule with</param>
  /// <param name="duration">The length of time to draw the capsule</param>
  /// <param name="depthTest">Should the capsule be obscured by objects in the scene</param>
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

  /// <summary>
  /// Draws the lines of the sphereical portion of a capsule
  /// </summary>
  /// <param name="position">Position of the sphere</param>
  /// <param name="radius">Radius of the sphere</param>
  /// <param name="direction">Sphere's forward direction</param>
  /// <param name="color">Color of the sphere</param>
  /// <param name="duration">Length of time to draw the sphere</param>
  /// <param name="depthTest">Should the sphere be obscured by objects in the scene</param>
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

  /// <summary>
  /// Draws a collider at a position with a rotation
  /// </summary>
  /// <param name="colliderA">Collider to draw</param>
  /// <param name="positionA">Worldspace position to draw collider at</param>
  /// <param name="rotationA">Rotation of collider</param>
  /// <param name="color">Color to draw with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the collider be obscured by objects in the scene?</param>
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

  /// <summary>
  /// Draws all the colliders in colliders using each collider's properties and transform.
  /// </summary>
  /// <param name="colliders">Array of colliders to draw</param>
  /// <param name="color">Color to draw all the colliders with</param>
  /// <param name="duration">Length of time to draw all the colliders</param>
  /// <param name="depthTest">Should the drawn colliders be obscured by the camera?</param>
  public static void DrawColliders(Collider[] colliders, Color color, float duration, bool depthTest)
  {
    foreach (Collider col in colliders)
    {
      DrawCollider(col, color, duration, depthTest);
    }

  }

  /// <summary>
  /// Draws a single collider
  /// </summary>
  /// <param name="col">Collider to draw</param>
  /// <param name="color">Color to draw the collider with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the drawn collider be obscured by the camera?</param>
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
    if (mc != null) { DrawMeshCollider(mc, color, duration, depthTest); }
  }


  /// <summary>
  /// Draw a Box collider
  /// </summary>
  /// <param name="col">Box Collider to draw</param>
  /// <param name="color">Color to draw the collider with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the drawn collider be obscured by the camera?</param>
  private static void DrawBoxCollider(BoxCollider col, Color color, float duration, bool depthTest)
  {
    Vector3 size = col.size;
    size.x *= col.transform.lossyScale.x;
    size.y *= col.transform.lossyScale.y;
    size.z *= col.transform.lossyScale.z;
    DebugDraw.DrawBox(col.transform.TransformPoint(col.center), size / 2, col.transform.rotation, color, duration, depthTest);
  }

  /// <summary>
  /// Draw a Sphere Collider
  /// </summary>
  /// <param name="col">Sphere Collider to draw</param>
  /// <param name="color">Color to draw the collider with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the drawn collider be obscured by the camera?</param>
  private static void DrawSphereCollider(SphereCollider col, Color color, float duration, bool depthTest)
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

  /// <summary>
  /// Draw a Capsule Collider
  /// </summary>
  /// <param name="col">Capsule collider to draw</param>
  /// <param name="color">Color to draw the collider with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the drawn collider be obscured by the camera?</param>
  private static void DrawCapsuleCollider(CapsuleCollider col, Color color, float duration, bool depthTest)
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

  /// <summary>
  /// Draw a mesh collider using it's shared mesh if non-convex, or AABB if convex (actual convex mesh is not exposed in unity)
  /// </summary>
  /// <param name="col">Mesh collider to draw</param>
  /// <param name="color">Color to draw the collider with</param>
  /// <param name="duration">Length of time to draw the collider</param>
  /// <param name="depthTest">Should the drawn collider be obscured by the camera?</param>
  public static void DrawMeshCollider(MeshCollider col, Color color, float duration, bool depthTest)
  {
    if (!col.convex)
    {
      Vector3[] verts = col.sharedMesh.vertices;
      int[] triangles = col.sharedMesh.triangles;
      Vector3 p0, p1, p2 = p1 = p0 = Vector3.zero;
      for (int i = 0; i < triangles.Length; i += 3)
      {
        p0 = col.transform.TransformPoint(verts[triangles[i]]);
        p1 = col.transform.TransformPoint(verts[triangles[i + 1]]);
        p2 = col.transform.TransformPoint(verts[triangles[i + 2]]);
        Debug.DrawLine(p0, p1, color, duration, depthTest);
        Debug.DrawLine(p0, p2, color, duration, depthTest);
        Debug.DrawLine(p1, p2, color, duration, depthTest);
      }
    }
    else
    {
      DrawBox(col.bounds.center, col.bounds.extents, Quaternion.identity, color, duration, depthTest);
    }
  }
}