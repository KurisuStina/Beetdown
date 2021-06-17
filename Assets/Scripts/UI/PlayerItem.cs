using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Image playerAvatar, playerWeapon;
    public TextMeshProUGUI playerName;

    private Player player;

    public void Initialize(Player newPlayer)
    {
        Debug.Log("Init Player");
        player = newPlayer;
        playerName.text = newPlayer.NickName;

        PlayerInfo info = (PlayerInfo)player.CustomProperties[PlayerInfo.playerInfo];
        //playerAvatar.sprite = info.character?.sprite;
        //playerWeapon.sprite = info.weapon?.sprite;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);

    }

}