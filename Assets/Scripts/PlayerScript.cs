using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	public Transform child3dObject;
	public float deltaSensitivity = 0.5f;
	private Rigidbody rigBod;

	private int rainDropHits = 0;


	public void Start(){
		child3dObject = transform.GetChild(0);
		rigBod = GetComponent<Rigidbody>();
	}


	public virtual void OnPointerDown(PointerEventData ped){
		Debug.Log(ped.pointerId);
		rigBod.mass = 0;
		rigBod.useGravity = false;
	}

	public virtual void OnDrag(PointerEventData ped){
			Vector2 temperedDelta = ped.delta * deltaSensitivity;
			transform.Translate(temperedDelta);
			child3dObject.Rotate(new Vector3(ped.delta.x * (1 + Random.value), ped.delta.x * (1 + Random.value), Random.value*10));
	}

	public virtual void OnPointerUp(PointerEventData ped){
		rigBod.mass = 1;
		rigBod.useGravity = true;
	}

}
