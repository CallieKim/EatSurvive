using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trap_skill : MonoBehaviour {

    // Use this for initialization
    public bool trap_click;
    public int trapSize = 5;
    public GameObject trapOriginal;
    public GameObject player;
    Text trapNumber;
    Bar_meat_control bar_meat;

    //GameObject[] traps = new GameObject[5];//함정을 저장할 배열
    public Queue<GameObject> traps = new Queue<GameObject>();

    void Start()
    {
        bar_meat = GameObject.Find("meatFill").GetComponent<Bar_meat_control>();//체력 게이지의 script를 찾아서 저장
        trapNumber = GameObject.Find("trapText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        trap_click = false;
        trapOriginal = GameObject.FindGameObjectWithTag("Trap");

        traps.Enqueue(trapOriginal);
        trapOriginal.SetActive(false);

        for (int i = 0; i < trapSize; i++)//trap 큐를 초기화 시킨다
        {
            GameObject trap = (GameObject)Instantiate(trapOriginal);
            //setPos(obj_fur);
            //obj_fur.transform.parent = gameObject.transform;
            traps.Enqueue(trap);
            trap.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //trapNumber.text = traps.Count.ToString();
        trapNumber.text = trapSize.ToString();
    }

    private void OnMouseDown()//trap 버튼을 클릭하면
    {
        trap_click = true;
        Debug.Log("trap skill clicked");
        trapSize--;
        bar_meat.decreaseHealthWithDec(10);//체력이 일정 수준만큼 감소된다
        //trap_click = true;
        setPos(traps.Dequeue());
        //trap_click = false;
    }

    //함정의 위치를 정해주는 함수
    void setPos(GameObject obj)
    {
        obj.transform.position = player.transform.position;
        obj.SetActive(true);
        //trap_click = false;
    }
    
    //함정을 다시 큐에 넣는다
    public void GetObj(GameObject obj)
    {
        trapSize++;
        traps.Enqueue(obj);//큐에 넣어주고
        obj.SetActive(false);//disable
    }
    
}
