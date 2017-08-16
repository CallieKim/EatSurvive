using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potato_control : MonoBehaviour {

    

    GameObject player;
    Score scoreScript;
    bool potato_col;
    GameObject pig;
    public float startTime;
    int aliveCount;


    // Use this for initialization
    public void Start()
    {
        //Debug.Log("called start");
        aliveCount = 0;
        pig = GameObject.FindGameObjectWithTag("enemy");
        scoreScript = GameObject.FindGameObjectWithTag("score").GetComponent<Score>();
        potato_col = false;
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Time.time-startTime>=6f)//생성한지 6초가 지나면 사라진다
        {
            //Debug.Log("disappear");
            wildPig_move.potatos.Enqueue(gameObject);//큐에 넣는다
            gameObject.SetActive(false);
        }
        
        /*
        if(gameObject.activeSelf)
        {
            aliveCount++;
        }
        if(aliveCount>50)
        {
            wildPig_move.potatos.Enqueue(gameObject);//큐에 넣는다
            gameObject.SetActive(false);
        }
        */
    }

    void OnMouseDown()
    {
        potato_col = true;
        //Debug.Log("potato clicked");
    }

    public void disappear()//다른 script에서 부르면 이 script를 붙인 gameobject가 사라진다
    {
        if (potato_col)//감자를 클릭했으면
        {
            //Debug.Log("eaten");
            potato_col = false;
            scoreScript.ScoreUp(3000);//점수는 3000 점 올린다
            wildPig_move.potatos.Enqueue(gameObject);//큐에 넣는다
            //gameObject.GetComponent<acornToTree>().Start();
            gameObject.SetActive(false);//감자는 사라져도 된다
        }
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")//플레이어 부딪쳤으면
        {
            disappear();
        }
    }
}
