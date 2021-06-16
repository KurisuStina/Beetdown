using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public const string playerNamePref = "PlayerName";


    public CharacterData character;
    public WeaponData weapon;

    public PlayerInfo(CharacterData charData, WeaponData weaponData)
    {
        charData = character;
        weaponData = weapon;
    }

    public void ChangeCharacter(CharacterData charData)
    {
        Debug.Log("Changed Char");
        character = charData;
    }

    public void ChangeWeapon(WeaponData weaponData)
    {
        Debug.Log("Changed Weapon");
        weapon = weaponData;
    }
}