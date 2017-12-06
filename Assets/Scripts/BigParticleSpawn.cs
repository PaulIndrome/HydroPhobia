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
	
	void Start () {
		if(!bigParticlePrefab)
			Debug.Log("No prefab for the big particle set");
		
		yPos = transform.position.y;
//		Instantiate(bigParticlePrefab, new Vector3(transform.position.x, transform.position.y - 2, 0), Quaternion.identity);
		StartCoroutine(spawnBigParticles());
	}
	
	IEnumerator spawnBigParticles(){
		while(true){
			spawnBigParticle();
			yield return new WaitForSeconds(delayBetweenSpawns);
		}
	}

	public void spawnBigParticle(){
		float xPos = Random.Range(xMin,xMax);
		Instantiate(bigParticlePrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
		
	}
}
