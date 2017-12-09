using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoubleTapForMenu : MonoBehaviour, IPointerDownHandler {

	[SerializeField]
	private bool menuOpened = false;
	[SerializeField]
	private bool doubleTapInitialized = false;
	[SerializeField]
	private float firstTapTime = 0f;
	[SerializeField]
	private float timeBetweenTaps = 0.2f;
	
	[SerializeField]
	private GameObject mainMenu, doubleTapInfoText;

	public void OnPointerDown(PointerEventData ped){
		Debug.Log("Tap");
		if(!doubleTapInitialized){
			doubleTapInitialized = true;
			firstTapTime = Time.time;
			StartCoroutine(ResetDoubleTapInit());
		} else if (Time.time - firstTapTime < timeBetweenTaps){
			//Debug.Log("Doubletap");
			PauseUnpauseGame();
		} 
	}

	public void PauseUnpauseGame(){
		doubleTapInitialized = false;
		menuOpened = !menuOpened;
		foreach(Transform child in mainMenu.transform){
			child.gameObject.SetActive(menuOpened);
		}
	}

	IEnumerator ResetDoubleTapInit(){
		yield return new WaitForSecondsRealtime(timeBetweenTaps+0.05f);
		doubleTapInitialized = false;
		yield break;
	}

	public void ToggleDoubleTapInfoText(){
		doubleTapInfoText.SetActive(!doubleTapInfoText.activeSelf);
	}
	
}
