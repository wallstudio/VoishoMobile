using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public enum Type: int {
        LEFT,
        CENTER,
        RIGHT
    }
    public Type type;

    GameManager Manager;

    void Start() {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        KeyCode k = type == Type.LEFT ? KeyCode.LeftArrow : (type == Type.CENTER ? KeyCode.DownArrow : KeyCode.RightArrow);
        if (Input.GetKeyDown(k)) {
            OnClick();
        }
    }

    public void OnClick () {
        Manager.ReciveInput(type);
	}
}
