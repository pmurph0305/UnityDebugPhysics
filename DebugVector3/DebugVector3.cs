using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// don't think the extra debugvector3, vector3 and vector3, debugvector3 methods are needed. (except where ref is used)
// just one debugvector3, debugvector3 and one vector3, vector3 covers all cases

// Why not extension methods?
// to have the custom operators
// and since all the methods will also have the static methods that vector3 has,
// the result is the same, just has the option to do more.
// primarily for the static origin property
// so you can set DebugVector3.origin to transform.position
// and view it more easily, as opposed to everything displaying at the world origin, which is less useful in my opinion

// static vs instance for origin
// + static seems easier as you can just set it to transform.postion, or whereever, before the call.
// - someone might forget to set the origin in the current script, but set it in a different script
// Alternatives? instance transform to set and use instead of origin if it is set

// TODO: rest of the methods
// alternative methods for passing in a vector3 instead of a debug vector3
// reduce as much casting for everything as possible.
// option to disable drawing of basic operators

[System.Serializable]
public struct DebugVector3
{
  public static bool ShowOperatorInputs = true;
  public static bool ShowOperatorResult = true;
  public static bool ShowMethodResult = true;
  public static bool ShowMethodInputs = true;
  public static float DrawLineTime = 0.0001f;
  public static float DrawPermanentChangeTime = 5.0f;
  public static Color DrawVectorColorA = Color.yellow;
  public static Color DrawVectorColorB = Color.blue;
  public static Color DrawVectorColorC = Color.magenta;
  public static Color DrawResultColor = Color.green;
  public static bool DepthTest = true;
  public static bool DrawVectorsWithArrows = true;
  public static float DrawVectorArrowScale = 0.1f;

  // static variables from normal Vector3

