using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badger_control : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 vel; // Holds the random velocity
    float switchDirection = 3;
    float curTime = 0;
    SpriteRenderer sr;

    void Start()
    {
        SetVel();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void SetVel()
    {
        if (Random.value > .5)
        {
            vel.x = 1 * 1.5f * Random.value;
            sr.flipX = true;
        }
        else
        {
            vel.x = -1 * 1.5f* Random.value;
            sr.flipX = false;
        }
        if (Random.value > .5)
        {
            vel.z = 1 * 1.5f * Random.value;
        }
        else
        {
            vel.z = -1 * 1.5f * Random.value;
        }
    }

    void Update()
    {
        if (curTime < switchDirection)
        {
            curTime += 1 * Time.deltaTime;
        }
        else
        {
            SetVel();
            if (Random.value > .5)
            {
                switchDirection += Random.value;
            }
            else
            {
                switchDirection -= Random.value;
            }
            if (switchDirection < 1)
            {
                switchDirection = 1 + Random.value;
            }
            curTime = 0;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = vel;
    }

    Vector3 startPosition;

    void Awake()
    {
        startPosition = GetComponent<Transform>().position;
    }

    void FreeRoam()
    {
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += startPosition;
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;
            nav.destination = finalPosition;
        }
    }
}
