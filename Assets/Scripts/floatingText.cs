using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingText : MonoBehaviour {
    public Animator anim;
    private Text gageText;


	// Use this for initialization
	void Start () {
        gageText = anim.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(string text)
    {
        gageText.text = text;
    }
}
