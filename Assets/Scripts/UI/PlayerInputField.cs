using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInputField : InputField
{

    protected override void Activate()
    {
        SavePlayerName();
        MenuManager.instance.OpenMenu("lobby");
    }

    override protected void InitInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerInfo.playerNamePref))
        {
            return;
        }
        string pName = PlayerPrefs.GetString(PlayerInfo.playerNamePref);
        inputField.text = pName;
    }

    void SavePlayerName()
    {
        string playerName = inputField.text;
        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(PlayerInfo.playerNamePref, playerName);
    }
}