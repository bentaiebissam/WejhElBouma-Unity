using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Runtime.InteropServices;



public class GooglePlay : MonoBehaviour {
    public GameObject Button;
    public Text namez;
	// Use this for initialization
	void Start () {

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LogInGoogle()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("dONE logged in");
                Button.gameObject.SetActive(false);
                namez.text = Social.localUser.userName; 
            }
            if (success==false)
            {
                Debug.Log("Login dONE goofed");
                namez.text ="ERROR!! soury"; 
            }
        });
    }
}
