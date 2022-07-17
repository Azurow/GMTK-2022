using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamagable
{
    public float startHealth;
    public Vector2 knockBack;
    public float damage;
    
    public Transform firePoint;

    public float Health { get; set; }
    public float speed;

    public Vector2 hitRegion;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;

    public AudioSource hitSound;
    public AudioSource deathSound;

    public GameObject bullet;

    public bool shootingMode;
    public float shootInterval;
    private float shootTimer;


    // Start is called before the first frame update
    void Start()
    {
        Health = startHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("switchModes", 7.5f, Random.Range(5f, 10f));
    }

    void Update()
    {
        animator.SetBool("isShooting", shootingMode);
        if(shootingMode)
        {
            shootTimer -= Time.deltaTime;
            Shoot();
        }
        else {
                
            // Walk to Player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }


        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(player.transform.position.y > transform.position.y)
        {
            animator.SetFloat("Vertical", 1f);
        } else 
        {
            animator.SetFloat("Vertical", -1f);
        }

        // Attacking Player
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, hitRegion, 0, Vector2.zero);
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.tag == "Player")
            {
                
                hit.collider.gameObject.GetComponent<IDamagable>().Damage(damage);
            }
        }
    }

    void Shoot()
    {
        if(shootTimer > 0) return;
        
        GameObject bulletObject = Instantiate(bullet, firePoint.position, Quaternion.identity);
        shootTimer = shootInterval;
    }

    void switchModes()
    {
        shootingMode = !shootingMode;
    }
    
    public void Damage(float damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            speed = 0;
            animator.SetTrigger("Death");
            ScoreManager.instance.AddPoint();
            deathSound.Play();
            shootingMode = false;
            Destroy(this.gameObject, 1.5f);
        }

        hitSound.Play();
    }
}
