using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeData : MonoBehaviour {


    public static int LifeInterval = 9000;
    public static int GameFps = 60;
    public static long Frame = 0;
    public static long PreFrame = 0;
    // Status
    public static int Life = 6;
    public static int Love = 0;
    public static int Hungery = 0;
    public static int Sick = 0;
    public static int Dirty = 0;
    public static int Rank = 0;
    // Config
    public static int Volume = 100;


    public static void Save() {
        Debug.Log("Save()");
    }
    public static void Load() {
        Debug.Log("Load()");
    }
    public static void Clear() {
        LifeInterval = 9000;
        GameFps = 60;
        Frame = 0;
        PreFrame = 0;
        // Status
        Life = 6;
        Love = 0;
        Hungery = 0;
        Sick = 0;
        Dirty = 0;
        Rank = 0;
        // Config
        Volume = 100;
    Debug.Log("Clear()");
    }


    Toggle debugSwitch;
    void Start() {
        debugSwitch = GameObject.Find("DebugSwitch").GetComponent<Toggle>();
    }
    
    void OnGUI() {
        if (!debugSwitch.isOn) return;
        int i = 0;
        int h = 30;
        GUIStyle gs = new GUIStyle();
        //LifeInterval = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(10, 10, 500, 100), LifeInterval, 0, 2000));
        gs.fontSize = h+3;
        GUI.Label(new Rect(5, i++ * h, 500, 1000), "LifeInterval " + LifeInterval, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "GameFps " + GameFps, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Frame " + Frame, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "PreFrame " + PreFrame, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Life " + Life, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Love " + Love, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Hungery " + Hungery, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Sick " + Sick, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Dirty " + Dirty, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Rank " + Rank, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 100), "Volume " + Volume, gs);
    }
}
