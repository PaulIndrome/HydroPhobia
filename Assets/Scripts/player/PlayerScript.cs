using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	public Transform child3dObject;

	[SerializeField]
	private float hitDelay, lerpToPointerSpeed = 1.0f;

	[SerializeField]
	private bool particleHitThisFrame, isDragging = false;

	private bool playerControlActive = true;

	private Rigidbody rigBod;
	private Camera mainCam;
	private Vector3 pointerInWorld;
	private Vector3 playerTwoRestingPos;
	private Vector3 velocity = Vector3.zero;

	


	public void Start(){
		child3dObject = transform.GetChild(0);
		rigBod = GetComponent<Rigidbody>();
		mainCam = Camera.main;
		playerTwoRestingPos = new Vector3(0,-5,0);
		StartCoroutine(LerpToRestingPos());
	}

	public virtual void OnPointerDown(PointerEventData ped){
		if(playerControlActive)
		{
			pointerInWorld = transform.position;
			rigBod.velocity = Vector3.zero;
			isDragging = true;
			StopCoroutine(LerpToRestingPos());
			StartCoroutine(LerpToPointer());
		}
	}

	public virtual void OnDrag(PointerEventData ped){
		if(playerControlActive)
		{
			pointerInWorld = ped.position;
			pointerInWorld.z = 10;
			pointerInWorld = mainCam.ScreenToWorldPoint(pointerInWorld);
			//transform.position = pointerInWorld;
			child3dObject.Rotate(new Vector3(ped.delta.x * (1 + Random.value), ped.delta.x * (1 + Random.value), Random.value*10));
		}
	}

	public virtual void OnPointerUp(PointerEventData ped){
		if(playerControlActive)
		{
			//Debug.Log("Up at " + ped.position);
			isDragging = false;
			StopCoroutine(LerpToPointer());
			StartCoroutine(LerpToRestingPos());
		}
	}

	public void BigParticleHitPlayerOne(){
		//Debug.Log("Big hit Player01");
		
		if(!RecentlyHitByParticle()){
			//Debug.Log("Big hit Player01 anew");
		}
	}

	public void BigParticleHitPlayerTwo(){
		return;
	}

	public void SmallParticleHitPlayerOne(){
		//Debug.Log("Small hit Player01");

		if(!RecentlyHitByParticle()){
			//Debug.Log("Small hit Player01 anew");
		}
	}

	public void SmallParticleHitPlayerTwo(){
		//Debug.Log("Small hit Player02");
		
		if(!RecentlyHitByParticle()){
			//Debug.Log("Small hit Player02 anew");
		}
	}

	public bool RecentlyHitByParticle(){
		if(!particleHitThisFrame){
			particleHitThisFrame = true;
			StartCoroutine(ParticleCollisionTimer());
			return false;
		} else {
			return true;
		}
	}

	public void EnableControls(){
		playerControlActive = true;
	}

	public void DisableControls(){
		playerControlActive = false;
		isDragging = false;
	}

	IEnumerator ParticleCollisionTimer(){
			yield return new WaitForSeconds(hitDelay);
			particleHitThisFrame = false;
			yield break;
	}

	IEnumerator LerpToPointer(){
		while(isDragging){
			rigBod.velocity = Vector3.zero;
			/*
			This code would make for a great starting point when trying to do a catapult style game
			Dragging from the object to a certain point and releasing make the object move towards the PointerUp position in a smooth motion
			transform.position = Vector3.Lerp(transform.position, pointerInWorld, rigBod.velocity.magnitude);	
			 */
			transform.position = Vector3.Lerp(transform.position, pointerInWorld, Time.deltaTime*lerpToPointerSpeed);
			yield return null;
		}
		yield break;
	}

	IEnumerator LerpToRestingPos(){
		while(!isDragging){
			if(CompareTag("Player01")){
				if((Vector3.zero - transform.position).magnitude < 0.05f){
					transform.position = Vector3.zero;
					break;
				}
				transform.position = Vector3.SmoothDamp(transform.position, Vector3.zero, ref velocity, 2.0f);
			}
			else{
				if((playerTwoRestingPos - transform.position).magnitude < 0.05f){
					transform.position = playerTwoRestingPos;
					break;
				}
				transform.position = Vector3.SmoothDamp(transform.position, playerTwoRestingPos, ref velocity, 2.0f);
			}
			yield return null;
		}
		yield break;
	}

}
