using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornToTree : MonoBehaviour {
    Sprite[] treeSprites;//나무및 도토리의 sprite를 저장한 배열이다
    //float lastUpdate;
    float startTime;//물체가 생긴 시간
    bool planted;//심어졌는지 판단하는 변수
    bool stopBig;//클 수 있는지 판단하는 변수
    float scaleSpeed = 0.003f;//나무가 커지는 속도
    public bool clickable;

    // Use this for initialization
    public void Start() {
        treeSprites = Resources.LoadAll<Sprite>("Background/trees");//함정의 모습 2가지를 배열에 저장한다
        gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[0];//처음에는 도토리의 모습이다
        gameObject.transform.localScale = new Vector3(0.04f, 0.04f, 1);
        startTime = Time.time;
        planted = false;
        clickable = false;
        stopBig = false;
    }

    // Update is called once per frame
    void Update() {
        
        if (Time.time - startTime >= 2f && !planted)//2초 지나면 나무로 모습이 바뀐다
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[1];
            gameObject.transform.localScale=new Vector3(0.3f, 0.3f, 1);//도토리의 크기에서 작은 나무의 크기로 scale 변환
            planted = true;//심어졌으니 true로 설정한다
            //lastUpdate = Time.time;

        }
        if(planted && !stopBig)//도토리가 심어졌고 아직 크는 중이면 
        {
            InvokeRepeating("Bigger", 0.5f, 1f);//커지는 함수를 1초마다 부른다
        }
        if(Time.time - startTime >= 4f && planted)//2초동안 커지고 그 이후로는 그만 커진다
        {
            CancelInvoke("Bigger");
            stopBig = true;
            clickable = true;//누를 수 있는 상태가 된다
        }
        
        //gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[2];

        //InvokeRepeating("Bigger", 0.5f, 1f);//커지는 함수를 1초마다 부른다
        //gameObject.transform.localScale = new Vector3(0.35f, 0.55f, 0);
    }
    void Bigger()//점점 커진다
    {
        //gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
        gameObject.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        /*
        if (gameObject.transform.localScale.x > 1f || gameObject.transform.localScale.y > 0.55f)
        {
            Debug.Log("stop bigger");
            CancelInvoke("Bigger");
        }
        */
    }
}
