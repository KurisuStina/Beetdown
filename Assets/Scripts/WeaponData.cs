using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Range(0, 100)] public int damage;
    [Range(0, 5)] public float attackSpeed; // attacks once every attackSpeed seconds
    [Range(0, 100)] public float range; // for ranged weapons, this is the maximum distance it can travel

    [Header("Screen Shake")]
    [Range(0, 5)]public float duration;
    [Range(0, 1)]public float magnitude;

    [Header("Ranged / Thrown")]
    public Sprite projectileSprite;
    [Range(0, 100)] public float speed;
    [Range(0, 90)] public float maxRecoil; // maximum angle of inaccuracy
}

