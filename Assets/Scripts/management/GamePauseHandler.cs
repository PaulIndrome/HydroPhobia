﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseHandler : MonoBehaviour, IPointerDownHandler {

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

	private Image gameOverImage;

	[SerializeField]
	private PlayerScript[] playerScripts;

	public void Awake(){
		if(playerScripts == null)
			Debug.LogError("No PlayerScripts set in GamePauseHandler");

		gameOverImage = IngameMenu.GetComponent<Image>();
		gameOverImage.enabled = false;
		IngameMenu.transform.GetChild(0).gameObject.SetActive(true);
	}
	

	public void OnPointerDown(PointerEventData ped){
		if(NewGameManager.gameOver){
			ForceGameOverStart();
		} else if(!doubleTapInitialized){
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

	public void ForceGameOverStart(){
		foreach(PlayerScript ps in playerScripts)
			ps.DisableControls();
		
		StartCoroutine(SlowDownTime(Time.time));
		
	}

	public void ForceGameOverEnd(){
		menuOpened = !menuOpened;
		Time.timeScale = (menuOpened) ? 0 : 1;
		IngameMenu.SetActive(true);
		IngameMenu.transform.GetChild(0).gameObject.SetActive(false);
		gameOverImage.enabled = true;
	}

	IEnumerator ResetDoubleTapInit(){
		yield return new WaitForSecondsRealtime(timeBetweenTaps+0.05f);
		doubleTapInitialized = false;
	}

	IEnumerator SlowDownTime(float startTime){
		while(Time.timeScale >= 0.1f){
			Time.timeScale -= Time.deltaTime / 1.5f;
			yield return null;
		}
		ForceGameOverEnd();
		yield break;
	}
	
}