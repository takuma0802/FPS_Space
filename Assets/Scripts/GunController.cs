using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {



	//サウンド系
	[SerializeField] AudioClip shotSound;
	AudioSource audioSource;
	//エフェクト系
	[SerializeField] private GameObject fireEffect;
	[SerializeField] private GameObject MuzzleFireEffect;
	[SerializeField] private GameObject MuzzleFire;
	//弾薬系
	[SerializeField] private GameObject FirstPersonCharacter;  //カメラの位置取得
	public int bullet;  //残弾数
	public int maxBullet;  //最大弾薬数
	public int bulletBox;  //弾倉の最大収容数
	float coolTime = 0.5f;
	float interval = 0f;
	//リロード系
	[SerializeField] private AudioClip reloadSound;
	float reloadTime = 2.5f;
	float reloadInterval = 3.0f;
	//ターゲット系
	float revivaltime;
	[SerializeField] private ScoreManager score;


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
		reloadInterval += Time.deltaTime;
		//クリック時に発砲
		if (Input.GetMouseButtonDown (0) && bullet > 0 && interval > coolTime && reloadInterval > reloadTime)
		{
			rayControll ();
			shot ();
		}
		//Rを押したらリロード
		if (Input.GetKeyDown (KeyCode.R))
		{
			reload ();
			reloadInterval = 0f;
		}
	}


	//Rayが当たった場所にエフェクト表示＆銃口にエフェクト表示
	void rayControll()
	{
		Ray ray = new Ray (FirstPersonCharacter.transform.position, FirstPersonCharacter.transform.forward);
		RaycastHit hit;
		GameObject cloneMuzzleFireEffect = (GameObject)Instantiate (MuzzleFireEffect, MuzzleFire.transform.position, Quaternion.identity);
		cloneMuzzleFireEffect.transform.LookAt (FirstPersonCharacter.transform);
		cloneMuzzleFireEffect.transform.parent = gameObject.transform;
		Destroy (cloneMuzzleFireEffect, 0.1f);

		if (Physics.Raycast (ray, out hit))
		{
			GameObject cloneFireEffect = (GameObject)Instantiate (fireEffect);
			cloneFireEffect.transform.position = hit.point - ray.direction;
			Destroy (cloneFireEffect, 0.5f);
			Target target = hit.collider.gameObject.GetComponent<Target> ();
			if (target != null) {
				target.Hit ();
				score.ScorePlus(hit.point);
			}
		}
	}

	//発砲時の弾薬の設定＆サウンド設定
	void shot()
	{
		bullet--;
		audioSource.PlayOneShot (shotSound);
		interval = 0;
	}

	void reload()
	{
		if (bullet < maxBullet) 
		{
			audioSource.PlayOneShot (reloadSound);
			int reloadBullet = maxBullet - bullet;
			if (reloadBullet <= bulletBox) {
				bulletBox -= reloadBullet;
				bullet = maxBullet;
			} else {
				bullet += bulletBox;
				bulletBox = 0;
			}
		}
	}
}
