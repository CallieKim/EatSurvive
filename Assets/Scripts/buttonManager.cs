using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour {

    Sprite[] howScenes;
    int moveScene;
    int animalScene;
    int effectScene;
    int UIScene;
    GameObject buttonPanel;

	// Use this for initialization
	void Start () {
        howScenes = Resources.LoadAll<Sprite>("HowToPlay");
        buttonPanel = GameObject.Find("buttonPanel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickChange()
    {

    }

    public void howToMove()//조작방식
    {

    }

    public void howAnimal()//동물설명
    {

    }
    public void howEffect()//상태설명
    {

    }
    public void howUI()//UI설명
    {

    }
    public void who()//제작
    {
        buttonPanel.GetComponent<Image>().sprite = howScenes[12];
    }
}
