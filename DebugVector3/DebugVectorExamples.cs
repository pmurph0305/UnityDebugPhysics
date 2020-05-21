using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVectorExamples : MonoBehaviour
{
  public bool ShowMethodResult = true;
  public bool ShowMethodInputs = true;
  public bool ShowOperatorResult = true;
  public bool ShowOperatorInputs = true;
  public Vector3 test;

  public DebugVector3 vector1 = DebugVector3.up;
  public DebugVector3 vector2 = DebugVector3.right;

  public float multDiv = 2.0f;
  // public Vector3 test;
  public bool DoDebugAdd = false;
  public bool DoDebugMinus = false;

  public bool DoDebugMultiply = false;
  public bool DoDebugDivide = false;


  public bool DoAngle = false;
  public bool DoClampMagnitude = false;
  public float MaxMagnitude;
  public bool DoDebugCross = false;
  public bool DoDebugDistance = false;
  public bool DoDebugDot = false;
  public bool DoDebugLerp = false;
  public float Lerp = 0.5f;
  public bool DoDebugLerpUnclamped = false;

  public bool DoDebugMax = false;
  public bool DoDebugMin = false;
  public bool DoDebugMoveTowards = false;
  public float MoveTowardsDistance = 0.25f;

  public bool DoDebugStaticNormalize = false;
  public bool DoDebugInstNormalize = false;

  public bool DoOrthoNormalize = false;

  public bool DoDebugProject = false;

  public bool DoDebugProjectOnPlane = false;
  public Vector3 PlaneNormal = Vector3.up;

  // Update is called once per frame
  void Update()
  {
    DebugVector3.ShowOperatorResult = ShowOperatorResult;
    DebugVector3.ShowOperatorInputs = ShowOperatorInputs;
    DebugVector3.ShowMethodInputs = ShowMethodInputs;
    DebugVector3.ShowMethodResult = ShowMethodResult;
    DebugVector3.origin = transform.position;
    Vector3 t = Vector3.one;
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

    if (DoAngle)
    {
      f = DebugVector3.Angle(vector1, Vector3.one);
      // float r0 = DebugVector3.Angle(vector1, Vector3.right);
      // float r2 = DebugVector3.Angle(Vector3.right, vector1);
      // float r1 = DebugVector3.Angle(Vector3.one, Vector3.right);
      // f = Vector3.Angle(vector1, vector2);
    }
    if (DoClampMagnitude)
    {
      result = DebugVector3.ClampMagnitude(vector1, MaxMagnitude);
      // t = Vector3.ClampMagnitude(vector1, MaxMagnitude);
    }
    if (DoDebugCross)
    {
      result = DebugVector3.Cross(vector1, vector2);
      // t = Vector3.Cross(vector1, vector2);
    }
    if (DoDebugDistance)
    {
      f = DebugVector3.Distance(vector1, vector2);
      // f = DebugVector3.Distance(Vector3.right, Vector3.up);
      // f = Vector3.Distance(vector1, vector2);
    }
    if (DoDebugDot)
    {
      f = DebugVector3.Dot(vector1, vector2);
      // f = Vector3.Dot(vector1, vector2);
    }
    if (DoDebugLerp)
    {
      result = DebugVector3.Lerp(vector1, vector2, Lerp);
      // t = Vector3.Lerp(vector1, vector2, Lerp);
    }
    if (DoDebugLerpUnclamped)
    {
      result = DebugVector3.LerpUnclamped(vector1, vector2, Lerp);
      // t = Vector3.LerpUnclamped(vector1, vector2, Lerp);
    }
    if (DoDebugMax)
    {
      result = DebugVector3.Max(vector1, vector2);
      // t = Vector3.Max(vector1, vector2);
    }
    if (DoDebugMin)
    {
      result = DebugVector3.Min(vector1, vector2);
      // t = Vector3.Min(vector1, vector2);
    }
    if (DoDebugMoveTowards)
    {
      result = DebugVector3.MoveTowards(vector1, vector2, MoveTowardsDistance);
      // t = Vector3.MoveTowards(vector1, vector2, MoveTowardsDistance);
    }
    if (DoDebugStaticNormalize)
    {
      result = DebugVector3.Normalize(vector1);
      // result = vector1;
      // // Vector3 t1 = DebugVector3.Normalize(this.transform.position);
      // DebugVector3 t2 = DebugVector3.Normalize(this.transform.position + result);
    }
    if (DoDebugInstNormalize)
    {
      vector1.Normalize();
      DoDebugInstNormalize = false;
    }
    if (DoOrthoNormalize)
    {
      Debug.Log("b:" + vector1 + " : " + vector2);
      DebugVector3.OrthoNormalize(ref vector1, ref vector2);
      Debug.Log("a:" + vector1 + " : " + vector2);
      DoOrthoNormalize = false;

      Vector3 v1 = Vector3.one;
      DebugVector3 v2 = new DebugVector3(0.5f, 0.1f, 3.2f);
      Debug.Log("v1b:" + v1 + " v2b:" + v2);
      DebugVector3.OrthoNormalize(ref v2, ref v1);
      Debug.Log("v1a:" + v1 + " v2a:" + v2);

    }

    if (DoDebugProject)
    {
      Vector3 r = DebugVector3.Project(vector1, vector2);
      DebugVector3 r2 = (DebugVector3)DebugVector3.Project(vector1, vector2);
      Vector3 a = DebugVector3.Project(vector1, Vector3.one);
    }

    if (DoDebugProjectOnPlane)
    {
      result = (DebugVector3)DebugVector3.ProjectOnPlane(vector1, PlaneNormal);
    }
  }
}
