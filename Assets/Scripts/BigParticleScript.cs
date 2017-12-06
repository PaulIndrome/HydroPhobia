using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticleScript : MonoBehaviour {

	public enum PlayerNum {One, Two};

	[SerializeField]
	private PlayerScript player01, player02;

	public GameObject particleExplosionPrefab;
	
	private ParticleSystem ps;

	private List<ParticleCollisionEvent> partCollisionEvents;


	public void Start(){
		ps = GetComponent<ParticleSystem>();
		partCollisionEvents = new List<ParticleCollisionEvent>();
	}

	public void OnParticleCollision(GameObject obj){
		
		//if(obj.CompareTag("Player01")){
		//	player01.BigParticleHitPlayerOne();
		//} else if (obj.CompareTag("Player02")){
		//	player01.BigParticleHitPlayerTwo();
		//}
		ParticlePhysicsExtensions.GetCollisionEvents(ps, obj, partCollisionEvents);
		foreach(ParticleCollisionEvent pce in partCollisionEvents){
			if(pce.colliderComponent.CompareTag("Player01")){
				//Debug.Log("Player01 hit");
				player01.BigParticleHitPlayerOne();
			} else if (pce.colliderComponent.CompareTag("Player02")){
				//Debug.Log("Player02 hit");
				player02.BigParticleHitPlayerTwo();
			}
		}

	}





}
