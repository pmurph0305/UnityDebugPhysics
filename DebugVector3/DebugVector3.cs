using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//TODO: orthonormalize methods since they use refs.

[System.Serializable]
public struct DebugVector3
{
  /// <summary>
  /// Should Vector3.Reflect draw the vector as reflected? (origin - result)
  /// </summary>
  public static bool DrawReflectAsReflected = true;
  /// <summary>
  /// Shound an arrow be drawn when drawing angles?
  /// </summary>
  public static bool DrawAngleArrow = true;
  /// <summary>
  /// When Vector.Dot is used, should the projection of A on B be drawn?
  /// </summary>
  public static bool DrawDotAsProjectionAonB = true;
  /// <summary>
  /// Should the axis to rotate around for Vector3.Angle be drawn?
  /// </summary>
  public static bool ShowAngleAxis = true;
  /// <summary>
  /// Should the sign be ignored when drawing angles for Vector3.SignedAngle
  /// If true, the angle drawn will always go towards what you're aiming for.
  /// </summary>
  public static bool IgnoreAngleSign = true;
  /// <summary>
  /// Should inputs for DebugVector3 Operators( +-/* ) be drawn?
  /// </summary>
  public static bool ShowOperatorInputs = true;
  /// <summary>
  /// Should the result for DebugVector3 Operators ( +-/* ) be drawn?
  /// </summary>
  public static bool ShowOperatorResult = true;
  /// <summary>
  /// Should the result of static methods be drawn?
  /// </summary>
  public static bool ShowMethodResult = true;
  /// <summary>
  /// Should the inputs for static methods be drawn?
  /// </summary>
  public static bool ShowMethodInputs = true;
  /// <summary>
  /// How long should each line be drawn for?
  /// </summary>
  public static float DrawLineTime = 0.0001f;
  /// <summary>
  /// How long should the methods that actually change an input vector be drawn? (Ie used in Normalize())
  /// </summary>
  public static float DrawPermanentChangeTime = 5.0f;
  /// <summary>
  /// Color to draw the first input vector for a method
  /// </summary>
  public static Color DrawVectorColorA = Color.yellow;
  /// <summary>
  /// Color to draw the second input vector for a method
  /// </summary>
  public static Color DrawVectorColorB = Color.blue;
  /// <summary>
  /// Color to draw the third input vector for a method
  /// </summary>
  public static Color DrawVectorColorC = Color.red;
  /// <summary>
  /// Color to draw the result of methods
  /// </summary>
  public static Color DrawResultColor = Color.green;
  /// <summary>
  /// Should the lines drawn be obscured by objects?
  /// </summary>
  public static bool DepthTest = true;
  /// <summary>
  /// Should the vectors be drawn with arrows point in it's direction
  /// </summary>
  public static bool DrawVectorsWithArrows = true;
  /// <summary>
  /// The scale of arrows to draw when drawing vectors with arrows
  /// </summary>
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



  public DebugVector3(float x, float y, float z)
  {
    v3 = new Vector3(x, y, z);
  }

  public DebugVector3(Vector3 vector)
  {
    v3 = vector;
  }

  /// <summary>
  /// Origin of all vectors. Change this to change the location that the drawning occurs at.
  /// </summary>
  public static Vector3 origin = Vector3.zero;

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

  /// <summary>
  /// Draws vectors for methods
  /// </summary>
  /// <param name="start">Start of vector</param>
  /// <param name="end">End of vector</param>
  /// <param name="color">Color to draw lines with</param>
  /// <param name="duration">Length of time to draw</param>
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

  /// <summary>
  /// Draws the Vector Operators +- etc.
  /// </summary>
  /// <param name="start">Start vector</param>
  /// <param name="end">End Vector</param>
  /// <param name="color">Color to draw Vector with</param>
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

