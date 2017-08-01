
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.Analytics;

public class FBscript : MonoBehaviour
{

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }

    void SetInit()
    {

        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }

        DealWithFBMenus(FB.IsLoggedIn);

    }

    void OnHideUnity(bool isGameShown)
    {

        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void FBlogin()
    {

        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {

        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }

    }

    //Facebook Share
    public void ShareToFB()
    {
        int HighScore = PlayerPrefs.GetInt("Score");
        FB.ShareLink(new System.Uri("https://developers.facebook.com/apps/1665291427129966/dashboard/"), "Wejh El Bouma Game That's what's up!!", "My HighScore is!! :" + HighScore.ToString(), callback: null);

    }
    //Facebook Share picture
    public void PictureShareToFB()
    {

        FB.ShareLink(new System.Uri("https://drive.google.com/file/d/0B0N1xjoM_P2iRDNaSUxIRmlidTA/view"), "Wejh El Bouma Game That's what's up!!", "Play Wejh El Bouma Now!! :", callback: null);
 
      /*  FB.FeedShare("oues.maher", null, "All Done", "This is caption!! wow", "much description such wow!!", new System.Uri("https://goo.gl/QonRgv"),"Media Source", callback: null);
    */
      } 
    void DealWithFBMenus(bool isLoggedIn)
    {

        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }

    }

    void DisplayUsername(IResult result)
    {

        Text UserName = DialogUsername.GetComponent<Text>();

        if (result.Error == null)
        {

            UserName.text = "Hi there, " + result.ResultDictionary["first_name"];
            Analytics.CustomEvent("userName",
        new Dictionary<string, object> { { "user name", UserName.ToString() } });

        }
        else
        {
            Debug.Log(result.Error);
        }

    }

    void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {

            Image ProfilePic = DialogProfilePic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());

        }

    }

}