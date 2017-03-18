using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	//サウンド系
	[SerializeField] AudioClip shotSound;
	AudioSource audioSource;
	//エフェクト系
	[SerializeField] GameObject fireEffect;
	[SerializeField] GameObject MuzzleFireEffect;
	[SerializeField] GameObject MuzzleFire;
	//弾薬系
	int bullet;  //残弾数
	[SerializeField] int maxBullet;  //最大弾薬数
	[SerializeField] int bulletBox;  //弾倉の最大収容数
	float coolTime = 0.1f;
	float interval = 0f;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		bullet = maxBullet;
	}
	
	// Update is called once per frame
	void Update () {
		interval += Time.deltaTime;
		if (Input.GetMouseButtonDown (0)) {
			rayControll ();
			shot ();
		}
	}

	//Rayが当たった場所にエフェクト表示＆銃口にエフェクト表示
	void rayControll(){
		if (bullet > 0 && interval > coolTime)
		{
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit = new RaycastHit ();

			if (Physics.Raycast (ray, out hit)) {
				Vector3 hitpoint = hit.point;
				GameObject cloneMuzzleFireEffect = (GameObject)Instantiate (MuzzleFireEffect, MuzzleFire.transform.position, Quaternion.identity);
				cloneMuzzleFireEffect.transform.parent = gameObject.transform;
				GameObject cloneFireEffect = (GameObject)Instantiate (fireEffect, hitpoint, Quaternion.identity);
				Destroy (cloneMuzzleFireEffect, 0.2f);
				Destroy (cloneFireEffect, 0.5f);
			}
		}
	}

	//発砲時の弾薬の設定＆サウンド設定
	void shot(){
		if (bullet > 0 && interval > coolTime) {
			bullet--;
			audioSource.PlayOneShot (shotSound);
			print (bullet);
			interval = 0;
		}
	}
}
