using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Badger_control : MonoBehaviour
{
    void Start()
    {
 
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Trap")
        {
            GameObject.FindGameObjectWithTag("Trap").GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
