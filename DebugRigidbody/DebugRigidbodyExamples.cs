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
  // Use this for initialization
  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    if (AddForce)
    {
      rigidbody.DebugAddForce(ForceVector, ForceMode);

    }
  }
}
