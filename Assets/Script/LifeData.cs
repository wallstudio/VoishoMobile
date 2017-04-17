using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeData : MonoBehaviour {


    public static int LifeInterval = 2000;
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
        Debug.Log("Clear()");
    }

    void OnGUI() {
        int i = 0;
        int h = 30;
        GUIStyle gs = new GUIStyle();
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
