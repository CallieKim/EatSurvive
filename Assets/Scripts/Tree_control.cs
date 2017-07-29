using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_control : MonoBehaviour {

    SpriteRenderer theSprite;

    GameObject player;

    bool tree_col;

    // Use this for initialization
    void Start () {
        theSprite = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        tree_col = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<Char_control>().tree_collided)//플레이어가 나무랑 부딪쳤으면
        {
            Debug.Log("tree collided on tree script");
            //theSprite.enabled = false;
            tree_col = true;//나무의 부딪침 변수는 true로 설정
        }
	}

    void OnMouseDown()
    {
        if(tree_col)//나무의 부딪침이 true이면, 즉 나무를 클릭하고 부딪친 상태라면
        {
            Debug.Log("tree click and tree collided");
            theSprite.enabled = false;//나무는 사라진다
        }
    }
}
