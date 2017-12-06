using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagePositioningHandler : MonoBehaviour {

	BoxCollider bottom;
	BoxCollider left;
	BoxCollider right;

	int screenWidth;
	int screenHeight;

	void Start(){
		BoxCollider[] boxCols = GetComponents<BoxCollider>();
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		
		//Debug.Log("" + boxCols[0].center.x + " " + boxCols[1].center.x + " " + boxCols[2].center.x + "\n" + screenWidth + " x " + screenHeight);
		
		bottom = boxCols[0];
		left = boxCols[1];
		right = boxCols[2];

		moveCageCollidersToScreenborders();
	}

	public void moveCageCollidersToScreenborders(){
		bottom.center.Set(0,-0.5f,0);
		left.center.Set(-0.5f,left.center.y,0);
		right.center.Set(screenWidth+0.5f,left.center.y,0);
	}
}
