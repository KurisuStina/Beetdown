using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static UnityAction<CharacterData, WeaponData> OnPlayerSpawn;

    PhotonView PV;

    private GameObject controller;

    [SerializeField]private CharacterData character;
    [SerializeField]private WeaponData weapon;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
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

        OnPlayerSpawn?.Invoke(character, weapon);
    }

    public void Initialize(PlayerInfo info)
    {
        character = info.character;
        weapon = info.weapon;
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }

    #endregion
}
