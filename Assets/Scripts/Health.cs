using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    public float maxHealth;
    [SerializeField] private float health;

    public float getHealth()
    {
        return health;
    }

    public void init()
    {
        health = maxHealth;
    }

    public void Heal(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public void Damage(float damage)
    {
        health = Mathf.Max(0, health - damage);
    }
    
    public bool isDead()
    {
        return health == 0;
    }
}
