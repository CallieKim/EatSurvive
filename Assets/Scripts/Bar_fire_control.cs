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
    public float dec_delay = 2.0f;
    GameObject player;
    //Char_control playerControl;
    Animator playerAnim;//player의 animator 

    private void Awake()//해상도 조절 함수
    {
        //Screen.SetResolution(Screen.width, Screen.width*16/9, true);
    }

    // Use this for initialization
    void Start()
    {
        cur_health = max_health;
        InvokeRepeating("decreaseHealth", 0.5f, dec_delay);//0.5초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
        dec_fire_health = dec_health + 0.4f;
        player = GameObject.FindGameObjectWithTag("Player");
        //playerControl = player.GetComponent<Char_control>();
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAnim.GetBool("is_onFire"))//player의 animation이 불을 키고 있으면
        {
            dec_health = dec_fire_health;
            //Debug.Log("dec_fire_health");
        }
        if (Char_control.collided_fire == true)//불켜진 상태에서 동물이랑 부딪쳤으면
        {
            decreaseHealthWithDec(10f);//인자 10f만큼 체력을 감소시킨다
            Char_control.collided_fire = false;
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

}
