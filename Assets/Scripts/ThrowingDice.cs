using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDice : MonoBehaviour
{
    public float speed;
    public float explosionTime = 2f;
    public float explosionRadius;
    private Rigidbody2D rb;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("Explode", explosionTime);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D col in cols)
        {
            IDamagable hit = col.GetComponent<IDamagable>();
            if(hit != null)
            {
                hit.Damage();
                Debug.Log("Damaged " + col.name);
            } 
        }

        Destroy(this.gameObject);
    }

    public void AdjustThrowSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void AdjustSlowDown(float newSpeed)
    {
        rb.drag = newSpeed;
    }
}
