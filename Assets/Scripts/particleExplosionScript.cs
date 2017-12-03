using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleExplosionScript : MonoBehaviour {

	public GameObject playerObject;
	public ParticleSystem particleBlip;
	public ParticleSystem particleSplurt;
	private float particleSystemDuration;

	public void Start(){
		Destroy(gameObject, 0.25f);
	}

	public void setTriggerObject(GameObject player){
		playerObject = player;
		transform.LookAt(playerObject.transform);
	}


}
