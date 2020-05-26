using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensionMethods
{

  /// <summary>
  /// Adds a force to the rigidbody
  /// </summary>
  /// <param name="force">Force vector in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddForce(this Rigidbody rigidbody, Vector3 force, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, force, mode);
    rigidbody.AddForce(force, mode);
  }

  /// <summary>
  /// Adds a force to the rigidbody
  /// </summary>
  /// <param name="x">Size of force along world x-axis</param>
  /// <param name="y">Size of force along world y-axis</param>
  /// <param name="z">Size of force along world z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void AddForce(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, new Vector3(x, y, z), mode);
  }

  /// <summary>
  /// Adds a torque to the rigidbody
  /// </summary>
  /// <param name="torque">Torque vector in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddTorque(this Rigidbody rigidbody, Vector3 torque, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, torque, mode);
    rigidbody.AddTorque(torque, mode);
  }

  /// <summary>
  /// Adds a torque to the rigidbody
  /// </summary>
  /// <param name="x">Size of torque along world x-axis</param>
  /// <param name="y">Size of torque along world y-axis</param>
  /// <param name="z">Size of torque along world z-axis</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddTorque(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawTorque(rigidbody, rigidbody.worldCenterOfMass, new Vector3(x, y, z), mode);
    rigidbody.AddTorque(x, y, z, mode);
  }
}
