using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]private ScoreManager score;
	[SerializeField]private GunController gun;
	[SerializeField]private Text time;
	[SerializeField]private Text pt;
	[SerializeField]private Text bulletBox;
	[SerializeField]private Text bullet;

	[SerializeField]float limitTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		limitTime += Time.deltaTime;
		time.text="Time" + limitTime.ToString("F1") + "s";
		pt.text = "Pt:" + score.score;
		bulletBox.text = "BulletBox:" + gun.bulletBox;
		bullet.text = "Bullet:" + gun.bullet + "/" + gun.maxBullet;

	}
}
