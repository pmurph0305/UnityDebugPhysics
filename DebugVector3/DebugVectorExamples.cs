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

  public float MultiplierDivider = 1.5f;

  public bool DoDebugAdd = false;
  public bool DoDebugMinus = false;

  public bool DoDebugMultiply = false;
  public bool DoDebugDivide = false;


  public bool DoAngle = false;
  public bool DoClampMagnitude = false;
  public float MaxMagnitude = 0.5f;
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

  public bool DoDebugReflect = false;

  public bool DoDebugRotateTowards = false;
  public float MaxRotateDelta = 0.2f;
  public float MaxMagnitudeDelta = 1.0f;

  public bool DoDebugScale = false;

  public bool DoDebugSignedAngle = false;
  public Vector3 signedAngleAxis;

  public bool DoDebugSlerp = false;
  public float Slerp = 0.5f;

  public bool DoDebugSlerpUnclamped = false;

  public bool DoDebugSmoothDamp = false;

  public float SmoothDampTime = 0.5f;
  public Vector3 SmoothDampVel = Vector3.one;
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
      result = vector1 * MultiplierDivider;
    }

    if (DoDebugDivide)
    {
      result = vector1 / MultiplierDivider;
    }

    if (DoAngle)
    {
      f = DebugVector3.Angle(vector1, vector2);
    }

    if (DoClampMagnitude)
    {
      result = DebugVector3.ClampMagnitude(vector1, MaxMagnitude);
    }

    if (DoDebugCross)
    {
      result = DebugVector3.Cross(vector1, vector2);
    }

    if (DoDebugDistance)
    {
      f = DebugVector3.Distance(vector1, vector2);
    }

    if (DoDebugDot)
    {
      f = DebugVector3.Dot(vector1, vector2);
    }

    if (DoDebugLerp)
    {
      result = DebugVector3.Lerp(vector1, vector2, Lerp);
    }

    if (DoDebugLerpUnclamped)
    {
      result = DebugVector3.LerpUnclamped(vector1, vector2, Lerp);
    }

    if (DoDebugMax)
    {
      result = DebugVector3.Max(vector1, vector2);
    }

    if (DoDebugMin)
    {
      result = DebugVector3.Min(vector1, vector2);
    }

    if (DoDebugMoveTowards)
    {
      result = DebugVector3.MoveTowards(vector1, vector2, MoveTowardsDistance);
    }

    if (DoDebugStaticNormalize)
    {
      result = DebugVector3.Normalize(vector1);
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
      result = DebugVector3.Project(vector1, vector2);
    }

    if (DoDebugProjectOnPlane)
    {
      result = (DebugVector3)DebugVector3.ProjectOnPlane(vector1, PlaneNormal);
    }

    if (DoDebugReflect)
    {
      result = DebugVector3.Reflect(vector1, PlaneNormal);
    }

    if (DoDebugRotateTowards)
    {
      result = DebugVector3.RotateTowards(vector1, vector2, MaxRotateDelta, MaxMagnitudeDelta);
    }

    if (DoDebugScale)
    {
      result = DebugVector3.Scale(vector1, vector2);
    }

    if (DoDebugSignedAngle)
    {
      f = DebugVector3.SignedAngle(vector1, vector2, signedAngleAxis);
    }

    if (DoDebugSlerp)
    {
      result = DebugVector3.Slerp(vector1, vector2, Slerp);
    }

    if (DoDebugSlerpUnclamped)
    {
      result = DebugVector3.SlerpUnclamped(vector1, vector2, Slerp);
    }

    if (DoDebugSmoothDamp)
    {
      result = DebugVector3.SmoothDamp(vector1, vector2, ref SmoothDampVel, SmoothDampTime);
    }
  }
}
