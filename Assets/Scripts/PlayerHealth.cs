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

    public AudioClip deathMusic;

    public GameObject LoosingScreen;

    public AudioSource hitSound;

    void Start()
    {
        maxHealth = 6;
        Health = maxHealth;
        Time.timeScale = 1;
        HealthHeartManager.instance.DrawHearts(); // Refreshes the hearts
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
            Time.timeScale = 0;
            LoosingScreen.SetActive(true);
            AudioManagerScript.instance.musicSource.clip = deathMusic;
            AudioManagerScript.instance.musicSource.time = 0;
            AudioManagerScript.instance.musicSource.Play();
        }

        hitSound.Play();
    }
}
