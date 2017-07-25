using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickListener : MonoBehaviour {

    bool firstClick = false;
    float runningTimerSecond;

    float delay = 0.25F;

    public DoubleClickListener() { }

    public DoubleClickListener(float delay)
    {
        this.delay = delay;
    }

    public bool isDoubleClicked()
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
            firstClick = false;
            return true;
        }

        return false;
    }
}
