using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageBottomCollisionHandler : MonoBehaviour {

	public void OnCollisionEnter(Collision collision){
			Destroy(collision.gameObject);
	}

}
