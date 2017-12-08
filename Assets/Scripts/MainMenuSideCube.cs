using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSideCube : MonoBehaviour {

	MeshRenderer mr;
	Material mrM;

	[SerializeField]
	private Color fadeInColor, fadeOutColor;

	Color currentGoalColor;


	public void Start(){
		mr = GetComponent<MeshRenderer>();
		mrM = mr.material;
		//fadeInColor = mrM.color;
		//fadeOutColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
		currentGoalColor = mrM.color;
		StartCoroutine(LerpColor());
	}

	void OnTriggerEnter(Collider col){
		currentGoalColor = fadeOutColor;
	}

	void OnTriggerExit(Collider col){
		currentGoalColor = fadeInColor;
	}

	IEnumerator LerpColor(){
		while(gameObject.activeSelf){
			mrM.color = Color.Lerp(mrM.color, currentGoalColor, Time.deltaTime*2);
			yield return null;
		}
		yield return null;
	}

}
