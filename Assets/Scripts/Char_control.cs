using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Char_control : MonoBehaviour {

    public Vector2 speed = new Vector2(5f, 2f);//캐릭터의 속도
    
    public Vector2 targetPosition;//마우스로 클릭한 위치
    public Vector2 relativePosition;//지금 캐릭터의 위치와 비교한 상대적인 위치(어느 방향, 얼마나 멀리)

    private Vector2 movement;//움직임을 저장하는 변수

    private int meatGauage=100;//체력 게이지
    private int fireGuage = 100;//장작 게이지

    //public float meatSliderValue;
    //public Slider meatSlider;

    public Image meatBar;  //Tells Unity that the gameObject is a UI Image
    public float meatFillAmount; //Fill Amount for UI Image

    public static bool collided = false;//부딪혔는지 아닌지

    private void Start()
    {
        //meatSliderValue = GameObject.Find("meatSlider").GetComponent<Slider>().value;
        //transform.position = new Vector3(-5f, -1f, 0f);
        //meatBar = GameObject.Find("meatFill").GetComponent<Image>();
        //meatFillAmount = meatBar.fillAmount;
    }

    void Update()
    {
        //마우스로 클릭한 위치 받는다
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //현재위치에 따른 상대적인 위치를 구한다 (클릭한 위치-현재 위치)
        // Update each frame to account for any movement
        relativePosition = new Vector2(
            targetPosition.x - gameObject.transform.position.x,
            targetPosition.y - gameObject.transform.position.y);


    }

    void FixedUpdate()
    {
        // 5 - If you are about to overshoot the target, reduce velocity to that distance
        //      Else cap the Movement by a maximum speed per direction (x then y)
        if (speed.x * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            movement.x = relativePosition.x;
        }
        else
        {
            movement.x = speed.x * Mathf.Sign(relativePosition.x);
        }
        if (speed.y * Time.deltaTime >= Mathf.Abs(relativePosition.y))
        {
            movement.y = relativePosition.y;
        }
        else
        {
            movement.y = speed.y * Mathf.Sign(relativePosition.y);
        }

        //방향에 따라 캐릭터의 x축 방향을 뒤집는다
        if(movement.x<0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX =true;
        }
        
        //물리엔진을 사용하여 캐릭터를 움직인다
        GetComponent<Rigidbody2D>().velocity = movement;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Animal")
        {
            collided = true;//부딪힌게 true
        }
    }

    



}
