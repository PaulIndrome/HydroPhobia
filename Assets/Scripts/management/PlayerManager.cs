using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public PlayerOneScript playerOne;
	public PlayerTwoScript playerTwo;

	public float lerpToPointerSpeedPlayer01;
	public float lerpToPointerSpeedPlayer02;

	public bool playerControl01;
	public bool playerControl02;

	public Vector3 playerRestingPos01;
	public Vector3 playerRestingPos02;

	public void EnablePlayerControl(){
		playerControl01 = true;
		playerControl02 = true;
	}
	public void DisablePlayerControl(){
		playerControl01 = false;
		playerControl02 = false;
	}

	public void ChangePlayerLerpSpeed(int playerNum, float multiplier){
		if(playerNum == 1){
			lerpToPointerSpeedPlayer01 *= multiplier;
		} else if (playerNum == 2){
			lerpToPointerSpeedPlayer02 *= multiplier;
		} else 
			return;
	}

	public void ChangePlayerLerpSpeed(int playerNum, int value){
		if(playerNum == 1){
			lerpToPointerSpeedPlayer01 += value;
		} else if (playerNum == 2){
			lerpToPointerSpeedPlayer02 += value;
		} else 
			return;
	}
}
