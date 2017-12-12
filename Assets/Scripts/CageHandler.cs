using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageHandler : MonoBehaviour {

	private Camera mainCam;

	int screenWidth;
	int screenHeight;

	public Vector3[] screenCornersInWorldSpace;
	[SerializeField]
	private LineRenderer[] leftAndRightBounds;

	void Start(){
		mainCam = Camera.main;
		leftAndRightBounds = GetComponentsInChildren<LineRenderer>();

		screenWidth = Screen.width;
		screenHeight = Screen.height;
		screenCornersInWorldSpace = new Vector3[4];
		
		
		FindScreenCornersInWorldSpace();
		DrawScreenBounds();
	}

	public void FindScreenCornersInWorldSpace(){
		//clockwise from top left corner
		screenCornersInWorldSpace[0] = mainCam.ScreenToWorldPoint(new Vector3(0,screenHeight,10));
		screenCornersInWorldSpace[1] = mainCam.ScreenToWorldPoint(new Vector3(screenWidth,screenHeight,10));
		screenCornersInWorldSpace[2] = mainCam.ScreenToWorldPoint(new Vector3(screenWidth,0,10));
		screenCornersInWorldSpace[3] = mainCam.ScreenToWorldPoint(new Vector3(0,0,10));
	}

	public void DrawScreenBounds(){
		//left bounds
		leftAndRightBounds[0].SetPosition(0, screenCornersInWorldSpace[0]);
		leftAndRightBounds[0].SetPosition(1, screenCornersInWorldSpace[3]);
		//right bounds
		leftAndRightBounds[1].SetPosition(0, screenCornersInWorldSpace[1]);
		leftAndRightBounds[1].SetPosition(1, screenCornersInWorldSpace[2]);
	}

}
