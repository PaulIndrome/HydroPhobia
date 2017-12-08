using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticle : MonoBehaviour {

	[SerializeField]
	private PlayerScript player01, player02;

	[SerializeField]
	private float fallingDelta = 1.0f;

	[SerializeField]
	private SmallParticleRelease mother;

	private SphereCollider particleCollider;

	public bool hasCollided = false;

	public void Awake(){
		particleCollider = GetComponent<SphereCollider>();

		player01 = GameObject.FindGameObjectWithTag("Player01").GetComponent<PlayerScript>();
		player02 = GameObject.FindGameObjectWithTag("Player02").GetComponent<PlayerScript>();

	}

	public void Start(){
		if(!player01 || !player02)
			Debug.LogError("Players not set up for BigParticlePrefab");

		
		StartCoroutine(MoveAndCheckForOutOfScreen());
	}

	public void setMother(SmallParticleRelease spr){
		mother = spr;
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
		} else {
			mother.SmallParticleDestroyed(gameObject);
			Destroy(gameObject);
		}
	}

	IEnumerator MoveAndCheckForOutOfScreen(){
		while(gameObject.activeSelf){
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - fallingDelta, 0), Time.deltaTime);
			if(transform.position.y <= -6){
				mother.SmallParticleDestroyed(gameObject);
				Destroy(gameObject);
			}
			yield return null;
		}
	}





}
