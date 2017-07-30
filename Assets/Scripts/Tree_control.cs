using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_control : MonoBehaviour {

    SpriteRenderer theSprite;

    GameObject player;

    bool tree_col;

    // Use this for initialization
    void Start () {
        //theSprite = gameObject.GetComponent<SpriteRenderer>();
        //player = GameObject.FindGameObjectWithTag("Player");
        tree_col = false;
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
        if(tree_col)//나무를 클릭했으면
        {
            gameObject.SetActive(false);//나무는 사라져도 된다
        }
        //gameObject.SetActive(false);
    }
}
