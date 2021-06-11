using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //Change this on each publish
    private const string GameVersion = "0.0.1";


    [SerializeField] public byte maxPlayersPerRoom = 4;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Connect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Game", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        InitRoom();

    }

    void InitRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-19, 19), -2), Quaternion.identity);

        GameObject playerCam = PhotonNetwork.Instantiate("Camera", new Vector3(player.transform.position.x, player.transform.position.y, -10), Quaternion.identity);
        CameraMove cMove = playerCam.gameObject.GetComponent<CameraMove>();
        cMove.SetPlayer(player);
        Debug.Log("Player Set to Camera");
    }
}