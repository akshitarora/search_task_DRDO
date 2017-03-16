using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using LitJson;

public class acc {
	public DateTime log;
	public string x;
	public string y;
	public string z;
}

public class accelerometer_log : MonoBehaviour {

	public Scene scene;
	public string json_acc;
	public string acc_final;
	public float interval = 0.5f;

	void AccToJson()
	{
		acc acc_log = new acc();

		acc_log.x = Input.acceleration.x.ToString();
		acc_log.y  = Input.acceleration.y.ToString();
		acc_log.z = Input.acceleration.z.ToString();
//		acc_log.x = "hax";
//		acc_log.y  = "hay";
//		acc_log.z = "haz";
		acc_log.log = DateTime.Now;

		json_acc = JsonMapper.ToJson(acc_log);

		//Debug.Log (json_acc);
	}


	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
		if (scene.name == "1") {
			Debug.Log ("accelerometer log started!");
			AccToJson ();
			acc_final = json_acc;
		}
	}
	
	// Update is called once per frame
	void Update () {
		scene = SceneManager.GetActiveScene ();
		interval -= Time.deltaTime;
		if (scene.name == "1" && interval < 0.0f) {
			AccToJson ();
			acc_final = acc_final + "\r\n" + json_acc;
			Debug.Log (acc_final);
			interval = 0.5f;
		}
	}
}
