using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugRigidbodyExamples : MonoBehaviour
{
  private Rigidbody rigidbody;

  public ForceMode ForceMode;
  public Vector3 ForceVector;
  public Vector3 PositionOffset = -Vector3.one;
  public float MinForceVectorLength = 0.0f;
  public bool AddForce;
  public bool AddTorque;
  public bool AddExplosionForce;
  public float ExplosionForce = 10.0f;
  public float ExplosionUpwardsModifier = 0.2f;
  public float ExplosionRadius = 1.0f;
  public bool AddForceAtPostion;
  // Use this for initialization
  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    DebugRigidbody.MinForceVectorLength = MinForceVectorLength;
    rigidbody.DebugVelocity();
    if (AddExplosionForce)
    {
      rigidbody.DebugAddExplosionForce(ExplosionForce, transform.position + PositionOffset, ExplosionRadius, ExplosionUpwardsModifier, ForceMode);
    }
    if (AddForce)
    {
      rigidbody.DebugAddForce(ForceVector, ForceMode);
    }
    if (AddTorque)
    {
      rigidbody.DebugAddTorque(ForceVector, ForceMode);
    }
    if (AddForceAtPostion)
    {
      rigidbody.DebugAddForceAtPosition(ForceVector, transform.position + PositionOffset, ForceMode);
    }
  }
}
