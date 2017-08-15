using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel_move : MonoBehaviour {

    public enum MovementState
    {
        walking, digging
    }

    public MovementState MovementType;

    public float speed;

    public GameObject Waypoint;
    GameObject WP; //waypoint 초기화
    bool wayP = false;

    float min_distance = -6.0f;
    float max_distance = 6.0f;
    float lastUpdate;
    Animator anim;//다람쥐의 animator
    GameObject acornOriginal;
    int treeSize;
    public static Queue<GameObject> trees = new Queue<GameObject>();//acorn 큐, 다람쥐는 도토리를 4개까지 심을 수 있다


// Use this for initialization
void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0.7f;
    }

    void Start()
    {
        //treeSize = 4;
        speed = 2f;
        createWaypoint();
        treeSize = 4;
        acornOriginal = GameObject.FindGameObjectWithTag("Tree");//acorn 원본, 복사할 대상

        trees.Enqueue(acornOriginal);
        acornOriginal.SetActive(false);

        for (int i = 0; i < treeSize; i++)//acorn 큐를 초기화 시킨다
        {
            GameObject tree = (GameObject)Instantiate(acornOriginal);
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            trees.Enqueue(tree);
            tree.SetActive(false);
        }
        //TreeQueueStart();
    }
    
    public void TreeQueueStart()//다시 다람쥐가 나타날때 나무 큐는 4개 있어야한다
    {
        treeSize = 4;
        /*
        for (int i = 0; i < treeSize; i++)//acorn 큐를 초기화 시킨다
        {
            //GameObject tree = (GameObject)Instantiate(acornOriginal);
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            trees.Enqueue(tree);
            tree.SetActive(false);
        }
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        if (MovementType == MovementState.walking)//걷고 있을때, 즉 움직인다
        {
            //anim.SetBool("is_moving", true);
            /*
            if(WP.transform.position==gameObject.transform.position)
            {
                createWaypoint();
            }
            */
            if (Time.time - lastUpdate >= 6f && treeSize > 0)//6초이상 움직였으면 도토리를 심는다, 도토리가 존재하면
            {
                setPos(trees.Dequeue());//그 자리에서 도토리를 심는다
                treeSize--;//도토리 개수는 준다
                lastUpdate = Time.time;

            }
            createWaypoint();//목표지점을 생성한다
        }
        /*
        else if (MovementType == MovementState.digging)//도토리 심고 있을때, 잠시 멈춘다
        {
            //createWaypoint();
            if (Time.time - lastUpdate >= 6f && acornSize>0)//6초이상 움직였으면 도토리를 심는다, 도토리가 존재하면
            {
                acorns.Dequeue().SetActive(true);//그 자리에서 도토리를 심는다
                acornSize--;//도토리 개수는 준다
                lastUpdate = Time.time;

            }
            createWaypoint();
            //anim.SetBool("is_moving", false);
        }
        */

        if (wayP)//목표지점이 존재하면 그쪽으로 이동한다
        {
            move();
        }

        if(treeSize==0 && GameObject.FindGameObjectWithTag("Tree")==null)//도토리가 더이상 없으면 사라진다 그리고 나무를 다 먹어야만 사라진다
        {
            Debug.Log("no tree");
            gameObject.SetActive(false);
        }

    }

    void createWaypoint()//목표지점 생성 이때 목표지점은 화면 밖을 벗어나면 안된다
    {
        //Debug.Log("create waypoint");
        if (!wayP)//목표지점이 존재하지 않으면 생성한다
        {
            float distanceX = transform.position.x + Random.Range(min_distance, max_distance);//waypont과 캐릭터 사이의 거리
            float distanceY = transform.position.y + Random.Range(min_distance, max_distance);
            if (Mathf.Abs(distanceY) <= 2.3f && Mathf.Abs(distanceX) <= 6.0f)//목표 지점이 화면 범위 내일때에만 생성된다
            {
                //Debug.Log("create waypoint");
                WP = Instantiate(Waypoint, new Vector2(distanceX, distanceY), Quaternion.identity) as GameObject;
                wayP = true;
            }
            else
            {
                //Debug.Log("goback");
                distanceX = transform.position.x - Random.Range(min_distance, max_distance);//waypont과 캐릭터 사이의 거리
                distanceY = transform.position.y - Random.Range(min_distance, max_distance);
            }
        }

    }

    void move()//다람쥐가 목표지점까지 이동한다
    {
        //Debug.Log("squirrel move");
        if (WP.transform.position.x - transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (WP.transform.position.x - transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, WP.transform.position, speed * Time.deltaTime);
    }

    private IEnumerator ChooseAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);//0.5초 동안 기다림
            if (!wayP)//목표지점이 없으면 
            {
                /*
                int num = Random.Range(0, 2);//0과1 중 고른다 idle, walking
                if (num == 0)
                {
                    MovementType = MovementState.digging;
                }
                else if (num == 1)
                {
                    MovementType = MovementState.walking;
                }
                */
                MovementType = MovementState.walking;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("WayPoint"))
        {
            //Debug.Log("entered waypoint");
            Destroy(WP);
            wayP = false;
            MovementType = MovementState.walking;
        }
    }

    void setPos(GameObject obj)//도토리의 위치를 정하고 게임에 나타나게 한다
    {
        obj.transform.position = gameObject.transform.position;
        obj.SetActive(true);
        obj.GetComponent<acornToTree>().Start();
        //trap_click = false;
    }
}
