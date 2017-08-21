using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flint_skill : MonoBehaviour {

    // Use this for initialization
    public bool flint_click;
    public bool fireOn;
    public GameObject fireBar;
    public float fireBarHealth;

	void Start () {
        flint_click = false;
        fireOn = false;
        fireBar = GameObject.Find("fireFill");
        //fireBarHealth = fireBar.GetComponent<Bar_fire_control>().cur_health;
	}
	
	// Update is called once per frame
	void Update () {
        fireBarHealth = fireBar.GetComponent<Bar_fire_control>().cur_health;//장작 게이지의 체력을 계속적으로 확인한다
    }

    private void OnMouseDown()//flint 버튼을 클릭하면
    {

        Debug.Log("flint click");
        if (fireOn && fireBarHealth > 0)//스킬이 이미 커져있는데 또 눌렀다면 꺼진다
        {
            //Debug.Log("fireOn"+fireOn);
            fireOn = false;
            //Debug.Log("fireOn" + fireOn);
        }
        //Debug.Log("flint skill clicked");
        else if(!fireOn && fireBarHealth>0){//스킬이 커져있지 않다면 그리고 장작 게이지가 0보다 크면 불을 킬 수 있다
            //Debug.Log("flint on"+flint_click);
            fireOn = true;
            //Debug.Log("fireOn" + fireOn);
            //Debug.Log(flint_click);
        }
        flint_click = true;//스킬을 눌러서 true로 설정한다

    }
}
