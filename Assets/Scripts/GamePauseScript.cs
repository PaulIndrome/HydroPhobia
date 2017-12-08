using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePauseScript : MonoBehaviour, IPointerDownHandler {

	[SerializeField]
	private bool menuOpened = false;
	[SerializeField]
	private bool doubleTapInitialized = false;
	[SerializeField]
	private float firstTapTime = 0f;
	[SerializeField]
	private float timeBetweenTaps = 0.2f;
	
	[SerializeField]
	private GameObject IngameMenu;
	

	public void OnPointerDown(PointerEventData ped){
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
		Time.timeScale = (menuOpened) ? 0 : 1;
		IngameMenu.SetActive(menuOpened);
	}

	IEnumerator ResetDoubleTapInit(){
		yield return new WaitForSecondsRealtime(timeBetweenTaps+0.05f);
		doubleTapInitialized = false;
	}
	
}
