using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour {

	public PlayerScript player;
	public GameObject particleExplosionPrefab;
	private ParticleSystem ps;
	private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
	private List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
	private List<ParticleCollisionEvent> partCollisionEvents = new List<ParticleCollisionEvent>();
	private int particleTriggers = 0;
	private Vector3 particleExile = Vector3.negativeInfinity;


	public void onEnabled(){
		ps = GetComponent<ParticleSystem>();
	}

	public void OnParticleTrigger(){
		if(ps == null)
			ps = GetComponent<ParticleSystem>();

		//int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

		//for(int i = 0; i < numEnter; i++){
		//	Debug.Log("ParticleTrigger " + particleTriggers++ + " at " + enter[i].position);
		//	GameObject pEx = Instantiate(particleExplosionPrefab, enter[i].position, Quaternion.identity);
		//	pEx.GetComponent<particleExplosionScript>().setTriggerObject(player.gameObject);
		//}

		ParticlePhysicsExtensions.GetTriggerParticles(ps, ParticleSystemTriggerEventType.Enter, enter);

		foreach(ParticleSystem.Particle p in enter){
				
				Debug.Log("ParticleTrigger " + particleTriggers++ + " at " + p.position + " with StartLifeTime of " + p.startLifetime + " ID " + p.GetHashCode());
				GameObject pEx = Instantiate(particleExplosionPrefab, p.position, Quaternion.identity);
				pEx.GetComponent<particleExplosionScript>().setTriggerObject(player.gameObject);
		}
	}

	//public void OnParticleCollision(GameObject other){
	//	Debug.Log("ParticleCollision Detected");
	//	if(!ps)
	//		ps = GetComponent<ParticleSystem>();
//
	//	if(other.CompareTag("Player")){
	//		ParticlePhysicsExtensions.GetCollisionEvents(ps, other, partCollisionEvents);
	//		foreach(ParticleCollisionEvent pce in partCollisionEvents){
	//			GameObject pEx = Instantiate(particleExplosionPrefab, pce.intersection, Quaternion.identity);
	//			pEx.GetComponent<particleExplosionScript>().setTriggerObject(player.gameObject);
	//			partCollisionEvents.Remove(pce);
	//		}
	//	}
	//}



}
