using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public AudioClip clickClip;
    //public AudioSource clickSource;

    public static SoundManager soundManager;


    void Awake()
    {
        if (SoundManager.soundManager == null)
        {
            SoundManager.soundManager = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        //clickSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClickSound()
    {
        //clickClip.
        //AudioSource.PlayClipAtPoint(clickClip)
    }
}
