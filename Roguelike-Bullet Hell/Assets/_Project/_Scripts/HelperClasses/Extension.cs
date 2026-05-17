using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    #region Vector3
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
    public static Vector3 Add(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(x: vector.x + x, y: vector.y + y, z: vector.z + z);
    }
    public static Vector3 Add(this Vector3 vector, Vector3 addVector)
    {
        return new Vector3(x: vector.x + addVector.x, y: vector.y + addVector.y, z: vector.z + addVector.z);
    }
    #endregion

    #region GameObject
    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if(component is null) component = gameObject.AddComponent<T>();
        return component;
    }
    public static T OrNull<T> (this T obj) where T : Object => obj ? obj : null;
    public static void DestroyChildren(this GameObject gameObject)
    {
        gameObject.transform.DestroyChildren();
    }
    public static void EnableChildren(this GameObject gameObject)
    {
        gameObject.transform.EnableChildren();
    }
    public static void DisableChildren(this GameObject gameObject)
    {
        gameObject.transform.DisableChildren();
    }
    #endregion

    #region Transform
    static void PerformActionOnChildren(this Transform parent, System.Action<Transform> action)
    {
        for (var i = parent.childCount - 1; i >= 0; i--)
            action(parent.GetChild(i));
    }
    public static IEnumerable<Transform> Children(this Transform parent)
    {
        foreach (Transform child in parent) yield return child;
    }
    public static void DestroyChildren(this Transform parent)
    {
        parent.PerformActionOnChildren(child => Object.Destroy(child.gameObject));
    }
    public static void EnableChildren(this Transform parent)
    {
        parent.PerformActionOnChildren(child => child.gameObject.SetActive(true));
    }
    public static void DisableChildren(this Transform parent)
    {
        parent.PerformActionOnChildren(child => child.gameObject.SetActive(false));
    }
    public static void MoveBy(this Transform transform, float x = 0, float y = 0, float z = 0)
    {
        transform.position = transform.position.Add(x, y, z);
    }
    public static void MoveBy(this Transform transform, Vector3 vector)
    {
        transform.position = transform.position.Add(vector);
    }
    public static void MoveByX(this Transform transform, Vector2 vector)
    {
        transform.position = transform.position += new Vector3(vector.x, 0, 0);
    }
    public static void MoveByXZ(this Transform transform, Vector2 vector)
    {
        transform.position = transform.position += new Vector3(vector.x, 0, vector.y);
    }
    public static void MoveTo(this Transform  transform, Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    #endregion

    #region ComponentExtensions
    
    #endregion
}
