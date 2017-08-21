using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour {
    //public GameObject[] characters;
    // this will store what character you have selected
    public GameObject charSelected;

    public GameObject char_meat;
    public GameObject char_fire;

    public static bool meat_char;
    public static bool fire_char;

    // Use this for initialization
    void Start () {
        meat_char = false;
        fire_char = false;
        charSelected = gameObject;
}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startWithHP()
    {
        meat_char = true;
        fire_char = false;
        DontDestroyOnLoad(charSelected);
        SceneManager.LoadScene("Field");
    }
    public void startWithMP()
    {
        fire_char = true;
        meat_char = false;
        DontDestroyOnLoad(charSelected);
        SceneManager.LoadScene("Field");
    }
}
