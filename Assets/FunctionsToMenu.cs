using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;


public class FunctionsToMenu : MonoBehaviour {
    public GameObject TUTOb;
    public GameObject panel;
    public GameObject PanelFB;
    

    private int TUTOint;
	// Use this for initialization
	void Start () {

       

        //Wanna buy some tuto kids?
        TUTOint = PlayerPrefs.GetInt("TUTOint");
        if (TUTOint == 0) TUTOb.gameObject.SetActive(false);
        if (TUTOint == 1) TUTOb.gameObject.SetActive(true);

        //0 : I want a tuto
        //1: I dont want no tuto => U sure bro?
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TUTOActivate()
    {
        PlayerPrefs.SetInt("TUTOint", 0);
        TUTOb.gameObject.SetActive(false);

    }

    public void StartButton() {

        SceneManager.LoadScene("Main");

    }
    public void PanelFBActivate()
    {
        PanelFB.gameObject.SetActive(true);

    }
    public void Easy()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        panel.gameObject.SetActive(false);
    }

    public void Hard()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        panel.gameObject.SetActive(false);
    }
    public void Difficulty()
    {
        panel.gameObject.SetActive(true);

    }
}

