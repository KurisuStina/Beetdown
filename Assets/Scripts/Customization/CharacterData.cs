using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create New Character")]
public class CharacterData : Selectable
{
    [Header("Stats")]
    [Displayable]public float MaxHealth;
    [Displayable]public float Speed;

    [Header("Body")]
    public Sprite Body;
    public Sprite LeftArm;
    public Sprite LeftFeet;
    public Sprite RightFeet;
}
