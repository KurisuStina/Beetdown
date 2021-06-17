using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public const string playerNamePref = "PlayerName";
    public const string playerCharacter = "CustomCharacter";
    public const string playerWeapon = "CustomWeapon";
    public const string playerInfo = "PlayerInfo";


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