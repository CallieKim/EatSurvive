using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_control : MonoBehaviour {
    public Sprite[] trapSprites;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change()//동물이 걸리면 함정의 모습이 바뀐다 그리고 사라진다
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trapSprites[0];
        StartCoroutine("disappear");
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(1f);//일정시간동안 기다린다
        gameObject.SetActive(false);
    }
}
