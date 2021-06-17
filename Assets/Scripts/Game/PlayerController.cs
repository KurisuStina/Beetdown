using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static UnityAction OnPlayerHit;

    #region Private Fields
    private PhotonView PV;

    private Rigidbody2D rb;
    private WeaponHandler wh;
    private CharacterData ch;

    private SpriteRenderer sr;
    private Animator anim;

    private PlayerManager playerManager;
    #endregion


    #region Private Serialized Fields/Debugging
    [SerializeField] private Vector2 moveDir;

    #endregion


    #region Public Fields

    public Health healthManager;

    [Header("Movement")]
    public float speed = 10f;
    public float jumpForce = 30f;

    public float gravity = 2f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.05f;

    [Header("Other Checks")]
    public float headCheckDistance = 0.05f;
    #endregion


    void Awake()
    {
        PV = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody2D>();
        wh = GetComponent<WeaponHandler>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

        if (!PV.IsMine)
        {
            gameObject.layer = 10;
            Destroy(rb);
        }
        healthManager.Initialize();
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        
        moveDir.x = Input.GetAxisRaw("Horizontal");

        #region Jumping
        if (Input.GetKeyDown(InputManager.instance.keybinds.jump) && grounded())
        {
            moveDir.y = jumpForce;
        }

        if (moveDir.y <= 0 && grounded())
        {
            moveDir.y = 0;
        }
        #endregion
        #region Combat
        if (Input.GetKey(InputManager.instance.keybinds.attack))
        {
            wh.Attack();
        }
        if (healthManager.isDead())
        {
            PV.RPC("Die", RpcTarget.AllBuffered);
        }
        #endregion
    }

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.velocity = new Vector2(moveDir.x * speed, moveDir.y);

        if (!grounded())
        {
            if (headHit())
            {
                moveDir.y = 0;
            }
            moveDir.y -= gravity;
        }
    }

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.Others, damage);
    }

    void Die()
    {
        playerManager.Die();
    }

    void Initialize(CharacterData charData, WeaponData weaponData)
    {
        Debug.Log("Initialized Player Controller");

        ch = charData;
        wh.Initialize(weaponData);
        //healthManager.Initialize(charData);

        sr.sprite = ch.sprite;
    }

    #region Helper Methods
    /// <summary>
    /// Checks if the player is grounded on the floor
    /// </summary>
    /// <returns></returns>
    bool grounded()
    {
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        RaycastHit2D boxCast = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector2.down, groundCheckDistance, groundLayer);
        Color rayColor;

        if (boxCast.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCol.bounds.center + new Vector3(boxCol.bounds.extents.x, 0), Vector2.down * (boxCol.bounds.extents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boxCol.bounds.center - new Vector3(boxCol.bounds.extents.x, 0), Vector2.down * (boxCol.bounds.extents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boxCol.bounds.center - new Vector3(boxCol.bounds.extents.x, boxCol.bounds.extents.y), Vector2.right * (boxCol.bounds.extents.y * 2), rayColor);

        return boxCast.collider != null;
    }

    /// <summary>
    /// Checks if there is a ground on top of the player
    /// </summary>
    /// <returns></returns>
    bool headHit()
    {
        if (jumping())
        {
            BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
            RaycastHit2D boxCast = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector2.up, groundCheckDistance, groundLayer);

            return boxCast.collider != null;
        }

        return false;
    }

    /// <summary>
    /// Checks if y velocity is greater than 0
    /// </summary>
    /// <returns></returns>
    bool jumping()
    {
        if (moveDir.y > 0)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Collision
    void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile projectile = col.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(projectile.getDamage());
            projectile.Hit();
        }
    }

    #endregion

    #region Photon RPC

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;

        Debug.Log("Hit");
        healthManager.Damage(damage);

        if (healthManager.isDead())
        {
            Die();
        }
    }

    #endregion
}
