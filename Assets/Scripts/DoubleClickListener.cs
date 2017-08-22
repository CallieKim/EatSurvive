using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickListener : MonoBehaviour {

    bool firstClick = false;
    bool doubleClick = false;
    bool TripleClick = false;
    bool FourClick = false;
    bool FiveClick = false;
    bool SixClick = false;

    int num = 0;

    float runningTimerSecond;
    GameObject player;

    float delay = 0.25F;

    public DoubleClickListener() { }

    public DoubleClickListener(float delay)
    {
        this.delay = delay;
    }

    public bool isDoubleClicked()
    {
        if(Char_control.attackState)
        {
            // If the time is too long we reset first click variable
            if (firstClick && (Time.time - runningTimerSecond) > delay)
            {
                firstClick = false;
            }

            if (!firstClick)
            {
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_meat_attack");
                //Debug.Log("char_meat_attack1");
                firstClick = true;
                runningTimerSecond = Time.time;
            }
            else
            {
                //Debug.Log("char_meat_attack2");
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_meat_attack");
                if (SelectMenu.meat_char)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_meat_attack");
                }
                else if(SelectMenu.fire_char)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_fire_attack");
                    Debug.Log("fire attack");
                }
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_fire_attack");
                //Debug.Log("doubleclick true");
                firstClick = false;
                Char_control.attackState = false;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Char_control>().MovementType = Char_control.MovementState.walking;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().
                return true;
            }
            Char_control.attackState = false;
            return false;
        }
        else 
        {
            // If the time is too long we reset first click variable
            if (firstClick && (Time.time - runningTimerSecond) > delay)
            {
                firstClick = false;
            }

            if (!firstClick)
            {
                firstClick = true;
                runningTimerSecond = Time.time;
            }
            else
            {
                //Debug.Log("doubleclick true");
                firstClick = false;
                return true;
            }

            return false;
        }

    }

    public bool isTripleClicked()
    {
        // If the time is too long we reset first click variable
        if (firstClick && (Time.time - runningTimerSecond) > delay)
        {
            firstClick = false;
        }

        if (!firstClick)
        {
            firstClick = true;
            runningTimerSecond = Time.time;
        }
        else//double click 성공
        {
            //Debug.Log("doubleclick true");
            firstClick = false;
            return true;
        }

        return false;
    }


}
