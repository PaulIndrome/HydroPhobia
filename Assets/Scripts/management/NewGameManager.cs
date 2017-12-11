using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour{

	public static NewGameManager instance {get; private set;}

	public GamePauseHandler gamePauseHandler;
	public BigParticleReleaseHandler bigParticleReleaseHandler;
	public SmallParticleReleaseHandler smallParticleReleaseHandler;
	
	public static bool gameOver = false;

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
	}

	public void ForceGameOver(){
		gamePauseHandler.ForceGameOverStart();
	}

	public void toggleDangerousBigParticles(){
		bigParticleReleaseHandler.dangerousBigParticlesActive = !bigParticleReleaseHandler.dangerousBigParticlesActive;
	}
	


}
