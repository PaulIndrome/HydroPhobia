using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialStep : MonoBehaviour {

	private int stepNumber;
	private StartTutorial tutorial;

	void Start(){
		tutorial = GetComponentInParent<StartTutorial>();
	}

	public void SetStepNumber(int number){
		stepNumber = number;
	}

	public void DisableStep(){
		tutorial.StepCompleted(stepNumber);
	}
}
