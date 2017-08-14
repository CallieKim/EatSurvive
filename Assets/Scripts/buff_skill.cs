using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buff_skill : MonoBehaviour {

    public bool skill_buff;
    Image bar;
    float dec_delay = 1.0f;
    float max_health=60f;
    float cur_health;
    float dec_health=1f;
    public float cooldownTimer;
    public bool cooldownStart;

	// Use this for initialization
	void Start () {
        skill_buff = false;
        bar = GameObject.Find("skillButton_buff").GetComponent<Image>();
        cur_health = max_health;
        cooldownTimer = 0;
        cooldownStart = false;
	}
	
	// Update is called once per frame
	public void timeGo () {
		if(cooldownStart)
        {
            if(cooldownTimer>0)
            {
                Debug.Log(cooldownTimer);
                cooldownTimer -= Time.deltaTime;
            }
            if(cooldownTimer<0)
            {
                cooldownTimer = 0;
                cooldownStart = false;
            }
        }
	}

    public void buffClicked()//누르면 발생한다
    {

        InvokeRepeating("decreaseHealth",0.0f, dec_delay);//0.0초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
        skill_buff = true;
        //cooldownStart = true;

    }

    public void decreaseHealth()//script의 dec_health만큼 감소시킨다
    {
        cur_health -= dec_health;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
        if (cur_health <= 0)
        {
            cur_health = max_health;
            CancelInvoke("decreaseHealth");
            bar.fillAmount = 100f;
        }
    }

    void setHealth(float myHealth)
    {
        //bar.fillAmount = myHealth;
        /*
        if(cur_health<=0)
        {
            cur_health = max_health;
            CancelInvoke("decreaseHealth");
        }
        */
        bar.fillAmount = myHealth;
    }
}
