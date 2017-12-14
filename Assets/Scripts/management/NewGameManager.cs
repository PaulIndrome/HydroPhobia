using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour{

	public static NewGameManager instance {get; private set;}

	private Camera mainCam;

	public GamePauseHandler gamePauseHandler;
	public BigParticleReleaseHandler bigParticleReleaseHandler;
	public SmallParticleReleaseHandler smallParticleReleaseHandler;
	public CageHandler cageHandler;
	public PlayerManager playerManager;
	
	public static bool gameOver = false;

	public static float worldXMin, worldXMax;

	void Awake(){
		if(instance == null){
			instance = this;
		} else if (instance != null){
			Destroy(gameObject);
		}

		gameOver = false;
		bigParticleReleaseHandler = GetComponent<BigParticleReleaseHandler>();
		smallParticleReleaseHandler = GetComponent<SmallParticleReleaseHandler>();
		gamePauseHandler = GetComponent<GamePauseHandler>();
		playerManager = GetComponent<PlayerManager>();
		
		if(cageHandler == null)
			cageHandler = GameObject.FindGameObjectWithTag("PlayerCageHandler").GetComponent<CageHandler>();

		mainCam = Camera.main;

		FindXMinXMax();

		SetBigParticleXRange();

	}

	public void ForceGameOver(){
		gamePauseHandler.ForceGameOverStart();
	}

	public void FindXMinXMax(){
		worldXMax = mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0,10)).x;
		worldXMin = mainCam.ScreenToWorldPoint(new Vector3(0,0,10)).x;
	}

	public void SetBigParticleXRange(){
		bigParticleReleaseHandler.xMin = worldXMin + 1;
		bigParticleReleaseHandler.xMax = worldXMax - 1;
	}

}
