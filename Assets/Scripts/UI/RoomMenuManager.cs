using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomMenuManager : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    void Awake()
    {
        MenuManager.OnMenuChange += SetName;
    }

    void SetName()
    {
        playerNameText.text = PlayerPrefs.GetString(PlayerInfo.playerNamePref);
    }
}