using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource playerAudio;

    [SerializeField]
    private AudioClip jumpAudio, hurtAudio;


	private void Awake()
	{
        instance = this;
	}


	public void JumpAudio()
    {
        playerAudio.clip = jumpAudio;
        playerAudio.Play();
    }

    public void HurtAudio()
    {
        playerAudio.clip = hurtAudio;
        playerAudio.Play();
    }

    /*public void CherryAudio()
    {
        playerAudio.clip = cherryAudio;
        playerAudio.Play();
    }

    public void GemAudio()
    {
        playerAudio.clip = gemAudio;
        playerAudio.Play();
    }*/
}
