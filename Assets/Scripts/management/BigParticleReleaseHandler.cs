using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticleReleaseHandler : MonoBehaviour {


	public float delayBetweenSpawns, fallingDelta, xMin, xMax, spawnPosX;
	private float spawnPosY;

	public bool dangerousBigParticlesActive = false;

	public GameObject bigParticlePrefab;
	public List<BigParticle> bigParticleList = new List<BigParticle>();

	void Start(){
		if(!bigParticlePrefab)
			Debug.Log("No prefab for the big particle set");
		if(delayBetweenSpawns == 0)
			delayBetweenSpawns = 2.0f;
		if(fallingDelta == 0)
			fallingDelta = 1.0f;
		if(xMin == 0 || xMax == 0){
			xMin = -1;
			xMax = 1;
		}

		spawnPosY = transform.position.y;

		StartCoroutine(spawnBigParticles());
	}

	public void SpawnBigParticle(){
		spawnPosX = Random.Range(xMin,xMax);
		BigParticle bp = Instantiate(bigParticlePrefab, new Vector3(transform.position.x + spawnPosX, spawnPosY, 0), Quaternion.identity).GetComponent<BigParticle>();;
		bigParticleList.Add(bp);
		bp.SetFallingDelta(fallingDelta);
		bp.ToggleDangerousParticle(dangerousBigParticlesActive);
	}

	public void ToggleDangerousParticles(bool dangerActive){
		foreach(BigParticle bp in bigParticleList){
			if(bp != null){
				bp.ToggleDangerousParticle(dangerActive);
				bp.SetFallingDelta(dangerActive?fallingDelta+1:fallingDelta);
			} else {
				continue;
			}
		}
		dangerousBigParticlesActive = dangerActive;
	}

	public void RemoveFromList(BigParticle bp){
		bigParticleList.Remove(bp);
	}

	IEnumerator spawnBigParticles(){
		while(!NewGameManager.gameOver){
			yield return new WaitForSeconds(delayBetweenSpawns);
			SpawnBigParticle();
		}
		yield break;
	}
}
