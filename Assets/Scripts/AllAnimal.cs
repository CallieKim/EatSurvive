using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAnimal : MonoBehaviour {
    GameObject pig;
    GameObject squirrel;
    GameObject rabbitOrigin;
    GameObject badgerOrigin;

    public static Queue<GameObject> rabbits = new Queue<GameObject>();//rabbit 큐, 토끼는 3마리까지
    public static int rabbitSize=3;
    public static Queue<GameObject> badgers = new Queue<GameObject>();//badger 큐, 오소리는 1마리가지
    public static int badgerSize=2;

    public bool rabbitRespawn;
    public bool badgerRespawn;

    // Use this for initialization
    void Start () {
        rabbitRespawn = false;
        badgerRespawn = false;
        pig = GameObject.FindGameObjectWithTag("enemy");
        squirrel = GameObject.Find("squirrel");
        rabbitOrigin = GameObject.Find("rabbit");
        //rabbits.Enqueue(rabbitOrigin);
        //rabbitOrigin.SetActive(false);
        badgerOrigin = GameObject.Find("badger");
        //badgers.Enqueue(badgerOrigin);
        //badgerOrigin.SetActive(false);

        for (int i=0;i<3;i++)//rabbit 큐를 초기화 한다
        {
            GameObject rabbit = (GameObject)Instantiate(rabbitOrigin);
            rabbits.Enqueue(rabbit);
            rabbit.SetActive(true);
        }
        rabbitOrigin.SetActive(false);
        //rabbitSize = 0;
        for (int i = 0; i < 2; i++)//badger 큐를 초기화 한다
        {
            GameObject badger = (GameObject)Instantiate(badgerOrigin);
            badgers.Enqueue(badger);
            badger.SetActive(true);
        }
        badgerOrigin.SetActive(false);
        //badgerSize = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(!pig.activeSelf)//돼지가 화면에 없으면
        {
            StartCoroutine("appearAgain",pig);
            //Debug.Log("appear aggain");
        }
        if(!squirrel.activeSelf)//다람쥐가 화면에 없으면
        {
            StartCoroutine("appearAgainSquirrel", squirrel);
        }
        /*
        if(!rabbit.activeSelf)//토끼가 화면에 없으면
        {

        }
        if(!badger.activeSelf)//오소리가 화면에 없으면
        {

        }
        */
        /*
        if(rabbits.Contains(GameObject.Find("rabbit")))
        {
            Debug.Log("respawn rabbit");
            rabbits.Dequeue().SetActive(true);
        }
        */
        if(rabbitSize==0)
        {
            rabbitRespawn = true;
        }
        if(badgerSize==0)
        {
            badgerRespawn = true;
        }
        if(rabbitRespawn)
        {
            rabbitRespawn = false;
            for (int i = 0; i < 3; i++)//rabbit 큐를 초기화 한다
            {
                //GameObject rabbit = (GameObject)Instantiate(rabbitOrigin);
                rabbits.Dequeue().SetActive(true);
                
            }
            rabbitSize = 3;
        }
        
        /*
        if(rabbits.Peek()!=null)
        {
            rabbits.Dequeue().SetActive(true);
        }
        */
        if(badgerRespawn)
        {
            //Debug.Log("respawn");
            badgerRespawn = false;
            badgers.Dequeue().SetActive(true);
            badgerSize--;
            for (int i = 0; i < 2; i++)//badger 큐를 초기화 한다
            {
                //Debug.Log("deqeue badger");
                //GameObject badger = (GameObject)Instantiate(badgerOrigin);
                badgers.Dequeue().SetActive(true);
                //badger.SetActive(true);
            }
            badgerSize = 2;
        }
	}

    IEnumerator appearAgain(GameObject other)//다시 나타나게 한다..45초 후에----------돼지
    {
        yield return new WaitForSeconds(45f);
        other.SetActive(true);
        other.transform.position = new Vector3(-5.57f,-2.11f,0);//나타나는 위치를 지정해준다
        pig.GetComponent<wildPig_move>().moveToStart();//시작위치로 이동한다
        //Debug.Log("again finish");
        StopCoroutine("appearAgain");
    }

    IEnumerator appearAgainSquirrel(GameObject other)//다시 나타나게 한다..15초 후에-----------다람쥐
    {
        yield return new WaitForSeconds(15f);
        other.SetActive(true);
        other.transform.position = new Vector3(-5.57f, -2.11f, 0);//나타나는 위치를 지정해준다
        squirrel.GetComponent<Squirrel_move>().TreeQueueStart();
        //Debug.Log("again finish");
        StopCoroutine("appearAgainSquirrel");
    }

    IEnumerator appearAgainRabbit(GameObject other)//다시 나타나게 한다..45초 후에---------토끼
    {
        yield return new WaitForSeconds(45f);
        other.SetActive(true);
        other.transform.position = new Vector3(-5.57f, -2.11f, 0);//나타나는 위치를 지정해준다
        pig.GetComponent<wildPig_move>().moveToStart();//시작위치로 이동한다
        //Debug.Log("again finish");
        StopCoroutine("appearAgain");
    }

    IEnumerator appearAgainBadger(GameObject other)//다시 나타나게 한다..45초 후에---------오소리
    {
        yield return new WaitForSeconds(45f);
        other.SetActive(true);
        other.transform.position = new Vector3(-5.57f, -2.11f, 0);//나타나는 위치를 지정해준다
        pig.GetComponent<wildPig_move>().moveToStart();//시작위치로 이동한다
        //Debug.Log("again finish");
        StopCoroutine("appearAgain");
    }
}
