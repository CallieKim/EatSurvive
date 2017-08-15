using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public int score;//점수
    public Text scoreText;//화면에 표기되는 점수 텍스트
    float lastUpdate;
    public int badgerKill;//죽인 오소리 수
    public int rabbitKill;//죽인 토끼 수

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText = gameObject.GetComponent<Text>();
        badgerKill = 0;
        rabbitKill = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastUpdate >= 1f)
        {
            score += 5;
            lastUpdate = Time.time;

        }
        scoreText.text = "점수: " + score;
	}
    /*
    void FixedUpdate()
    {
        if (Time.time - lastUpdate >= 1f)
        {
            score += 5;
            lastUpdate = Time.time;

        }
    }
    */
    public void ScoreUp(int number)//주어진 수만큼 점수가 증가한다
    {
        score += number;
    }
}
