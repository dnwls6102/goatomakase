using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour
{
    public static BtnSound instance;
    public AudioSource audioSource;
    public AudioClip btnSound;
    public AudioClip Fail1;
    public AudioClip Nice1;

    // Start is called before the first frame update
    void Awake()
    {
        if (BtnSound.instance == null)
        {
            BtnSound.instance = this;
        }
    }

    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(btnSound);
    }
    public void PlayFailSound()
    {
        audioSource.PlayOneShot(Fail1);
    }
    public void PlayNiceSound()
    {
        audioSource.PlayOneShot(Nice1);
    }

}
