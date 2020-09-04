using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource isGotAudio;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        isGotAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void IsGot()
    {
        GetComponent<Collider2D>().enabled = false;
        Anim.SetTrigger("isGot");
        isGotAudio.Play();
    }

}
