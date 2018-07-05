using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour {

	public bool startTutorialDebug = false;

	[SerializeField]
	private TutorialStep[] tutorialSteps;

	void Start(){

		foreach(Transform t in transform){
			t.gameObject.SetActive(true);
		}

		tutorialSteps = GetComponentsInChildren<TutorialStep>();
		for(int i = 0; i < tutorialSteps.Length; i++){
			tutorialSteps[i].SetStepNumber(i);
			tutorialSteps[i].gameObject.SetActive(false);
		}
		
		if(startTutorialDebug){
			BeginTutorial();
		} else if(PlayerPrefs.GetInt("tutorialDone") == 0){
			PlayerPrefs.SetInt("tutorialDone", 1);
			BeginTutorial();
		}
	}

	public void BeginTutorial(){
		if(tutorialSteps.Length > 1){
			for(int i = 1; i < tutorialSteps.Length; i++)
				tutorialSteps[i].gameObject.SetActive(false);
		}
		
		tutorialSteps[0].gameObject.SetActive(true);

		NewGameManager.instance.gamePauseHandler.CloseIngameMenuForTutorial();
	}

	public void StepCompleted(int stepCompleted){
		if(stepCompleted == tutorialSteps.Length - 1){
			tutorialSteps[stepCompleted].gameObject.SetActive(false);
			EndTutorial();
			return;
		}
		tutorialSteps[stepCompleted].gameObject.SetActive(false);
		tutorialSteps[stepCompleted+1].gameObject.SetActive(true);
	}

	public void EndTutorial(){
		NewGameManager.instance.gamePauseHandler.PauseUnpauseGame(false);
	}
}
