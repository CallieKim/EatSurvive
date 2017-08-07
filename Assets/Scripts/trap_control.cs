using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_control : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void disappear()//다른 script에서 부르면 이 script를 붙인 gameObject가 사라진다
    {
        gameObject.SetActive(false);
    }
}
