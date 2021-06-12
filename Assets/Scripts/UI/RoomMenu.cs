using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomMenu : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    void Awake()
    {
        MenuManager.instance.menuChange += SetName;
    }

    void SetName()
    {
        playerNameText.text = PlayerPrefs.GetString(PlayerInfo.playerNamePref);
    }
}