  /// <summary>
  /// Draws a vector between start and end with color
  /// </summary>
  /// <param name="start">Start position</param>
  /// <param name="end">End position</param>
  /// <param name="color">Color of lines to draw with</param>
  private static void DrawVector(Vector3 start, Vector3 end, Color color)
  {
    DrawVector(start, end, color, DrawLineTime);
  }

  /// <summary>
  /// Draws an angle/arc between from and to using rotate towards
  /// </summary>
  /// <param name="from">From vector</param>
  /// <param name="to">To Vector</param>
  /// <param name="angle">Angle to rotate</param>
  /// <param name="color">Color to draw lines with</param>
  public static void DrawAngleBetween(Vector3 from, Vector3 to, Color color)
  {
    // if (IgnoreAngleSign && angle < 0) { angle *= -1; }
    DebugDraw.DrawAngleBetween(origin, from, to, color, DrawAngleArrow, DrawVectorArrowScale, DrawLineTime, DepthTest);
  }


  // Static Methods

  /// <summary>
  /// Calculates the smaller of the two possible angles between from and to
  /// </summary>
  /// <param name="from">The vector from which the angular distance is measured</param>
  /// <param name="to">The Vector to which the angular distance is measured</param>
  /// <returns>Smallest angle between from and to</returns>
  public static float Angle(Vector3 from, Vector3 to)
  {
    float angle = Vector3.Angle(from, to);
    DrawVector(origin, origin + from, DrawVectorColorA);
    DrawVector(origin, origin + to, DrawVectorColorB);
    Vector3 axis = Vector3.Cross(from, to);
    if (ShowAngleAxis)
    {
      DrawVector(origin, origin + axis.normalized, DrawResultColor);
    }
    DrawAngleBetween(from, to, DrawResultColor);
    return angle;
  }


