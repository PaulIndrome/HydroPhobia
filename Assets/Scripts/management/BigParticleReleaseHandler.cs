using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigParticleReleaseHandler : MonoBehaviour {

	public float DelayBetweenSpawns {
		get { return delayBetweenSpawns; }
		set {
			delayBetweenSpawns = Mathf.Clamp(value, 1.5f, 2.5f);
		}
	}

	public float delayBetweenSpawns, xMin, xMax, spawnPosX; 
	public float fallingDelta;
	public float bigParticleScore;

	private float spawnPosY;

	public bool dangerousBigParticlesActive = false;

	public GameObject bigParticlePrefab;

	public Text bigParticleScoreText;

	public List<BigParticle> bigParticleList = new List<BigParticle>();

	void Start(){
		if(!bigParticlePrefab)
			Debug.Log("No prefab for the big particle set");
		if(DelayBetweenSpawns == 0)
			DelayBetweenSpawns = 2.0f;
		if(fallingDelta == 0)
			fallingDelta = 1.0f;
		if(xMin == 0 || xMax == 0){
			xMin = -1;
			xMax = 1;
		}

		spawnPosY = transform.position.y;

		bigParticleScoreText.text = "0";

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
		dangerousBigParticlesActive = dangerActive;
		fallingDelta *= (dangerActive) ? 2f : 0.5f;
		UpdateBigParticles();
	}

	public void SetFallingDelta(float multiplier){
		fallingDelta *= multiplier;
	}

	public void UpdateBigParticles(){
		foreach(BigParticle bp in bigParticleList){
			if(bp != null){
				bp.SetFallingDelta(fallingDelta);
				bp.ToggleDangerousParticle(dangerousBigParticlesActive);
				if(dangerousBigParticlesActive) bp.PlayParticleSystem(1);
				else bp.PlayParticleSystem(2);
			} else {
				continue;
			}
		}
	}

	public void UpdateBigParticleScore(int minusPlus){
		bigParticleScore += minusPlus;
		bigParticleScoreText.text = "" + Mathf.Clamp(bigParticleScore, 0, 99999);
	}

	public void RemoveFromList(BigParticle bp){
		if(bigParticleList.Contains(bp))
			bigParticleList.Remove(bp);
	}

	IEnumerator spawnBigParticles(){
		while(!NewGameManager.gameOver){
			yield return new WaitForSeconds(DelayBetweenSpawns);
			SpawnBigParticle();
		}
		yield break;
	}
}
