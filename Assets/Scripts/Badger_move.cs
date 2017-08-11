using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badger_move : MonoBehaviour {
    public GameObject meatOriginal;//복사대상이 될 고기
    //public GameObject badger;
    public bool madeMeat;//고기를 만들었는지 확인하는 변수
    public bool trapSense;//함정을 감지하는지 판단하는 변수
    public bool collided_player;//플레이어와 부딪혔는지 아닌지 판단하는 변수
    public int amountOfClicks;
    private DoubleClickListener dbl;// = new DoubleClickListener(); // (optionnal: pass a float as the delay)

    public enum MovementState
    {
        idle,walking,dead,fire_walking
    }

    public MovementState MovementType;

    public float speed = 0.8f;
    float speed_attacked;
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
        //badger = GameObject.Find("badger");
        madeMeat = false;
        collided_player = false;
        amountOfClicks = 0;
        speed_attacked=speed*0.1f;
        dbl = gameObject.AddComponent<DoubleClickListener>();
        
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
            /*
            if (!madeMeat)//고기를 아직 생성하지 않았다면
            {
                Instantiate(meatOriginal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);//고기를 그 자리에서 한번만 생성한다
                madeMeat = true;//고기를 생성했다
            }
            */
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && collided_player) {//플레이어와 부딪힌상태에서 2번 클릭했으면 죽는다
            if (dbl.isDoubleClicked())
            {
                //Debug.Log("badger double clicked");
                //gameObject.SetActive(false);
                MovementType = MovementState.dead;
            }
        }

    }

    void createWaypoint()//목표지점 생성 이때 목표지점은 화면 밖을 벗어나면 안된다
    {
        if(!wayP)
        {
            float distanceX=transform.position.x+Random.Range(min_distance,max_distance);//waypont과 캐릭터 사이의 거리
            float distanceY= transform.position.y + Random.Range(min_distance, max_distance);
            if(Mathf.Abs(distanceY)<=2.3f && Mathf.Abs(distanceX)<=6.0f)//목표 지점이 화면 범위 내일때에만 생성된다
            {
                WP = Instantiate(Waypoint, new Vector2(distanceX, distanceY), Quaternion.identity) as GameObject;
                wayP = true;
            }
            //WP = Instantiate(Waypoint, new Vector2(distanceX,distanceY),Quaternion.identity) as GameObject;
            //wayP = true;         
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
            if (!madeMeat)//고기를 아직 생성하지 않았다면
            {
                Instantiate(meatOriginal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);//고기를 그 자리에서 한번만 생성한다
                madeMeat = true;//고기를 생성했다
            }
            //wayP = false;//움직이지 않는다
            //Destroy(WP);
            gameObject.SetActive(false);//사라진다
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
        else if (col.tag == "Trap")//동물이 함정이랑 부딪히면 발생
        {
            //col.GetComponent<SpriteRenderer>().enabled = false;
            //GameObject.FindGameObjectWithTag("Trap").GetComponent<SpriteRenderer>().enabled = false;
               // col.GetComponent<trap_control>().disappear();//함정은 사라진다
                //col.gameObject.SetActive(false);//부딪힌 함정만 사라진다
            gameObject.SetActive(false);//오소리는 사라진다
            col.GetComponent<trap_control>().Change();//부딪힌 함정의 모습이 바뀐다
                //MovementType = MovementState.dead;
            //col.GetComponent<trap_control>().disappear();//함정은 사라진다
            //MovementType = MovementState.dead;

        }
        else if(col.tag=="Player")//플레이어와 부딪히는 상태인 동안
        {
            Debug.Log("badger collided with player");
            collided_player = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)//부딪히는 범위에서 빠져나왔으면
    {
        if(collision.tag=="Player")
        {
            collided_player = false;//플레이어와 부딪힌게 아니기 때문에 false로 표현
        }
    }
    /*
    private void OnMouseDown()//오소리를 클릭했을때 호출된다
    {
        Debug.Log("clicked badger");
        if(collided_player)//클릭한 상태에서 플레이어와 부딪혔으면
        {
            speed = speed_attacked;
            if(amountOfClicks>3)
            {
                gameObject.SetActive(false);
            }
            else
            {
                amountOfClicks++;
            }
        }
    }
    */
    /*
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked badger");
        }
    }
    */
}
