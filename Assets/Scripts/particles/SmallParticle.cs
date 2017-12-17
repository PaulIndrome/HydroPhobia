using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticle : MonoBehaviour {

	[SerializeField] private PlayerOneScript player01;
	[SerializeField] private PlayerTwoScript player02;

	[SerializeField] private SmallParticleRelease mother;

	private Rigidbody rigBod;

	public bool hasCollided = false;

	public void Awake(){
		rigBod = GetComponent<Rigidbody>();
		rigBod.mass = Random.Range(0.4f, 0.9f);

		player01 = NewGameManager.instance.playerManager.playerOne;
		player02 = NewGameManager.instance.playerManager.playerTwo;

	}

	public void setMother(SmallParticleRelease spr){
		mother = spr;
		StartCoroutine(CheckForOutOfScreen());
	}

	public void OnCollisionEnter(Collision col){
		hasCollided = true;
		GameObject collider = col.gameObject;
		//Debug.Log("Collision on " + collider.name);
		if(collider.GetComponent<PlayerOneScript>() != null){
			mother.SmallParticleDestroyed(gameObject);
			player01.SmallParticleHit();
			Destroy(gameObject);
		} else if (collider.GetComponent<PlayerTwoScript>() != null){
			mother.SmallParticleCaught(gameObject);
			player02.SmallParticleHit();
			Destroy(gameObject);
		} else if(collider.GetComponent<CageBottomCollisionHandler>() == null && collider.transform.parent.GetComponent<CageHandler>() != null){
			rigBod.velocity = Vector3.Reflect(rigBod.velocity, col.contacts[0].normal)*1.2f;
		} else {
			//every small particle that is not caught reduces Bube's and Sube's speed
			NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(1, 0.8f);
			NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(2, 0.9f);
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
