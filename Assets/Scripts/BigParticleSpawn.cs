using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticleSpawn : MonoBehaviour {

	[SerializeField]
	private int delayBetweenSpawns = 0;

	[SerializeField]
	private float xMin, xMax;
	private float yPos;

	public GameObject bigParticlePrefab;
	public List<BigParticle> bigParticles = new List<BigParticle>();
	
	void Start () {
		if(!bigParticlePrefab)
			Debug.Log("No prefab for the big particle set");
		if(xMin == 0 || xMax == 0){
			xMin = -1;
			xMax = 1;
		}
		
		yPos = transform.position.y;
	}

	public void SpawnBigParticle(bool dangerActive, float fallingDelta){
		float xPos = Random.Range(xMin,xMax);
		BigParticle bp = Instantiate(bigParticlePrefab, new Vector3(transform.position.x + xPos, yPos, 0), Quaternion.identity).GetComponent<BigParticle>();;
		bigParticles.Add(bp);
		bp.SetFallingDelta(fallingDelta);
		if(dangerActive)
			bp.ToggleDangerousParticle(dangerActive);
	}

	public void ToggleDangerousParticles(bool dangerActive){
		foreach(BigParticle bp in bigParticles){
			bp.ToggleDangerousParticle(dangerActive);
		}
		NewGameManager.instance.bigParticleReleaseHandler.dangerousBigParticlesActive = true;
	}

	public void RemoveFromList(BigParticle bp){
		bigParticles.Remove(bp);
	}
}
