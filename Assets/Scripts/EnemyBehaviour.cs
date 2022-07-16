using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamagable
{
    public float startHealth;
    public Vector2 knockBack;
    public float damage;


    public float Health { get; set; }
    public float speed;

    public Vector2 hitRegion;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Health = startHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Walk to Player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

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
    
    public void Damage(float damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            speed = 0;
            animator.SetTrigger("Death");
            Destroy(this.gameObject, 1.5f);
        }
    }
}
