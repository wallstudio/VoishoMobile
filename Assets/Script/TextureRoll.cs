using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRoll : MonoBehaviour {

    public int TexNum = 0;
    public Sprite[] sprites;
    SpriteRenderer Sr;

	// Use this for initialization
	void Start () {
        Sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Sr.sprite = sprites[TexNum];
	}

    public int GetLen() {
        return sprites.Length;
    }
}
