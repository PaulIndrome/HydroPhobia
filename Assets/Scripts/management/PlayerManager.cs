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

	public void Awake(){
		//lerpToPointerSpeedPlayer01 = 1f;
		//lerpToPointerSpeedPlayer02 = 1f;
		//
		//playerControl01 = true;
		//playerControl02 = true;
		//
		//playerRestingPos01 = Vector3.zero;
		//playerRestingPos02 = new Vector3(0,-5,0);
	}

	public void PlayerOneShieldActive(){

	}


	public void EnablePlayerControl(){
		playerControl01 = true;
		playerControl02 = true;
	}
	public void DisablePlayerControl(){
		playerControl01 = false;
		playerControl02 = false;
	}
}
