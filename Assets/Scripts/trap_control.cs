using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_control : MonoBehaviour {
    public Sprite[] trapSprites;
    public GameObject meatOriginal;//복사대상이 될 고기
    public bool madeMeat;//고기를 만들었는지 확인하는 변수
    Score scoreScript;//score script를 저장하는 변수
    trap_skill trapSkill;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change(int number)//동물이 걸리면 함정의 모습이 바뀐다 그리고 사라진다 + 점수도 추가된다(2배)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trapSprites[0];
        scoreScript.ScoreUp(number * 2);
        StartCoroutine("disappear");
        Instantiate(meatOriginal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);//고기를 그 자리에서 한번만 생성한다  
    }

    IEnumerator disappear()//일정시간동안 동물이 잡힌 모습으로 지내다가 trap 큐에 들어간다
    {
        yield return new WaitForSeconds(5f);//일정시간동안 기다린다
        trapSkill.GetObj(gameObject);//함정 큐에 다시 넣는다
    }
}
