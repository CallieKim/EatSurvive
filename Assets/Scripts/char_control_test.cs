﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class char_control_test : MonoBehaviour {

    public enum MovementState
    {
        idle, walking, running, dead, onFire, attack
    }

    public MovementState MovementType;

    //public Vector2 speed = new Vector2(5f, 2f);//캐릭터의 속도
    public float walk_speed;
    public float run_speed;

    public Vector2 targetPosition;//마우스로 클릭한 위치
    public Vector2 relativePosition;//지금 캐릭터의 위치와 비교한 상대적인 위치(어느 방향, 얼마나 멀리)

    //private Vector2 movement;//움직임을 저장하는 변수

    //private int meatGauage=100;//체력 게이지
    //private int fireGuage = 100;//장작 게이지



    public static bool collided = false;//불안킨 상태로 부딪혔는지 아닌지
    public static bool collided_fire = false;//불킨 상태로 부딪혔는지 아닌지
    public static bool attackState = false;
    public bool tree_collided = false;//나무랑 부딪혔는지 아닌지 판단하는 변수
    public bool meat_collided = false;//고기랑 부딪혔는지 아닌지 판단하는 변수
    //public bool animal_collided = false;
    public Collider2D hitCollider;

    bool clicked = false;//클릭해야만 움직인다

    Animator anim;//캐릭터의 animator
    //public Sprite[] hurtSprites;
    public GameObject blood;
    public GameObject fracture;
    GameObject gamecontrol;//정지 메뉴를 보이기 위해서

    private DoubleClickListener dbl = new DoubleClickListener(); // (optionnal: pass a float as the delay)

    public bool walking = false;
    public bool running = false;

    flint_skill flintSkill;
    trap_skill trapSkill;
    buff_skill buffSkill;

    public bool dead;
    public Score scoreScript;
    GameObject ScoreObject;



    private void Start()
    {

        dead = false;

        //hurtSprites = Resources.LoadAll<Sprite>("UI/abrasion");
        walk_speed = 2.0f;//플레이어 걷는 속도 설정한다
        run_speed = 5.0f;//플레이어 뛰는 속도 설정한다
        anim = gameObject.GetComponent<Animator>();

    }

    void range_restrict()//클릭한 위치가 화면 위치를 벗어나는것을 막는 함수
    {
        if (targetPosition.y > 2.4f)
        {
            targetPosition.y = 2.4f;
        }
        else if (targetPosition.y < -2.5f)
        {
            targetPosition.y = -2.5f;
        }
        if (targetPosition.x > 6.4f)
        {
            targetPosition.x = 6.4f;
        }
        else if (targetPosition.x < -6.4f)
        {
            targetPosition.x = -6.4f;
        }
    }

    void Update()
    {
        //마우스로 클릭한 위치 받는다
        if (Input.GetKeyDown(KeyCode.Mouse0) && !attackState )//한번 클릭했으면 그리고 스킬들을 누른 상태가 아니라면
        {

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(targetPosition);
            //Debug.Log("targetpos is " + targetPosition);
            //hitCollider = Physics2D.OverlapPoint(targetPosition);
            clicked = true;//클릭했으므로 true로 설정, 움직인다
                           //MovementType = MovementState.walking;
                           //Debug.Log("target position is x: " + targetPosition.x + ", y: " + targetPosition.y);

            range_restrict();//클릭한 위치가 화면 위치를 벗어나는것을 막는 함수
                             /*
                             if (dbl.isDoubleClicked())//그러나 만약 두번 클릭했으면, 동물과 부딪힌 상태가 아닐때,
                             {
                                 //Debug.Log("double clicked");
                                 MovementType = MovementState.running;//플레이어는 뛰는 상태로 바뀐다
                                 //anim.SetBool("is_run", true);
                                 //anim.SetBool("is_idle", false);
                                 //anim.SetBool("is_walk", false);
                                 //running = true;
                             }
                             else {
                                 //Debug.Log("one clicked");
                                 //walking = true;
                                 MovementType = MovementState.walking;
                             }
                             */
            MovementType = MovementState.walking;

        }


        //현재위치에 따른 상대적인 위치를 구한다 (클릭한 위치-현재 위치)
        // Update each frame to account for any movement
        relativePosition = new Vector2(
            targetPosition.x - gameObject.transform.position.x,
            targetPosition.y - gameObject.transform.position.y);

        if (MovementType == MovementState.walking)
        {
            anim.SetBool("is_walk", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_run", false);
            anim.SetBool("is_attack", false);
            //anim.SetBool("is_onFire", false);
        }
        else if (MovementType == MovementState.idle)
        {
            anim.SetBool("is_idle", true);
            anim.SetBool("is_walk", false);
            anim.SetBool("is_run", false);
            anim.SetBool("is_attack", false);
            //anim.SetBool("is_onFire", false);
        }
        else if (MovementType == MovementState.running)
        {
            anim.SetBool("is_run", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_walk", false);
            anim.SetBool("is_attack", false);
            // anim.SetBool("is_onFire", false);
        }

        else if (MovementType == MovementState.dead)// && !dead 안 붙인 버전이다
        {
            anim.SetBool("is_dead", true);
            anim.SetBool("is_attack", false);
            anim.SetBool("is_run", false);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_walk", false);
            anim.SetBool("is_onFire", false);
            dead = true;//죽은게 사실이니 true로 설정한다
            //InsertRank(GameObject.FindGameObjectWithTag("score").GetComponent<Score>().score);//점수를 랭킹에 넣는다


        }

        if (gameObject.transform.position.x == targetPosition.x && gameObject.transform.position.y == targetPosition.y)//클릭한 곳에 도착했으면 평상시 상태로 바뀐다
        {
            MovementType = MovementState.idle;
        }



    }

    void FixedUpdate()
    {

        if (relativePosition.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (relativePosition.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (clicked)//클릭했다면
        {
            Vector2 nowpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);//현재 위치
            if (MovementType == MovementState.walking)//걷는 상태라면 걷기 속도로 이동
            {
                gameObject.transform.position = Vector2.MoveTowards(nowpos, targetPosition, walk_speed * Time.deltaTime);//현재위치에서 클릭한 위치로 이동
            }
            else if (MovementType == MovementState.running)//뛰는 상태라면 뛰는 속도로 이동
            {
                gameObject.transform.position = Vector2.MoveTowards(nowpos, targetPosition, run_speed * Time.deltaTime);//현재위치에서 클릭한 위치로 이동
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Animal")//동물과 부딪혔다면
        {
            //animal_collided = true;
            //Debug.Log("animal collided with player");
            if (!anim.GetBool("is_onFire"))//불안켜진 상태에서 부딪혔다면
            {
                //Debug.Log("collided without fire");
                collided = true;//부딪힌게 true, 체력게이지가 10f만큼 닳는다
            }
            else if (anim.GetBool("is_onFire"))//불킨 상태에서 부딪혔다면
            {
                collided_fire = true;//불켜진 상태로 부딪힌게 true, 장작게이지가 10f만큼 닳는다
            }
        }


    }



}