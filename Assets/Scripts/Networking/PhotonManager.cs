using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;

    //Change this on each publish
    private const string GameVersion = "0.0.1";


    [SerializeField] public byte maxPlayersPerRoom = 4;

    public TMP_InputField roomNameInput;
    public TextMeshProUGUI roomNameText;

    [Header("Room List")]
    public Transform rooms;
    public GameObject roomItemPrefab;

    [Header("Player List")]
    public Transform players;
    public GameObject playerItemPrefab;

    public GameObject startButton;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.JoinOrCreateRoom("Game", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
    }



    #region Room Methods
    public void JoinRoom(RoomInfo roomInfo)
    {

        PhotonNetwork.JoinRoom(roomInfo.Name);
        MenuManager.instance.OpenMenu("room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Instantiate(playerItemPrefab, players).GetComponent<PlayerItem>().Initialize(player);
        }

        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }


    public void CreateRoom()
    {
        Debug.Log("Created room: " + roomNameInput.text);
        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }


    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        MenuManager.instance.OpenMenu("roomlist");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
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
        Instantiate(playerItemPrefab, players).GetComponent<PlayerItem>().Initialize(newPlayer);
    }

    #endregion
}