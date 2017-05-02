using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {

    GameObject MsgBord;

	// Use this for initialization
	void Start () {
        //MsgBord = transform.parent.gameObject;
        //MsgBord.SetActive(false);
        //StartCoroutine();
    }
	
    IEnumerator DelayOpen() {
        yield return new WaitForSeconds(1.0f);
        MsgBord.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void Close() {
        transform.parent.gameObject.SetActive(false);
    }
    
}
