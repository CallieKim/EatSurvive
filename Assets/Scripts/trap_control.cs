using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_control : MonoBehaviour {
    public Sprite[] trapSprites;
    public GameObject meatOriginal;//복사대상이 될 고기
    public bool madeMeat;//고기를 만들었는지 확인하는 변수
    Score scoreScript;//score script를 저장하는 변수
    trap_skill trapSkill;
    public bool trapclick;//플레이어가 함정 수거하려고 클릭했는지 아닌지 판단하는 변수
    public bool trapuUsed;//일회용이라서 이미 사용했는지 아닌지 판단하는 변수
    public GameObject barGage;//체력 게이지를 저장하는 변수

    private void Awake()
    {
        trapSprites = Resources.LoadAll<Sprite>("Background/traps");//함정의 모습 2가지를 배열에 저장한다
    }
    // Use this for initialization
    void Start () {
        //trapSprites = Resources.LoadAll<Sprite>("traps");
        //GameObject trap = new GameObject();
        //trap.AddComponent<SpriteRenderer>();
        //trap.GetComponent<SpriteRenderer>().sprite = trapSprites[1];
        meatOriginal = GameObject.FindGameObjectWithTag("meat");
        scoreScript = GameObject.FindGameObjectWithTag("score").GetComponent<Score>();
        trapSkill = GameObject.Find("skillButton_trap").GetComponent<trap_skill>();
        //trapclick = false;
        trapuUsed = false;
        barGage = GameObject.Find("meatFill");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change(int number)//동물이 걸리면 함정의 모습이 바뀐다 그리고 사라진다 + 점수도 추가된다(2배)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trapSprites[0];
        trapuUsed = true;//사용했으니 true로 설정한다
        scoreScript.ScoreUp(number * 2);
        StartCoroutine("disappear");
        Instantiate(meatOriginal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);//고기를 그 자리에서 한번만 생성한다  
    }

    IEnumerator disappear()//일정시간동안 동물이 잡힌 모습으로 지내다가 trap 큐에 들어간다
    {
        //gameObject.SetActive(false);
        //barGage.GetComponent<Bar_meat_control>().increaseHealth(10f);//함정을 수거했으니 체력이 다시 찬다
        //trapuUsed = false;//큐에 넣기 전에 사용횟수를 초기화한다
        //trapclick = false;//큐에 넣기 전에 초기화한다
        //trapSkill.GetObj(gameObject);//함정 큐에 다시 넣는다
        yield return new WaitForSeconds(5f);//일정시간동안 기다린다
        //gameObject.SetActive(true);
        trapuUsed = false;//큐에 넣기 전에 사용횟수를 초기화한다
        trapclick = false;//큐에 넣기 전에 초기화한다
        trapSkill.GetObj(gameObject);//5초후에 함정 큐에 다시 넣는다
    }

    private void OnTriggerEnter2D(Collider2D col)//플레이어가 함정을 클릭하고 다가올때 수거
    {
        
        if(col.tag=="Player")
        {
            if(trapclick)//함정을 눌렀다면
            {
                //gameObject.SetActive(false);
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                barGage.GetComponent<Bar_meat_control>().increaseHealth(5f);//함정을 수거했으니 약소하게나마 체력이 다시 찬다
                trapclick = false;//큐에 넣기 전에 초기화한다
                trapuUsed = false;//큐에 넣기 전에 초기화한다
                trapSkill.GetObj(gameObject);//함정을 큐에 넣는다
                //gameObject.SetActive(false);//함정은 사라진다

            }
        }
       
    }

    private void OnMouseDown()//함정을 클릭했을면 변수를 true로 설정한다
    {
        //Debug.Log("trap clicked");
        trapclick = true;
    }
    
}
