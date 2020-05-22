using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

  public Vector3 lookAtPosition;
  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    DebugDraw.DrawPoint(lookAtPosition, Color.red, Time.deltaTime);
    Camera.main.transform.LookAt(this.transform.position + lookAtPosition);
  }
}