  /// <summary>
  /// Shorthand for writing DebugVector3(0, 0, 1)
  /// </summary>
  public static DebugVector3 forward = new DebugVector3(0, 0, 1);
  /// <summary>
  /// Shorthand for writing DebugVector3(0, 0, -1)
  /// </summary>
  public static DebugVector3 back = new DebugVector3(0, 0, -1);
  /// <summary>
  /// Shorthand for writing DebugVector3(0, 1, 0)
  /// </summary>
  public static DebugVector3 up = new DebugVector3(0, 1, 0);
  /// <summary>
  /// Shorthand for writing new DebugVector3(0, -1, 0)
  /// </summary>
  public static DebugVector3 down = new DebugVector3(0, -1, 0);
  /// <summary>
  /// Shorthand for writing new DebugVector3(1, 0, 0)
  /// </summary>
  public static DebugVector3 right = new DebugVector3(1, 0, 0);
  /// <summary>
  /// Shorthand for writing DebugVector3(-1, 0, 0)
  /// </summary>
  public static DebugVector3 left = new DebugVector3(-1, 0, 0);
  /// <summary>
  /// Shorthand for writing DebugVector3(0, 0, 0)
  /// </summary>
  public static DebugVector3 zero = new DebugVector3(0, 0, 0);
  /// <summary>
  /// Shorthand for writing DebugVector3(1, 1, 1)
  /// </summary>
  public static DebugVector3 one = new DebugVector3(1, 1, 1);
  /// <summary>
  /// Shorthand for writing DebugVector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity)
  /// </summary>
  public static DebugVector3 negativeInfinity = new DebugVector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
  /// <summary>
  /// Shorthand for writing DebugVector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity)
  /// </summary>
  public static DebugVector3 positiveInfinity = new DebugVector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);


  public static bool DrawDotAsProjectionAonB = true;
  public DebugVector3(float x, float y, float z)
  {
    v3 = new Vector3(x, y, z);
  }

  public DebugVector3(Vector3 vector)
  {
    v3 = vector;
  }

  public static Vector3 origin;

  [SerializeField]
  private Vector3 v3;

  public float x
  {
    get { return v3.x; }
    set { v3.x = value; }
  }

  public float y
  {
    get { return v3.y; }
    set { v3.y = value; }
  }

  public float z
  {
    get { return v3.z; }
    set { v3.z = value; }
  }

  public float magnitude
  {
    get { return v3.magnitude; }
  }

  public DebugVector3 normalized
  {
    get { return (DebugVector3)v3.normalized; }
  }

  public float sqrMagnitude
  {
    get { return v3.sqrMagnitude; }
  }

  public float this[int index]
  {
    get
    {
      if (index == 0) return v3.x;
      if (index == 1) return v3.y;
      if (index == 2) return v3.z;
      throw new System.Exception("Cannot access vector property at " + index);
    }
    set
    {
      if (index == 0) { v3.x = value; return; }
      if (index == 1) { v3.y = value; return; }
      if (index == 2) { v3.z = value; return; }
      throw new System.Exception("Cannot access vector property at " + index);
    }
  }





  // Static functions

  // Drawing

  private static void DrawVector(Vector3 start, Vector3 end, Color color, float duration)
  {
    // only draw individual if we allow it
    if (!ShowMethodInputs && (color == DrawVectorColorA || color == DrawVectorColorB || color == DrawVectorColorC)) return;
    // only draw result if we want it.
    if (!ShowMethodResult && color == DrawResultColor) return;
    // Debug.DrawLine(start, end, color, DrawLineTime, DepthTest);
    if (DrawVectorsWithArrows)
    {
      DebugDraw.DrawVector(start, end, color, DrawVectorArrowScale, duration, DepthTest);
    }
    else
    {
      DebugDraw.DrawLine(start, end, color, duration, DepthTest);
    }
  }

  private static void DrawVectorOperator(Vector3 start, Vector3 end, Color color)
  {
    if (!ShowOperatorInputs && (color == DrawVectorColorA || color == DrawVectorColorB || color == DrawVectorColorC)) return;
    // only draw result if we want it.
    if (!ShowOperatorResult && color == DrawResultColor) return;
    // Debug.DrawLine(start, end, color, DrawLineTime, DepthTest);
    if (DrawVectorsWithArrows)
    {
      DebugDraw.DrawVector(start, end, color, DrawVectorArrowScale, DrawLineTime, DepthTest);
    }
    else
    {
      DebugDraw.DrawLine(start, end, color, DrawLineTime, DepthTest);
    }
  }

  private static void DrawVector(Vector3 start, Vector3 end, Color color)
  {
    DrawVector(start, end, color, DrawLineTime);
  }


  // Static Methods


  /// <summary>
  /// Calculates the smaller of the two possible angles between from and to
  /// </summary>
  /// <param name="from">The vector from which the angular distance is measured</param>
  /// <param name="to">The Vector to which the angular distance is measured</param>
  /// <returns>Smallest angle between from and to</returns>
  public static float Angle(DebugVector3 from, DebugVector3 to)
  {
    float angle = Vector3.Angle(from, to);
    DrawVector(origin, origin + from.v3, DrawVectorColorA);
    DrawVector(origin, origin + to.v3, DrawVectorColorB);
    // draw an arc between the vectors?
    return angle;
  }
  /// <summary>
  /// Calculates the smaller of the two possible angles between from and to
  /// </summary>
  /// <param name="from">The vector from which the angular distance is measured</param>
  /// <param name="to">The Vector to which the angular distance is measured</param>
  /// <returns>Smallest angle between from and to</returns>
  public static float Angle(DebugVector3 from, Vector3 to)
  {
    return Angle(from, (DebugVector3)to);
  }
  /// <summary>
  /// Calculates the smaller of the two possible angles between from and to
  /// </summary>
  /// <param name="from">The vector from which the angular distance is measured</param>
  /// <param name="to">The Vector to which the angular distance is measured</param>
  /// <returns>Smallest angle between from and to</returns>
  public static float Angle(Vector3 from, Vector3 to)
  {
    return Angle((DebugVector3)from, (DebugVector3)to);
  }


  /// <summary>
  /// Returns a copy of vector with its magnitude clamped to max length
  /// </summary>
  /// <param name="vector">Vector to clamp</param>
  /// <param name="maxLength">max length to clamp to</param>
  /// <returns>Vector in same direction clamped to maxLength</returns>
  public static DebugVector3 ClampMagnitude(DebugVector3 vector, float maxLength)
  {
    DebugVector3 result = (DebugVector3)Vector3.ClampMagnitude(vector, maxLength);
    DrawVector(origin, origin + vector.v3, DrawVectorColorA);
    DrawVector(origin, origin + result.v3, DrawResultColor);
    return result;
  }
  /// <summary>
  /// Returns a copy of vector with its magnitude clamped to max length
  /// </summary>
  /// <param name="vector">Vector to clamp</param>
  /// <param name="maxLength">max length to clamp to</param>
  /// <returns>Vector in same direction clamped to maxLength</returns>
  public static DebugVector3 ClampMagnitude(Vector3 vector, float maxLength)
  {
    return ClampMagnitude((DebugVector3)vector, maxLength);
  }


  /// <summary>
  /// Cross product of two vectors, the vector perpindicular to the two input vectors
  /// </summary>
  /// <param name="lhs">Left hand side of cross (or thumb of left hand)</param>
  /// <param name="rhs">Right hand side of cross (or index of left hand)</param>
  /// <returns>The vector perpindicular to the two input vectors using left hand rule</returns>
  public static DebugVector3 Cross(DebugVector3 lhs, DebugVector3 rhs)
  {
    Vector3 result = Vector3.Cross(lhs, rhs);
    DrawVector(origin, origin + lhs.v3, DrawVectorColorA);
    DrawVector(origin, origin + rhs.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Cross product of two vectors, the vector perpindicular to the two input vectors
  /// </summary>
  /// <param name="lhs">Left hand side of cross (or thumb of left hand)</param>
  /// <param name="rhs">Right hand side of cross (or index of left hand)</param>
  /// <returns>The vector perpindicular to the two input vectors using left hand rule</returns>
  public static DebugVector3 Cross(DebugVector3 lhs, Vector3 rhs)
  {
    return Cross(lhs, (DebugVector3)rhs);
  }
  /// <summary>
  /// Cross product of two vectors, the vector perpindicular to the two input vectors
  /// </summary>
  /// <param name="lhs">Left hand side of cross (or thumb of left hand)</param>
  /// <param name="rhs">Right hand side of cross (or index of left hand)</param>
  /// <returns>The vector perpindicular to the two input vectors using left hand rule</returns>
  public static DebugVector3 Cross(Vector3 lhs, Vector3 rhs)
  {
    return Cross((DebugVector3)lhs, (DebugVector3)rhs);
  }


  /// <summary>
  /// Calculates the distance between a and b
  /// </summary>
  /// <param name="a">Vector A</param>
  /// <param name="b">Vector B</param>
  /// <returns>Distance between Vector A and Vector B</returns>
  public static float Distance(DebugVector3 a, DebugVector3 b)
  {
    float result = Vector3.Distance(a, b);
    DrawVector(origin, origin + a.v3, DrawVectorColorA);
    DrawVector(origin, origin + b.v3, DrawVectorColorB);
    DebugDraw.DrawLine(origin + a.v3, origin + b.v3, DrawResultColor, DrawLineTime, DepthTest);
    return result;
  }
  public static float Distance(DebugVector3 a, Vector3 b)
  {
    return Distance(a, (DebugVector3)b);
  }
  public static float Distance(Vector3 a, Vector3 b)
  {
    return Distance((DebugVector3)a, (DebugVector3)b);
  }


  /// <summary>
  /// Calculates the dot product of two vectors
  /// </summary>
  /// <param name="lhs">Left hand size of dot</param>
  /// <param name="rhs">Right hand size of dot</param>
  /// <returns>The dot product of two vectors</returns>
  public static float Dot(DebugVector3 lhs, DebugVector3 rhs)
  {
    DrawVector(origin, origin + lhs.v3, DrawVectorColorA);
    DrawVector(origin, origin + rhs.v3, DrawVectorColorB);
    if (DrawDotAsProjectionAonB)
    {
      Vector3 f = Vector3.Project(lhs.v3, rhs.v3.normalized);
      DrawVector(origin, origin + f, DrawResultColor);
    }
    return Vector3.Dot(lhs, rhs);
  }
  /// <summary>
  /// Calculates the dot product of two vectors
  /// </summary>
  /// <param name="lhs">Left hand size of dot</param>
  /// <param name="rhs">Right hand size of dot</param>
  /// <returns>The dot product of two vectors</returns>
  public static float Dot(DebugVector3 lhs, Vector3 rhs)
  {
    return Dot(lhs, (DebugVector3)rhs);
  }
  /// <summary>
  /// Calculates the dot product of two vectors
  /// </summary>
  /// <param name="lhs">Left hand size of dot</param>
  /// <param name="rhs">Right hand size of dot</param>
  /// <returns>The dot product of two vectors</returns>
  public static float Dot(Vector3 lhs, Vector3 rhs)
  {
    return Dot((DebugVector3)lhs, (DebugVector3)rhs);
  }


  /// <summary>
  /// Linearly interpolates between two points
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 Lerp(DebugVector3 a, DebugVector3 b, float t)
  {
    Vector3 result = Vector3.Lerp(a, b, t);
    DrawVector(origin, origin + a.v3, DrawVectorColorA);
    DrawVector(origin, origin + b.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Linearly interpolates between two points
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 Lerp(DebugVector3 a, Vector3 b, float t)
  {
    return Lerp(a, (DebugVector3)b, t);
  }
  /// <summary>
  /// Linearly interpolates between two points
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 Lerp(Vector3 a, Vector3 b, float t)
  {
    return Lerp((DebugVector3)a, (DebugVector3)b, t);
  }


  /// <summary>
  /// Linearly interpolates between two points where t is unclamped.
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 LerpUnclamped(DebugVector3 a, DebugVector3 b, float t)
  {
    Vector3 result = Vector3.LerpUnclamped(a, b, t);
    DrawVector(origin, origin + a.v3, DrawVectorColorA);
    DrawVector(origin, origin + b.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Linearly interpolates between two points where t is unclamped.
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 LerpUnclamped(DebugVector3 a, Vector3 b, float t)
  {
    return LerpUnclamped(a, (DebugVector3)b, t);
  }
  /// <summary>
  /// Linearly interpolates between two points where t is unclamped.
  /// When t is 0, returns a. When t is 1, returns b.
  /// </summary>
  /// <param name="a">Point A</param>
  /// <param name="b">Point B</param>
  /// <param name="t">Interpolant</param>
  /// <returns>Linear interpolation of a and b by t.</returns>
  public static DebugVector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
  {
    return LerpUnclamped((DebugVector3)a, (DebugVector3)b, t);
  }


  /// <summary>
  /// Returns a vector made from the largest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from largest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Max(DebugVector3 lhs, DebugVector3 rhs)
  {
    Vector3 result = Vector3.Max(lhs, rhs);
    DrawVector(origin, origin + lhs.v3, DrawVectorColorA);
    DrawVector(origin, origin + rhs.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Returns a vector made from the largest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from largest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Max(DebugVector3 lhs, Vector3 rhs)
  {
    return Max(lhs, (DebugVector3)rhs);
  }
  /// <summary>
  /// Returns a vector made from the largest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from largest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Max(Vector3 lhs, Vector3 rhs)
  {
    return Max((DebugVector3)lhs, (DebugVector3)rhs);
  }


  /// <summary>
  /// Returns a vector made from the smallest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from smallest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Min(DebugVector3 lhs, DebugVector3 rhs)
  {
    Vector3 result = Vector3.Min(lhs, rhs);
    DrawVector(origin, origin + lhs.v3, DrawVectorColorA);
    DrawVector(origin, origin + rhs.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Returns a vector made from the smallest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from smallest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Min(DebugVector3 lhs, Vector3 rhs)
  {
    return Min(lhs, (DebugVector3)rhs);
  }
  /// <summary>
  /// Returns a vector made from the smallest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from smallest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Min(Vector3 lhs, Vector3 rhs)
  {
    return Min((DebugVector3)lhs, (DebugVector3)rhs);
  }


  /// <summary>
  /// Calculates a position between the points specified by current and target,
  /// moving no further than the distance specified by maxDistanceDelta.
  /// </summary>
  /// <param name="current">Position to move from</param>
  /// <param name="target">Position to move towards</param>
  /// <param name="maxDistanceDelta">Max distance to move per call</param>
  /// <returns></returns>
  public static DebugVector3 MoveTowards(DebugVector3 current, DebugVector3 target, float maxDistanceDelta)
  {
    Vector3 result = Vector3.MoveTowards(current, target, maxDistanceDelta);
    DrawVector(origin, origin + current.v3, DrawVectorColorA);
    DrawVector(origin, origin + target.v3, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
  /// <summary>
  /// Calculates a position between the points specified by current and target,
  /// moving no further than the distance specified by maxDistanceDelta.
  /// </summary>
  /// <param name="current">Position to move from</param>
  /// <param name="target">Position to move towards</param>
  /// <param name="maxDistanceDelta">Max distance to move per call</param>
  /// <returns></returns>
  public static DebugVector3 MoveTowards(DebugVector3 current, Vector3 target, float maxDistanceDelta)
  {
    return MoveTowards(current, (DebugVector3)target, maxDistanceDelta);
  }
  /// <summary>
  /// Calculates a position between the points specified by current and target,
  /// moving no further than the distance specified by maxDistanceDelta.
  /// </summary>
  /// <param name="current">Position to move from</param>
  /// <param name="target">Position to move towards</param>
  /// <param name="maxDistanceDelta">Max distance to move per call</param>
  /// <returns></returns>
  public static DebugVector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
  {
    return MoveTowards((DebugVector3)current, (DebugVector3)target, maxDistanceDelta);
  }


  /// <summary>
  /// Calculates a vector with a magnitude of 1.
  /// </summary>
  /// <param name="value">Vector to normalize</param>
  /// <returns>Vector with a magnitude of 1</returns>
  public static DebugVector3 Normalize(DebugVector3 value)
  {
    DrawVector(origin, origin + value.v3, DrawVectorColorA);
    DrawVector(origin, origin + value.v3.normalized, DrawResultColor);
    return (DebugVector3)value.v3.normalized;
  }
  /// <summary>
  /// Calculates a vector with a magnitude of 1.
  /// </summary>
  /// <param name="value">Vector to normalize</param>
  /// <returns>Vector with a magnitude of 1</returns>
  public static DebugVector3 Normalize(Vector3 value)
  {
    return Normalize((DebugVector3)value);
  }


  /// <summary>
  /// Makes this vector have a magnitude of 1.
  /// </summary>
  public void Normalize()
  {
    DrawVector(origin, origin + v3, DrawVectorColorA, DrawPermanentChangeTime);
    this.v3 = v3.normalized;
    DrawVector(origin, origin + v3, DrawResultColor, DrawPermanentChangeTime);
  }


  /// <summary>
  /// Makes vectors normalized and orthognal to each other.
  /// </summary>
  /// <param name="normal">Vector to normalize</param>
  /// <param name="tangent">Tangent to make orthogonal to normal</param>
  public static void OrthoNormalize(ref DebugVector3 normal, ref DebugVector3 tangent)
  {
    DrawVector(origin, origin + normal, DrawVectorColorA, DrawPermanentChangeTime);
    DrawVector(origin, origin + tangent, DrawVectorColorB, DrawPermanentChangeTime);
    Vector3.OrthoNormalize(ref normal.v3, ref tangent.v3);
    DrawVector(origin, origin + normal, DrawResultColor, DrawPermanentChangeTime);
    DrawVector(origin, origin + tangent, DrawResultColor, DrawPermanentChangeTime);
  }
  /// <summary>
  /// Makes vectors normalized and orthognal to each other.
  /// </summary>
  /// <param name="normal">Vector to normalize</param>
  /// <param name="tangent">Tangent to make orthogonal to normal</param>
  public static void OrthoNormalize(ref Vector3 normal, ref DebugVector3 tangent)
  {
    DebugVector3 n = (DebugVector3)normal;
    OrthoNormalize(ref n, ref tangent);
    normal = n.v3;
  }
  /// <summary>
  /// Makes vectors normalized and orthognal to each other.
  /// </summary>
  /// <param name="normal">Vector to normalize</param>
  /// <param name="tangent">Tangent to make orthogonal to normal</param>
  public static void OrthoNormalize(ref DebugVector3 normal, ref Vector3 tangent)
  {
    DebugVector3 t = (DebugVector3)tangent;
    OrthoNormalize(ref normal, ref t);
    tangent = t.v3;
  }
  /// <summary>
  /// Makes vectors normalized and orthognal to each other.
  /// </summary>
  /// <param name="normal">Vector to normalize</param>
  /// <param name="tangent">Tangent to make orthogonal to normal</param>
  public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
  {
    DebugVector3 n = (DebugVector3)normal;
    DebugVector3 t = (DebugVector3)tangent;
    OrthoNormalize(ref n, ref t);
    tangent = t.v3;
    normal = n.v3;
  }

  // TODO: other orthonormalize with normal, tangent, binormal
  // for all combinations of vector3 and debugvector3 (8 methods)


  /// <summary>
  /// Projects a vector onto another vector
  /// </summary>
  /// <param name="vector">Vector to project</param>
  /// <param name="onNormal">Normalized vector to project on</param>
  /// <returns>Vector vector projected on onNormal</returns>
  public static DebugVector3 Project(Vector3 vector, Vector3 onNormal)
  {
    Vector3 result = Vector3.Project(vector, onNormal);
    DrawVector(origin, origin + vector, DrawVectorColorA);
    if (result.sqrMagnitude > onNormal.sqrMagnitude)
    {
      DrawVector(origin, origin + result, DrawResultColor);
      DrawVector(origin, origin + onNormal, DrawVectorColorB);
    }
    else
    {
      DrawVector(origin, origin + onNormal, DrawVectorColorB);
      DrawVector(origin, origin + result, DrawResultColor);
    }
    return (DebugVector3)result;
  }
  /// <summary>
  /// Projects a vector onto another vector
  /// </summary>
  /// <param name="vector">Vector to project</param>
  /// <param name="onNormal">Normalized vector to project on</param>
  /// <returns>Vector vector projected on onNormal</returns>
  public static DebugVector3 Project(DebugVector3 vector, DebugVector3 onNormal)
  {
    return Project(vector.v3, onNormal.v3);
  }


  /// <summary>
  /// Projects a vector onto a plane defined by a normal orthogonal to the plane.
  /// </summary>
  /// <param name="vector">Vector to project</param>
  /// <param name="planeNormal">Normal orthogonal to plane to project on</param>
  /// <returns>Vector projected on plane defined by planeNormal</returns>
  public static DebugVector3 ProjectOnPlane(DebugVector3 vector, DebugVector3 planeNormal)
  {
    return ProjectOnPlane(vector.v3, planeNormal.v3);
  }
  /// <summary>
  /// Projects a vector onto a plane defined by a normal orthogonal to the plane.
  /// </summary>
  /// <param name="vector">Vector to project</param>
  /// <param name="planeNormal">Normal orthogonal to plane to project on</param>
  /// <returns>Vector projected on plane defined by planeNormal</returns>
  public static DebugVector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
  {
    Vector3 result = Vector3.ProjectOnPlane(vector, planeNormal);
    DrawVector(origin, origin + planeNormal.normalized, DrawVectorColorB);
    // draw vector we're project after on the off chance it's the same as the plane normal.
    DrawVector(origin, origin + vector, DrawVectorColorA);
    DebugDraw.DrawPlane(origin, planeNormal, 1, DrawVectorColorB, DrawLineTime, DepthTest);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }

  /// <summary>
  /// Reflects a vector off the plane defined by normal.
  /// The visual shown is origin - vector returned (to better display the actual reflection)
  /// </summary>
  /// <param name="inDirection">Vector to reflect, treated as an arrow coming in to the plane</param>
  /// <param name="inNormal">Normalized vector orthogonal to the plane.</param>
  /// <returns>Actual reflected vector (Generally you want to use -returned value as the actual reflection)</returns>
  public static DebugVector3 Reflect(Vector3 inDirection, Vector3 inNormal)
  {
    Vector3 result = Vector3.Reflect(inDirection, inNormal);
    DrawVector(origin, origin + inNormal.normalized, DrawVectorColorB);
    DrawVector(origin + inDirection, origin, DrawVectorColorA);
    DebugDraw.DrawPlane(origin, inNormal, 1f, DrawVectorColorB, DrawLineTime, DepthTest);
    DrawVector(origin, origin - result, DrawResultColor);
    return (DebugVector3)result;
  }

  /// <summary>
  /// Rotates a vector current towards target and while changing its magnitude
  /// </summary>
  /// <param name="current">Vector to move</param>
  /// <param name="target">Target to move towards</param>
  /// <param name="maxRadiansDelta">Max degrees to rotate current towards target (or away if negative)</param>
  /// <param name="maxMagnitudeDelta">Maximum change in magnitude</param>
  /// <returns>Vector rotated towards target</returns>
  public static DebugVector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
  {
    Vector3 result = Vector3.RotateTowards(current, target, maxRadiansDelta, maxMagnitudeDelta);
    DrawVector(origin, origin + current, DrawVectorColorA);
    DrawVector(origin, origin + target, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }

  /// <summary>
  /// Multiplies two vectors component wise
  /// </summary>
  /// <param name="a">Vector a</param>
  /// <param name="b">Vector b</param>
  /// <returns>Vector3(a.x * b.x, a.y * b.y, a.z * b.z)</returns>
  public static DebugVector3 Scale(Vector3 a, Vector3 b) {
    Vector3 result = Vector3.Scale(a, b);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origni + result, DrawResultColor);
    return (DebugVector3)result
  }

  // Operators

  public static implicit operator Vector3(DebugVector3 a) { return new Vector3(a.x, a.y, a.z); }
  public static explicit operator DebugVector3(Vector3 a) { return new DebugVector3(a); }


  public static DebugVector3 operator +(DebugVector3 a, DebugVector3 b)
  {
    DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
    DrawVectorOperator(origin + a.v3, origin + a.v3 + b.v3, DrawVectorColorB);
    DrawVectorOperator(origin, origin + a.v3 + b.v3, DrawResultColor);
    return new DebugVector3(a.x + b.x, a.y + b.y, a.z + b.z);
  }
  public static DebugVector3 operator +(DebugVector3 a, Vector3 b)
  {
    return a + (DebugVector3)b;
  }
  public static DebugVector3 operator +(Vector3 a, DebugVector3 b)
  {
    return (DebugVector3)a + b;
  }


  public static DebugVector3 operator -(DebugVector3 a, DebugVector3 b)
  {
    DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
    DrawVectorOperator(origin + a.v3, origin + a.v3 - b.v3, DrawVectorColorB);
    DrawVectorOperator(origin, origin + a.v3 - b.v3, DrawResultColor);
    return new DebugVector3(a.x - b.x, a.y - b.y, a.z - b.z);
  }
  public static DebugVector3 operator -(DebugVector3 a, Vector3 b)
  {
    return a - (DebugVector3)b;
  }
  public static DebugVector3 operator -(Vector3 a, DebugVector3 b)
  {
    return (DebugVector3)a - b;
  }


  public static bool operator ==(DebugVector3 a, DebugVector3 b)
  {
    return (a.x == b.x && a.y == b.y && a.z == b.z);
  }

  public override bool Equals(object o)
  {
    return this.GetHashCode() == o.GetHashCode();
  }
  public override int GetHashCode()
  {
    return v3.GetHashCode();
  }


  public static bool operator !=(DebugVector3 a, DebugVector3 b)
  {
    return (a.x != b.x || a.y != b.y || a.z != b.z);
  }

  public static DebugVector3 operator *(DebugVector3 a, float m)
  {
    if (m > 1)
    {
      DrawVectorOperator(origin, origin + a.v3 * m, DrawResultColor);
      DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
    }
    else
    {
      DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
      DrawVectorOperator(origin, origin + a.v3 * m, DrawResultColor);
    }
    return new DebugVector3(a.x * m, a.y * m, a.z * m);
  }

  public static DebugVector3 operator /(DebugVector3 a, float d)
  {
    if (d >= 1)
    {
      DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
      DrawVectorOperator(origin, origin + a.v3 / d, DrawResultColor);
    }
    else
    {
      DrawVectorOperator(origin, origin + a.v3 / d, DrawResultColor);
      DrawVectorOperator(origin, origin + a.v3, DrawVectorColorA);
    }
    return new DebugVector3(a.x / d, a.y / d, a.z / d);
  }

  public override string ToString()
  {
    return v3.ToString();
  }

  /// <summary>
  /// Sets the x, y, and z components on an existing Vector3
  /// </summary>
  /// <param name="newX">X component</param>
  /// <param name="newY">Y component</param>
  /// <param name="newZ">Z component</param>
  public void Set(float newX, float newY, float newZ)
  {
    DrawVector(origin, origin + v3, DrawVectorColorA, DrawPermanentChangeTime);
    v3.x = newX;
    v3.y = newY;
    v3.z = newZ;
    DrawVector(origin, origin + v3, DrawResultColor, DrawPermanentChangeTime);
  }
}

// Custom property drawer emulates vector3 drawer.
#if (UNITY_EDITOR)
[CustomPropertyDrawer(typeof(DebugVector3))]
public class DebugVector3PropertyDrawer : PropertyDrawer
{

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    // double line height if we're not in wide mode like regular vector3 fields.
    if (EditorGUIUtility.wideMode) { return EditorGUIUtility.singleLineHeight; }
    else
    {
      return EditorGUIUtility.singleLineHeight * 2;
    }
  }

  public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
  {
    // // get all properties
    // SerializedProperty x = prop.FindPropertyRelative("x");
    // SerializedProperty y = prop.FindPropertyRelative("_y");
    // SerializedProperty z = prop.FindPropertyRelative("_z");
    SerializedProperty v3 = prop.FindPropertyRelative("v3");
    SerializedProperty x = v3.FindPropertyRelative("x");
    SerializedProperty y = v3.FindPropertyRelative("y");
    SerializedProperty z = v3.FindPropertyRelative("z");
    // create a vector
    Vector3 val = new Vector3(x.floatValue, y.floatValue, z.floatValue);
    // use a vector3 field.
    val = EditorGUI.Vector3Field(pos, label, val);
    // use the values from the field.
    v3.vector3Value = val;
  }
}
#endif

