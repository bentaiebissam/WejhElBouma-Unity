using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

using UnityEngine.SceneManagement;

public class ArrowMananger : MonoBehaviour
{
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;

    public GameObject Health;
    public GameObject SlowDown;

    public Text SlowDownLeftText;
    public Text ConfusLeftText;
    public Text ScoreText;
    public Text Achievement; /**/
    public GameObject StormyTip;
    public GameObject SleepyTip;

    public AudioSource GoodGuyBackground;
    public AudioSource GoodGuyGoodSelection;
    public AudioSource GoodGuyBadSelection;
    public AudioSource PartyBackground; 
    public AudioSource PartyGoodselection;
    public AudioSource PartyBadSelection;
    public AudioSource Confusebackground;
    public AudioSource ConfuseGoodSelection;
    public AudioSource ConfuseBadSelection;
    public AudioSource DisturbBackground;
    public AudioSource DisturBadSelection;


   // public AudioSource SlowDownSound;
    

    public GameObject Bouma;
    public GameObject Stormy;
    public GameObject Sleepy;
    public GameObject Party;
    private GameObject Current;

    public Text BoumaModeText;
    public GameObject Canvas;

    public Text ConfuseText;

    //buttons
    public GameObject TriangleB;
    public GameObject O;
    public GameObject SquareB;
    public GameObject X;

    public GameObject TriangleB1;
    public GameObject O1;
    public GameObject SquareB1;
    public GameObject X1;

    public GameObject TUTO;

    private int seperator;
    private int next;
    private Queue<GameObject> last = new Queue<GameObject>();
    private int confuse;
    private float speed;
    private int done;
    private int time;
    private int score;
    private int powerUp;
    private int Difficulty;

    AudioSource fail;
    AudioSource Background;
    AudioSource GoodSelection;

    int SlowDownLeft;
    int ConfusLeft;


    private string BoumaMode;
    int TUTOint;

    // Use this for initialization
    void Start()
    {   //Need a TUTO bro?
        TUTOint = PlayerPrefs.GetInt("TUTOint");
        if (TUTOint == 0) { TUTO.gameObject.SetActive(true); /*Start();*/ }
        if (TUTOint == 1) TUTO.gameObject.SetActive(false);


       // Gotit(); //to try


        Current = Bouma;
        Current.gameObject.SetActive(true);
        BoumaMode = "Good Guy Bouma";
        Background = GoodGuyBackground;
        fail = GoodGuyBadSelection;
        GoodSelection = GoodGuyGoodSelection;
        SoundManagment();

        Difficulty = PlayerPrefs.GetInt("Difficulty");
        if (Difficulty == 0)
        {
            speed = 0.01f;
            confuse = 100;
        }
        if (Difficulty == 1)
        {
            speed = 0.2f;
            confuse = 50;
        }


        score = 0;
        SetScore();
        SetConfuse();
       // NewOne();

        done = 0;
        time = 0;
        PlayerPrefs.SetInt("done", 0);
        PlayerPrefs.SetInt("SlowDownLeft", 0);
        PlayerPrefs.SetInt("ConfusLeft", 0);

    }


