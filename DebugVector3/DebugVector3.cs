using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Why not extension methods?
// operators aren't allowed on extension methods, so replacing all Vector3 with DebugVector3 works better for covering everything.

// why draw from Vector a's origin?
// the math is the exact same, but we want to draw offset from the world space origin for better visualization.
// ie: draw the math at the object the math is happening if the origin is set.

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
    this._x = x;
    this._y = y;
    this._z = z;
  }

  public DebugVector3(Vector3 vector)
  {
    t = null;
    _origin = Vector3.zero;
    v3 = vector;
    this._x = vector.x;
    this._y = vector.y;
    this._z = vector.z;
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
  [SerializeField]
  private float _x;
  public float x
  {
    get { return _x; }
    set
    {
      _x = value;
      v3.x = value;
    }
  }
  [SerializeField]
  private float _y;
  public float y
  {
    get { return _y; }
    set
    {
      _y = value;
      v3.y = value;
    }
  }
  [SerializeField]
  private float _z;
  public float z
  {
    get { return _z; }
    set
    {
      _z = value;
      v3.z = value;
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

  public static DebugVector3 Cross(DebugVector3 lhs, DebugVector3 rhs)
  {
    Vector3 result = Vector3.Cross(lhs, rhs);
    DrawVector(lhs.origin, lhs.origin + lhs.v3, DrawVectorColorA);
    DrawVector(rhs.origin, rhs.origin + rhs.v3, DrawVectorColorB);
    DrawVector(lhs.origin, lhs.origin + result, DrawResultColor);
    return new DebugVector3(result);
  }
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

  // Operators
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

  public static implicit operator Vector3(DebugVector3 a) { return new Vector3(a.x, a.y, a.z); }

  public static explicit operator DebugVector3(Vector3 a) { return new DebugVector3(a.x, a.y, a.z); }

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
    // get all properties
    SerializedProperty x = prop.FindPropertyRelative("_x");
    SerializedProperty y = prop.FindPropertyRelative("_y");
    SerializedProperty z = prop.FindPropertyRelative("_z");
    SerializedProperty v3 = prop.FindPropertyRelative("v3");
    // create a vector
    Vector3 val = new Vector3(x.floatValue, y.floatValue, z.floatValue);
    // use a vector3 field.
    val = EditorGUI.Vector3Field(pos, label, val);
    // use the values from the field.
    x.floatValue = val.x;
    y.floatValue = val.y;
    z.floatValue = val.z;
    v3.vector3Value = val;
  }
}
#endif