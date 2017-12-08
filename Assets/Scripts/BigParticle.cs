using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticle : MonoBehaviour {

	[SerializeField]
	private PlayerScript player01, player02;

	[SerializeField]
	private float fallingDelta = 1.0f;

	[SerializeField]
	private GameObject smallParticleReleaserPrefab;

	private ParticleSystem ps;
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

		ps = GetComponent<ParticleSystem>();
		
		StartCoroutine(MoveAndCheckForOutOfScreen());
	}

	public void OnCollisionEnter(Collision col){
		hasCollided = true;
		GameObject collider = col.gameObject;
		//Debug.Log("Collision on " + collider.name);
		if(collider.CompareTag("Player01") && !NewGameManager.smallParticlesActive){
			//Debug.Log("Small Particles are flying: " + NewGameManager.smallParticlesActive);
			GameObject smallParticleReleaser = Instantiate(smallParticleReleaserPrefab, transform.position, Quaternion.identity);
			smallParticleReleaser.GetComponent<SmallParticleRelease>().RotateSmallParticleReleaserTowards(player01.transform.position);
			player01.BigParticleHitPlayerOne();
			NewGameManager.SmallParticleReleaseOccured(smallParticleReleaser);
			Destroy(gameObject);
		} else if (collider.CompareTag("Player02")){
			player02.BigParticleHitPlayerTwo();
			Destroy(gameObject);
		} else {
			if(!ps.isPlaying){
				transform.LookAt(new Vector3(0,transform.position.y, 0));
				ps.Play();
				particleCollider.isTrigger = true;
			}
		}
	}

	IEnumerator MoveAndCheckForOutOfScreen(){
		while(gameObject.activeSelf){
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - fallingDelta, 0), Time.deltaTime);
			if(transform.position.y <= -6){
				Destroy(gameObject);
			}
			yield return null;
		}
		yield break;
	}





}
