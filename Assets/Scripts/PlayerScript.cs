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

	private float rigBodDefaultMass;
	private Rigidbody rigBod;
	private Camera mainCam;

	private Vector3 lastIntersection;
	private Vector3 pointerInWorld;

	


	public void Start(){
		child3dObject = transform.GetChild(0);
		rigBod = GetComponent<Rigidbody>();
		mainCam = Camera.main;
		rigBodDefaultMass = rigBod.mass;
		lastIntersection = Vector3.zero;
		
	}

	public virtual void OnPointerDown(PointerEventData ped){
		//Debug.Log("Down at " + ped.position);
		pointerInWorld = transform.position;
		rigBod.mass = 0;
		rigBod.useGravity = false;
		rigBod.velocity = Vector3.zero;
		isDragging = true;
		StartCoroutine(LerpToPointer());
	}

	public virtual void OnDrag(PointerEventData ped){
		pointerInWorld = ped.position;
		pointerInWorld.z = 10;
		pointerInWorld = mainCam.ScreenToWorldPoint(pointerInWorld);
		//transform.position = pointerInWorld;
		child3dObject.Rotate(new Vector3(ped.delta.x * (1 + Random.value), ped.delta.x * (1 + Random.value), Random.value*10));
	}

	public virtual void OnPointerUp(PointerEventData ped){
		//Debug.Log("Up at " + ped.position);
		rigBod.mass = rigBodDefaultMass;
		rigBod.useGravity = true;
		isDragging = false;
		StopCoroutine(LerpToPointer());
	}

	public void BigParticleHitPlayerOne(){
		//Debug.Log("Big hit Player01");
		
		if(!RecentlyHitByParticle()){
			Debug.Log("Big hit Player01 anew");
		}
	}

	public void BigParticleHitPlayerTwo(){
		//Debug.Log("Big hit Player02");
		
		if(!RecentlyHitByParticle()){
			Debug.Log("Big hit Player02 anew");
		}
	}

	public void SmallParticleHitPlayerOne(){
		//Debug.Log("Small hit Player01");

		if(!RecentlyHitByParticle()){
			Debug.Log("Small hit Player01 hard");
		}
	}

	public void SmallParticleHitPlayerTwo(){
		//Debug.Log("Small hit Player02");
		
		if(!RecentlyHitByParticle()){
			Debug.Log("Small hit Player02 hard");
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

	IEnumerator ParticleCollisionTimer(){
			yield return new WaitForSeconds(hitDelay);
			//Debug.Log("Boop");
			particleHitThisFrame = false;
			yield return null;
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
		yield return null;
	}

}
