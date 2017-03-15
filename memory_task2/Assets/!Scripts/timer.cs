using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour {

	public static int subject;
	public static float totalTime = 600.0f;
	public float timeLeft = totalTime;

	IEnumerator Requests_accept (string c, string toc, string comm) {
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + subject + "&toc=" + toc + "&comm=" + comm;
		Debug.Log (url);

		WWW www = new WWW (url);
		yield return www;

		string html = www.text;
		Debug.Log (html);
	}

	// Use this for initialization
	void Start () {
		string localDate1 = DateTime.Now.ToString();
		subject = Convert.ToInt16(UnityEngine.Random.value * 10000);
		StartCoroutine (Requests_accept("GAME_INITIALIZED", localDate1, "starting up!"));
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			string localDate1 = DateTime.Now.ToString();
			StartCoroutine (Requests_accept("GAME_OVER", localDate1, "Game over!"));
			SceneManager.LoadScene ("2");
		}	
	}
}
