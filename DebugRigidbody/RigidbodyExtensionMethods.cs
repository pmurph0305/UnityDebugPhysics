using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensionMethods
{
  /// <summary>
  /// Applies a force to a rigidbody that simulates explosion effects
  /// </summary>
  /// <param name="explosionForce">The force of the explosion (this may be modified by distance)</param>
  /// <param name="explosionPosition">Center of the sphere within which the explosion has an effect</param>
  /// <param name="explosionRadius">Radius of the sphere within which the explosion effects</param>
  /// <param name="upwardsModifier">Adjustment to position of the explosion to make it seem like it lifts things</param>
  /// <param name="mode">The type of force to apply</param>
  public static void DebugAddExplosionForce(this Rigidbody rigidbody, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawExplosionForce(rigidbody, explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
    rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);

  }

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
  public static void DebugAddForce(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForce(rigidbody, rigidbody.worldCenterOfMass, new Vector3(x, y, z), mode);
    rigidbody.AddForce(x, y, z, mode);
  }

  /// <summary>
  /// Applies force at position.static As a result this will apply a torque and force on the object.
  /// </summary>
  /// <param name="force">Force vector in world coodrinates</param>
  /// <param name="position">Position to apply force at in world coordinates</param>
  /// <param name="mode">Type of force to apply</param>
  public static void DebugAddForceAtPosition(this Rigidbody rigidbody, Vector3 force, Vector3 position, ForceMode mode = ForceMode.Force)
  {
    DebugRigidbody.DrawForceAtPostion(rigidbody, force, position, mode);
    rigidbody.AddForceAtPosition(force, position, mode);
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

  /// <summary>
  /// Draws the velocity vector of the rigidbody
  /// </summary>
  public static void DebugVelocity(this Rigidbody rigidbody)
  {
    DebugRigidbody.DrawVelocity(rigidbody);
  }

}
