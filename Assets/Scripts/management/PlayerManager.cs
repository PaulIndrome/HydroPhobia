using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	public delegate void HealthImpactDelegate(PlayerEnum pEnum, int impact);
	public static event HealthImpactDelegate HealthImpactEvent;

	public PlayerOneScript playerOne;
	public PlayerTwoScript playerTwo;

	public float lerpToPointerSpeedPlayer01;
	public float lerpToPointerSpeedPlayer02;

	public bool playerControl01;
	public bool playerControl02;

	public Vector3 playerRestingPos01;
	public Vector3 playerRestingPos02;

	public TextMesh player01LerpSpeedText;
	public TextMesh player02LerpSpeedText;	

	public void EnablePlayerControl(){
		playerControl01 = true;
		playerControl02 = true;
	}
	public void DisablePlayerControl(){
		playerControl01 = false;
		playerControl02 = false;
	}

	public void TogglePlayerControl(bool enabled){
		playerControl01 = playerControl02 = enabled;
	}

	public void ChangePlayerLerpSpeed(int playerNum, float multiplier){
		if(playerNum == 1){
			lerpToPointerSpeedPlayer01 *= multiplier;
		} else if (playerNum == 2){
			lerpToPointerSpeedPlayer02 *= multiplier;
		} else 
			return;
		
		UpdatePlayerText();
	
	}

	public void ChangePlayerLerpSpeed(int playerNum, int value){
		if(playerNum == 1){
			lerpToPointerSpeedPlayer01 += value;
		} else if (playerNum == 2){
			lerpToPointerSpeedPlayer02 += value;
		} else 
			return;

		UpdatePlayerText();
	}

	public void UpdatePlayerText(){
		//Debug.Log("Player 01: " + lerpToPointerSpeedPlayer01.ToString());
		//Debug.Log("Player 02: " + lerpToPointerSpeedPlayer02.ToString());
		if(lerpToPointerSpeedPlayer01.ToString().Length > 4){
			char[] p01 = lerpToPointerSpeedPlayer01.ToString().ToCharArray();
			player01LerpSpeedText.text = "" + p01[0] + p01[1] + p01[2];
			//Debug.Log("Player 01 actual: " + player01LerpSpeedText.text);
		} else {
			player01LerpSpeedText.text = "" + lerpToPointerSpeedPlayer01;
		}
		if(lerpToPointerSpeedPlayer02.ToString().Length > 4){
			char[] p02 = lerpToPointerSpeedPlayer02.ToString().ToCharArray();
			player02LerpSpeedText.text = "" + p02[0] + p02[1] + p02[2];
			//Debug.Log("Player 02 actual: " + player02LerpSpeedText.text);
		} else {
			player02LerpSpeedText.text = "" + lerpToPointerSpeedPlayer02;
		}
	}

	public static void HealthImpact(PlayerEnum pEnum, int impact){
		HealthImpactEvent(pEnum, impact);
	}
}
