using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCubesHandler : MonoBehaviour {

	[SerializeField]
	private float rotationChangeDelay = 5;
	private float timeSpentRotatingSinceLastChange = 0;
	private float outerRotationAngle = 1;

	private Vector3 flairCubeOuterFinalPosition = new Vector3(1.5f, 0,0);
	private Quaternion fromRotation;
	private Quaternion fromRotationOuter;
	private Quaternion toRotation;
	private Transform flairCube;
	private Transform flairCubeOuterCarrier;
	private Transform flairCubeOuter;


	// Use this for initialization
	void Start () {
		flairCube = transform.GetChild(0);
		flairCubeOuterCarrier = transform.GetChild(0).GetChild(0);
		flairCubeOuter = transform.GetChild(0).GetChild(0).GetChild(0);
		toRotation = Random.rotationUniform;
		fromRotation = flairCube.rotation;
		fromRotationOuter = flairCubeOuterCarrier.rotation;
		//timeSpentRotatingSinceLastChange = 0;
		//rotationChangeDelay = 5;
		StartCoroutine(RotateCubeRandomly());
		StartCoroutine(ChangeRotationDirections());
		StartCoroutine(PositionOuterCube());
	}
	
	IEnumerator ChangeRotationDirections(){
		while(flairCube.gameObject.activeSelf){
			fromRotationOuter = flairCubeOuterCarrier.rotation;
			fromRotation = flairCube.rotation;
			toRotation = Random.rotationUniform;
			timeSpentRotatingSinceLastChange = 0;
			yield return new WaitForSeconds(rotationChangeDelay);
		}
		yield return null;
	}

	IEnumerator RotateCubeRandomly(){
		while(flairCube.gameObject.activeSelf){
			timeSpentRotatingSinceLastChange += Time.deltaTime;
			float t = timeSpentRotatingSinceLastChange / rotationChangeDelay;
			//Debug.Log(t);
			flairCube.rotation = Quaternion.Slerp(fromRotation, toRotation, t);
			flairCubeOuterCarrier.rotation = Quaternion.SlerpUnclamped(fromRotationOuter, Quaternion.Inverse(toRotation), t*4);
			//outerRotationAngle = outerRotationAngle++ % 360;
			//flairCubeOuter.RotateAround(transform.position, Vector3.forward, outerRotationAngle);
			yield return null;
		}
		yield return null;
	}

	IEnumerator PositionOuterCube(){
		while(flairCubeOuter.localPosition.x  > 1.52f){
			flairCubeOuter.localPosition = Vector3.Slerp(flairCubeOuter.localPosition, flairCubeOuterFinalPosition, Time.deltaTime/2f);
			yield return null;
		}
		yield return null;
	}
}
