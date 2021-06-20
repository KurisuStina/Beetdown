using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Image playerCharacter, playerWeapon;
    public TextMeshProUGUI playerName;

    public Player player { get; private set; }
    [SerializeField] private string pName;

    [SerializeField] private Vector2 playerInfo;

    public void Initialize(Player newPlayer)
    {
        player = newPlayer;
        playerName.text = newPlayer.NickName + " " + player.ActorNumber;

        SetUI();
        //Selection.OnCustomChange += SetUI;
    }

    void Update()
    {
        SetUI();
    }

    private void OnDestroy()
    {
        //Selection.OnCustomChange -= SetUI;
    }


    //public void SetUI()
    //{
    //    foreach(Player p in PhotonNetwork.PlayerList)
    //    {
    //        Vector2 pInfo = Vector2.zero;
    //        foreach (Transform playerItem in transform.parent)
    //        {
    //            PlayerItem item = playerItem.GetComponent<PlayerItem>();
    //            if (p.ActorNumber == item.player.ActorNumber)
    //            {
    //                if (p.CustomProperties.ContainsKey(PlayerInfo.playerInfo))
    //                    pInfo = (Vector2)p.CustomProperties[PlayerInfo.playerInfo];
    //                item.playerCharacter.sprite = RoomManager.instance.Characters[(int)pInfo.x].UI_sprite;
    //                item.playerWeapon.sprite = RoomManager.instance.Weapons[(int)pInfo.y].UI_sprite;
    //            }
    //        }
    //        Debug.Log("Set Player: " + p.NickName + "  x: " + pInfo.x + "   y: " + pInfo.y);
    //    }
    //}

    public void SetUI()
    {
        Vector2 info = Vector2.zero;
        if(player.CustomProperties.ContainsKey(PlayerInfo.playerInfo))
            info = (Vector2)player.CustomProperties[PlayerInfo.playerInfo];

        playerCharacter.sprite = RoomManager.instance.Characters[(int)info.x].sprite;
        playerWeapon.sprite = RoomManager.instance.Weapons[(int)info.y].sprite;
    }

    #region Photon
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
    #endregion
}