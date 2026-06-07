using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container<T>
{
    private readonly List<T> values = new();
    public List<T> Values => values;

    public void Add(T value)
    {
        values.Add(value);
    }
    public void Remove(T value)
    {
        values.Remove(value);
    }
    public void Clear()
    {
        values.Clear();
    }
}