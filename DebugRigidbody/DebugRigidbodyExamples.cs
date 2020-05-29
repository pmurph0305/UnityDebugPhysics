using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugRigidbodyExamples : MonoBehaviour
{
  private Rigidbody m_rigidbody;
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

  public bool AddForceRelative;
  public bool AddTorqueRelative;

  public bool ClosestPointOnBounds;
  public bool GetPointVelocity;
  public bool GetRelativePointVelocity;

  public bool Sweep;
  public Vector3 SweepDirection = Vector3.forward;
  public bool SweepAll;
  // Use this for initialization
  void Start()
  {
    m_rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    DebugRigidbody.MaxDrawSweepDistance = 10f;
    DebugRigidbody.MinForceVectorLength = MinForceVectorLength;
    m_rigidbody.DebugVelocity();
    if (AddExplosionForce)
    {
      m_rigidbody.DebugAddExplosionForce(ExplosionForce, transform.position + PositionOffset, ExplosionRadius, ExplosionUpwardsModifier, ForceMode);
    }
    if (AddForce)
    {
      m_rigidbody.DebugAddForce(ForceVector, ForceMode);
    }
    if (AddTorque)
    {
      m_rigidbody.DebugAddTorque(ForceVector, ForceMode);
    }
    if (AddForceAtPostion)
    {
      m_rigidbody.DebugAddForceAtPosition(ForceVector, transform.position + PositionOffset, ForceMode);
    }
    if (AddForceRelative)
    {
      m_rigidbody.DebugAddRelativeForce(ForceVector, ForceMode);
    }
    if (AddTorqueRelative)
    {
      m_rigidbody.DebugAddRelativeTorque(ForceVector, ForceMode);
    }
    if (ClosestPointOnBounds)
    {
      m_rigidbody.DebugClosestPointOnBounds(PositionOffset);
    }
    if (GetPointVelocity)
    {
      m_rigidbody.DebugGetPointVelocity(transform.TransformPoint(PositionOffset));
    }
    if (GetRelativePointVelocity)
    {
      m_rigidbody.DebugGetRelativePointVelocity(PositionOffset);
    }
    if (Sweep)
    {
      RaycastHit hit;
      if (m_rigidbody.DebugSweepTest(SweepDirection, out hit))
      {
        // do something
      }
    }
    if (SweepAll)
    {
      RaycastHit[] hits = m_rigidbody.DebugSweepTestAll(SweepDirection);
      foreach (RaycastHit hit in hits)
      {
        // do something
      }
    }
  }
}
