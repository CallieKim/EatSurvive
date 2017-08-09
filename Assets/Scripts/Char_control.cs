using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Char_control : MonoBehaviour {

    public enum MovementState
    {
        idle, walking, running, dead,onFire
    }

    public MovementState MovementType;

    //public Vector2 speed = new Vector2(5f, 2f);//캐릭터의 속도
    public float walk_speed;
    public float run_speed;
    
    public Vector2 targetPosition;//마우스로 클릭한 위치
    public Vector2 relativePosition;//지금 캐릭터의 위치와 비교한 상대적인 위치(어느 방향, 얼마나 멀리)

    private Vector2 movement;//움직임을 저장하는 변수

    private int meatGauage=100;//체력 게이지
    private int fireGuage = 100;//장작 게이지

    public Image meatBar;  //Tells Unity that the gameObject is a UI Image
    public float meatFillAmount; //Fill Amount for UI Image

    public static bool collided = false;//불안킨 상태로 부딪혔는지 아닌지
    public static bool collided_fire = false;//불킨 상태로 부딪혔는지 아닌지
    public bool tree_collided = false;//나무랑 부딪혔는지 아닌지 판단하는 변수
    public bool meat_collided = false;//고기랑 부딪혔는지 아닌지 판단하는 변수
    public Collider2D hitCollider;

    bool clicked = false;//동물이랑 부딪혔는지 아닌지 판단하는 변수

    Animator anim;//캐릭터의 animator

    private DoubleClickListener dbl = new DoubleClickListener(); // (optionnal: pass a float as the delay)

    public bool walking = false;
    public bool running = false;

    flint_skill flintSkill;
    trap_skill trapSkill;


    private void Start()
    {
        walk_speed = 2.0f;//플레이어 걷는 속도 설정한다
        run_speed = 5.0f;//플레이어 뛰는 속도 설정한다
        anim = gameObject.GetComponent<Animator>();
        flintSkill = GameObject.Find("skillButton_flint").GetComponent<flint_skill>();
        trapSkill = GameObject.Find("skillButton_trap").GetComponent<trap_skill>();
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !flintSkill.flint_click && !trapSkill.trap_click)//한번 클릭했으면 그리고 스킬들을 누른 상태가 아니라면
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("targetpos is " + targetPosition);
            //hitCollider = Physics2D.OverlapPoint(targetPosition);
            //clicked = true;//클릭했으므로 true로 설정
                           //MovementType = MovementState.walking;
                           //Debug.Log("target position is x: " + targetPosition.x + ", y: " + targetPosition.y);

            range_restrict();//클릭한 위치가 화면 위치를 벗어나는것을 막는 함수

            if (dbl.isDoubleClicked())//그러나 만약 두번 클릭했으면
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
            
        }
        else if(flintSkill.flint_click)//flint 스킬 누른상태라면
        {
            anim.SetBool("is_onFire", true);//불에 타는 상태로 바뀐다
            flintSkill.flint_click = false;//다시 스킬 눌렀는지 확인하는 변수를 false로 설정한다->그래야 움직일 수 있음
        }
        else if(trapSkill.trap_click)//trap 스킬을 누른상태라면 trap이 생성된다
        {
            //Debug.Log("should make trap");
            trapSkill.trap_click = false;//다시 스킬 눌렀는지 확인하는 변수를 false로 설정한다->그래야 움직일 수 있음
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
            //anim.SetBool("is_onFire", false);
        }
        else if (MovementType == MovementState.idle)
        {
            anim.SetBool("is_idle", true);
            anim.SetBool("is_walk", false);
            anim.SetBool("is_run", false);
            //anim.SetBool("is_onFire", false);
        }
        else if (MovementType == MovementState.running)
        {
            anim.SetBool("is_run", true);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_walk", false);
           // anim.SetBool("is_onFire", false);
        }
        else if (MovementType == MovementState.dead)
        {
            anim.SetBool("is_dead", true);
            anim.SetBool("is_run", false);
            anim.SetBool("is_idle", false);
            anim.SetBool("is_walk", false);
            anim.SetBool("is_onFire", false);
        }

        if (gameObject.transform.position.x == targetPosition.x && gameObject.transform.position.y == targetPosition.y)//클릭한 곳에 도착했으면 평상시 상태로 바뀐다
        {
            MovementType = MovementState.idle;
        }


    }

    void FixedUpdate()
    {

        if(relativePosition.x>0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (relativePosition.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(clicked)//클릭했다면
        {
            Vector2 nowpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);//현재 위치
            if (MovementType == MovementState.walking)//걷는 상태라면 걷기 속도로 이동
            {
             gameObject.transform.position = Vector2.MoveTowards(nowpos, targetPosition, walk_speed * Time.deltaTime);//현재위치에서 클릭한 위치로 이동
            }
            else if(MovementType==MovementState.running)//뛰는 상태라면 뛰는 속도로 이동
            {
                gameObject.transform.position = Vector2.MoveTowards(nowpos, targetPosition, run_speed * Time.deltaTime);//현재위치에서 클릭한 위치로 이동
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Animal")//불 안켜진 상태에서 동물과 부딪혔다면
        {
            //Debug.Log("animal collided with player");
            if(!anim.GetBool("is_onFire"))//불안켜진 상태에서 부딪혔다면
            {
                Debug.Log("collided without fire");
                collided = true;//부딪힌게 true, 체력게이지가 10f만큼 닳는다
            }else if(anim.GetBool("is_onFire"))//불킨 상태에서 부딪혔다면
            {
                collided_fire = true;//불켜진 상태로 부딪힌게 true, 장작게이지가 10f만큼 닳는다
            }
        }
        else if (other.tag == "Tree")//나무랑 부딪혔으면
        {
            //tree_collided = true;
            Debug.Log("collided tree on char script");
            other.GetComponent<Tree_control>().disappear();
        }
        
        else if(other.tag=="meat")//고기랑 부딪혔으면
        {
            Debug.Log("meat collided");
            //meat_collided = true;
            //other.GetComponent<GameObject>().GetComponent<SpriteRenderer>().enabled = false;
            //GameObject.FindGameObjectWithTag("meat").GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<meat_control>().disappear();
            
            
        }
        
    }

    



}
