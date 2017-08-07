using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_skill : MonoBehaviour {

    // Use this for initialization
    public bool trap_click;
    void Start()
    {
        trap_click = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()//flint 버튼을 클릭하면
    {
        Debug.Log("trap skill clicked");
        trap_click = true;
    }

}
