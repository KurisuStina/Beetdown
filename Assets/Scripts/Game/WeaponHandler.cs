using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class WeaponHandler : MonoBehaviour
{
    //For calculating recoil when shooting weapon
    public const float maxDamage = 100f;
    //For calculating number of melee attacks
    public const int maxCombo = 2;
    private PhotonView PV;
    public WeaponData data;

    public GameObject weaponHolder;
    private Animator weaponAnim;

    [Header("Melee")]
    private BoxCollider2D weaponCollider;

    [Header("Debugging")]
    [SerializeField] private bool canAttack = true;
    [SerializeField] private int meleeCombo = 0;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        weaponAnim = weaponHolder.GetComponent<Animator>();

        weaponCollider = weaponHolder.GetComponent<BoxCollider2D>();
    }

    public void Initialize(WeaponData weaponData)
    {
        data = weaponData;

        weaponHolder.GetComponentInChildren<SpriteRenderer>().sprite = weaponData.sprite;
    }

    void Update()
    {
        PV.RPC("RPC_UpdateAngle", RpcTarget.All, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //weaponHolder.transform.localRotation = Quaternion.Euler(0, 0, InputManager.instance.mouseAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) + 90);
    }

    public void Attack()
    {
        switch (data.type)
        {
            case WeaponType.Melee:
                if (!canAttack)
                    break;

                StartCoroutine(attackTimer());
                break;

            case WeaponType.Ranged:
                if (!canAttack)
                    break;

                PV.RPC("RPC_Shoot", RpcTarget.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), data.ProjectileSpeed);
                StartCoroutine(attackTimer());

                break;
        }
    }

    #region Photon RPC
    [PunRPC]
    void RPC_UpdateAngle(Vector3 mousePosition)
    {
        weaponHolder.transform.localRotation = Quaternion.Euler(0, 0, InputManager.instance.mouseAngle(mousePosition, transform.position) + 90);
    }

    [PunRPC]
    void RPC_Shoot(Vector3 mousePosition, float speed)
    {
        float angle = InputManager.instance.mouseAngle(mousePosition, transform.position);
        Projectile projectile = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Projectile"), transform.position, Quaternion.Euler(0, 0, angle + getShootOffset())).GetComponent<Projectile>();
        projectile.Set(data.projectileSprite, 
            InputManager.instance.mouseDirection(mousePosition, transform.position) * speed, data.Damage, 
            data.Range, 
            data.projectileSprite.bounds.extents, 
            PV.IsMine,
            data.Duration,
            data.Magnitude);
    }

    #endregion

    IEnumerator attackTimer()
    {
        canAttack = false;
        switch (data.type)
        {
            case WeaponType.Melee:
                weaponAnim.SetTrigger("Melee_start");
                meleeCombo = (meleeCombo + 1) % (maxCombo);
                weaponAnim.SetInteger("Melee_Combo", meleeCombo);
                break;

            case WeaponType.Ranged:
                weaponAnim.SetTrigger("Ranged_start");
                break;
        }
        yield return new WaitForSeconds(data.AttackSpeed);
        switch (data.type)
        {
            case WeaponType.Melee:
                weaponAnim.SetTrigger("Melee_end");
                break;

            case WeaponType.Ranged:
                weaponAnim.SetTrigger("Ranged_end");
                break;
        }
        canAttack = true;
    }

    #region Helper Methods


    public float getShootOffset()
    {
        return Random.Range(-data.Inaccuracy, data.Inaccuracy);
    }


    #endregion
}
