using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInputField : InputField
{

    protected override void Activate()
    {
        Debug.Log("Created Room: " + inputField.text);
        PhotonManager.instance.CreateRoom();
    }

}
