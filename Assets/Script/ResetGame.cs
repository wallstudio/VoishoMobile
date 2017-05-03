using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

	
    public void Restart() {

        LifeData.Clear();
#if UNITY_ANDROID
        SceneManager.LoadScene("Android", LoadSceneMode.Single);
#elif UNITY_STANDALONE_WIN
        SceneManager.LoadScene("Windows", LoadSceneMode.Single);
#endif
    }
}
