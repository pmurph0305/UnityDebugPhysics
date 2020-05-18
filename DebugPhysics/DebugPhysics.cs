using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugPhysics
{
  // All:
  // BoxCast, BoxCastAll, BoxCastNonAlloc (done)
  // CapsuleCast, CapsuleCastAll, CapsuleCastNonAlloc (done)
  // CheckBox, CheckCapsule, CheckSphere (done)
  // ClosestPoint
  // Compute Penetration
  // GetIgnoreCollisionLayer
  // IgnoreCollision
  // IgnoreLayerCollision
  // LineCast (done)
  // OverlapBox, OverlapBoxNonAlloc, OverlapCapsule, OverlapCapsuleNonAlloc, OverlapSphere, OverlapSphereNonAlloc,
  // Raycast, RaycastAll, RaycastNonAlloc (done)
  // Simulate
  // SphereCast, SphereCastAll, SphereCastNonAlloc (done)


  // DebugRigidbody
  // since things are appled with a rigidbody instance
  // ie rigidbody.AddForce
  // would need to make it work with find and replace
  // ie rigidbody.AddForce => DebugRigidbody.AddForce(rigidbody rb.. etc)
  // or DebugPhysics.AddForce(rigidbody rb etc...)

  public static Color HitColor = Color.green;
  public static Color NoHitColor = Color.red;
  public static Color HitNormalColor = Color.cyan;

  private static float drawLineTime = 0.0001f;
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

  public static bool DepthTest = false;
  public static bool DrawHitPoints = true;
  public static bool DrawHitNormals = true;

}
