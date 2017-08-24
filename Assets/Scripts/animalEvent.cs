using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class animalEvent : MonoBehaviour {
    Sprite[] rabbitEvents;
    Sprite[] badgerEvents;
    Sprite[] buffEvents;
    public Score scoreScript;
    public bool rabbitHappen;//토끼 이벤트가 발생할때 true
    public bool badgerHappen;//오소리 이벤트가 발생할때 true
    GameObject eventpanel;
    GameObject button1;//토끼
    GameObject button2;//토끼
    GameObject button3;//토끼--부정
    GameObject button4;//오소리
    GameObject button5;//오소리
    GameObject button6;//오소리--부정
    public bool selected1;//선택지 1을 선택했는지 아닌지
    public bool selected2;//선택지 2을 선택했는지 아닌지
    public bool selected3;//선택지 3을 선택했는지 아닌지----부정적
    public bool selected4;
    public bool selected5;
    public bool end;//이벤트 끝났는지 판단하는 변수
    int sceneNumber;//몇번째 장면인지 알려주는 변수
    GameObject timeBar;
    GameObject timefill;
    float cur_health;
    float dec_health = 1f;
    float max_health=30f;//30초동안 버프가 주어진다
    float dec_delay = 1.0f;
    public bool eventOn;//이벤트중인지 판단하는 변수이다
    GameObject barMeat;
    GameObject barFire;

    public static bool meat_invincible;//체력무적일때 true
    public static bool fire_invincible;//장작무적일때 true

    private void Awake()
    {
        meat_invincible = false;
        fire_invincible = false;
        timeBar = GameObject.Find("timeBar");//시간 담는 게이지를 찾는다
        timefill = GameObject.Find("timeFill");//시간 게이지를 찾는다
        barMeat = GameObject.Find("meatFill");
        barFire = GameObject.Find("fireFill");

    }
    // Use this for initialization
    void Start () {
        eventOn = false;
        timeBar.SetActive(false);
        cur_health = max_health;
        //InvokeRepeating("decreaseHealth", 0.5f, dec_delay);//0.5초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
        sceneNumber = 0;
        end = false;
        selected1 = false;
        selected2 = false;
        selected3 = false;
        selected4 = false;
        selected5 = false;
        button1 = GameObject.Find("select1");
        button1.SetActive(false);
        button2 = GameObject.Find("select2");
        button2.SetActive(false);
        button3 = GameObject.Find("select3");
        button3.SetActive(false);
        button4 = GameObject.Find("select4");
        button4.SetActive(false);
        button5 = GameObject.Find("select5");
        button5.SetActive(false);
        button6 = GameObject.Find("select6");
        button6.SetActive(false);
        eventpanel = GameObject.Find("eventPanel");
        eventpanel.SetActive(false);
        scoreScript = GameObject.FindGameObjectWithTag("score").GetComponent<Score>();
        rabbitEvents = Resources.LoadAll<Sprite>("Event/rabbitEvent");//토끼 이벤트 모습들을 배열에 저장한다
        badgerEvents = Resources.LoadAll<Sprite>("Event/badgerEvent");//오소리 이벤트 모습들을 배열에 저장한다
        buffEvents = Resources.LoadAll<Sprite>("Event/buff");//버프 이미지들을 배열에 저장한다
        rabbitHappen = false;
        badgerHappen = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("event update");
		if(scoreScript.rabbitKill>39 && !rabbitHappen && !eventOn)//토끼를 40마리 잡으면 이벤트 발생한다, 시간 이벤트 발생중 아니면
        {
            GameObject.Find("field").GetComponent<AudioSource>().volume = 0.2f;
            //Debug.Log("event happen");
            Time.timeScale = 0;
            eventpanel.SetActive(true);
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[13];
            rabbitHappen = true;
            scoreScript.rabbitKill = 0;

        }
        else if(scoreScript.badgerKill>14 & !badgerHappen && !eventOn)//오소리를 15마리 잡으면 이벤트 발생한다, 시간 이벤트 발생중 아니면
        {
            GameObject.Find("field").GetComponent<AudioSource>().volume = 0.2f;
            Time.timeScale = 0;
            eventpanel.SetActive(true);
            eventpanel.GetComponent<Image>().sprite = badgerEvents[13];
            badgerHappen = true;
            scoreScript.badgerKill = 0;//잡은 마리수 초기화

        }
	}

    public void EventChange()//클릭할때마다 장면 바뀐다
    {
        if(rabbitHappen)//토끼 이벤트 발생할때
        {
            rabbitEventChange();
        }
        else if(badgerHappen)
        {

            badgerEventChange();
        }

    }

    public void Skip()//스킵 버튼을 누르면 바로 선택지로 이동한다
    {
        if(rabbitHappen)//토끼 이벤트 발생중일때
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[6];
            showButton();

        }
        else if(badgerHappen)//오소리 이벤트 발생중일때
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[5];
            showButton();
        }
    }

    public void rabbitEventChange()
    {
        if (end)//이벤트 끝날때 됐으면
        {
            sceneNumber = 0;//장면 다시 초기화한다
            endEvent();
        }
        if (sceneNumber == 0)//이벤트 첫 시작화면
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[0];
            sceneNumber++;
        }
        else if(sceneNumber==1)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[1];
            sceneNumber++;
        }
        else if(sceneNumber==2)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[2];
            sceneNumber++;
        }
        else if (sceneNumber == 3)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[3];
            sceneNumber++;
        }
        else if (sceneNumber == 4)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[4];
            sceneNumber++;
        }
        else if (sceneNumber == 5)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[5];
            sceneNumber++;
        }
        else if (sceneNumber == 6)
        {
            eventpanel.GetComponent<Image>().sprite = rabbitEvents[6];
            showButton();
            sceneNumber++;
        }

        if (selected1)//선택지를 선택했으면 버프를 보여준다
        {
            showBuff(buffEvents[2]);//점수 1.5배
            SoundManager.instance.PlaySound("good_buff");
            Rabbit_move.rabbitScore = 150;
            Badger_move.badgerScore = 450;
            end = true;
        }
        else if(selected2)
        {
            showBuff(buffEvents[3]);//체력 무적
            SoundManager.instance.PlaySound("good_buff");
            barMeat.GetComponent<Bar_meat_control>().invincible();
            barMeat.GetComponent<Bar_meat_control>().enabled = false;
            meat_invincible = true;
            fire_invincible = false;
            end = true;
        }
        else if(selected3)
        {
            showBuff(buffEvents[0]);//부정적 버프
            SoundManager.instance.PlaySound("bad_buff");
            Rabbit_move.rabbitScore = 50;
            Badger_move.badgerScore = 150;
            end = true;
        }

    }

    public void badgerEventChange()
    {
        if (end)//이벤트 끝날때 됐으면
        {
            sceneNumber = 0;//장면 번홏 초기화 한다
            endEvent();
        }

        if (sceneNumber == 0)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[0];
            sceneNumber++;
        }
        else if (sceneNumber == 1)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[1];
            sceneNumber++;
        }
        else if (sceneNumber == 2)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[2];
            sceneNumber++;
        }
        else if (sceneNumber == 3)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[3];
            sceneNumber++;
        }
        else if (sceneNumber == 4)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[4];
            sceneNumber++;
        }
        
        else if (sceneNumber == 5)
        {
            eventpanel.GetComponent<Image>().sprite = badgerEvents[5];
            showButton();
            sceneNumber++;
        }
        
        if (selected4)//선택지를 선택했으면 버프를 보여준다
        {
            showBuff(buffEvents[1]);//타격 강화
            SoundManager.instance.PlaySound("good_buff");
            end = true;
        }
        else if (selected5)
        {
            showBuff(buffEvents[4]);//장작 무적
            SoundManager.instance.PlaySound("good_buff");
            barFire.GetComponent<Bar_fire_control>().invincible();
            barFire.GetComponent<Bar_fire_control>().enabled = false;
            meat_invincible = false;
            fire_invincible = true;
            end = true;
        }
        else if (selected3)//부정적 버프
        {
            showBuff(buffEvents[0]);
            //동물 잡는 점수가 절반으로 줄어든다
            SoundManager.instance.PlaySound("bad_buff");
            Rabbit_move.rabbitScore = 50;
            Badger_move.badgerScore = 150;
            end = true;
        }
    }

    public void showButton()//선택지를 보여준다
    {
        if(rabbitHappen)
        {
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
        }
        else if(badgerHappen)
        {
            button4.SetActive(true);
            button5.SetActive(true);
            button6.SetActive(true);
        }

    }
    public void hideButton()//선택지를 숨긴다
    {
        if (rabbitHappen)
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
        }
        else if (badgerHappen)
        {
            button4.SetActive(false);
            button5.SetActive(false);
            button6.SetActive(false);
        }

    }

    public void endEvent()//이벤트를 끝낸다
    {
        selected1 = false;//선택지들 초기화한다
        selected2 = false;
        selected3 = false;
        selected4 = false;
        selected5 = false;
        end = false;
        badgerHappen = false;
        rabbitHappen = false;
        Time.timeScale = 1;
        eventpanel.SetActive(false);
        timeBar.SetActive(true);
        GameObject.Find("field").GetComponent<AudioSource>().volume = 1f;
        eventOn = true;//시간 이벤트 발생중이니 true로 설정한다
        InvokeRepeating("decreaseHealth", 0.0f, dec_delay);//0.0초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다

    }

    public void selectButton1()//긍정 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = rabbitEvents[10];
        hideButton();
        selected1=true;
    }
    public void selectButton2()//긍정 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = rabbitEvents[11];
        hideButton();
        selected2 = true;
    }
    public void selectButton3()//안좋은 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = rabbitEvents[12];
        hideButton();
        selected3 = true;
    }
    public void selectButton4()//긍정 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = badgerEvents[6];
        hideButton();
        selected4 = true;
    }
    public void selectButton5()//긍정 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = badgerEvents[7];
        hideButton();
        selected5 = true;
    }
    public void selectButton6()//부정 선택지, 반응 보여준다
    {
        eventpanel.GetComponent<Image>().sprite = badgerEvents[8];
        hideButton();
        selected3 = true;
    }



    public void showBuff(Sprite scene)
    {
        eventpanel.GetComponent<Image>().sprite = scene;
    }

    public void decreaseHealth()//script의 dec_health만큼 감소시킨다
    {
        if(cur_health<0)//시간 게이지가 다 떨어지면 사라진다, 이벤트 효과가 사라진다
        {
            cur_health = max_health;//체력 다시 초기화한다
            CancelInvoke("decreaseHealth");
            eventOn = false;//시간 이벤트 끝났으니 false로 설정한다
            timeBar.SetActive(false);//시간 게이지 사라지게 한다
            effectDisappear();//이벤트 효과 사라지게 한다
        }
        cur_health -= dec_health;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
    }


    void setHealth(float myHealth)
    {
        timefill.GetComponent<Image>().fillAmount = myHealth;
    }

    public void effectDisappear()//이벤트 효과 사라지게 한다
    {
        //점수가 원래대로 돌아온다
        Rabbit_move.rabbitScore = 100;
        Badger_move.badgerScore = 300;
        meat_invincible = false;
        fire_invincible = false;
        if (!barMeat.GetComponent<Bar_meat_control>().isActiveAndEnabled)//체력바 스크립트가 꺼져있으면 다시 킨다
        {
            barMeat.GetComponent<Bar_meat_control>().enabled = true;
            barMeat.GetComponent<Bar_meat_control>().notInvincible();
        }
        else if(!barFire.GetComponent<Bar_fire_control>().isActiveAndEnabled)//장작바 스크립트가 꺼져있으면 다시 킨다
        {
            barFire.GetComponent<Bar_fire_control>().enabled = true;
            barFire.GetComponent<Bar_fire_control>().notInvincible();
        }
    }
}
