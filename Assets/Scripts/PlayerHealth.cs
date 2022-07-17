using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public float maxHealth;
    public float Health {get; set;}

    public float damageCoolDown;
    private float damageCooldownTimer;

    void Start()
    {
        maxHealth = 6;
        Health = maxHealth;
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
        HealthHeartManager.instance.DrawHearts(); // Refreshes the hearts
        Debug.Log("Hit Player");
        
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
