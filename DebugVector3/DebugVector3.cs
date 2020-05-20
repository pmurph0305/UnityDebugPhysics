﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Why not extension methods?
// operators aren't allowed on extension methods, so replacing all Vector3 with DebugVector3 works better for covering everything.

// why draw from Vector a's origin?
// the math is the exact same, but we want to draw offset from the world space origin for better visualization.
// ie: draw the math at the object the math is happening if the origin is set.


// TODO: rest of the methods
// alternative methods for passing in a vector3 instead of a debug vector3
[System.Serializable]
public struct DebugVector3
{
  public static bool ShowOperatorResult = true;
  public static bool ShowOperatorIndividual = true;
  public static float DrawLineTime = 0.0001f;
  public static Color DrawVectorColorA = Color.yellow;
  public static Color DrawVectorColorB = Color.blue;
  public static Color DrawResultColor = Color.green;
  public static bool DepthTest = true;
  public static DebugVector3 forward = new DebugVector3(0, 0, 1);
  public static DebugVector3 up = new DebugVector3(0, 1, 0);
  public static DebugVector3 right = new DebugVector3(1, 0, 0);
  public static DebugVector3 zero = new DebugVector3(0, 0, 0);

  public static bool DrawVectorsWithArrows = true;
  public static float DrawVectorArrowScale = 0.1f;

  public static bool DrawDotAsProjectionAonB = true;
  public DebugVector3(float x, float y, float z)
  {
    t = null;
    _origin = Vector3.zero;
    v3 = new Vector3(x, y, z);
  }

  public DebugVector3(Vector3 vector)
  {
    t = null;
    _origin = Vector3.zero;
    v3 = vector;
  }

  private Vector3 _origin;
  public Vector3 origin
  {
    get
    {
      if (t != null)
      {
        return t.position;
      }
      return _origin;
    }
    set { _origin = value; }
  }
  public Transform t;



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

  // Drawing

  private static void DrawVector(Vector3 start, Vector3 end, Color color)
  {
    // only draw individual if we allow it
    if (!ShowOperatorIndividual && (color == DrawVectorColorA || color == DrawVectorColorB)) return;
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
    DrawVector(from.origin, from.origin + from.v3, DrawVectorColorA);
    DrawVector(from.origin, from.origin + to.v3, DrawVectorColorB);
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
    DrawVector(vector.origin, vector.origin + vector.v3, DrawVectorColorA);
    DrawVector(vector.origin, vector.origin + result.v3, DrawResultColor);
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
    DrawVector(lhs.origin, lhs.origin + lhs.v3, DrawVectorColorA);
    DrawVector(rhs.origin, rhs.origin + rhs.v3, DrawVectorColorB);
    DrawVector(lhs.origin, lhs.origin + result, DrawResultColor);
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
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin, a.origin + b.v3, DrawVectorColorB);
    DebugDraw.DrawLine(a.v3, b.v3, DrawResultColor, DrawLineTime, DepthTest);
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
    DrawVector(lhs.origin, lhs.origin + lhs.v3, DrawVectorColorA);
    DrawVector(lhs.origin, lhs.origin + rhs.v3, DrawVectorColorB);
    if (DrawDotAsProjectionAonB)
    {
      Vector3 f = Vector3.Project(lhs.v3, rhs.v3.normalized);
      DrawVector(lhs.origin, lhs.origin + f, DrawResultColor);
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
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin, a.origin + b.v3, DrawVectorColorB);
    DrawVector(a.origin, a.origin + result, DrawResultColor);
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
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin, a.origin + b.v3, DrawVectorColorB);
    DrawVector(a.origin, a.origin + result, DrawResultColor);
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
    DrawVector(lhs.origin, lhs.origin + lhs.v3, DrawVectorColorA);
    DrawVector(lhs.origin, lhs.origin + rhs.v3, DrawVectorColorB);
    DrawVector(lhs.origin, lhs.origin + result, DrawResultColor);
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
    DrawVector(lhs.origin, lhs.origin + lhs.v3, DrawVectorColorA);
    DrawVector(lhs.origin, lhs.origin + rhs.v3, DrawVectorColorB);
    DrawVector(lhs.origin, lhs.origin + result, DrawResultColor);
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
    DrawVector(current.origin, current.origin + current.v3, DrawVectorColorA);
    DrawVector(current.origin, current.origin + target.v3, DrawVectorColorB);
    DrawVector(current.origin, current.origin + result, DrawResultColor);
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
    DrawVector(value.origin, value.origin + value.v3, DrawVectorColorA);
    DrawVector(value.origin, value.origin + value.v3.normalized, DrawResultColor);
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
    DrawVector(origin, origin + v3, DrawVectorColorA);
    this.v3 = v3.normalized;
    DrawVector(origin, origin + v3, DrawResultColor);
  }

  // Operators

  public static implicit operator Vector3(DebugVector3 a) { return new Vector3(a.x, a.y, a.z); }
  public static explicit operator DebugVector3(Vector3 a) { return new DebugVector3(a); }


  public static DebugVector3 operator +(DebugVector3 a, DebugVector3 b)
  {
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin + a.v3, a.origin + a.v3 + b.v3, DrawVectorColorB);
    DrawVector(a.origin, a.origin + a.v3 + b.v3, DrawResultColor);
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
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin + a.v3, a.origin + a.v3 - b.v3, DrawVectorColorB);
    DrawVector(a.origin, a.origin + a.v3 - b.v3, DrawResultColor);
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
    DrawVector(a.origin, a.origin + a.v3 * m, DrawResultColor);
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    return new DebugVector3(a.x * m, a.y * m, a.z * m);
  }

  public static DebugVector3 operator /(DebugVector3 a, float d)
  {
    DrawVector(a.origin, a.origin + a.v3, DrawVectorColorA);
    DrawVector(a.origin, a.origin + a.v3 / d, DrawResultColor);
    return new DebugVector3(a.x / d, a.y / d, a.z / d);
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

