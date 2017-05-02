using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

	
    public void Restart() {

        LifeData.Clear();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
