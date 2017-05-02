using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour {

    public void Post() {
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL("#けだまっち （体験版）で遊びました！ @yukawallstudio"));
    }
}
