using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodActive : MonoBehaviour {

    public static bool alive;
	// Use this for initialization
	void Start () {
        alive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.activeSelf)//출혈 생겼으면
        {
            alive = true;
        }
        else if(!gameObject.activeSelf)//출혈 안 생겼으면
        {
            //Debug.Log(alive);
            alive = false;
        }
	}
}
