using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected Rigidbody2D rb;
    protected AudioSource deathAudio;


    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(rb);;
        Anim.SetTrigger("death");
        deathAudio.Play();
    }
 
}
