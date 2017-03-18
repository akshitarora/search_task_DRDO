using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour {

	public int subject;
	public float totalTime = 1800.0f;
	public float timeLeft;

	IEnumerator Requests_accept (string c, string toc, string comm) {
		float time1 = totalTime - timeLeft;
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + subject + "&toc=" + toc + " " + (time1) + "&comm=" + comm;
		Debug.Log (url);

		WWW www = new WWW (url);
		yield return www;

		string html = www.text;
		Debug.Log (html);
	}

	// Use this for initialization
	void Start () {
		timeLeft = totalTime;
		string localDate1 = DateTime.Now.ToString();
		subject = Convert.ToInt16(UnityEngine.Random.value * 10000);
		StartCoroutine (Requests_accept("GAME_INITIALIZED", localDate1, "starting up!"));
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;	
		float time1 = totalTime - timeLeft;
		Debug.Log (time1);
		if (timeLeft < 0.0f) {
			//string localDate1 = DateTime.Now.ToString();
			//StartCoroutine (Requests_accept("GAME_OVER", localDate1, "Game over!"));
			Debug.Log ("Game over!");
			SceneManager.LoadSceneAsync ("!Scenes/2");
			Debug.Log ("Scene changed");
		}	
	}
}