    void Update()
    {
        TUTOint = PlayerPrefs.GetInt("TUTOint");
        if (TUTOint == 0) { TUTO.gameObject.SetActive(true);/*date();*/ }
        if (TUTOint ==1) { TUTO.gameObject.SetActive(false);

           
        time++;
        score++;
        SetScore();

        //Randomly selecting a Bouma mode every 400frame
        if (time > 400)
        {
            speed = speed * 0.9f;

            

                //Just done with a mode ? => Unlock it's achivement 	

                if (BoumaMode == "Good Guy Bouma") { Social.ReportProgress("CgkI69TCipoOEAIQAQ", 100.0f, (bool success) => { if (success == true) Achievement.text = "Good job! You got achiement"; else Achievement.text = "Great Job! But not really"; }); }
                if (BoumaMode=="Stormy Bouma") { Social.ReportProgress("CgkI69TCipoOEAIQBA", 100.0f, (bool success) => { if (success == true) Debug.Log("Achivemen done"); if (success == false) Debug.Log("doune goofed & Achivement not done"); }); }
                if (BoumaMode == "Party Bouma") { Social.ReportProgress("CgkI69TCipoOEAIQAg", 100.0f, (bool success) => { if (success == true) Debug.Log("Achivemen done"); if (success == false) Debug.Log("doune goofed & Achivement not done"); }); }
                if (BoumaMode=="Sleepy Bouma") { Social.ReportProgress("CgkI69TCipoOEAIQAw", 100.0f, (bool success) => { if (success == true) Debug.Log("Achivemen done"); if (success == false) Debug.Log("doune goofed & Achivement not done"); }); }
                
                //It's a new mode, it's a new day.

                int mode = Random.Range(0, 6);
                if (mode == 0)  BoumaMode = "Good Guy Bouma";
                if (mode == 1 || mode == 5) BoumaMode = "Stormy Bouma";
                if (mode == 2) BoumaMode = "Party Bouma";
                if (mode == 4) BoumaMode = "Sleepy Bouma";
                time = 0;

        }   //Changing sound according to chosen boumaMode 
            SoundManagment();
            //Drawing the Bouma accordingly  
            if (BoumaMode == "Good Guy Bouma")
        {
                Background.Stop();
                GoodGuyBackground.Play();
                Background = GoodGuyBackground;
                SetButtons();
            Current.gameObject.SetActive(false);
            Current = Bouma;
            Current.gameObject.SetActive(true);
            SleepyTip.gameObject.SetActive(false);
            StormyTip.gameObject.SetActive(false);

        }
        if (BoumaMode == "Stormy Bouma")
        {
                Background.Stop();
                Confusebackground.Play();
                Background = Confusebackground;
            SwitchButtons();
            Current.gameObject.SetActive(false);
            Current = Stormy;
            Current.gameObject.SetActive(true);
            SleepyTip.gameObject.SetActive(false);
            StormyTip.gameObject.SetActive(true);

        }
        if (BoumaMode == "Party Bouma")
        {
                Background.Stop();
                PartyBackground.Play();
                Background = PartyBackground;
                SetButtons();
            Current.gameObject.SetActive(false);
            Current = Party;
            //    speed += speed;
            Current.gameObject.SetActive(true);
            SleepyTip.gameObject.SetActive(false);
            StormyTip.gameObject.SetActive(false);
        }
        if (BoumaMode == "Sleepy Bouma")
        {
                Background.Stop();
                DisturbBackground.Play();
                Background = DisturbBackground;
            SetButtons();
            Current.gameObject.SetActive(false);
            Current = Sleepy;
            Current.gameObject.SetActive(true);
            SleepyTip.gameObject.SetActive(true);
            StormyTip.gameObject.SetActive(false);
        }


        SetBoumaMode();
        //Checking if there's an arrow who passed the edge
        done = PlayerPrefs.GetInt("done", done);
        if (done == 2) //Reached the edge
        {
            try
            {
                // Debug.Log("You have reached the edge, i destroyed the one on the edge , took away 10pts and set done to zero");

                done = 0;
                PlayerPrefs.SetInt("done", 0);
                GameObject wrongdidi = last.Dequeue();
                Destroy(wrongdidi);
                if (BoumaMode.CompareTo("Sleepy Bouma") != 0)
                {
                    confuse -= 10;
                    SetConfuse();
                    fail.Play();

                }

            }
            catch (System.InvalidOperationException)
            {
                PlayerPrefs.SetInt("done", 0);
            }

        }
        PlayerPrefs.SetInt("done", 0);
        speed += 0.00019f;

        //Bouma mode is faster
        if (BoumaMode == "Speed Bouma")
            PlayerPrefs.SetFloat("Speed", speed + 0.01f);
        else
            PlayerPrefs.SetFloat("Speed", speed);
        seperator++;
        if (seperator > 100) NewOne();



        ConfusLeft = PlayerPrefs.GetInt("ConfusLeft");
        SlowDownLeft = PlayerPrefs.GetInt("SlowDownLeft");
        SetConfusLeft();
        SetSlowDownLeft();

        if (confuse < 0)
        {
            SceneManager.LoadScene("Game Over");
            PlayerPrefs.SetInt("Score", score);
            Analytics.CustomEvent("Death",new Dictionary<string,object>
                      {
                            { "Bouma Mode", BoumaMode},
                            {"Score",score }
                         });

        }
    }
//here

    }

    void DeactivateSound()
    {
         fail.Stop();
         Background.Stop();
         GoodSelection.Stop();

    }
    void SwitchButtons()
    {
        TriangleB.gameObject.SetActive(false);
        X.gameObject.SetActive(false);
        SquareB.gameObject.SetActive(false);
        O.gameObject.SetActive(false);

        TriangleB1.gameObject.SetActive(true);
        X1.gameObject.SetActive(true);
        SquareB1.gameObject.SetActive(true);
        O1.gameObject.SetActive(true);


    }

    void SetButtons()
    {
        TriangleB.gameObject.SetActive(true);
        X.gameObject.SetActive(true);
        SquareB.gameObject.SetActive(true);
        O.gameObject.SetActive(true);

        TriangleB1.gameObject.SetActive(false);
        X1.gameObject.SetActive(false);
        SquareB1.gameObject.SetActive(false);
        O1.gameObject.SetActive(false);


    }

