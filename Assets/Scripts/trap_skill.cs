using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trap_skill : MonoBehaviour {

    // Use this for initialization
    public Sprite[] trapSprites;
    public bool trap_click;
    public int trapSize = 5;
    //public int trapSize_fire = 3;
    public GameObject trapOriginal;
    public GameObject player;
    Text trapNumber;
    Bar_meat_control bar_meat;
    Bar_fire_control bar_fire;

    GameObject skill_icon_meat;//체력 캐릭터일때 나올 아이콘
    GameObject skill_icon_fire;//장작 캐릭터일때 나올 아이콘

    //GameObject[] traps = new GameObject[5];//함정을 저장할 배열
    public Queue<GameObject> traps = new Queue<GameObject>();

    private void Awake()
    {
        skill_icon_meat = GameObject.Find("trap_meat");
        skill_icon_meat.SetActive(false);
        skill_icon_fire = GameObject.Find("trap_fire");
        skill_icon_fire.SetActive(false);
    }
    void Start()
    {
        if(SelectMenu.fire_char)//장작 캐릭터가 선택될때
        {
            trapSize = 3;
            skill_icon_fire.SetActive(true);
        }
        else if(SelectMenu.meat_char)//체력 캐릭터가 선택될때
        {
            trapSize = 5;
            skill_icon_meat.SetActive(true);
        }
        
        trapSprites = Resources.LoadAll<Sprite>("Background/traps");//함정의 모습 2가지를 배열에 저장한다
        bar_meat = GameObject.Find("meatFill").GetComponent<Bar_meat_control>();//체력 게이지의 script를 찾아서 저장
        bar_fire = GameObject.Find("fireFill").GetComponent<Bar_fire_control>();//장작 게이지의 script를 찾아서 저장
        trapNumber = GameObject.Find("trapText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        trap_click = false;
        trapOriginal = GameObject.FindGameObjectWithTag("Trap");

        traps.Enqueue(trapOriginal);
        trapOriginal.SetActive(false);

        for (int i = 0; i < trapSize; i++)//trap 큐를 초기화 시킨다
        {
            GameObject trap = (GameObject)Instantiate(trapOriginal);
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            traps.Enqueue(trap);
            trap.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //trapNumber.text = traps.Count.ToString();
        trapNumber.text = trapSize.ToString();
    }

    private void OnMouseDown()//trap 버튼을 클릭하면
    {
        trap_click = true;
        //Debug.Log("trap skill clicked");
        /*
        trapSize--;
        bar_meat.decreaseHealthWithDec(10);//체력이 일정 수준만큼 감소된다
        //trap_click = true;
        setPos(traps.Dequeue());
        //trap_click = false;
        */
        if(trapSize > 0)
        {
            trapSize--;
            if(SelectMenu.meat_char)//체력 캐릭터이면
            {
                bar_meat.decreaseHealthWithDec(10);//체력이 일정 수준만큼 감소된다-------------10
            }
            else if(SelectMenu.fire_char)//장작 캐릭터이면
            {
                bar_fire.decreaseHealthWithDec(15);//장작이 일정 수준만큼 감소된다------------15
            }
            //bar_meat.decreaseHealthWithDec(10);//체력이 일정 수준만큼 감소된다
            setPos(traps.Dequeue());
        }
    }

    //함정의 위치를 정해주는 함수
    void setPos(GameObject obj)
    {
        obj.transform.position = player.transform.position;
        if(SelectMenu.fire_char)//장작 캐릭터이면
        {
            obj.GetComponent<SpriteRenderer>().sprite = trapSprites[0];
            //obj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        obj.SetActive(true);
        //trap_click = false;
    }
    
    //함정을 다시 큐에 넣는다
    public void GetObj(GameObject obj)
    {
        trapSize++;
        traps.Enqueue(obj);//큐에 넣어주고
        if(SelectMenu.meat_char)//체력 캐릭터이면
        {
            obj.GetComponent<SpriteRenderer>().sprite = trapSprites[2];
        }
        else if(SelectMenu.fire_char)//장작 캐릭터이면
        {
            obj.GetComponent<SpriteRenderer>().sprite = trapSprites[0];
        }
        //obj.GetComponent<SpriteRenderer>().sprite = trapSprites[1];
        obj.SetActive(false);//disable
    }
    
}
