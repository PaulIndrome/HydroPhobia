using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticle : MonoBehaviour {

	[SerializeField]
	private PlayerScript player01, player02;

	[SerializeField]
	private SmallParticleRelease mother;

	private Rigidbody rigBod;

	public bool hasCollided = false;

	public void Awake(){
		rigBod = GetComponent<Rigidbody>();
		rigBod.mass = Random.Range(0.4f, 0.9f);

		player01 = GameObject.FindGameObjectWithTag("Player01").GetComponent<PlayerScript>();
		player02 = GameObject.FindGameObjectWithTag("Player02").GetComponent<PlayerScript>();

	}

	public void Start(){
		if(!player01 || !player02)
			Debug.LogError("Players not set up for BigParticlePrefab");
	}

	public void setMother(SmallParticleRelease spr){
		mother = spr;
		StartCoroutine(CheckForOutOfScreen());
	}

	public void OnCollisionEnter(Collision col){
		hasCollided = true;
		GameObject collider = col.gameObject;
		//Debug.Log("Collision on " + collider.name);
		if(collider.CompareTag("Player01")){
			mother.SmallParticleDestroyed(gameObject);
			player01.SmallParticleHitPlayerOne();
			Destroy(gameObject);
		} else if (collider.CompareTag("Player02")){
			mother.SmallParticleCaught(gameObject);
			player02.SmallParticleHitPlayerTwo();
			Destroy(gameObject);
		} else if(collider.CompareTag("PlayerCage")){
			rigBod.velocity = Vector3.Reflect(rigBod.velocity, col.contacts[0].normal)*1.2f;
		} else {
			mother.SmallParticleDestroyed(gameObject);
			Destroy(gameObject);
		}
	}

	IEnumerator CheckForOutOfScreen(){
		while(gameObject.activeSelf){
			if(transform.position.y <= -6){
				mother.SmallParticleDestroyed(gameObject);
				Destroy(gameObject);
				yield break;
			} else if (transform.position.x > NewGameManager.worldXMax || transform.position.x < NewGameManager.worldXMin){
				mother.SmallParticleDestroyed(gameObject);
				Destroy(gameObject);
				yield break;
			}
			yield return new WaitForSecondsRealtime(0.3f);
		}
		yield break;
	}

	public void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, transform.forward);
	}






}
