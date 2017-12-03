using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour {

	public PlayerScript player;
	public GameObject particleExplosionPrefab;
	private ParticleSystem ps;
	private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
	private int particleTriggers = 0;
	private Vector3 particleExile = Vector3.negativeInfinity;


	public void onEnabled(){
		ps = GetComponent<ParticleSystem>();
	}

	public void OnParticleTrigger(){
		if(ps == null)
			ps = GetComponent<ParticleSystem>();

		//int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		ParticlePhysicsExtensions.GetTriggerParticles(ps, ParticleSystemTriggerEventType.Enter, enter);

		//for(int i = 0; i < numEnter; i++){
		//	enter[i].
		//	Debug.Log("ParticleTrigger " + particleTriggers++ + " at " + enter[i].position);
		//	GameObject pEx = Instantiate(particleExplosion, enter[i].position, Quaternion.identity);
		//	pEx.GetComponent<particleExplosionScript>().setTriggerObject(player.gameObject);
		//}

		foreach(ParticleSystem.Particle p in enter){
				Debug.Log("ParticleTrigger " + particleTriggers++ + " at " + p.position + " with StartLifeTime of " + p.startLifetime + " ID " + p.GetHashCode());
				GameObject pEx = Instantiate(particleExplosionPrefab, p.position, Quaternion.identity);
				pEx.GetComponent<particleExplosionScript>().setTriggerObject(player.gameObject);
		}

		

		
		
	}



}
