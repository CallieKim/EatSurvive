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
		if(player.GetComponent<Char_control>().tree_collided)
        {
            Debug.Log("tree collided on tree script");
            //theSprite.enabled = false;
            tree_col = true;
        }
	}

    void OnMouseDown()
    {
        if(tree_col)
        {
            theSprite.enabled = false;
        }
    }
}
