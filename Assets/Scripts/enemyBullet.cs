using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Enemy") return;

        if(col.collider.tag == "Player")
        {
            col.collider.GetComponent<IDamagable>().Damage(1);
        }
        Destroy(gameObject);
    }
}
