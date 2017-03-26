using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	[SerializeField]private GameObject headMarker; 
	public int score;


	// Use this for initialization
	void Start () {
		
	}

	public void ScorePlus(Vector3 hitPosition)
	{
		Vector3 markerPos = headMarker.transform.position;
		float distance = Vector3.Distance (hitPosition, markerPos);
		score += Score(distance);
	}

	public int Score(float distance){
		if (distance < 0.02f) {
			return 100;
		} else if (distance < 0.04f) {
			return 90;
		} else if (distance < 0.08f) {
			return 70;
		} else if (distance < 0.12f) {
			return 60;
		} else if (distance < 0.20f) {
			return 50;
		} else {
			return 30;
		}
	}
}
