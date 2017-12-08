using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public void loadLevel(string level){
		Time.timeScale = 1;
		SceneManager.LoadScene(level);
	}

	public void quitGame(){
		Application.Quit();
	}
}
