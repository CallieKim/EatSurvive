using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene("Field");
            //pause = false;
            //int scene = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(scene, LoadSceneMode.Single);
            SceneManager.LoadScene("Field");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {

    }
}
