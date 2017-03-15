using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class game_over : MonoBehaviour {

	IEnumerator Requests_go (string c, string toc, string comm) {
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + "gameOver" + "&toc=" + toc + "&comm=" + comm;
		//Debug.Log (url);

		WWW www = new WWW (url);
		yield return www;

		string html = www.text;
		Debug.Log (html);
	}


	// Use this for initialization
	void Start () {
		string localDate1 = DateTime.Now.ToString();
		StartCoroutine (Requests_go("GAME_OVER", localDate1, "Game over!"));
	}
}
