using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

    GameObject myChar;
    GameObject char_meat;
    GameObject char_fire;

    void Awake()
    {
        Debug.Log("awake");
        char_meat = GameObject.Find("char_meat");
        char_meat.SetActive(false);
        char_fire = GameObject.Find("char_fire");
        char_fire.SetActive(false);
        //myChar = Instantiate(LoadSelected.charSelected) as Transform;
        /*
        if(GameObject.Find("CharacterController").GetComponent<SelectMenu>().meat)
        {
            char_meat.SetActive(true);
        }
        else if(GameObject.Find("CharacterController").GetComponent<SelectMenu>().fire)
        {
            char_fire.SetActive(true);
        }
        */
        if(SelectMenu.meat_char)
        {
            char_meat.SetActive(true);
        }
        else if(SelectMenu.fire_char)
        {
            char_fire.SetActive(true);
        }

    }
}
