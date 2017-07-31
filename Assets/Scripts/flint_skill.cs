using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flint_skill : MonoBehaviour {

    // Use this for initialization
    public bool flint_click;
	void Start () {
        flint_click = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()//flint 버튼을 클릭하면
    {
        Debug.Log("flint skill clicked");
        flint_click = true;
    }
}
