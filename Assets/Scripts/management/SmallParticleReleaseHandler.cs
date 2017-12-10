using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticleReleaseHandler : MonoBehaviour {

	public static SmallParticleReleaseHandler instance {get; private set;}

	[SerializeField]
	private static float bigParticleFallSpeed, smallParticleFallSpeed, bigParticleDelay;

	[SerializeField]
	public static int smallParticlesToReleaseFirst, smallParticlesToReleaseNext, smallParticlesReleasedLast;

	public static bool smallParticlesActive = false;

	private static List<GameObject> smallParticleReleaserList = new List<GameObject>();

	public void Awake(){
		if(instance == null){
			instance = this;
		} else if (instance != null){
			Destroy(gameObject);
		}

		Reset();
	}

	public void Reset(){
		smallParticlesActive = false;

		smallParticleReleaserList.Clear();

		smallParticlesToReleaseFirst = 5;

		smallParticlesToReleaseNext = smallParticlesToReleaseFirst;
	}

	public static void SmallParticleReleaseOccured(GameObject occurrence){
		smallParticleReleaserList.Add(occurrence);
	}

	public static void SmallParticleReleaseFinished(GameObject finished){
		smallParticleReleaserList.Remove(finished);
		smallParticlesActive = false;
		NewGameManager.instance.bigParticleReleaseHandler.bigParticleSpawn.ToggleDangerousParticles(false);
	}

	public static void ComputeSmallParticlesToReleaseNext(int smallParticlesReleased, int smallParticlesCaught){
		if(smallParticlesReleased == 1 && smallParticlesCaught == 0){
			Debug.Log("GameOver, big guy has starved");
			Time.timeScale = 0;
			NewGameManager.gameOver = true;
		} else if (smallParticlesReleased >= 2 && smallParticlesCaught == 0){
			smallParticlesToReleaseNext /= 2;
		} else if (smallParticlesCaught >= smallParticlesReleased){
			smallParticlesToReleaseNext += smallParticlesCaught;
		} else {
			smallParticlesToReleaseNext = smallParticlesCaught;
		}
		//Debug.Log("Small Particles To Release Next: " + smallParticlesToReleaseNext);
	}
}
