using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallParticleReleaseHandler : MonoBehaviour {

	[SerializeField]
	private static float bigParticleFallSpeed, smallParticleFallSpeed, bigParticleDelay;

	[SerializeField]
	public static int smallParticlesToReleaseFirst, smallParticlesToReleaseNext, smallParticlesReleasedLast;

	public static bool smallParticlesActive = false;

	private static List<GameObject> smallParticleReleaserList = new List<GameObject>();

	public void Start(){
		smallParticlesActive = false;

		smallParticleReleaserList.Clear();

		smallParticlesToReleaseFirst = 10;

		smallParticlesToReleaseNext = smallParticlesToReleaseFirst;
	}

	public static void SmallParticleReleaseOccured(GameObject occurrence){
		smallParticleReleaserList.Add(occurrence);
	}

	public static void SmallParticleReleaseFinished(GameObject finished){
		smallParticleReleaserList.Remove(finished);
		smallParticlesActive = false;
	}
}
