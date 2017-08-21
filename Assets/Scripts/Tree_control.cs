using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_control : MonoBehaviour {

    SpriteRenderer theSprite;

    GameObject player;

    bool tree_col;
    Bar_fire_control bar_fire;
    GameObject score;

    // Use this for initialization
    void Start () {
        //theSprite = gameObject.GetComponent<SpriteRenderer>();
        //player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("score");
        tree_col = false;
        bar_fire = GameObject.Find("fireFill").GetComponent<Bar_fire_control>();
    }
	
	// Update is called once per frame
	void Update () {

        /*if (player.GetComponent<Char_control>().tree_collided)//플레이어가 나무랑 부딪쳤으면
        {
            Debug.Log("tree collided on tree script");
            //theSprite.enabled = false;
            tree_col = true;//나무의 부딪침 변수는 true로 설정
        }
        */
	}

    void OnMouseDown()
    {
        tree_col = true;
    }

    public void disappear()//다른 script에서 부르면 이 script를 붙인 gameobject가 사라진다
    {
        if(tree_col && gameObject.GetComponent<acornToTree>().clickable)//나무를 클릭했으면, 그리고 다 큰 상태면
        {
            tree_col = false;
            Squirrel_move.trees.Enqueue(gameObject);//큐에 넣는다
            gameObject.GetComponent<acornToTree>().Start();
            gameObject.SetActive(false);//나무는 사라져도 된다
            //bar_fire.increaseHealth(20f);//장작 게이지가 20f만큼 증가한다
            if(SelectMenu.fire_char)//장작 캐릭터면 점수도 올라간다, 장작 게이지도 더 많이 올라간다
            {
                bar_fire.increaseHealth(40f);//장작 게이지가 40f만큼 증가한다
                score.GetComponent<Score>().ScoreUp(50);
            }
            else if(SelectMenu.meat_char)//체력 캐릭터면
            {
                bar_fire.increaseHealth(20f);//장작 게이지가 20f만큼 증가한다
            }
        }
        //gameObject.SetActive(false);
    }
}
