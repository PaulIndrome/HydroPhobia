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

	private ParticleSystem[] ps;
	public MeshRenderer meshRenderer;
	public Material bigParticleNormalMat, bigParticleDangerousMat;
	public AnimationCurve blinkingCurve;

	public bool hasCollided = false;
	public bool dangerousParticle = false;

	public void Awake(){
		player01 = GameObject.FindGameObjectWithTag("Player01").GetComponent<PlayerScript>();
		player02 = GameObject.FindGameObjectWithTag("Player02").GetComponent<PlayerScript>();
	}

	public void Start(){
		if(!player01 || !player02)
			Debug.LogError("Players not set up for BigParticlePrefab");
		if(meshRenderer == null)
			meshRenderer = GetComponentInChildren<MeshRenderer>();
			
		ps = GetComponentsInChildren<ParticleSystem>();

		StartCoroutine(MoveAndCheckForOutOfScreen());
	}

	public void OnCollisionEnter(Collision col){
		hasCollided = true;
		GameObject collider = col.gameObject;
		//Debug.Log("Collision on " + collider.name);
		if(collider.CompareTag("Player01") && !SmallParticleReleaseHandler.smallParticlesActive){
			BigParticleFoodAndDanger();
		} else if (collider.CompareTag("Player01") && SmallParticleReleaseHandler.smallParticlesActive){
			BigParticleAverted();
		} else if (collider.CompareTag("Player02")){
			BigParticleKillsSube();
		} 
	}

	public void ToggleDangerousParticle(bool activeDanger){
		if(activeDanger) {
			dangerousParticle = true;
			StartCoroutine(FlashDanger());
		} else {
			dangerousParticle = false;
		}
	}

	public void SetFallingDelta(float fd){
		fallingDelta = fd;

		if(meshRenderer == null)
			meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	public void BigParticleFoodAndDanger(){
		GameObject smallParticleReleaser = Instantiate(smallParticleReleaserPrefab, transform.position, Quaternion.identity);
		smallParticleReleaser.GetComponent<SmallParticleRelease>().RotateSmallParticleReleaserTowards(player01.transform.position);
		player01.BigParticleHitPlayerOne();
		SmallParticleReleaseHandler.SmallParticleReleaseOccured(smallParticleReleaser);

		NewGameManager.instance.bigParticleReleaseHandler.RemoveFromList(this);

		dangerousParticle = false;

		Destroy(gameObject);
	}

	public void BigParticleAverted(){
		player01.BigParticleHitPlayerOne();
		NewGameManager.instance.bigParticleReleaseHandler.RemoveFromList(this);
		dangerousParticle = false;
		Destroy(gameObject);
	}

	public void BigParticleKillsSube(){
		player02.BigParticleHitPlayerTwo();
		ps[0].Play();
		meshRenderer.enabled = false;
		NewGameManager.instance.ForceGameOver();
	}

	IEnumerator MoveAndCheckForOutOfScreen(){
		while(gameObject.activeSelf){
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + Random.Range(-1f,1f), transform.position.y - fallingDelta, 0), Time.deltaTime);
			if(transform.position.y <= -6){
				NewGameManager.instance.bigParticleReleaseHandler.RemoveFromList(this);
				Destroy(gameObject);
			}
			yield return null;
		}
		yield break;
	}

	IEnumerator FlashDanger(){
		float timeElapsed = 0;
		//transform.localScale.Set(1.5f,1.5f,1.5f);
		while(dangerousParticle){
			timeElapsed += Time.deltaTime;
			meshRenderer.material.Lerp(bigParticleNormalMat, bigParticleDangerousMat, blinkingCurve.Evaluate(timeElapsed % 1));
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f,1.5f,1.5f), timeElapsed);
			yield return null;
		}
		meshRenderer.material = bigParticleNormalMat;
		ps[2].Play();
		transform.localScale = Vector3.one;
		yield break;
	}





}
