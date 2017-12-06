using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticle : MonoBehaviour {

	[SerializeField]
	private PlayerScript player01, player02;

	public GameObject particleExplosionPrefab;

	private ParticleSystem ps;
	private SphereCollider particleCollider;

	public void Awake(){
		particleCollider = GetComponent<SphereCollider>();

		player01 = GameObject.FindGameObjectWithTag("Player01").GetComponent<PlayerScript>();
		player02 = GameObject.FindGameObjectWithTag("Player02").GetComponent<PlayerScript>();
	}

	public void Start(){
		if(!player01 || !player02)
			Debug.LogError("Players not set up for BigParticlePrefab");

		ps = GetComponent<ParticleSystem>();

	}

	public void OnCollisionEnter(Collision col){
		GameObject collider = col.collider.gameObject;
		Debug.Log("Collision on " + collider.name);
		if(collider.CompareTag("Player01")){
			player01.BigParticleHitPlayerOne();
			Destroy(gameObject);
		} else if (collider.CompareTag("Player02")){
			player02.BigParticleHitPlayerTwo();
			Destroy(gameObject);
		} else {
			if(!ps.isPlaying){
				transform.LookAt(new Vector3(0,transform.position.y, 0));
				ps.Play();
				StartCoroutine(checkForOutOfScreen());
				particleCollider.isTrigger = true;
			}
		}
	}

	IEnumerator checkForOutOfScreen(){
		while(gameObject.activeSelf){
			if(transform.position.y <= -4){
				Destroy(gameObject);
			}
			yield return new WaitForSeconds(3.0f);
		}
	}





}
