using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Char_control : MonoBehaviour {

    //public Vector2 speed = new Vector2(5f, 2f);//캐릭터의 속도
    public float speed;
    
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
    public bool tree_collided = false;
    public Collider2D hitCollider;

    bool clicked = false;

    private void Start()
    {
        speed = 3.0f;
    }

    void Update()
    {
        //마우스로 클릭한 위치 받는다
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("targetpos is " + targetPosition);
            //hitCollider = Physics2D.OverlapPoint(targetPosition);
            clicked = true;
        }

        //현재위치에 따른 상대적인 위치를 구한다 (클릭한 위치-현재 위치)
        // Update each frame to account for any movement
        relativePosition = new Vector2(
            targetPosition.x - gameObject.transform.position.x,
            targetPosition.y - gameObject.transform.position.y);


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
        if(clicked)
        {
            Vector2 nowpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            //Debug.Log(nowpos + " is nowpos and " + targetPosition + "is targetpos");
            gameObject.transform.position = Vector2.MoveTowards(nowpos, targetPosition, speed*Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Animal")
        {
            collided = true;//부딪힌게 true
        }
        else if (other.tag == "Tree")//나무랑 부딪혔으면
        {
            tree_collided = true;
            Debug.Log("collided tree on char script");
        }
    }

    



}
