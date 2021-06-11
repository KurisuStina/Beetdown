using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviourPunCallbacks
{
    private Camera m_cam;
    private Vector3 velocity;

    [Header("Properties")]
    public GameObject player;

    public float dampSpeed = 0.075f;

    public float maxDistance = 3f;


    [Header("Debugging")]
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 targetPos;

    
    void Start()
    {
        m_cam = GetComponent<Camera>();
        if (!photonView.IsMine)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        playerPos = player.transform.position;

        offset = (InputManager.instance.mousePos() - player.transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        targetPos = new Vector3(player.transform.position.x + offset.x * maxDistance, player.transform.position.y + offset.y * maxDistance, m_cam.transform.position.z);
        //Vector3 target = new Vector3(playerPos.x, playerPos.y, m_cam.transform.position.z);

        m_cam.transform.position = Vector3.SmoothDamp(m_cam.transform.position, targetPos, ref velocity, dampSpeed);
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

}
