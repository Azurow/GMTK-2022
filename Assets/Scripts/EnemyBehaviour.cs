using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamagable
{
    public float startHealth;
    public float Health { get; set; }
    public float speed;

    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Health = startHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Walk to Player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
    public void Damage()
    {
        Health--;

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
