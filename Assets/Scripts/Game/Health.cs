using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public float getHealth()
    {
        return health;
    }

    public void Initialize(CharacterData character)
    {
        maxHealth = character.MaxHealth;
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
