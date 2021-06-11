using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerInputField : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button continueButton;

    void Start()
    {
        InitInputField();
    }

    void InitInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerInfo.playerNamePref))
        {
            SetContinueButton("");
            return;
        }
        Debug.Log("init input field");
        string pName = PlayerPrefs.GetString(PlayerInfo.playerNamePref);
        nameInputField.text = pName;
        SetContinueButton(pName);
    }

    public void SetContinueButton(string playerName)
    {
        Debug.Log("Set name");
        continueButton.interactable = !string.IsNullOrEmpty(playerName);
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(PlayerInfo.playerNamePref, playerName);
    }
}