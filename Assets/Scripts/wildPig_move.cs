using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildPig_move : MonoBehaviour {
    wildPig_track trackScript;
    GameObject player;
    float patrolSpeed = 2.0f;//목표지점 사이 움직이는 속도
    float pauseDuration = 0f;//목표지점에서 얼마나 오래있을지
    bool loop = true; //목표지점 반복을 계속 할 것인지
    public float trackSpeed = 4.0f;
    int waypointsize = 4;//4개의 목표지점들
    public int whichWP;//어떤 목표지점으로 가야할지 지정
    public GameObject WPorigin;//복사할 대상인 원래 목표지점
    public GameObject[] waypoints = new GameObject[4];//waypoint 큐, 4개의 목표지점들이 있다
    Animator pigAnim;//돼지의 애니메이션 조절 변수
    GameObject sight;//돼지의 시야
    public static Queue<GameObject> potatos = new Queue<GameObject>();//potato 큐, 멧돼지는 땅팔때마다 감자를 내놓는다
    GameObject potatoOrigin;
    int potatoSize = 4;

    // Use this for initialization
    void Start () {
        potatoOrigin = GameObject.Find("potato");
        potatos.Enqueue(potatoOrigin);
        potatoOrigin.SetActive(false);
        for (int i = 0; i < potatoSize; i++)//potato 큐를 초기화 시킨다
        {
            GameObject potato = (GameObject)Instantiate(potatoOrigin);
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            potatos.Enqueue(potato);
            potato.SetActive(false);
        }
        trackScript = GameObject.FindGameObjectWithTag("wildPig_sight").GetComponent<wildPig_track>();
        player = GameObject.FindGameObjectWithTag("Player");
        pigAnim = gameObject.GetComponent<Animator>();
        sight = GameObject.FindGameObjectWithTag("wildPig_sight");
        whichWP = 0;
        for (int i = 0; i < waypointsize; i++)//waypoint 큐를 초기화 시킨다
        {
            GameObject WP = (GameObject)Instantiate(WPorigin);
            if(i==0)//위치를 각각 정한다
            {
                WP.transform.position = new Vector3(-5.28f, 0.18f, 0);
            }
            else if(i==1)
            {
                WP.transform.position = new Vector3(-0.43f, 2.67f, 0);
            }
            else if(i==2)
            {
                WP.transform.position = new Vector3(5.43f, 0.04f, 0);
            }
            else if(i==3)
            {
                WP.transform.position = new Vector3(-0.18f, -1.88f, 0);
            }
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            waypoints[i] = WP;
            WP.SetActive(false);
        }
    }

    void flip(GameObject other)//뒤집는 함수
    {
        if(other.transform.position.x>gameObject.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(trackScript.track);
		if(trackScript.track)//시야에 플레이어가 들어갔으면 플레이어를 쫓아간다
        {
            //Debug.Log("tracking");
            flip(player);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, trackSpeed * Time.deltaTime);//쫓아간다
            StopAllCoroutines();
            //trackScript.track = false;
        }
        else if(whichWP==0)
        {
            //Debug.Log("moving in 0");
            waypoints[3].SetActive(false);
            waypoints[0].SetActive(true);
            flip(waypoints[0]);
            move(waypoints[0]);
        }
        else if (whichWP == 1)
        {
            waypoints[0].SetActive(false);
            waypoints[1].SetActive(true);
            flip(waypoints[1]);
            move(waypoints[1]);
        }
        else if (whichWP == 2)
        {
            waypoints[1].SetActive(false);
            waypoints[2].SetActive(true);
            flip(waypoints[2]);
            move(waypoints[2]);
        }
        else if (whichWP == 3)
        {
            waypoints[2].SetActive(false);
            waypoints[3].SetActive(true);
            flip(waypoints[3]);
            move(waypoints[3]);
        }
        
        if(gameObject.transform.position==player.transform.position)//펠리어와 같은 위치로 이동하면 다시 갈길 간다 이때는 목표지점 0으로 간다
        {
            whichWP = 0;
            waypoints[3].SetActive(false);
            waypoints[2].SetActive(false);
            waypoints[1].SetActive(false);
            waypoints[0].SetActive(true);
            flip(waypoints[0]);
            move(waypoints[0]);
            trackScript.track = false;
        }
        
        //waypoints[0].SetActive(true);
        //move(waypoints[0]);




        digging();//목표지점에 도착하면 구덩이를 판다
	}

    public void move(GameObject waypoint)//목표지점을 받으면 거기로 이동한다
    {
        //Debug.Log("move");
        pigAnim.SetBool("is_walking", true);//걷는 애니메이션 활성화
        pigAnim.SetBool("is_digging", false);//구덩이 안 파게 한다
        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, patrolSpeed * Time.deltaTime);//목표지점으로 순찰한다
        //GameObject.FindGameObjectWithTag("wildPig_sight").SetActive(true);
        sight.SetActive(true);
    }

    public void moveToStart()
    {
        pigAnim.SetBool("is_walking", true);//걷는 애니메이션 활성화
        pigAnim.SetBool("is_digging", false);//구덩이 안 파게 한다
        //whichWP = 0;
        //transform.position = Vector2.MoveTowards(transform.position, waypoints[0].transform.position, patrolSpeed * Time.deltaTime);//목표지점으로 순찰한다
        //GameObject.FindGameObjectWithTag("wildPig_sight").SetActive(true);
        //sight.SetActive(true);
    }

    public void digging()//구덩이를 파면서 감자가 나오게 한다
    {
        for(int i=0;i<waypointsize;i++)
        {
            if(transform.position == waypoints[i].transform.position)//만약 목표지점에 도착했으면
            {
                pigAnim.SetBool("is_digging", true);//구덩이 파는 애니메이션 활성화
                pigAnim.SetBool("is_walking", false);//걷지 않게 한다
                sight.SetActive(false);
            }
        }
        StartCoroutine("changeWP");//목표지점을 바꾼다
    }

    IEnumerator changeWP()//목표지점 바꿀때 감자도 생긴다
    {
        //Debug.Log("changeWP");
        yield return new WaitForSeconds(4.5f);//4.5초동안 기다린다
        if(whichWP<3)
        {
            whichWP++;
            setPos(potatos.Dequeue());
            StopAllCoroutines();
        }
        else if(whichWP==3)//다시 처음 지점으로 돌아갈때 멧돼지는 사라진다
        {
            whichWP = 0;
            setPos(potatos.Dequeue());
            StopAllCoroutines();
            waypoints[3].SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void setPos(GameObject obj)//감자의 위치를 정하고 게임에 나타나게 한다
    {
        //obj.transform.position = gameObject.transform.position;
        obj.SetActive(true);
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<potato_control>().Start();
        //trap_click = false;
    }

    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")//플레이어와 부딪치면 다시 갈길 간다
        {
            whichWP = 0;
            waypoints[3].SetActive(false);
            waypoints[2].SetActive(false);
            waypoints[1].SetActive(false);
            waypoints[0].SetActive(true);
            flip(waypoints[0]);
            move(waypoints[0]);
            //trackScript.track = false;
        }
    }
    */
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
