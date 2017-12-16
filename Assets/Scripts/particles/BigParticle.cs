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
	IEnumerator MoveAndCheckForOutOfScreen(){
		float startTime = Time.time;
		while(gameObject.activeSelf){
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + Mathf.Sin(Time.time - startTime)*0.5f, transform.position.y - fallingDelta, 0), Time.deltaTime);
			if(transform.position.y <= -6){
				//every big particle that is not caught reduces Bube's and Sube's speed
				NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(2, 0.9f);
				NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(1, 0.9f);
				
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