    //Setting Stuff up
    void SetScore()
    {
        ScoreText.text = "Score :" + score.ToString();


    }
    void SetConfuse()
    {
        ConfuseText.text = "Sanity : " + confuse.ToString();

    }

    void SetBoumaMode()
    {
        BoumaModeText.text = BoumaMode;
    }

    void SetConfusLeft()
    {
        ConfusLeftText.text = ConfusLeft.ToString() + "/3";

    }

    void SetSlowDownLeft()
    {
        SlowDownLeftText.text = SlowDownLeft.ToString() + "/3";

    }


    //Power Ups Click
  


   

    //PowerUpsButons
    public void PowerUpAddSanity()
    {   if (ConfusLeft > 0) {
            confuse += 15;
            SetConfuse();
            ConfusLeft--;
            PlayerPrefs.SetInt("ConfusLeft", ConfusLeft);
            Analytics.Transaction("Confusion", 15, "Confu$", null, null);

            SetConfusLeft();

          }
   }

    public void PowerUpSlowDown()
    {   if (SlowDownLeft > 0)
        {
            speed = speed * 0.3f;
            SlowDownLeft--;
            PlayerPrefs.SetInt("SlowDownLeft", SlowDownLeft);
            Analytics.Transaction("SlowDown", 3, "Time%", null, null);

            SetSlowDownLeft();

        }
    }
    void NewOne()
    {

        seperator = 0;
        next = Random.Range(1, 5);
        powerUp = Random.Range(1, 9);

        if (next == 1) /*Up.gameObject.SetActive(true);*/
        {
            GameObject myUp = Instantiate(Up, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            myUp.transform.SetParent(Canvas.transform);
            RectTransform myUPRT = (RectTransform)myUp.transform;
            myUPRT.anchoredPosition = new Vector2(0, 250f);

            last.Enqueue(myUp);

        }
        if (next == 2)
        {
            GameObject myDown = Instantiate(Down) as GameObject; myDown.transform.SetParent(Canvas.transform);
            RectTransform myDownRT = (RectTransform)myDown.transform;
            myDownRT.anchoredPosition = new Vector2(0, 250f);
            last.Enqueue(myDown);

        }
        if (next == 3)
        {
            GameObject myLeft = Instantiate(Left) as GameObject; myLeft.transform.SetParent(Canvas.transform);
            RectTransform myLeftRT = (RectTransform)myLeft.transform;
            myLeftRT.anchoredPosition = new Vector2(0, 250f);
            last.Enqueue(myLeft);

        }
        if (next == 4)
        {
            GameObject myRight = Instantiate(Right) as GameObject; myRight.transform.SetParent(Canvas.transform);
            RectTransform myRightRT = (RectTransform)myRight.transform;
            myRightRT.anchoredPosition = new Vector2(0, 250f);
            last.Enqueue(myRight);

        }
        //powerUp = PlayerPrefs.GetInt("NoPowerUp");

        if (powerUp == 3) { SetUpPowerUpHealth(); PlayerPrefs.SetInt("NoPowerUp", 0); }
        if (powerUp == 5) { SetUpPowerUpSlowDown(); PlayerPrefs.SetInt("NoPowerUp", 0);
    }

            //     for (int i = 5; i > 1; i--) last[i - 1] = last[i]; last[4] = next;
        }
    //SoundManagment Duh
    void SoundManagment()
    {
       
        if (BoumaMode=="Good Guy Bouma")
        {
            //DeactivateSound();
            Background = GoodGuyBackground;
            fail = GoodGuyBadSelection;
            GoodSelection = GoodGuyGoodSelection;

        }
        if (BoumaMode == "Stormy Bouma")
        {
            //DeactivateSound();
            Background = Confusebackground;
            fail = ConfuseBadSelection;
            GoodSelection = ConfuseGoodSelection;
        }

    
        if (BoumaMode=="Party Bouma")
        {
            //DeactivateSound();
            Background = PartyBackground;
            fail = PartyBadSelection;
            GoodSelection = PartyGoodselection;


        }
        if (BoumaMode=="Sleepy Bouma")
        {
           // DeactivateSound();
            Background = DisturbBackground;
            fail = DisturBadSelection;
        }
    }

    //Drawing Health&SlowDown
    void SetUpPowerUpHealth()
    {
        GameObject MyHealth = Instantiate(Health) as GameObject;
        MyHealth.transform.SetParent(Canvas.transform);
        RectTransform myHealthRT = (RectTransform)MyHealth.transform;
        myHealthRT.anchoredPosition = new Vector2(0, 250f);

    }
    void SetUpPowerUpSlowDown()
    {
        GameObject MySlowDown = Instantiate(SlowDown) as GameObject;
        MySlowDown.transform.SetParent(Canvas.transform);
        RectTransform MySlowDownRT = (RectTransform)MySlowDown.transform;
        MySlowDownRT.anchoredPosition = new Vector3(0, 250f);

    }



    //Buttons
     public void UpPress()
    {
        if (last.Count != 0)
        {

            try
            {
                GameObject upidi = last.Dequeue();
                if (upidi != null)
                    if (upidi.gameObject.transform.name.Contains("Triangle"))
                    {
                        Destroy(upidi);
                        GoodSelection.Play();
                    }
                    else
                    {
                        PlayerPrefs.SetInt("done", 1);
                        PlayerPrefs.SetInt("NoPowerUp", 1);
                        Destroy(upidi);
                        confuse -= 5;
                        SetConfuse();
                        fail.Play();
                    }
            }
            catch (System.InvalidOperationException)
            {
                // Debug.LogException(op, this);  
                SetConfuse();
                PlayerPrefs.SetInt("done", 1);
                PlayerPrefs.SetInt("NoPowerUp", 1);

            }
        }
    }  

     public void DownPress()
    {
        if (last.Count != 0)
        {
            try
            {
                GameObject downindi = last.Dequeue();
                if (downindi != null)
                    if (downindi.gameObject.transform.name.Contains("X"))
                {

                     Destroy(downindi);
                        GoodSelection.Play();
                    }
                else
                {
                    PlayerPrefs.SetInt("done", 1);
                    PlayerPrefs.SetInt("NoPowerUp", 1);
                    Destroy(downindi);
                    confuse -= 5;
                    SetConfuse();
                    fail.Play();

                    }
            }
            catch (System.InvalidOperationException )
            {
                // Debug.LogException(op, this);
            }

        }
    }


    public void LeftPress()
    {
        if (last.Count != 0)
        {
            try
            {
                GameObject leftidi = last.Dequeue();
                if (leftidi != null)
                    if (leftidi.gameObject.transform.name.Contains("Square"))
                {

                    Destroy(leftidi);
                    GoodSelection.Play();
                    }
                else
                {
                    PlayerPrefs.SetInt("done", 1);
                    PlayerPrefs.SetInt("NoPowerUp", 1);
                    Destroy(leftidi);
                    confuse -= 5;
                    SetConfuse();
                    fail.Play();

                    }
            }

            catch (System.InvalidOperationException )
            {
                // Debug.LogException(op, this);
            }
        }
    }

   public  void RightPress()
    {
        if (last.Count != 0)
        {
            try
            {
                GameObject rightidi = last.Dequeue();
                if (rightidi != null)
                    if (rightidi.gameObject.transform.name.Contains("O"))
                {

                     Destroy(rightidi);
                     GoodSelection.Play();


                    }
                else
                {
                    PlayerPrefs.SetInt("done", 1);
                    PlayerPrefs.SetInt("NoPowerUp", 1);
                    Destroy(rightidi);
                    confuse -= 5;
                    SetConfuse();
                    fail.Play();
                    }
            }
            catch (System.InvalidOperationException )
            {
                // Debug.LogException(op, this);
            }
        }
    }


    //Buttons v2.0 /*
     public void UpButton()
    {
        if (BoumaMode == "Good Guy Bouma" || BoumaMode == "Party Bouma") UpPress();
        if (BoumaMode == "Stormy Bouma") DownPress();
               
        if (BoumaMode == "Sleepy Bouma") { confuse -= 15; SetConfuse(); fail.Play(); }

    }

    public void DownButton()
    {
        if (BoumaMode == "Good Guy Bouma" || BoumaMode == "Party Bouma") DownPress();
        if (BoumaMode == "Stormy Bouma") UpPress();
        if (BoumaMode == "Sleepy Bouma") { confuse -= 15; SetConfuse(); fail.Play(); }
    }

    public void LeftButton()
    {
        if (BoumaMode == "Good Guy Bouma" || BoumaMode == "Party Bouma") LeftPress();
        if (BoumaMode == "Stormy Bouma") RightPress();
        if (BoumaMode == "Sleepy Bouma") { confuse -= 15; SetConfuse(); fail.Play(); }
    }
    public void RightButton()
    {
        if (BoumaMode == "Good Guy Bouma"|| BoumaMode == "Party Bouma") RightPress();
        if (BoumaMode == "Stormy Bouma") LeftPress();
        if (BoumaMode == "Sleepy Bouma") { confuse -= 15; SetConfuse(); fail.Play(); }

    }

    //TUTO Buttons
   
    public void Gotit()
    {
        TUTO.gameObject.SetActive(false);
        PlayerPrefs.SetInt("TUTOint", 1);
    }
}
