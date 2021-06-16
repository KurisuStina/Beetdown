using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class Displayable : Attribute
{

}

public class Selectable : ScriptableObject
{
    [Header("Properties")]
    public string Name;
    public string Description;
    public Sprite sprite;
}
