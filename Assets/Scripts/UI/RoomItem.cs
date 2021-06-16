using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    private RoomInfo info;

    void Start()
    {

    }

    public void Initialize(RoomInfo roomInfo)
    {
        info = roomInfo;
        text.text = roomInfo.Name;
    }

    public void OnClick()
    {
        Launcher.instance.JoinRoom(info);
    }
}