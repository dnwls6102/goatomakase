using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour
{
    public static BtnSound instance;
    public AudioSource audioSource;
    public AudioClip btnSound;
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
}
