using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallParticleReleaseHandler : MonoBehaviour {


	[SerializeField]
	public int smallParticlesToReleaseFirst, smallParticlesToReleaseNext, smallParticlesReleasedLast;

	public static bool smallParticlesActive = false;

	public static List<GameObject> smallParticleReleaserList = new List<GameObject>();

	public float smallParticleScore;

	public Text smallParticleScoreText;

	public void Awake(){
		Reset();
	}

	public void Reset(){
		smallParticleScoreText.text = "0";

		smallParticlesActive = false;

		smallParticleReleaserList.Clear();

		smallParticlesToReleaseFirst = 5;

		smallParticlesToReleaseNext = smallParticlesToReleaseFirst;
	}

	public static void SmallParticleReleaseOccured(GameObject occurrence){
		NewGameManager.instance.bigParticleReleaseHandler.ToggleDangerousParticles(true);
		smallParticleReleaserList.Add(occurrence);
	}

	public void SmallParticleReleaseFinished(GameObject finished){
		smallParticleReleaserList.Remove(finished);
		NewGameManager.instance.bigParticleReleaseHandler.ToggleDangerousParticles(false);
		smallParticlesActive = false;
	}

	public void ComputeSmallParticlesToReleaseNext(int smallParticlesReleased, int smallParticlesCaught){
		if(smallParticlesReleased == 1 && smallParticlesCaught == 0){
			NewGameManager.instance.ForceGameOver();
		} else if (smallParticlesReleased >= 2 && smallParticlesCaught == 0){
			smallParticlesToReleaseNext /= 2;
		} else if (smallParticlesCaught >= smallParticlesReleased){
			smallParticlesToReleaseNext += Mathf.Max(1, smallParticlesCaught / 2);
		} else {
			smallParticlesToReleaseNext = smallParticlesCaught;
		}
	}

	public void UpdateSmallParticleScore(int plusMinus){
		smallParticleScore += plusMinus;
		smallParticleScoreText.text = "" + Mathf.Clamp(smallParticleScore, 0, 99999);
	}

}
