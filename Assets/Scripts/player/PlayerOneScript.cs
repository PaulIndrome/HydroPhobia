using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOneScript : Player {

	public void Start(){
		child3dObject = transform.GetChild(0);
		rigBod = GetComponent<Rigidbody>();
		mainCam = Camera.main;

		playerRestingPos = GrabPlayerRestingPos();
		
		playerControlActive = GrabPlayerControlActive();

		Debug.Log(playerControlActive + " " + GrabLerpToPointerSpeed());

		StartCoroutine(LerpToRestingPos());
	}

	public override void BigParticleHit(){

	}

	public override void SmallParticleHit(){
		
	}

	public override float GrabLerpToPointerSpeed(){
		return NewGameManager.instance.playerManager.lerpToPointerSpeedPlayer01;
	}

	public override bool GrabPlayerControlActive(){
		return NewGameManager.instance.playerManager.playerControl01;
	}

	public override Vector3 GrabPlayerRestingPos(){
		return NewGameManager.instance.playerManager.playerRestingPos01;
	}




}
