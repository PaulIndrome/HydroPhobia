using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseHandler : MonoBehaviour, IPointerDownHandler {

	private bool gameOver = false;

	[SerializeField]
	private bool menuOpened = false;
	private bool doubleTapInitialized = false;
	[SerializeField]
	private int tapsForAction = 3;
	[SerializeField] 
	private int counter = 0;
	private float firstTapTime = 0f, lastTapTime = 0f;
	[SerializeField]
	private float timeBetweenTaps = 0.2f;

	private int firstTapID;
	
	[SerializeField]
	private GameObject ingameMenu;
	[SerializeField]
	private Button continueButton, tutorialButton;

	private Image gameOverImage;


	public void Awake(){
		gameOverImage = ingameMenu.GetComponent<Image>();
		gameOverImage.enabled = false;
		continueButton.gameObject.SetActive(true);
		tutorialButton.gameObject.SetActive(true);

		gameOver = false;
	}
	

	public void OnPointerDown(PointerEventData ped){
		firstTapID = ped.pointerId;
		Vector3 pointerInWorld = ped.position;
		pointerInWorld.z = 10;
		pointerInWorld = Camera.main.ScreenToWorldPoint(pointerInWorld);
		if(NewGameManager.gameOver && !gameOver){
			ForceGameOverStart();
		} else if(gameOver){
			return;
		} else if(!doubleTapInitialized){
			counter++;
			doubleTapInitialized = true;
			lastTapTime = firstTapTime = Time.unscaledTime;
			StartCoroutine(ResetDoubleTapInit());
		} else if (Time.unscaledTime - lastTapTime < timeBetweenTaps && ped.pointerId == firstTapID){
			counter++;
			lastTapTime = Time.unscaledTime;
			if (counter >= tapsForAction) {
				PauseUnpauseGame();
			}
		} 
	}

	public void PauseUnpauseGame(){
		counter = 0;
		doubleTapInitialized = false;
		menuOpened = !menuOpened;
		Time.timeScale = (menuOpened) ? 0 : 1;
		ingameMenu.SetActive(menuOpened);
	}

	public void PauseUnpauseGame(bool pause){
		counter = 0;
		doubleTapInitialized = false;
		menuOpened = pause;
		Time.timeScale = (pause) ? 0 : 1;
		ingameMenu.SetActive(pause);
		NewGameManager.instance.playerManager.TogglePlayerControl(!pause);
	}

	public void CloseIngameMenuForTutorial(){
		counter = 0;
		doubleTapInitialized = false;
		menuOpened = false;
		Time.timeScale = 0f;

		NewGameManager.instance.playerManager.DisablePlayerControl();

		ingameMenu.SetActive(false);
	}

	public void ForceGameOverStart(){
		gameOver = true;
		NewGameManager.instance.playerManager.DisablePlayerControl();
		StartCoroutine(SlowDownTime(3f));
	}

	public void ForceGameOverEnd(){
		NewGameManager.instance.playerManager.DisablePlayerControl();
		menuOpened = true;
		Time.timeScale = 0;
		ingameMenu.SetActive(true);
		continueButton.gameObject.SetActive(false);
		tutorialButton.gameObject.SetActive(false);
		gameOverImage.enabled = true;
	}

	IEnumerator ResetDoubleTapInit(){
		yield return new WaitUntil(() => Time.unscaledTime - lastTapTime > timeBetweenTaps);
		doubleTapInitialized = false;
		counter = 0;
		firstTapID = -1;
		yield return null;
	}

	IEnumerator SlowDownTime(float timeToStop){
		for(float t = 0f; t < timeToStop; t = t + Time.unscaledDeltaTime){
			Time.timeScale = Mathf.Lerp(1f, 0f, t / timeToStop);
			yield return null;
		}
		ForceGameOverEnd();
		yield break;
	}
	
}
