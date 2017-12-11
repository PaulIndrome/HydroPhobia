using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticleRelease : MonoBehaviour {

	[SerializeField]
	private int smallParticlesReleased, smallParticlesCaught;
	[SerializeField]
	private float smallParticleSprayDelay, initialForceMultiplier;

	public float minXRotation, maxXRotation;

	[SerializeField]
	private GameObject smallParticlePrefab;

	private List<GameObject> smallParticlesList = new List<GameObject>();

	private Vector3 smallParticleSpawnPoint;


	public void Start(){
		if(initialForceMultiplier == 0)
			initialForceMultiplier = 1f;
		SmallParticleReleaseHandler.smallParticlesActive = true;
	}

	public void RotateSmallParticleReleaserTowards(Vector3 playerPosition){
		transform.LookAt(playerPosition);
		transform.Translate(0,0,-1);
		minXRotation = transform.eulerAngles.x - 25;
		maxXRotation = minXRotation + 50;
		StartSmallParticleRelease(SmallParticleReleaseHandler.smallParticlesToReleaseNext);
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
			SmallParticleReleaseHandler.ComputeSmallParticlesToReleaseNext(smallParticlesReleased, smallParticlesCaught);
			SmallParticleReleaseHandler.SmallParticleReleaseFinished(gameObject);
			Destroy(gameObject);
			return true;
		} else {
			return false;
		}
	}

	IEnumerator SpraySmallParticles(){
		for(int i = 0; i<smallParticlesReleased;i++){
			SpraySmallParticle();
			yield return new WaitForSeconds(smallParticleSprayDelay);
		}
		yield return null;
	}

	public void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, transform.forward);
	}

	public void SpraySmallParticle(){
		transform.eulerAngles = new Vector3(Mathf.Lerp(minXRotation,maxXRotation, Random.Range(0f,1f)), transform.eulerAngles.y, transform.eulerAngles.z);
		GameObject smallParticle = Instantiate(smallParticlePrefab, transform.position, transform.rotation);
		smallParticle.GetComponent<SmallParticle>().setMother(this);
		Vector3 sprayDirection = new Vector3(0, Random.Range(-1,1), Random.Range(-1,-2) * initialForceMultiplier).normalized;
		smallParticle.GetComponent<Rigidbody>().AddForce(-transform.forward * initialForceMultiplier, ForceMode.Impulse);
		smallParticlesList.Add(smallParticle);
	}

}
