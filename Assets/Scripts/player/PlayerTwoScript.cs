﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTwoScript : Player {

	public void Start(){
		child3dObject = transform.GetChild(0);
		rigBod = GetComponent<Rigidbody>();
		mainCam = Camera.main;

		playerRestingPos = GrabPlayerRestingPos();
		
		playerControlActive = GrabPlayerControlActive();

		StartCoroutine(LerpToRestingPos());
	}

	public override void BigParticleHit(){

	}

	public override void SmallParticleHit(){
		// every small particle that is caught by Sube increases Bube's speed
		NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(1, 1.1f);
	}

	public override float GrabLerpToPointerSpeed(){
		return NewGameManager.instance.playerManager.lerpToPointerSpeedPlayer02;
	}

	public override bool GrabPlayerControlActive(){
		return NewGameManager.instance.playerManager.playerControl02;
	}

	public override Vector3 GrabPlayerRestingPos(){
		return NewGameManager.instance.playerManager.playerRestingPos02;
	}




}
