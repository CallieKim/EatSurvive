using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildPig_move : MonoBehaviour {
    wildPig_track trackScript;
    GameObject player;
    public float trackSpeed = 4.0f;

	// Use this for initialization
	void Start () {
        trackScript = GameObject.FindGameObjectWithTag("wildPig_sight").GetComponent<wildPig_track>();
        player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(trackScript.track);
		if(trackScript.track)
        {
            //Debug.Log("tracking");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, trackSpeed * Time.deltaTime);
        }
	}
    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")
        {

        }
    }
    */
    /*
    void move()//토끼가 목표지점까지 이동한다
    {
        if (WP.transform.position.x - transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (WP.transform.position.x - transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, WP.transform.position, speed * Time.deltaTime);
        //var rotation = Quaternion.LookRotation(WP.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotateSpeed*Time.deltaTime);
    }
    */

}
