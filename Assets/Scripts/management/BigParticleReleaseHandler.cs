using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticleReleaseHandler : MonoBehaviour {


	public float delayBetweenSpawns, fallingDelta;

	public bool dangerousBigParticlesActive = false;

	public BigParticleSpawn bigParticleSpawn;

	void Start(){
		if(delayBetweenSpawns == 0)
			delayBetweenSpawns = 2.0f;
		if(fallingDelta == 0)
			fallingDelta = 1.0f;

		StartCoroutine(spawnBigParticles());
	}

	IEnumerator spawnBigParticles(){
		while(!NewGameManager.gameOver){
			yield return new WaitForSeconds(delayBetweenSpawns);
			bigParticleSpawn.SpawnBigParticle(dangerousBigParticlesActive, fallingDelta);
		}
		yield break;
	}
}
