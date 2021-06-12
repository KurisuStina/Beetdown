using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class Displayable : Attribute
{

}

public enum WeaponType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : ScriptableObject
{

    [Header("Properties")]
    public string Name;
    public string Description;

    public Sprite sprite;

    public WeaponType type;

    [Displayable] [Range(0, 100)] public int Damage;
    [Displayable] [Range(0, 5)] public float AttackSpeed; // attacks once every attackSpeed seconds
    [Displayable] [Range(0, 100)] public float Range; // for ranged weapons, this is the maximum distance it can travel

    [Header("Screen Shake")]
    [Displayable] [Range(0, 5)]public float Duration;
    [Displayable] [Range(0, 1)]public float Magnitude;

    [Header("Ranged / Thrown")]
    public Sprite projectileSprite;
    [Displayable] [Range(0, 100)] public float Speed;
    [Displayable] [Range(0, 90)] public float Inaccuracy; // maximum angle of inaccuracy
}

