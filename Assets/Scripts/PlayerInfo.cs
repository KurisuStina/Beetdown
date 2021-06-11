using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo info;

    public const string playerNamePref = "PlayerName";
    public const string playerWeaponPref = "PlayerWeapon";

    public string playerName;
    public int playerWeapon;

    void Start()
    {
        info = this;

    }


    void Update()
    {

    }
}