using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour {
    public static AudioSource Mainbgm;


	// Use this for initialization
	void Start () {
		if(Mainbgm!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Mainbgm = gameObject.GetComponent<AudioSource>();
            Mainbgm.mute = false;
            DontDestroyOnLoad(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
