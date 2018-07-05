using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoubleTapForMenu : MonoBehaviour, IPointerDownHandler {

	[SerializeField]
	private int tapsForAction = 2;
	[SerializeField] 
	private int counter = 0;
	[SerializeField]
	private bool menuOpened = false;
	[SerializeField]
	private bool doubleTapInitialized = false;
	[SerializeField]
	private float firstTapTime = 0f, lastTapTime = 0f;
	[SerializeField]
	private float timeBetweenTaps = 0.2f;
	
	[SerializeField]
	private GameObject mainMenu, doubleTapInfoText;

	public void OnPointerDown(PointerEventData ped){
		if(!doubleTapInitialized){
			counter++;
			doubleTapInitialized = true;
			lastTapTime = firstTapTime = Time.time;
			StartCoroutine(ResetDoubleTapInit());
		} else if (Time.time - lastTapTime < timeBetweenTaps){
			counter++;
			lastTapTime = Time.time;
			if (counter >= tapsForAction) {
				OpenCloseMenu();
			}
		}
	}

	public void OpenCloseMenu(){
		doubleTapInitialized = false;
		counter = 0;
		menuOpened = !menuOpened;
		foreach(Transform child in mainMenu.transform){
			child.gameObject.SetActive(menuOpened);
		}
	}

	IEnumerator ResetDoubleTapInit(){
		yield return new WaitUntil(() => Time.time - lastTapTime > timeBetweenTaps);
		doubleTapInitialized = false;
		counter = 0;
		yield break;
	}

	public void ToggleDoubleTapInfoText(){
		doubleTapInfoText.SetActive(!doubleTapInfoText.activeSelf);
	}
	
}
