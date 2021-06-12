using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RoomInfo info;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(RoomInfo roomInfo)
    {
        info = roomInfo;
        text.text = roomInfo.Name;
    }

    public void OnClick()
    {
        PhotonManager.instance.JoinRoom(info);
    }
}