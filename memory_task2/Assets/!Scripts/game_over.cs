using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using LitJson;

public class game_over : MonoBehaviour {

	int subject;
	public Scene scene;
	public string acc_final;

	IEnumerator Requests_go (string c, string toc, string comm) {
		if (c == "GAME_OVER") {
			string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + subject + "&toc=" + toc + "&comm=" + comm;
			WWW www = new WWW (url);
			yield return www;
			string html = www.text;
			Debug.Log (html);
			string aa = c + " , " + subject + " , " + toc + " , " + comm;
			if (Application.platform != RuntimePlatform.Android) {
				using(System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\akshi\Desktop\logs_memory.txt", true)) { 
					file.WriteLine(aa);
				}
			} 
			else {
				using(System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + @"/logs_memory.txt", true)) {
					file.WriteLine(aa);
				} 
			}
		} else if (c== "accelerometer_values"){
			string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + subject + "&toc=" + toc;
			//JSON objects are not being sent to the URL through GET variable. There are double quotes in string. To avoid that we will use post method. 
			WWWForm form = new WWWForm();
			form.AddField ("comm", comm);
			WWW www = new WWW (url, form);
			yield return www;
			Debug.Log (url);
			string html = www.text;
			Debug.Log (html);
			string aa = c + " , " + subject + " , " + toc + " , " + comm;
			if (Application.platform != RuntimePlatform.Android) {
				using(System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\akshi\Desktop\logs_memory.txt", true)) { //we can make the path generic to Application.persistentDataPath + "\logs.txt"
					file.WriteLine(aa);
				}
			} else {
				using(System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + @"/logs_memory.txt", true)) {
					file.WriteLine(aa);
				}
			}
		}
	}

	void Start () {
		acc_final = GameObject.FindGameObjectWithTag("acc").GetComponent<accelerometer_log> ().acc_final;
		acc_final = acc_final + "]";
		subject = GameObject.FindGameObjectWithTag("acc").GetComponent<accelerometer_log> ().subject;
		string localDate1 = DateTime.Now.ToString();
		StartCoroutine (Requests_go("accelerometer_values", localDate1, acc_final));
		StartCoroutine (Requests_go("GAME_OVER", localDate1, "Game over!"));
	}
}