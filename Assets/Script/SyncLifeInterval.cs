using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncLifeInterval : MonoBehaviour {
    Slider slider;
    Text show;
	// Use this for initialization
	void Start () {
        LifeData.Clear();
        slider = gameObject.GetComponent<Slider>();
        slider.value = LifeData.LifeInterval;
        show = GameObject.Find("IntervalShow").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        LifeData.LifeInterval = Mathf.RoundToInt(slider.value);
        show.text = ((slider.maxValue - LifeData.LifeInterval)/1000+2).ToString();

    }
}
