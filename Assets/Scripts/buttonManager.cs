using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

    Sprite[] howScenes;
    Sprite[] howUIScenes;
    int moveScene;
    int animalScene;
    int effectScene;
    int UIScene;
    int whoScene;
    GameObject buttonPanel;
    public bool move;
    public bool animal;
    public bool effect;
    public bool ui;
    public bool who;
    GameObject button0;
    GameObject button1;
    GameObject button2;
    GameObject button3;
    GameObject button4;
    GameObject back;
    Sprite backGround;


	// Use this for initialization
	void Start () {
        howScenes = Resources.LoadAll<Sprite>("HowToPlay");
        howUIScenes = Resources.LoadAll<Sprite>("howUI");
        buttonPanel = GameObject.Find("buttonPanel");
        moveScene = 0;
        animalScene = 0;
        effectScene = 0;
        UIScene = 0;
        whoScene = 0;
        button0 = GameObject.Find("Button0");
        button0.SetActive(true);
        button1 = GameObject.Find("Button1");
        button1.SetActive(true);
        button2 = GameObject.Find("Button2");
        button2.SetActive(true);
        button3 = GameObject.Find("Button3");
        button3.SetActive(true);
        button4 = GameObject.Find("Button4");
        button4.SetActive(true);
        backGround = buttonPanel.GetComponent<Image>().sprite;
        back = GameObject.Find("Back");
        back.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goBack()
    {
        SceneManager.LoadScene("Start");
    }

    void showButton()//초기화하고 버튼들을 다시 보여준다
    {
        moveScene = 0;
        animalScene = 0;
        effectScene = 0;
        UIScene = 0;
        whoScene = 0;
        button0.SetActive(true);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);
        back.SetActive(true);
    }

    void hideButton()//버튼들을 숨긴다
    {
        button0.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        back.SetActive(false);
    }

    public void clickChange()
    {
        //Debug.Log("clickChange");
        if(move)
        {
            //hideButton();
            howToMove();
        }
        else if(animal)
        {
            //hideButton();
            howAnimal();
        }
        else if(effect)
        {
            //hideButton();
            howEffect();
        }
        else if(ui)
        {
            //hideButton();
            howUI();
        }
        else if(who)
        {
            //hideButton();
            whoMake();
        }
        else
        {
            showButton();
            buttonPanel.GetComponent<Image>().sprite = backGround;
        }
    }

    public void howToMove()//조작방식
    {
        hideButton();
        move = true;
        if(moveScene==0)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[0];
            moveScene++;
        }
        else if(moveScene==1)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[1];
            moveScene++;
        }
        else if(moveScene==2)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[5];
            moveScene++;
        }
        else if(moveScene==3)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[6];
            moveScene++;
            move = false;
        }
    }

    public void howAnimal()//동물설명
    {
        hideButton();
        animal = true;
        if(animalScene==0)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[7];
            animalScene++;
        }
        else if(animalScene==1)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[8];
            animalScene++;
        }
        else if(animalScene==2)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[9];
            animalScene++;
        }
        else if(animalScene==3)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[10];
            animalScene++;
            animal = false;
        }
    }
    public void howEffect()//상태설명
    {
        hideButton();
        effect = true;
        if(effectScene==0)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[2];
            effectScene++;
        }
        else if(effectScene==1)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[3];
            effectScene++;
            effect = false;
        }
                      
    }
    public void howUI()//UI설명-----------문제 생긴다...왜??
    {
        hideButton();
        ui = true;
        if(UIScene==0)
        {
            Debug.Log("ui0");
            buttonPanel.GetComponent<Image>().sprite = howUIScenes[2];
            //buttonPanel.GetComponent<Image>().sprite = howScenes[13];
            UIScene++;
        }
        else if (UIScene == 1)
        {
            Debug.Log("ui1");
            buttonPanel.GetComponent<Image>().sprite = howUIScenes[3];
            //buttonPanel.GetComponent<Image>().sprite = howScenes[14];
            UIScene++;
        }
        else if (UIScene == 2)
        {
            Debug.Log("ui2");
            buttonPanel.GetComponent<Image>().sprite = howUIScenes[0];
           // buttonPanel.GetComponent<Image>().sprite = howScenes[11];
            UIScene++;
        }
        else if(UIScene==3)
        {
            Debug.Log("ui3");
            buttonPanel.GetComponent<Image>().sprite = howUIScenes[1];
            //buttonPanel.GetComponent<Image>().sprite = howScenes[12];
            UIScene++;
            ui = false;
        }
    }
    public void whoMake()//제작
    {
        hideButton();
        who = true;
        if(whoScene==0)
        {
            buttonPanel.GetComponent<Image>().sprite = howScenes[4];
            whoScene++;
        }
        else if(whoScene==1)
        {
            whoScene++;
            who = false;
        }
    }
}
