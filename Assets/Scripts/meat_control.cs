﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meat_control : MonoBehaviour {

    bool meat_col;
	// Use this for initialization
	void Start () {
        meat_col = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("meat clicked");
        meat_col = true;//meat를 클릭해서 true로 설정한다
    }

    public void disappear()//다른 script에서 부르면 이 script를 붙인 gameobject가 사라진다
    {
        if(meat_col)//meat를 클릭했을때만 사라진다
        {
            gameObject.SetActive(false);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Debug.Log("collided with player");
            gameObject.SetActive(false);
        }
    }
    */
}
