using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseButton : MonoBehaviour {
    public bool pause;
    public GameObject pauseUI;
    public GameObject player;
	// Use this for initialization
	void Start () {
        pause = false;
        pauseUI = GameObject.Find("pauseMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        pauseUI.SetActive(false);
        //Debug.Log(pause);
        Time.timeScale = 1;//scene 로드될때 사물들이 움직이게 한다
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("pauseButton"))
        {
            onPause();
        }
		
	}

    public void onPause()
    {
        pause = !pause;
        if (pause)//정지상태라면
        {
            pauseUI.SetActive(true);//정지 메뉴가 보이게 한다
            Time.timeScale = 0;
            player.GetComponent<Char_control>().enabled = false;//화면 다른곳을 클릭해도 플레이어는 움직이지 않는다
        }
        else if (!pause)//정지상태가 아니라면
        {
            pauseUI.SetActive(false);//정지 메뉴를 안보이게 한다
            Time.timeScale = 1;
            player.GetComponent<Char_control>().enabled = true;//플레이어가 다시 움직일 수 있게 한다
        }
    }

    public void Resume()
    {
        pause = false;
        pauseUI.SetActive(false);//정지 메뉴를 안보이게 한다
        Time.timeScale = 1;//다시 움직이게 한다
        player.GetComponent<Char_control>().enabled = true;//플레이어가 다시 움직일 수 있게 한다

    }

    public void mainMenu()//메인 메뉴로 돌아간다
    {

    }
    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        //SceneManager.LoadScene("Field");
        //pause = false;
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
