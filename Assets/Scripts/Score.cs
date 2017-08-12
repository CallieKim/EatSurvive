using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public int score;//점수
    public Text scoreText;//화면에 표기되는 점수 텍스트

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "점수: " + score;
	}

    public void ScoreUp(int number)//주어진 수만큼 점수가 증가한다
    {
        score += number;
    }
}
