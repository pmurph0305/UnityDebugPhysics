﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPhysicsExample : MonoBehaviour
{
  public float RayCastLength = 100.0f;
  public float SphereCastRadius = 0.5f;
  public bool DoRaycast = false;
  public bool DoRaycastHit = false;
  public bool DoRayCastAll = false;
  public bool DoRayCastAllNonAlloc = false;
  public bool DoSphereCast = false;
  public bool DoSphereCastHit = false;
  public bool DoSphereCastAll = false;
  public bool DoSphereCastAllNonAlloc = false;

  public bool DoLinecast = false;
  public bool DoLinecastHit = false;
  // Update is called once per frame
  void Update()
  {
    if (DoRaycast)
    {
      if (DebugPhysics.Raycast(transform.position, transform.forward, RayCastLength))
      {
      }
    }
    if (DoRaycastHit)
    {
      RaycastHit hit;
      if (DebugPhysics.Raycast(transform.position, transform.forward, out hit, RayCastLength))
      {
      }
    }
    if (DoRayCastAll)
    {
      RaycastHit[] hits = DebugPhysics.RaycastAll(transform.position, transform.forward, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        //do something
      }
    }
    if (DoRayCastAllNonAlloc)
    {
      RaycastHit[] hits = new RaycastHit[10];
      DebugPhysics.RaycastNonAlloc(transform.position, transform.forward, hits, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        //do something
      }
    }


    if (DoSphereCast)
    {
      if (DebugPhysics.SphereCast(new Ray(this.transform.position, this.transform.forward), SphereCastRadius, RayCastLength))
      {

      }
    }
    if (DoSphereCastHit)
    {
      RaycastHit hit;
      if (DebugPhysics.SphereCast(transform.position, SphereCastRadius, this.transform.forward, out hit, RayCastLength))
      {
      }
    }
    if (DoSphereCastAll)
    {
      RaycastHit[] hits = DebugPhysics.SphereCastAll(transform.position, SphereCastRadius, this.transform.forward, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        // do something
      }
    }
    if (DoSphereCastAllNonAlloc)
    {
      RaycastHit[] hits = new RaycastHit[10];
      DebugPhysics.SphereCastNonAlloc(transform.position, SphereCastRadius, transform.forward, hits, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        // do something
      }
    }

    if (DoLinecast)
    {
      if (DebugPhysics.Linecast(transform.position, transform.position + transform.forward * RayCastLength))
      {
        // do something
      }
    }
    if (DoLinecastHit)
    {
      RaycastHit hit;
      if (DebugPhysics.Linecast(transform.position, transform.position + transform.forward * RayCastLength, out hit))
      {
        // do something
      }
    }
  }
}
