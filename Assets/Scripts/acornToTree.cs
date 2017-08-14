using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornToTree : MonoBehaviour {
    Sprite[] treeSprites;
    float lastUpdate;

    // Use this for initialization
    void Start() {
        treeSprites = Resources.LoadAll<Sprite>("Background/trees");//함정의 모습 2가지를 배열에 저장한다
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - lastUpdate >= 3f)//3초 지나면 모습이 바뀐다
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[0];
            lastUpdate = Time.time;

        }
        //gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[2];

        InvokeRepeating("Bigger", 0.5f, 1f);
        //gameObject.transform.localScale = new Vector3(0.35f, 0.55f, 0);
    }
    void Bigger()
    {
        gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
        if (gameObject.transform.localScale.x > 0.35f || gameObject.transform.localScale.y > 0.55f)
        {
            Debug.Log("stop bigger");
            CancelInvoke("Bigger");
        }
    }
}
