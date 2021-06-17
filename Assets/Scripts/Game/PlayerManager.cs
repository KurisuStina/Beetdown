using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
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

    }


    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }

    #endregion
}
