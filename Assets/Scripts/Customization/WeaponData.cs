using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum WeaponType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : Selectable
{
    [Displayable] public WeaponType type;

    [Displayable] [Range(0, WeaponHandler.maxDamage)] public int Damage;
    [Displayable] [Range(0, 5)] public float AttackSpeed; // attacks once every attackSpeed seconds
    [Displayable] [Range(0, 100)] public float Range; // for ranged weapons, this is the maximum distance it can travel

    [Header("Ranged / Thrown")]
    public Sprite projectileSprite;
    [Displayable] [Range(0, 100)] public float ProjectileSpeed;
    [Displayable] [Range(0, 90)] public float Inaccuracy; // maximum angle of inaccuracy

    [Header("Screen Shake")]
    [Range(0, 5)] public float Duration;
    [Range(0, 1)] public float Magnitude;
}

