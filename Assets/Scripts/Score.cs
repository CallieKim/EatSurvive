using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public static int PlayerScore;//점수
    public Text scoreText;//화면에 표기되는 점수 텍스트
    float lastUpdate;
    public int badgerKill;//죽인 오소리 수
    public int rabbitKill;//죽인 토끼 수

    public Text Ranking;
    public TextMesh RankingText;
    public bool isRanking = false;
    public GameObject RankingMenu;

    private void Awake()
    {
        RankingMenu = GameObject.Find("scoreRank");
    }
    // Use this for initialization
    void Start () {
        //RankingMenu = GameObject.Find("scoreRank");
        //RankingMenu.GetComponent<Text>().text = Ranking.text;
        PlayerScore = 0;
        scoreText = gameObject.GetComponent<Text>();
        badgerKill = 0;
        rabbitKill = 0;

        for(int i=0;i<5;i++)
        {
            //Debug.Log("hi");
            Ranking.text =
                "Ranking\n\n " +
                "1. " + PlayerPrefs.GetInt("0") + "\n\n" +
                "2. " + PlayerPrefs.GetInt("1") + "\n\n" +
                "3. " + PlayerPrefs.GetInt("2") + "\n\n" +
                "4. " + PlayerPrefs.GetInt("3") + "\n\n" +
                "5. " + PlayerPrefs.GetInt("4");
        }
        //RankingMenu.GetComponent<Text>().text = Ranking.text;
        //Debug.Log(Ranking.text);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastUpdate >= 1f)
        {
            PlayerScore += 5;
            lastUpdate = Time.time;

        }
        scoreText.text = "점수: " + PlayerScore;
        /*
        if(GameObject.Find("gameOverMenu").activeSelf)//게임오버 메뉴가 나타나면
        {
            Debug.Log("rank");
            RankingMenu.GetComponent<Text>().text = Ranking.text;
        }
        */
	}

    public void updateRank()
    {
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log("hi");
            Ranking.text =
                "Ranking\n\n " +
                "1. " + PlayerPrefs.GetInt("0") + "\n\n" +
                "2. " + PlayerPrefs.GetInt("1") + "\n\n" +
                "3. " + PlayerPrefs.GetInt("2") + "\n\n" +
                "4. " + PlayerPrefs.GetInt("3") + "\n\n" +
                "5. " + PlayerPrefs.GetInt("4");
        }
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
        PlayerScore += number;
    }
}
