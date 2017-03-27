using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour {
	
	[SerializeField]Camera camera;
	[SerializeField]GameObject snipe;
	bool sniper;

	// Use this for initialization
	void Start(){
		print (camera.fieldOfView);
		sniper = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			if (sniper == false) 
			{
				sniper = true;
				snipe.SetActive(true);
				camera.fieldOfView = 15.0f;
			} else {
				sniper = false;
				snipe.SetActive(false);
				camera.fieldOfView = 60.0f;
			}
		}
	}
}
