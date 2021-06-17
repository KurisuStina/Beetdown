using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class WeaponHandler : MonoBehaviour
{
    private PhotonView PV;
    public WeaponData data;

    [Header("Debugging")]
    [SerializeField] private bool canAttack = true;


    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Initialize(WeaponData weaponData)
    {
        data = weaponData;
    }


    public void Attack()
    {
        switch (data.type)
        {
            case WeaponType.Melee:
                if (!canAttack)
                    break;

                break;

            case WeaponType.Ranged:
                if (!canAttack)
                    break;

                PV.RPC("Shoot", RpcTarget.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), data.ProjectileSpeed);
                StartCoroutine(attackTimer());

                break;
        }
    }

    #region Photon RPC
    [PunRPC]
    void UpdateAngle(float mouseAngle)
    {

    }

    [PunRPC]
    void Shoot(Vector3 mousePosition, float speed)
    {
        Projectile projectile = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Projectile"), transform.position, Quaternion.Euler(0, 0, InputManager.instance.mouseAngle(mousePosition, transform.position) + getShootOffset())).GetComponent<Projectile>();
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
        yield return new WaitForSeconds(data.AttackSpeed);
        canAttack = true;
    }


    #region Helper Methods


    public float getShootOffset()
    {
        return Random.Range(-data.Inaccuracy, data.Inaccuracy);
    }


    #endregion
}
