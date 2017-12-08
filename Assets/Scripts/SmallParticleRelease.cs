using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticleRelease : MonoBehaviour {

	[SerializeField]
	private int smallParticlesReleased, smallParticlesCaught;
	[SerializeField]
	private float smallParticleSprayDelay;

	[SerializeField]
	private GameObject smallParticlePrefab;

	private List<GameObject> smallParticlesList = new List<GameObject>();

	private Vector3 smallParticleSpawnPoint;

	public void Start(){
		NewGameManager.smallParticlesActive = true;
	}

	public void RotateSmallParticleReleaserTowards(Vector3 playerPosition){
		transform.LookAt(playerPosition);
		transform.Translate(0,0,-1);
		StartSmallParticleRelease(NewGameManager.smallParticlesToReleaseNext);
	}

	public void StartSmallParticleRelease(int amountParticlesToRelease){
		smallParticlesReleased = amountParticlesToRelease;
		
		StartCoroutine(SpraySmallParticles());
	}

	public void SmallParticleCaught(GameObject smallParticle){
		smallParticlesList.Remove(smallParticle);
		smallParticlesCaught++;
		IsFinished();
	}

	public void SmallParticleDestroyed(GameObject smallParticle){
		smallParticlesList.Remove(smallParticle);
		IsFinished();
	}

	public bool IsFinished(){
		if(smallParticlesList.Count == 0){
			NewGameManager.ComputeSmallParticlesToReleaseNext(smallParticlesReleased, smallParticlesCaught);
			NewGameManager.SmallParticleReleaseFinished(gameObject);
			Destroy(gameObject);
			return true;
		} else {
			return false;
		}
	}

	IEnumerator SpraySmallParticles(){
		for(int i = 0; i<smallParticlesReleased;i++){
			GameObject smallParticle = Instantiate(smallParticlePrefab, transform.position, Quaternion.identity);
			smallParticle.transform.SetParent(gameObject.transform);
			smallParticle.GetComponent<SmallParticle>().setMother(this);
			smallParticle.GetComponent<Rigidbody>().AddForce(Vector3.back);
			smallParticlesList.Add(smallParticle);
			yield return new WaitForSeconds(smallParticleSprayDelay);
		}
		yield return null;
	}

}
