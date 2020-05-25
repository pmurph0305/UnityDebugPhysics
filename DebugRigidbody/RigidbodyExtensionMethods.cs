using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensionMethods
{

  public static void DebugAddForce(this Rigidbody rigidbody, Vector3 force, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, force, mode);
    rigidbody.AddForce(force, mode);
  }

}