  /// <summary>
  /// Returns a copy of vector with its magnitude clamped to max length
  /// </summary>
  /// <param name="vector">Vector to clamp</param>
  /// <param name="maxLength">max length to clamp to</param>
  /// <returns>Vector in same direction clamped to maxLength</returns>
  public static DebugVector3 ClampMagnitude(Vector3 vector, float maxLength)
  {
    Vector3 result = Vector3.ClampMagnitude(vector, maxLength);
    DrawVector(origin, origin + vector, DrawVectorColorA);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Cross product of two vectors, the vector perpindicular to the two input vectors
  /// </summary>
  /// <param name="lhs">Left hand side of cross (or thumb of left hand)</param>
  /// <param name="rhs">Right hand side of cross (or index of left hand)</param>
  /// <returns>The vector perpindicular to the two input vectors using left hand rule</returns>
  public static DebugVector3 Cross(Vector3 lhs, Vector3 rhs)
  {
    Vector3 result = Vector3.Cross(lhs, rhs);
    DrawVector(origin, origin + lhs, DrawVectorColorA);
    DrawVector(origin, origin + rhs, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Calculates the distance between a and b
  /// </summary>
  /// <param name="a">Vector A</param>
  /// <param name="b">Vector B</param>
  /// <returns>Distance between Vector A and Vector B</returns>
  public static float Distance(Vector3 a, Vector3 b)
  {
    float result = Vector3.Distance(a, b);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DebugDraw.DrawLine(origin + a, origin + b, DrawResultColor, DrawLineTime, DepthTest);
    return result;
  }


  /// <summary>
  /// Calculates the dot product of two vectors
  /// </summary>
  /// <param name="lhs">Left hand size of dot</param>
  /// <param name="rhs">Right hand size of dot</param>
  /// <returns>The dot product of two vectors</returns>
  public static float Dot(Vector3 lhs, Vector3 rhs)
  {
    DrawVector(origin, origin + lhs, DrawVectorColorA);
    DrawVector(origin, origin + rhs, DrawVectorColorB);
    if (DrawDotAsProjectionAonB)
    {
      Vector3 f = Vector3.Project(lhs, rhs.normalized);
      DrawVector(origin, origin + f, DrawResultColor);
    }
    return Vector3.Dot(lhs, rhs);
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
    Vector3 result = Vector3.Lerp(a, b, t);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
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
    Vector3 result = Vector3.LerpUnclamped(a, b, t);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Returns a vector made from the largest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from largest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Max(Vector3 lhs, Vector3 rhs)
  {
    Vector3 result = Vector3.Max(lhs, rhs);
    DrawVector(origin, origin + lhs, DrawVectorColorA);
    DrawVector(origin, origin + rhs, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Returns a vector made from the smallest components of the two vectors.
  /// </summary>
  /// <param name="lhs">Vector 1</param>
  /// <param name="rhs">Vector 2</param>
  /// <returns>Vector made from smallest components of Vectors lhs and rhs</returns>
  public static DebugVector3 Min(Vector3 lhs, Vector3 rhs)
  {
    Vector3 result = Vector3.Min(lhs, rhs);
    DrawVector(origin, origin + lhs, DrawVectorColorA);
    DrawVector(origin, origin + rhs, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
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
    Vector3 result = Vector3.MoveTowards(current, target, maxDistanceDelta);
    DrawVector(origin, origin + current, DrawVectorColorA);
    DrawVector(origin, origin + target, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Calculates a vector with a magnitude of 1.
  /// </summary>
  /// <param name="value">Vector to normalize</param>
  /// <returns>Vector with a magnitude of 1</returns>
  public static DebugVector3 Normalize(Vector3 value)
  {
    DrawVector(origin, origin + value, DrawVectorColorA);
    DrawVector(origin, origin + value.normalized, DrawResultColor);
    return (DebugVector3)value.normalized;
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
    if (DrawReflectAsReflected)
    {
      DrawVector(origin, origin - result, DrawResultColor);
    }
    else
    {
      DrawVector(origin, origin + result, DrawResultColor);
    }
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
  public static DebugVector3 Scale(Vector3 a, Vector3 b)
  {
    Vector3 result = Vector3.Scale(a, b);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// </summary>
  /// Returns the signed angle in degress between from and to. The smaller of the two possible angles is returned.
  /// <summary>
  /// <param name="from">The vector from which the angle is measured</param>
  /// <param name="to">The vector to which the angle is measured</param>
  /// <param name="axis">A vector around which the other vecotors are rotated</param>
  /// <returns>Signed angle in degrees between from and to, positive for clockwise direction, negative for anti-clockwise direction.</returns>
  public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
  {
    float result = Vector3.SignedAngle(from, to, axis);
    DrawVector(origin, origin + from, DrawVectorColorA);
    DrawVector(origin, origin + to, DrawVectorColorB);
    DrawVector(origin, origin + axis, DrawVectorColorC);
    DrawAngleBetween(from, to, DrawResultColor);
    return result;
  }

  /// <summary>
  /// Spherically interpolates between two vectors with t clamped to [0,1]
  /// </summary>
  /// <param name="a">Vector a</param>
  /// <param name="b">Vector b</param>
  /// <param name="t">Interpolation amount, clamped to [0,1]</param>
  /// <returns>Spherically interpolated vector between a and b by t</returns>
  public static DebugVector3 Slerp(Vector3 a, Vector3 b, float t)
  {
    Vector3 result = Vector3.Slerp(a, b, t);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }

  /// <summary>
  /// Spherically interpolates between two vectors with t unclamped
  /// </summary>
  /// <param name="a">Vector a</param>
  /// <param name="b">Vector b</param>
  /// <param name="t">Interpolation amount</param>
  /// <returns>Spherically interpolated vector between a and b by t</returns>
  public static DebugVector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
  {
    Vector3 result = Vector3.SlerpUnclamped(a, b, t);
    DrawVector(origin, origin + a, DrawVectorColorA);
    DrawVector(origin, origin + b, DrawVectorColorB);
    DrawVector(origin, origin + result, DrawResultColor);
    return (DebugVector3)result;
  }


  /// <summary>
  /// Gradually changes a vector towards a desired goal over time. The vector is smoothed by some spring-damper function which will never overshoot.
  /// </summary>
  /// <param name="current">Current position</param>
  /// <param name="target">Target position</param>
  /// <param name="currentVelocity">Current velocity, value is modified by the function every time you call it</param>
  /// <param name="smoothTime">Approximately the time it will take to reach the target, smaller reaches target faster</param>
  /// <param name="maxSpeed">Optionally allows you to clamp maximum speed</param>
  /// <param name="deltaTime">Time since the last call to this function, by default Time.deltaTime</param>
  /// <returns>Next smoothed position from current position to target</returns>
  public static DebugVector3 SmoothDamp(Vector3 current, Vector3 target,
   ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
  {
    DebugDraw.DrawPoint(current, DrawVectorColorA, DrawLineTime, DepthTest);
    DebugDraw.DrawPoint(target, DrawVectorColorB, DrawLineTime, DepthTest);
    Vector3 result = Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    DrawVector(current, current + currentVelocity, DrawResultColor, DrawPermanentChangeTime);
    DebugDraw.DrawPoint(result, DrawResultColor, DrawLineTime, DepthTest);
    return (DebugVector3)result;
  }
  /// <summary>
  /// Gradually changes a vector towards a desired goal over time. The vector is smoothed by some spring-damper function which will never overshoot.
  /// </summary>
  /// <param name="current">Current position</param>
  /// <param name="target">Target position</param>
  /// <param name="currentVelocity">Current velocity, value is modified by the function every time you call it</param>
  /// <param name="smoothTime">Approximately the time it will take to reach the target, smaller reaches target faster</param>
  /// <param name="maxSpeed">Optionally allows you to clamp maximum speed</param>
  /// <param name="deltaTime">Time since the last call to this function, by default Time.deltaTime</param>
  /// <returns>Next smoothed position from current position to target</returns>
  public static DebugVector3 SmoothDamp(Vector3 current, Vector3 target,
   ref DebugVector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
  {
    return SmoothDamp(current, target, ref currentVelocity.v3, smoothTime, maxSpeed, deltaTime);
  }
  /// <summary>
  /// Gradually changes a vector towards a desired goal over time. The vector is smoothed by some spring-damper function which will never overshoot.
  /// </summary>
  /// <param name="current">Current position</param>
  /// <param name="target">Target position</param>
  /// <param name="currentVelocity">Current velocity, value is modified by the function every time you call it</param>
  /// <param name="smoothTime">Approximately the time it will take to reach the target, smaller reaches target faster</param>
  /// <param name="maxSpeed">Optionally allows you to clamp maximum speed</param>
  /// <returns>Next smoothed position from current position to target</returns>
  public static DebugVector3 SmoothDamp(Vector3 current, Vector3 target,
   ref Vector3 currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity)
  {
    return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, Time.deltaTime);
  }
  /// <summary>
  /// Gradually changes a vector towards a desired goal over time. The vector is smoothed by some spring-damper function which will never overshoot.
  /// </summary>
  /// <param name="current">Current position</param>
  /// <param name="target">Target position</param>
  /// <param name="currentVelocity">Current velocity, value is modified by the function every time you call it</param>
  /// <param name="smoothTime">Approximately the time it will take to reach the target, smaller reaches target faster</param>
  /// <param name="maxSpeed">Optionally allows you to clamp maximum speed</param>
  /// <returns>Next smoothed position from current position to target</returns>
  public static DebugVector3 SmoothDamp(Vector3 current, Vector3 target,
  ref DebugVector3 currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity)
  {
    return SmoothDamp(current, target, ref currentVelocity.v3, smoothTime, maxSpeed, Time.deltaTime);
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

