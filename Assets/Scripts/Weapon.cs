using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private Animator animator;


    public AudioSource prepareSound;
    public AudioSource throwSoundSource;
    public AudioClip[] throwSounds;

    private bool isShooting;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Shooting and shot preview mechanics
        if(Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }
        
        if(Input.GetButton("Fire2"))
        {
            isShooting = false;
            GetComponent<LineRenderer>().enabled = false;
        }

        if (Input.GetButton("Fire1") && isShooting)
        {
            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPosition(0, firePoint.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, (firePoint.transform.position + mousePos) / 2);
            GetComponent<LineRenderer>().SetPosition(2, mousePos);
        }

        if(Input.GetButtonUp("Fire1") && isShooting)
        {
            animator.SetBool("isAttack", true);
            GetComponent<LineRenderer>().enabled = false;
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetTrigger("Attack");
        prepareSound.Play();
    }

    public void StopAttack()
    {
        if(animator.GetBool("isAttack")) animator.SetBool("isAttack", false);

    }

    public void PlaythrowSound()
    {
        throwSoundSource.clip = throwSounds[Random.Range(0, throwSounds.Length)];
        throwSoundSource.Play();
    }

    public void PlayprepareSound()
    {
        prepareSound.Play();
    }
}
