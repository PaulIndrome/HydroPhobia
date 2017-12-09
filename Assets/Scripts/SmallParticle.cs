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
	private Rigidbody rigBod;

	public bool hasCollided = false;

	public void Awake(){
		particleCollider = GetComponent<SphereCollider>();
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
		StartCoroutine(MoveAndCheckForOutOfScreen());
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
			//transform.Translate(Vector3.down * Time.deltaTime * fallingDelta, Space.World);
			if(transform.position.y <= -6){
				mother.SmallParticleDestroyed(gameObject);
				Destroy(gameObject);
			}
			yield return null;
		}
		yield break;
	}

	public void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, transform.forward);
	}





}
