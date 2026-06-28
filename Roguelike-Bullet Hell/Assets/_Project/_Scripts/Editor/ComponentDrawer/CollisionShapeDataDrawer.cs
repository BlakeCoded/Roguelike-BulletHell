using System.Collections;
using System.Collections.Generic;
using Collision;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CollisionShapeData))]
public class CollisionShapeDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var type = property.FindPropertyRelative("Type");
        var radius = property.FindPropertyRelative("Radius");
        var halfExtents = property.FindPropertyRelative("HalfExtents");
        var height = property.FindPropertyRelative("Height");

        Rect line = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(line, type);

        line.y += EditorGUIUtility.singleLineHeight + 2;

        ShapeType shape = (ShapeType)type.enumValueIndex;

        switch (shape)
        {
            case ShapeType.Sphere:
                EditorGUI.PropertyField(line, radius);
                break;

            case ShapeType.Box:
                EditorGUI.PropertyField(line, halfExtents);
                break;

            case ShapeType.Capsule:
                EditorGUI.PropertyField(line, radius);
                line.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(line, height);
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var type = property.FindPropertyRelative("Type");
        ShapeType shape = (ShapeType)type.enumValueIndex;

        int lines = 1;

        switch (shape)
        {
            case ShapeType.Sphere:
                lines += 1;
                break;

            case ShapeType.Box:
                lines += 1;
                break;

            case ShapeType.Capsule:
                lines += 2;
                break;
        }

        return lines * (EditorGUIUtility.singleLineHeight + 2);
    }
}
