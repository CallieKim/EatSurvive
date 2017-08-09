using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_move : MonoBehaviour {
    float pos;
    public float speed;
    public static cloud_move current;
    private Renderer _renderer;

    // Use this for initialization
    void Start () {
        current = this;
        pos = 0;
        speed = 0.0005f;
        _renderer = GetComponent<Renderer>();
        _renderer.sortingLayerName = "Background";//보이는 순서를 정한다
    }
	
	// Update is called once per frame
	void Update () {
        pos += speed;//속도에 따라 위치를 이동한다
        if(pos>1.0f)
        {
            pos -= 1.0f;
        }
        _renderer.material.mainTextureOffset = new Vector2(pos, 0);
	}
}
