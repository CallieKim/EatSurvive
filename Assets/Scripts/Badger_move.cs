using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badger_move : MonoBehaviour {
    public GameObject meatOriginal;//복사대상이 될 고기
    public GameObject badger;
    public bool madeMeat;//고기를 만들었는지 확인하는 변수

    public enum MovementState
    {
        idle,walking,dead,fire_walking
    }

    public MovementState MovementType;

    float speed = 0.8f;
    //float rotateSpeed = 2.0f;

    public GameObject Waypoint;
    GameObject WP; //waypoint 초기화
    bool wayP=false;

    float min_distance = -2.0f;
    float max_distance = 2.0f;

    Animator anim;//오소리의 animator


	// Use this for initialization
	void Awake () {
        anim=GetComponent<Animator>();
        anim.speed = 0.7f;
	}

    void Start()
    {
        StartCoroutine(ChooseAction());
        meatOriginal = GameObject.FindGameObjectWithTag("meat");
        badger = GameObject.Find("badger");
        madeMeat = false;
    }

    // Update is called once per frame
    void Update () {
        if (MovementType == MovementState.walking)
        {
            anim.SetBool("is_moving",true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_dead", false);
            anim.SetBool("is_onFire", false);
            createWaypoint();
        }
        else if(MovementType==MovementState.idle)
        {
            anim.SetBool("is_idle", true);
            anim.SetBool("is_moving", false);
            anim.SetBool("is_dead", false);
            anim.SetBool("is_onFire", false);
        }
        else if(MovementType==MovementState.dead)//죽은후에 고기생성, 1초후에 사라져야 한다
        {
            anim.SetBool("is_dead", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_moving", false);
            anim.SetBool("is_onFire", false);
            if (!madeMeat)//고기를 아직 생성하지 않았다면
            {
                Instantiate(meatOriginal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);//고기를 그 자리에서 한번만 생성한다
                madeMeat = true;//고기를 생성했다
            }
            //Instantiate(meatOriginal,new Vector3(transform.position.x,transform.position.y,0), Quaternion.identity);//고기를 그 자리에서 생성한다
            StartCoroutine(Dead());//죽었을때 실행되는 함수 코루틴을 부른다
        }
        else if (MovementType == MovementState.fire_walking)
        {
            anim.SetBool("is_onFire", true);
            anim.SetBool("is_dead", false);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_moving", false);
        }

        if (wayP)
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

    IEnumerator Dead()//죽었을때 실행된다
    {
        while(true)
        {
            wayP = false;//움직이지 않는다
            Destroy(WP);
            yield return new WaitForSeconds(0.7f);//0.7초동안 기다린다
            //wayP = false;//움직이지 않는다
            //Destroy(WP);
            badger.SetActive(false);//사라진다
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("WayPoint"))
        {
            //Debug.Log("entered waypoint");
            Destroy(WP);
            wayP = false;
            MovementType = MovementState.idle;
        }
        else if (col.tag == "Trap")
        {
            //col.GetComponent<SpriteRenderer>().enabled = false;
            //GameObject.FindGameObjectWithTag("Trap").GetComponent<SpriteRenderer>().enabled = false;
               // col.GetComponent<trap_control>().disappear();//함정은 사라진다
                col.gameObject.SetActive(false);//부딪힌 함정만 사라진다
                MovementType = MovementState.dead;
            //col.GetComponent<trap_control>().disappear();//함정은 사라진다
            //MovementType = MovementState.dead;

        }

    }

}
