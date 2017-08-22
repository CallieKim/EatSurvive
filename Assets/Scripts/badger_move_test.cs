using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badger_move_test : MonoBehaviour {

    public GameObject meatOriginal;//복사대상이 될 고기
    //public GameObject badger;
    public bool madeMeat;//고기를 만들었는지 확인하는 변수
    public bool trapSense;//함정을 감지하는지 판단하는 변수
    public bool collided_player;//플레이어와 부딪혔는지 아닌지 판단하는 변수
    public int amountOfClicks;
    private DoubleClickListener dbl;// = new DoubleClickListener(); // (optionnal: pass a float as the delay)
    Score scoreScript;//점수 script를 저장하는 변수
    public static int badgerScore;//오소리의 점수
    GameObject barGage;
    GameObject barGageFire;
    public bool dead;//죽었는지 판단하는 변수이다
    GameObject player;//플레이어

    public int hit = 0;


    public enum MovementState
    {
        idle, walking, dead, fire_walking
    }

    public MovementState MovementType;

    public float speed = 0.8f;
    float speed_attacked;
    //float rotateSpeed = 2.0f;

    public GameObject Waypoint;
    GameObject WP; //waypoint 초기화
    bool wayP = false;

    float min_distance = -4.0f;
    float max_distance = 4.0f;

    Animator anim;//오소리의 animator


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0.7f;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dead = false;
        StartCoroutine(ChooseAction());

        //badger = GameObject.Find("badger");
        madeMeat = false;
        collided_player = false;
        amountOfClicks = 0;
        speed_attacked = speed * 0.1f;


    }

    // Update is called once per frame
    void Update()
    {
        if (MovementType == MovementState.walking)
        {
            anim.SetBool("is_moving", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_dead", false);
            anim.SetBool("is_onFire", false);
            //Debug.Log("create waypoint");
            createWaypoint();
        }

        else if (MovementType == MovementState.idle)
        {
            anim.SetBool("is_idle", true);
            anim.SetBool("is_moving", false);
            anim.SetBool("is_dead", false);
            anim.SetBool("is_onFire", false);
        }

        else if (MovementType == MovementState.dead && !dead)//죽은후에 , 1초후에 사라져야 한다
        {
            anim.SetBool("is_dead", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_moving", false);
            anim.SetBool("is_onFire", false);
            dead = true;
            //scoreScript.ScoreUp(100);//점수가 추가된다

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

        /*
        if (Input.GetKeyDown(KeyCode.Mouse0) && collided_player)
        {//플레이어와 부딪힌상태에서 2번 클릭했으면 죽는다
            Char_control.attackState = true;


            if (dbl.isDoubleClicked())
            {
                //Debug.Log("double clicked");
                //player.GetComponent<Char_control>().MovementType = Char_control.MovementState.attack;//공격 애니메이션으로 바꾼다
                //Debug.Log("badger double clicked");
                //gameObject.SetActive(false);
                //player.GetComponent<Animator>().Play("char_meat_attack");
                MovementType = MovementState.dead;
                //player.GetComponent<Animator>().Play("char_meat_standing_noFire");
                if (Char_control.collided)//불 안 킨 상태로 부딪쳤으면 체력이 깎인다
                {
                    Char_control.collided = false;
                    if (!animalEvent.meat_invincible)//체력무적아닐때
                    {
                        barGage.GetComponent<Bar_meat_control>().decreaseHealthWithDec(10f);//체력게이지가 10만큼 감소된다
                    }
                    //barGage.GetComponent<Bar_meat_control>().decreaseHealthWithDec(10f);//체력게이지가 10만큼 감소된다
                }
                else if (Char_control.collided_fire)//불 킨 상태로 부딪쳤으면 장작이 깎인다
                {
                    Char_control.collided_fire = false;
                    if (!animalEvent.fire_invincible)//장작무적아닐때
                    {
                        barGageFire.GetComponent<Bar_fire_control>().decreaseHealthWithDec(10f);//장작 게이지가 10만큼 깎인다
                    }
                    //barGageFire.GetComponent<Bar_fire_control>().decreaseHealthWithDec(10f);//장작 게이지가 10만큼 깎인다
                }
                //barGage.GetComponent<Bar_meat_control>().decreaseHealthWithDec(10f);//체력게이지가 10만큼 감소된다
            }



        }

        */

    }

    void createWaypoint()//목표지점 생성 이때 목표지점은 화면 밖을 벗어나면 안된다
    {
        if (!wayP)
        {
            float distanceX = transform.position.x + Random.Range(min_distance, max_distance);//waypont과 캐릭터 사이의 거리
            float distanceY = transform.position.y + Random.Range(min_distance, max_distance);
            if (Mathf.Abs(distanceY) <= 2.3f && Mathf.Abs(distanceX) <= 6.0f)//목표 지점이 화면 범위 내일때에만 생성된다
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
        //Debug.Log("badger move");
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

    public IEnumerator ChooseAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);//1.5초 동안 기다림
            if (!wayP)
            {

                int num = Random.Range(0, 1);//0과1 중 고른다 idle, walking
                //Debug.Log(num);
                if (num == 0)
                {
                    MovementType = MovementState.idle;
                }
                else if (num == 1)
                {
                    MovementType = MovementState.walking;
                }

                MovementType = MovementState.walking;
            }
        }
    }

    IEnumerator Dead()//죽었을때 실행된다 + 점수도 추가된다
    {
        while (true)
        {
            wayP = false;//움직이지 않는다
            Destroy(WP);
            yield return new WaitForSeconds(1.5f);//일정시간동안 시체가 보인다



            gameObject.SetActive(false);//사라진다
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("WayPoint"))
        {
            //Debug.Log("entered waypoint");
            Destroy(WP);
            wayP = false;
            //MovementType = MovementState.idle;
        }

        else if (col.tag == "Player")//플레이어와 부딪히는 상태인 동안
        {
            //Debug.Log("badger collided with player");
            collided_player = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)//부딪히는 범위에서 빠져나왔으면
    {
        if (collision.tag == "Player")
        {
            collided_player = false;//플레이어와 부딪힌게 아니기 때문에 false로 표현
        }
    }


    private void OnMouseDown()
    {
        if(collided_player)
        {
            Char_control.attackState = true;
            hit++;


            if(hit==6)
            {
                MovementType = MovementState.dead;

                if (Char_control.collided)//불 안 킨 상태로 부딪쳤으면 체력이 깎인다
                {
                    Char_control.collided = false;
                    if (!animalEvent.meat_invincible)//체력무적아닐때
                    {
                        barGage.GetComponent<Bar_meat_control>().decreaseHealthWithDec(10f);//체력게이지가 10만큼 감소된다
                    }
                    //barGage.GetComponent<Bar_meat_control>().decreaseHealthWithDec(10f);//체력게이지가 10만큼 감소된다
                }
                else if (Char_control.collided_fire)//불 킨 상태로 부딪쳤으면 장작이 깎인다
                {
                    Char_control.collided_fire = false;
                    if (!animalEvent.fire_invincible)//장작무적아닐때
                    {
                        barGageFire.GetComponent<Bar_fire_control>().decreaseHealthWithDec(10f);//장작 게이지가 10만큼 깎인다
                    }
                }
            }

        }


    }
}
