using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badger_move : MonoBehaviour {

    public enum MovementState
    {
        idle,walking,dead
    }

    public MovementState MovementType;

    float speed = 0.8f;
    //float rotateSpeed = 2.0f;

    public GameObject Waypoint;
    GameObject WP; //waypoint 초기화
    bool wayP=false;

    float min_distance = -2.0f;
    float max_distance = 2.0f;

    Animator anim;


	// Use this for initialization
	void Awake () {
        anim=GetComponent<Animator>();
        anim.speed = 0.7f;
	}

    void Start()
    {
        StartCoroutine(ChooseAction());
    }

    // Update is called once per frame
    void Update () {
        if (MovementType == MovementState.walking)
        {
            anim.SetBool("is_moving",true);
            createWaypoint();
        }
        else if(MovementType==MovementState.idle)
        {
            anim.SetBool("is_idle", true);
        }
        else if(MovementType==MovementState.dead)
        {
            anim.SetBool("is_dead", true);
        }

        if(wayP)
        {
            move();
        }
	}

    void createWaypoint()//목표지점 생성
    {
        if(!wayP)
        {
            float distanceX=transform.position.x+Random.Range(min_distance,max_distance);//waypont과 캐릭터 사이의 거리
            float distanceY= transform.position.y + Random.Range(min_distance, max_distance);
            WP = Instantiate(Waypoint, new Vector2(distanceX,distanceY),Quaternion.identity) as GameObject;
            wayP = true;
        }
        
    }

    void move()//오소리가 목표지점까지 이동한다
    {
        if(WP.transform.position.x-transform.position.x<0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(WP.transform.position.x - transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position,WP.transform.position, speed*Time.deltaTime);
        //var rotation = Quaternion.LookRotation(WP.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotateSpeed*Time.deltaTime);
    }

    private IEnumerator ChooseAction()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);//1.5초 동안 기다림
            if(!wayP)
            {
                int num = Random.Range(0, 2);//0과1 중 고른다 idle, walking
                if(num==0)
                {
                    MovementType = MovementState.idle;
                }
                else if(num==1)
                {
                    MovementType = MovementState.walking;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("WayPoint"))
        {
            Debug.Log("entered waypoint");
            Destroy(WP);
            wayP = false;
            MovementType = MovementState.idle;
        }
        else if (col.tag == "Trap")
        {
            col.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.FindGameObjectWithTag("Trap").GetComponent<SpriteRenderer>().enabled = false;
            MovementType = MovementState.dead;

        }

    }
}
