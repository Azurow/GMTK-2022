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
    
    public GameObject resultDisplay;
    public Sprite[] resultSprites;
    public int rollResult; //Result the dice gives (1-6)

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Throwing speed depending on distance to mouse
        calculatedSpeed = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(calculatedSpeed > maxSpeed)
        {
            calculatedSpeed = maxSpeed;
        }
        rb.velocity = transform.right * calculatedSpeed;

        //Random system
        int[] weights = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6};
        rollResult = weights[Random.RandomRange(0, weights.Length)];
        Invoke("Explode", explosionTime);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject resultDisplayObject = Instantiate(resultDisplay, transform.position, Quaternion.identity);
        resultDisplayObject.GetComponent<SpriteRenderer>().sprite = resultSprites[rollResult - 1];
        Destroy(resultDisplayObject, 1.5f);

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D col in cols)
        {
            IDamagable hit = col.GetComponent<IDamagable>();
            if(hit != null)
            {
                hit.Damage(rollResult / 2);
                Debug.Log("Damaged " + col.name);
            } 
        }

        Destroy(this.gameObject);
    }
}
