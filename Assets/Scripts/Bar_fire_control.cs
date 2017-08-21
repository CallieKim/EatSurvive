using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_fire_control : MonoBehaviour {

    public Image bar;
    public float max_health = 100f;
    public float cur_health = 0f;
    public float dec_health;//불을 안 키고 있을때 감소될 수치
    public float dec_fire_health;//불을 키고 있을때 감소될 수치
    public float dec_delay;
    public bool noFire;//장작 게이지가 0인지 아닌지 판단하는 변수
    GameObject player;
    //Char_control playerControl;
    Animator playerAnim;//player의 animator 
    flint_skill flintSkill;

    private void Awake()//해상도 조절 함수
    {
        //Screen.SetResolution(Screen.width, Screen.width*16/9, true);
    }

    // Use this for initialization
    void Start()
    {
        if (SelectMenu.meat_char)//체력 천천히 주는 캐릭터
        {
            //Debug.Log("meat");
            dec_health = 0.2f;
            dec_delay = 0.1f;
        }
        else if (SelectMenu.fire_char)//장작 천천히 주는 캐릭터
        {
            dec_health = 0.1f;
            dec_delay = 0.1f;
        }
        noFire = false;//장작 게이지는 0이 아니다
        cur_health = max_health;
        InvokeRepeating("decreaseHealth", 0.5f, dec_delay);//0.5초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
        dec_fire_health = dec_health + 0.1f;
        player = GameObject.FindGameObjectWithTag("Player");
        //playerControl = player.GetComponent<Char_control>();
        playerAnim = player.GetComponent<Animator>();
        flintSkill=GameObject.Find("skillButton_flint").GetComponent<flint_skill>();
    }

    // Update is called once per frame
    void Update()
    {
        if(noFire && cur_health>0)//장작 게이지가 0으로 판단했는데 게이지가 >0 이면 다시 줄어든다
        {
            InvokeRepeating("decreaseHealth", 0.1f, dec_delay);//0.1초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
            noFire = false;
            //playerAnim.SetBool("is_onFire", true);
        }
        if(playerAnim.GetBool("is_onFire"))//player의 animation이 불을 키고 있으면
        {
            dec_health = dec_fire_health;
            //Debug.Log("dec_fire_health");
        }
        else if(!playerAnim.GetBool("is_onFire"))//player의 animation이 불을 끄고 있으면
        {
            dec_health = dec_fire_health - 0.1f;
        }
        /*
        if (Char_control.collided_fire == true)//불켜진 상태에서 동물이랑 부딪쳤으면
        {
            decreaseHealthWithDec(10f);//인자 10f만큼 체력을 감소시킨다
            Char_control.collided_fire = false;
        }
        */
        if (cur_health < 0)//체력이 0이 될때
        {
            cur_health = 0f;
            //Debug.Log("player dead");
            CancelInvoke("decreaseHealth");//체력이 감소되는 함수를 취소하고
            noFire = true;//장작 게이지가 0이다
            flintSkill.fireOn = false;//불켜지는 스킬을 끈다
            //playerAnim.SetBool("is_onFire", false);//불켜진 상태로 존재하지 못한다
            //Debug.Log("there is no fire"+playerAnim.GetBool("is_onFire"));
            //player.GetComponent<Char_control>().MovementType = Char_control.MovementState.dead;//캐릭터 상태를 죽은 상태로 바꾼다
        }
        else if (cur_health > 100)//최대 체력을 100으로 고정시킨다
        {
            cur_health = 100;
        }
    }

    void decreaseHealth()
    {
        cur_health -= dec_health;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
    }
    public void decreaseHealthWithDec(float dec)//인자 dec만큼 체력을 감소시킨다
    {
        cur_health -= dec;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
    }

    public void increaseHealth(float inc_health)//인자 inc_health만큼 체력을 증가시킨다
    {
        cur_health += inc_health;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
    }

    void setHealth(float myHealth)
    {
        bar.fillAmount = myHealth;
    }

    public void invincible()//무적일때 체력을 안깎는다
    {
        CancelInvoke("decreaseHealth");
    }

    public void notInvincible()//무적이 아니면 다시 체력이 깎인다
    {
        InvokeRepeating("decreaseHealth", 0.5f, dec_delay);//0.5초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
    }

}
