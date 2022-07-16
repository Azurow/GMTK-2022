using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDice : MonoBehaviour
{
    public float maxSpeed;
    float calculatedSpeed; //The speed after the calculating the distance of the mouse into it
    public float explosionTime = 2f;
    public float explosionRadius;
    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    public GameObject dicePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        calculatedSpeed = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(calculatedSpeed > maxSpeed)
        {
            calculatedSpeed = maxSpeed;
        }
        rb.velocity = transform.right * calculatedSpeed;

        Invoke("Explode", explosionTime);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Instantiate(dicePrefab, transform.position, Quaternion.identity);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D col in cols)
        {
            IDamagable hit = col.GetComponent<IDamagable>();
            if(hit != null)
            {
                hit.Damage(1);
                Debug.Log("Damaged " + col.name);
            } 
        }

        Destroy(this.gameObject);
    }
}
