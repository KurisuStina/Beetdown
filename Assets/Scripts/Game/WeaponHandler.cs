using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponHandler : MonoBehaviour
{
    private PhotonView pv;
    public WeaponData data;

    [Header("Debugging")]
    [SerializeField] private bool canAttack = true;

    void Start()
    {
        pv = GetComponent<PlayerMove>().photonView;
    }

    void Update()
    {

    }

    public void Attack()
    {
        switch (data.type)
        {
            case WeaponType.Melee:

                break;

            case WeaponType.Ranged:
                if (!canAttack)
                    break;
                    
                pv.RPC("Shoot", RpcTarget.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), data.ProjectileSpeed);
                StartCoroutine(attackTimer());

                break;
        }
    }

    [PunRPC]
    void Shoot(Vector3 mousePosition, float speed)
    {
        Projectile projectile = PhotonNetwork.Instantiate("Projectile", transform.position, Quaternion.Euler(0, 0, InputManager.instance.mouseAngle(mousePosition, transform.position) + getShootOffset())).GetComponent<Projectile>();
        projectile.Set(data.projectileSprite, 
            InputManager.instance.mouseDirection(mousePosition, transform.position) * speed, data.Damage, 
            data.Range, 
            data.projectileSprite.bounds.extents, 
            pv.IsMine,
            data.Duration,
            data.Magnitude);
    }


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
