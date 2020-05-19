using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVectorExamples : MonoBehaviour
{
  public Vector3 test;

  public DebugVector3 vector1 = DebugVector3.up;
  public DebugVector3 vector2 = DebugVector3.right;

  public float multDiv = 2.0f;
  // public Vector3 test;
  public bool DoDebugAdd = false;
  public bool DoDebugMinus = false;

  public bool DoDebugMultiply = false;
  public bool DoDebugDivide = false;

  public bool DoDebugDot = false;
  public bool DoDebugCross = false;

  // Use this for initialization
  void Start()
  {
    vector1.t = transform;
    vector2.t = transform;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 t = Vector3.zero;
    DebugVector3 result = DebugVector3.zero;
    float f = 0.0f;
    if (DoDebugAdd)
    {
      result = vector1 + vector2;
    }
    if (DoDebugMinus)
    {
      result = vector1 - vector2;
    }
    if (DoDebugMultiply)
    {
      result = vector1 * multDiv;
    }
    if (DoDebugDivide)
    {
      result = vector1 / multDiv;
    }
    if (DoDebugDot)
    {
      f = DebugVector3.Dot(vector1, vector2);
    }
    if (DoDebugCross)
    {
      result = DebugVector3.Cross(vector1, vector2);
      // Vector3 n = DebugVector3.Cross(vector1, vector2);
    }
  }
}
