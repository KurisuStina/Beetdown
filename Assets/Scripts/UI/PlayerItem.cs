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
    private PlayerInfo playerInfo;

    public void Initialize(Player newPlayer)
    {
        player = newPlayer;
        playerName.text = newPlayer.NickName;

        RoomManager.instance.playerItem = this;
            
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

    public void UpdateUI(PlayerInfo p_Info)
    {
        //playerAvatar.sprite = p_Info.character?.sprite;
        //playerWeapon.sprite = p_Info.weapon?.sprite;

        if (!photonView.IsMine)
        {
            Debug.Log("called rpc");
            photonView.RPC("punUpdateUI", RpcTarget.AllBufferedViaServer);
        }
        
    }

    [PunRPC]
    public void punUpdateUI()
    {
        Debug.Log(playerInfo);
        playerAvatar.sprite = playerInfo.character?.sprite;
        playerWeapon.sprite = playerInfo.weapon?.sprite;
    }
}