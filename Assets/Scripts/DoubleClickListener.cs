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

                firstClick = true;
                runningTimerSecond = Time.time;
            }
            else
            {

                if (SelectMenu.meat_char)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_meat_attack");
                    SoundManager.instance.PlaySoundTime("attack",3.9f);
                }
                else if(SelectMenu.fire_char)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("char_fire_attack");
                    SoundManager.instance.PlaySoundTime("attack", 3.9f);
                }

                firstClick = false;
                Char_control.attackState = false;
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
