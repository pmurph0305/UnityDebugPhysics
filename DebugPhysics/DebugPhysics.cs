using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugPhysics
{
  // DebugRigidbody
  // since things are appled with a rigidbody instance
  // ie rigidbody.AddForce
  // would need to make it work with find and replace
  // ie rigidbody.AddForce => DebugRigidbody.AddForce(rigidbody rb.. etc)
  // or DebugPhysics.AddForce(rigidbody rb etc...)

  //TransformExtensionMethods for rotation.

  /// <summary>
  /// Color to draw with when the result is a hit, overlap, check is true, or collision is not ignored
  /// Also used to draw any relevant points like hit.point and closest point.
  /// </summary>
  public static Color HitColor = Color.green;

  /// <summary>
  /// Color to draw with when the result is not a hit, overlap, check is false, or collision is ignored
  /// </summary>
  public static Color NoHitColor = Color.red;

  /// <summary>
  /// Color of lines to use to draw raycast hit normals
  /// </summary>
  public static Color HitNormalColor = Color.cyan;

  /// <summary>
  /// Color of colliders to draw overlapped colliders with
  /// </summary>
  public static Color OverlapColor = Color.blue;

  private static float drawLineTime = 0.0001f;
  /// <summary>
  /// Length of time to do all drawing
  /// </summary>
  public static float DrawLineTime
  {
    get
    {
      return drawLineTime;
    }
    set
    {
      if (value <= 0)
      {
        Debug.LogWarning("Cannot set draw line time below 0, setting draw line time to 0.0001f by default.");
        drawLineTime = 0.0001f;
      }
      else
      {
        drawLineTime = value;
      }
    }
  }


  private static float maxDrawLength = 1000f;
  /// <summary>
  /// Maximum length to draw raycasts
  /// </summary>
  public static float MaxDrawLength
  {
    get { return maxDrawLength; }
    set
    {
      if (value < Mathf.Infinity)
      {
        maxDrawLength = value;
      }
      else
      {
        Debug.LogWarning("Cannot set max draw length to Infinity. Setting max draw length to 1000f.");
        maxDrawLength = 1000f;
      }
    }
  }


  /// <summary>
  /// Draw the results with a depth test?
  /// </summary>
  public static bool DepthTest = false;

  /// <summary>
  /// Draw the hit points of raycast hits?
  /// </summary>
  public static bool DrawHitPoints = true;

  /// <summary>
  /// Draw the normals of raycast hits?
  /// </summary>
  public static bool DrawHitNormals = true;

  /// <summary>
  /// Draw the Colliders that are overlapping?
  /// </summary>
  public static bool DrawOverlapColliders = true;

  /// <summary>
  /// Draw the colliders when using ComputePenetration
  /// </summary>
  public static bool DrawPenetrationColliders = true;

  /// <summary>
  /// Draw the collider that you want to find the closest point on?
  /// </summary>
  public static bool DrawClosestPointCollider = true;

  /// <summary>
  /// Duration to draw colliders for IgnoreCollision
  /// </summary>
  public static float IgnoreCollisionDrawTime = 5f;
}
