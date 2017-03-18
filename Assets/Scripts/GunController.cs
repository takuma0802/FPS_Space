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
	[SerializeField] GameObject FirstPersonCharacter;  //カメラの位置取得
	[SerializeField] int bullet;  //残弾数
	[SerializeField] int maxBullet;  //最大弾薬数
	[SerializeField] int bulletBox;  //弾倉の最大収容数
	float coolTime = 0.1f;
	float interval = 0f;
	//リロード系
	[SerializeField] AudioClip reloadSound;


	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		bullet = maxBullet;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//インターバル
		interval += Time.deltaTime;
		//クリック時に発砲
		if (Input.GetMouseButtonDown (0))
		{
			rayControll ();
			shot ();
		}
		//Rを押したらリロード
		if (Input.GetKeyDown (KeyCode.R))
		{
			reload ();
		}
	}


	//Rayが当たった場所にエフェクト表示＆銃口にエフェクト表示
	void rayControll()
	{
		if (bullet > 0 && interval > coolTime)
		{
			Ray ray = new Ray (FirstPersonCharacter.transform.position, FirstPersonCharacter.transform.forward);
			RaycastHit hit = new RaycastHit ();
			Vector3 hitpoint = hit.point;
			GameObject cloneMuzzleFireEffect = (GameObject)Instantiate (MuzzleFireEffect, MuzzleFire.transform.position, Quaternion.identity);
			cloneMuzzleFireEffect.transform.LookAt (FirstPersonCharacter.transform);
			cloneMuzzleFireEffect.transform.parent = gameObject.transform;
			Destroy (cloneMuzzleFireEffect, 0.1f);

			if (Physics.Raycast (ray, out hit))
			{
				GameObject cloneFireEffect = (GameObject)Instantiate (fireEffect, hitpoint, Quaternion.identity);
				Destroy (cloneFireEffect, 0.5f);
			}
		}
	}

	//発砲時の弾薬の設定＆サウンド設定
	void shot()
	{
		if (bullet > 0 && interval > coolTime)
		{
			bullet--;
			audioSource.PlayOneShot (shotSound);
			print (bullet);
			interval = 0;
		}
	}

	void reload()
	{
		if (bullet < maxBullet) 
		{
			int reloadBullet = maxBullet - bullet;	
			if (reloadBullet <= bulletBox) {
				bulletBox -= reloadBullet;
				bullet = maxBullet;
				audioSource.PlayOneShot (reloadSound);
				print (bulletBox);
			} else {
				bullet += bulletBox;
				bulletBox = 0;
				audioSource.PlayOneShot (reloadSound);
				print (bulletBox);
			}
		}
	}
}
