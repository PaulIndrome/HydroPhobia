using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigParticle : MonoBehaviour {

	[SerializeField]
	private PlayerOneScript player01;
	private PlayerTwoScript player02;

	[SerializeField]
	private float fallingDelta = 1.0f;

	[SerializeField]
	private GameObject smallParticleReleaserPrefab;

	private ParticleSystem[] ps;
	private Rigidbody rigBod;
	public MeshRenderer meshRenderer;
	public Material bigParticleNormalMat, bigParticleDangerousMat;
	public AnimationCurve blinkingCurve;

	public bool hasCollided = false;
	public bool dangerousParticle = false;

	public void Awake(){
		player01 = NewGameManager.instance.playerManager.playerOne;
		player02 = NewGameManager.instance.playerManager.playerTwo;
	}

	public void Start(){
		rigBod = GetComponent<Rigidbody>();

		if(meshRenderer == null)
			meshRenderer = GetComponentInChildren<MeshRenderer>();
			
		ps = GetComponentsInChildren<ParticleSystem>();

		StartCoroutine(MoveAndCheckForOutOfScreen());
	}

	public void OnCollisionEnter(Collision col){
		hasCollided = true;
		GameObject collider = col.gameObject;
		//Debug.Log("Collision on " + collider.name);
		if(collider.GetComponent<PlayerOneScript>() != null && !SmallParticleReleaseHandler.smallParticlesActive){
			BigParticleFoodAndDanger();
		} else if (collider.GetComponent<PlayerOneScript>() != null && SmallParticleReleaseHandler.smallParticlesActive){
			BigParticleAverted();
		} else if (collider.GetComponent<PlayerTwoScript>() != null){
			BigParticleKillsSube();
		} else if (collider.layer == LayerMask.NameToLayer("CageBottom")){
			Debug.Log("Big Particle not caught");
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
		player01.BigParticleHit();
		SmallParticleReleaseHandler.SmallParticleReleaseOccured(smallParticleReleaser);

		NewGameManager.instance.bigParticleReleaseHandler.RemoveFromList(this);

		DisableDangerAndDestroy();
	}

	public void BigParticleAverted(){
		player01.BigParticleHit();
		NewGameManager.instance.bigParticleReleaseHandler.RemoveFromList(this);

		DisableDangerAndDestroy();
	}

	public void BigParticleKillsSube(){
		player02.BigParticleHit();
		ps[0].Play();
		meshRenderer.enabled = false;
		NewGameManager.instance.ForceGameOver();
	}

	public void DisableDangerAndDestroy(){
		dangerousParticle = false;
		Destroy(gameObject);
	}

	public void PlayParticleSystem(int index){
		ps[index].Play();
	}

	IEnumerator MoveAndCheckForOutOfScreen(){
		float startTime = Time.time;
		while(gameObject.activeSelf){
			rigBod.MovePosition(Vector3.Lerp(transform.position, new Vector3(transform.position.x + Mathf.Sin(Time.time - startTime)*0.5f, transform.position.y - fallingDelta, 0), Time.deltaTime));
			//transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + Mathf.Sin(Time.time - startTime)*0.5f, transform.position.y - fallingDelta, 0), Time.deltaTime);
			if(transform.position.y <= -6){
				/*
				//every big particle that is not caught reduces Bube's and Sube's speed
				NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(2, 0.8f);
				NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(1, 0.9f);
				*/
				//every big particle not caught hurts Sube more than Bube
				PlayerManager.HealthImpact(PlayerEnum.Sube, -15);
				PlayerManager.HealthImpact(PlayerEnum.Bube, -3);
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
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f,1.5f,1.5f), timeElapsed % 1);
			yield return null;
		}
		meshRenderer.material = bigParticleNormalMat;
		transform.localScale = Vector3.one;
		yield break;
	}





}
