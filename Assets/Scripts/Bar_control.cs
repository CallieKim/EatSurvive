using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_control : MonoBehaviour {
    public Image bar;
    public float max_health = 100f;
    public float cur_health = 0f;
    public float dec_health;
    public float dec_delay;
    GameObject player;

	// Use this for initialization
	void Start () {
        cur_health = max_health;
        InvokeRepeating("decreaseHealth",0.5f,dec_delay);
        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (Char_control.collided == true)
        {
            cur_health -= 10f;
            float calc_health = cur_health / max_health;
            setHealth(calc_health);
            Char_control.collided = false;
        } 
        else if(player.GetComponent<Char_control>().tree_collided)
        {
            cur_health += 10f;
            float calc_health = cur_health / max_health;
            setHealth(calc_health);
            player.GetComponent<Char_control>().tree_collided = false;
        }
	}

    void decreaseHealth()
    {
        cur_health -= dec_health;
        float calc_health = cur_health / max_health;
        setHealth(calc_health);
    }

    void setHealth(float myHealth)
    {
        bar.fillAmount = myHealth;
    }


}
