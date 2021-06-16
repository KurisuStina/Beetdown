using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public CharacterData default_char;
    public WeaponData default_weapon;

    public PlayerInfo playerInfo;
    public PlayerItem playerItem;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        playerInfo = new PlayerInfo(default_char, default_weapon);

        Selection.OnCustomChange += UpdatePlayerItem;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector2.zero, Quaternion.identity);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Player Methods
    public void UpdatePlayerItem()
    {
        playerItem?.UpdateUI(playerInfo);
    }
    #endregion
}
