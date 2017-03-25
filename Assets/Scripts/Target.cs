using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	[SerializeField] private GameObject target;
	public int targetLife;
	Animator anim;


	// Use this for initialization
	void Start () {
		anim = target.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void Hit(){
		targetLife--;
		if (targetLife == 0)
		{
			//anim.SetBool ("IsBroken", true);
			//Invoke ("Revival", 10f);
		}
	}

	void Revival(){
		anim.SetBool ("IsBroken", false);
		targetLife = 5;
	}

}
