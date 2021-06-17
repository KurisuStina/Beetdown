using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using System.IO;


public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    public static event UnityAction OnPlayerListChange;

    //Change this on each publish
    private const string GameVersion = "0.0.1";

    #region Public Fields
    [SerializeField] public byte maxPlayersPerRoom = 4;

    public TMP_InputField roomNameInput;
    public TextMeshProUGUI roomNameText;

    [Header("Room List")]
    public Transform rooms;
    public GameObject roomItemPrefab;

    [Header("Player List")]
    public Transform players;
    public GameObject playerItemPrefab;

    [Header("Start")]
    public GameObject startButton;
    #endregion

    //public delegate void OnPlayerCustomize();
    //public static event OnPlayerCustomize onPlayerCustomize;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        OnPlayerListChange += SetStartButton;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }



    #region Methods

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
            return;
        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        MenuManager.instance.OpenMenu("room");
    }
    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        MenuManager.instance.OpenMenu("room");
    }
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }


    #endregion

    #region PlayerListChange Methods

    void SetStartButton()
    {
        Debug.Log("Start button changed");
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    #endregion

    #region Photon Methods    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Instantiate(playerItemPrefab, players).GetComponent<PlayerItem>().Initialize(player);
        }

        OnPlayerListChange?.Invoke();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform roomItem in rooms)
        {
            Destroy(roomItem.gameObject);
        }
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList || room.PlayerCount == maxPlayersPerRoom)
            {
                continue;
            }

            Instantiate(roomItemPrefab, rooms).GetComponent<RoomItem>().Initialize(room);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject playerItem = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerItem"), Vector2.zero, Quaternion.identity);
        playerItem.GetComponent<PlayerItem>().Initialize(newPlayer);
        playerItem.transform.SetParent(players);
        playerItem.transform.localScale = new Vector3(1, 1, 1);

        OnPlayerListChange?.Invoke();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        OnPlayerListChange?.Invoke();
    }

    #endregion
}