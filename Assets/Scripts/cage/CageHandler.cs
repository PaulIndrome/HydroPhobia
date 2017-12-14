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

	private BoxCollider[] boxColliders;

	void Start(){
		mainCam = Camera.main;
		leftAndRightBounds = GetComponentsInChildren<LineRenderer>();
		boxColliders = GetComponentsInChildren<BoxCollider>();

		screenWidth = Screen.width;
		screenHeight = Screen.height;
		screenCornersInWorldSpace = new Vector3[4];
		
		FindScreenCornersInWorldSpace();
		DrawScreenBounds();
		RepositionCageSides();
		ScaleCageBottom();
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

	public void RepositionCageSides(){
		//left cage
		Vector3 leftCenterPos = new Vector3(screenCornersInWorldSpace[0].x - boxColliders[0].size.x / 4, boxColliders[0].center.y, 0);
		boxColliders[0].center = leftCenterPos;
		//right cage
		Vector3 rightCenterPos = new Vector3(screenCornersInWorldSpace[1].x + boxColliders[1].size.x / 4, boxColliders[1].center.y, 0);
		boxColliders[1].center = rightCenterPos;
	}

	public void ScaleCageBottom(){
		Vector3 bottomCageScale = new Vector3(screenCornersInWorldSpace[1].x * 2.25f, boxColliders[2].size.y, boxColliders[2].size.z);
		boxColliders[2].size = bottomCageScale;
	}

}
