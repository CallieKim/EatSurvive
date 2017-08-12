using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildPig_track : MonoBehaviour {

    public bool track;//플레이어가 시야에 들었는지 판단하는 변수

	// Use this for initialization
	void Start () {
        track = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")//플레이어가 멧돼지의 시선안에 들면 
        {
            //Debug.Log("wildPig will track player");
            track = true;
        }
    }
}
