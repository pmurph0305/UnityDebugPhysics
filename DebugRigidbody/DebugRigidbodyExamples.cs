using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugRigidbodyExamples : MonoBehaviour
{
  private Rigidbody rigidbody;
  public ForceMode ForceMode;
  public Vector3 ForceVector;
  public bool AddForce;
  public bool AddTorque;

  public bool AddExplosionForce;
  public Vector3 ExplosionPositionOffset = -Vector3.one;
  public float ExplosionForce = 10.0f;
  public float ExplosionUpwardsModifier = 0.2f;
  public float ExplosionRadius = 1.0f;
  // Use this for initialization
  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    rigidbody.DebugVelocity();
    if (AddExplosionForce)
    {
      rigidbody.DebugAddExplosionForce(ExplosionForce, transform.position + ExplosionPositionOffset, ExplosionRadius, ExplosionUpwardsModifier, ForceMode);
    }
    if (AddForce)
    {
      rigidbody.DebugAddForce(ForceVector, ForceMode);
    }
    if (AddTorque)
    {
      rigidbody.DebugAddTorque(ForceVector, ForceMode);
    }
  }
}
