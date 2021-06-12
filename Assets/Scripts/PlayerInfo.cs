using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo info;

    public const string playerNamePref = "PlayerName";
    public string playerName;

    void Start()
    {
        info = this;

    }


    void Update()
    {

    }
}