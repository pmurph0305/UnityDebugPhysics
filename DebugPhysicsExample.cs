using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPhysicsExample : MonoBehaviour
{

  public bool doSphereCast = false;
  public bool doRaycast = true;

  public bool doVector = true;

  public float RayCastLength = 1.0f;
  public float SphereCastRadius = 0.5f;


  // Update is called once per frame
  void Update()
  {
    RaycastHit hit;
    if (doRaycast)
    {
      if (DebugPhysics.Raycast(transform.position, this.transform.forward, out hit, RayCastLength))
      {

      }
    }
    if (doSphereCast)
    {
      if (DebugPhysics.SphereCast(transform.position, SphereCastRadius, this.transform.forward, out hit, RayCastLength))
      {

      }
    }
    if (doVector)
    {
      DebugDraw.DrawVector(transform.position, transform.forward, RayCastLength, Color.yellow, Time.deltaTime);
    }
  }
}
