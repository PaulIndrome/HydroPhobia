using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCageCollision : MonoBehaviour {

	public void OnCollisionEnter(Collision col){
		Rigidbody rigBod = col.gameObject.GetComponent<Rigidbody>();
		if(rigBod != null)
			rigBod.velocity = Vector3.Reflect(rigBod.velocity, col.contacts[0].normal)*2;
	
	}
}
