using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public float _health;
    public float Health {get; set;}

    public float damageCoolDown;
    private float damageCooldownTimer;

    void Start()
    {
        Health = _health;
    }


    void Update()
    {
        damageCooldownTimer -= Time.deltaTime;
    }

    public void Damage(float damage)
    {
        if(damageCooldownTimer > 0) return;

        Health -= damage;
        damageCooldownTimer = damageCoolDown;
        Debug.Log("Hit Player");
        
        if (Health <= 0)
        {
            Debug.Log("Loose");
        }
    }
}