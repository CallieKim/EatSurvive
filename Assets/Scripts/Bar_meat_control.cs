﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_meat_control : MonoBehaviour {

    public Image bar;
    public float max_health = 100f;
    public float cur_health = 0f;
    public float dec_health;
    float run_dec_health;
    float walk_dec_health;
    public float dec_delay = 2.0f;
    GameObject player;


    // Use this for initialization
    void Start()
    {
        cur_health = max_health;
        walk_dec_health = dec_health;
        run_dec_health = dec_health + 0.2f;
        InvokeRepeating("decreaseHealth", 0.5f, dec_delay);//0.5초후에 깎이는데, dec_delay만큼 decreaseHealth함수를 반복한다
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Char_control.collided == true)//동물이랑 부딪쳤으면
        {
            cur_health -= 10f;
            float calc_health = cur_health / max_health;
            setHealth(calc_health);
            Char_control.collided = false;
        }
        if(player.GetComponent<Char_control>().MovementType==Char_control.MovementState.running)//player가 뛰는상태라면 체력이 더 급격하게 감소해야 한다
        {
            Debug.Log("run_dec_health");
            dec_health =run_dec_health;//체력이 감소되는 양을 뛸때 감소되는 양으로 바꾼다
        }
        if (player.GetComponent<Char_control>().MovementType == Char_control.MovementState.walking)//player가 걷는상태라면 체력 감소되는 양을 원래대로 롤백
        {
            Debug.Log("walk_dec_health");
            dec_health = walk_dec_health;//체력이 감소되는 양을 걸을때 감소되는 양으로 바꾼다
        }
        if (player.GetComponent<Char_control>().MovementType == Char_control.MovementState.idle)//player가 idle상태라면 체력 감소되는 양을 원래대로 롤백
        {
            Debug.Log("idle_dec_health");
            dec_health = walk_dec_health;//체력이 감소되는 양을 걸을때 감소되는 양으로 바꾼다
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