using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInputField : InputField
{

    protected override void Activate()
    {
        Debug.Log("Created Room: " + inputField.text);
        Launcher.instance.CreateRoom();
    }

    protected override void Return()
    {
        MenuManager.instance.OpenMenu("lobby");
    }
}
