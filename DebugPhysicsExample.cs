using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPhysicsExample : MonoBehaviour
{
  public Vector3 BoxCastHalfExtents = new Vector3(0.5f, 0.5f, 0.5f);
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

  public bool DoBoxCast = false;
  public bool DoBoxCastHit = false;

  public bool DoBoxCastAll = false;
  public bool DoBoxCastAllNonAlloc = false;

  public bool DoCapsuleCast = false;

  public bool DoCapsuleCastHit = false;
  public bool DoCapsuleCastAll = false;
  public bool DoCapsuleCastNonAlloc = false;

  public bool DoBoxCheck = false;
  public bool DoCapsuleCheck = false;
  public bool DoSphereCheck = false;


  public bool DoBoxOverlap = false;
  public bool DoBoxOverlapNonAlloc = false;
  public bool DoCapsuleOverlap = false;
  public bool DoCapsuleOverlapNonAlloc = false;
  public bool DoSphereOverlap = false;
  public bool DoSphereOverlapNonAlloc = false;
  // Update is called once per frame
  void Update()
  {

    // Raycasts.
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

    if (DoBoxCast)
    {
      if (DebugPhysics.BoxCast(transform.position, BoxCastHalfExtents, transform.forward, transform.rotation, RayCastLength))
      {
        // do something
      }
    }

    if (DoBoxCastHit)
    {
      RaycastHit hit;
      if (DebugPhysics.BoxCast(transform.position, BoxCastHalfExtents, transform.forward, out hit, transform.rotation, RayCastLength))
      {
        // do something
      }
    }

    if (DoBoxCastAll)
    {
      RaycastHit[] hits = DebugPhysics.BoxCastAll(transform.position, BoxCastHalfExtents, transform.forward, transform.rotation, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        // do something
      }
    }

    if (DoBoxCastAllNonAlloc)
    {
      RaycastHit[] hits = new RaycastHit[10];
      int numHits = DebugPhysics.BoxCastNonAlloc(transform.position, BoxCastHalfExtents, transform.forward, hits, transform.rotation, RayCastLength);
      if (numHits > 0)
      {
        foreach (RaycastHit hit in hits)
        {
          // do something
        }
      }
    }

    if (DoCapsuleCast)
    {
      if (DebugPhysics.CapsuleCast(transform.position, transform.position + point2, SphereCastRadius, transform.forward, RayCastLength))
      {
        // do something
      }
    }

    if (DoCapsuleCastHit)
    {
      RaycastHit hit;
      if (DebugPhysics.CapsuleCast(transform.position, transform.position + point2, SphereCastRadius, transform.forward, out hit, RayCastLength))
      {
        // do something
      }
    }

    if (DoCapsuleCastAll)
    {
      RaycastHit[] hits = DebugPhysics.CapsuleCastAll(transform.position, transform.position + point2, SphereCastRadius, transform.forward, RayCastLength);
      foreach (RaycastHit hit in hits)
      {
        // do something
      }
    }

    if (DoCapsuleCastNonAlloc)
    {
      RaycastHit[] hits = new RaycastHit[10];
      int numHits = DebugPhysics.CapsuleCastNonAlloc(transform.position, transform.position + point2, SphereCastRadius, transform.forward, hits, RayCastLength);
      if (numHits > 0)
      {
        foreach (RaycastHit hit in hits)
        {
          // do something
        }
      }
    }


    // BoxCapsuleSphere Check
    if (DoBoxCheck)
    {
      if (DebugPhysics.CheckBox(transform.position, BoxCastHalfExtents, transform.rotation))
      {

      }
    }
    if (DoCapsuleCheck)
    {
      if (DebugPhysics.CheckCapsule(transform.position, transform.position + point2, SphereCastRadius))
      {

      }
    }
    if (DoSphereCheck)
    {
      if (DebugPhysics.CheckSphere(transform.position, SphereCastRadius))
      {

      }
    }

    // Overlaps
    if (DoBoxOverlap)
    {
      Collider[] colliders = DebugPhysics.OverlapBox(transform.position, BoxCastHalfExtents, transform.rotation);
      if (colliders.Length > 0)
      {
        // do something
      }
    }
    if (DoBoxOverlapNonAlloc)
    {
      Collider[] colliders = new Collider[8];
      DebugPhysics.OverlapBoxNonAlloc(transform.position, BoxCastHalfExtents, colliders, transform.rotation);
      foreach (Collider c in colliders)
      {
        // do something

      }
    }
    if (DoSphereOverlap)
    {
      Collider[] colliders = DebugPhysics.OverlapSphere(transform.position, SphereCastRadius);
      if (colliders.Length > 0)
      {
        // do something
      }
    }
    if (DoSphereOverlapNonAlloc)
    {
      Collider[] colliders = new Collider[8];
      DebugPhysics.OverlapSphereNonAlloc(transform.position, SphereCastRadius, colliders);
      foreach (Collider c in colliders)
      {
        // do something
      }
    }
    if (DoCapsuleOverlap)
    {
      Collider[] colliders = DebugPhysics.OverlapCapsule(transform.position, transform.position + point2, SphereCastRadius);
      if (colliders.Length > 0)
      {
        // do something
      }
    }
    if (DoCapsuleOverlapNonAlloc)
    {
      Collider[] colliders = new Collider[8];
      DebugPhysics.OverlapCapsuleNonAlloc(transform.position, transform.position + point2, SphereCastRadius, colliders);
      foreach (Collider c in colliders)
      {
        // do something
      }
    }
  }

  public Vector3 point2 = Vector3.up;
}
