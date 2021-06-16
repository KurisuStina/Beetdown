using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

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

    }

    #endregion
}
