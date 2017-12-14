using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	enum PlayerNum {player01, player02};
	public Transform child3dObject;

	[HideInInspector] public float hitDelay;
	[HideInInspector] public bool isDragging;
	[HideInInspector] public bool playerControlActive;
	[HideInInspector] public Rigidbody rigBod;
	[HideInInspector] public Camera mainCam;
	[HideInInspector] public Vector3 pointerInWorld;
	[HideInInspector] public Vector3 playerRestingPos;
	[HideInInspector] public Vector3 velocity = Vector3.zero;
	

	public void OnPointerDown(PointerEventData ped){
		if(GrabPlayerControlActive())
		{
			pointerInWorld = transform.position;
			rigBod.velocity = Vector3.zero;
			isDragging = true;
			StopCoroutine(LerpToRestingPos());
			StartCoroutine(LerpToPointer());
		}
	}

	public void OnDrag(PointerEventData ped){
		if(GrabPlayerControlActive())
		{
			pointerInWorld = ped.position;
			pointerInWorld.z = 10;
			pointerInWorld = mainCam.ScreenToWorldPoint(pointerInWorld);
			child3dObject.Rotate(new Vector3(ped.delta.x * (1 + Random.value), ped.delta.x * (1 + Random.value), Random.value*10));
		}
	}

	public void OnPointerUp(PointerEventData ped){
		if(GrabPlayerControlActive())
		{
			isDragging = false;
			StopCoroutine(LerpToPointer());
			StartCoroutine(LerpToRestingPos());
		}
	}

	public virtual void BigParticleHit(){

	}

	public virtual void SmallParticleHit(){
		
	}

	public virtual float GrabLerpToPointerSpeed(){
		return 0.0f;
	}

	public virtual bool GrabPlayerControlActive(){
		return false;
	}

	public virtual Vector3 GrabPlayerRestingPos(){
		return Vector3.up;
	}


	public virtual IEnumerator LerpToPointer(){
		while(isDragging){
			rigBod.velocity = Vector3.zero;
			/*
			This code would make for a great starting point when trying to do a catapult style game
			Dragging from the object to a certain point and releasing make the object move towards the PointerUp position in a smooth motion
			transform.position = Vector3.Lerp(transform.position, pointerInWorld, rigBod.velocity.magnitude);	
			 */
			transform.position = Vector3.Lerp(transform.position, pointerInWorld, Time.deltaTime * GrabLerpToPointerSpeed());
			yield return null;
		}
		yield break;
	}

	public virtual IEnumerator LerpToRestingPos(){
		while(!isDragging){
				if((playerRestingPos - transform.position).magnitude < 0.05f){
					transform.position = playerRestingPos;
					yield break;
				}
				transform.position = Vector3.SmoothDamp(transform.position, playerRestingPos, ref velocity, 2.0f);
			yield return null;
		}
		yield break;
	}

}
