using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    private GameObject controller;

    
    //private Player player;
    [SerializeField] private Vector2 playerInfo;

    public Player player { get; private set; }
    public CharacterData character { get; private set; }
    public WeaponData weapon { get; private set; }
    void Awake()
    {
        PV = GetComponent<PhotonView>();

        player = RoomManager.GetPlayer(PV.Owner.ActorNumber);
        if (player.CustomProperties.ContainsKey(PlayerInfo.playerInfo))
            playerInfo = (Vector2)player.CustomProperties[PlayerInfo.playerInfo];
        else
        {
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
            customProperties[PlayerInfo.playerInfo] = Vector2.zero;
            player.CustomProperties = customProperties;
        }

        character = RoomManager.instance.Characters[(int)playerInfo.x];
        weapon = RoomManager.instance.Weapons[(int)playerInfo.y];
    }

    void Start()
    {
        if (PV.IsMine)
            CreateController();
    }

    #region Methods

    void CreateController()
    {
        Debug.Log("Created Player Controller");
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector2.zero, Quaternion.identity, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }

    #endregion

    #region Photon RPC

    #endregion

    #region Helper Methods


    #endregion
}
