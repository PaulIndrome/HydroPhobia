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

		StartCoroutine(LerpToRestingPos());
	}

	public override void BigParticleHit(){
		/*
		//every big particle that is caught by Bube increases Sube's speed
		NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(2, 1.05f);
		*/
		//every big particle that is caught by Bube increases their spawn rate
		NewGameManager.instance.bigParticleReleaseHandler.DelayBetweenSpawns *= 0.96f;
		//every big particle caught by Bube heals Sube
		PlayerManager.HealthImpact(PlayerEnum.Sube, 1);
		//increase big particle score
		NewGameManager.instance.bigParticleReleaseHandler.UpdateBigParticleScore(1);
	}

	public override void SmallParticleHit(){
		/*
		//every small particle that is caught by Bube reduces Sube's speed
		NewGameManager.instance.playerManager.ChangePlayerLerpSpeed(2, 0.85f);
		*/
		//every small particle that is caught by Bube decreases the big particle spawn rate
		NewGameManager.instance.bigParticleReleaseHandler.DelayBetweenSpawns *= 1.04f;
		//decrease small particle score
		NewGameManager.instance.smallParticleReleaseHandler.UpdateSmallParticleScore(-1);
		//every small particle caught by Bube hurts both
		PlayerManager.HealthImpact(PlayerEnum.Sube, -3);
		PlayerManager.HealthImpact(PlayerEnum.Bube, -3);
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
