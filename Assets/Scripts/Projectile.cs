using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class Projectile : MonoBehaviour
{
    private Vector3 startPos;
    private SpriteRenderer sRend;
    private Rigidbody2D rb;

    // Properties
    private float damage;
    private float range;

    private float shakeDuration;
    private float shakeMagnitude;


    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(startPos, transform.position) > range)
        {
            Destroy();
        }
        if(sRend == null)
        {
            Destroy();
        }
    }

    public void Set(Sprite pSprite, Vector2 velocity, float damage, float range, Vector3 size, bool isAlly, float shakeDuration, float shakeMagnitude)
    {
        //Components
        sRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sRend.sprite = pSprite;
        rb.velocity = velocity;

        //Stats
        this.damage = damage;
        this.range = range;
        transform.localScale = size;

        //Camera Shake
        this.shakeDuration = shakeDuration;
        this.shakeMagnitude = shakeMagnitude;

        if (isAlly)
        {
            gameObject.layer = 11; //Sets layer to "Projectile"
        }
        else
        {
            gameObject.layer = 12; //Sets layer to "EnemyProjectile"
        }

        
    }

    public float getDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy();
        CameraShake.StartShake(shakeDuration, shakeMagnitude);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region Collisions

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.layer)
        {
            case 8: //Ground
                Destroy();
                break;

            case 10: //Enemy
                
                break;
        }
    }

    #endregion
}
