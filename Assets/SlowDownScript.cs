using UnityEngine;
using System.Collections;

public class SlowDownScript : MonoBehaviour {

    Vector3 initalPosition;
    float speed;
    int done;
    int SlowDownLeft;
    int edge;



    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("AcrossTheEdge", 0);
        speed = PlayerPrefs.GetFloat("Speed");
    }


    void Update()
    {
        //Translating arrows to the left
        transform.position = new Vector2(transform.position.x , transform.position.y - 2f - 20f * speed);



        if (transform.position.y<500f)
        {
            Destroy(gameObject);
            //Made it safely => adding the power-up 
            SlowDownLeft = PlayerPrefs.GetInt("SlowDownLeft");
            if (SlowDownLeft < 3) SlowDownLeft++;
            PlayerPrefs.SetInt("SlowDownLeft", SlowDownLeft);

        }

        done = PlayerPrefs.GetInt("NoPowerUp");
        edge = PlayerPrefs.GetInt("AcrossTheEdge");
        if (done >= 1 || edge==2)
        {
        //    Debug.Log("I destroyed PowerUP");
            Destroy(gameObject);
            PlayerPrefs.SetInt("NoPowerUp", 0);
            PlayerPrefs.SetInt("AcrossTheEdge", 0);
        }
    }
}
