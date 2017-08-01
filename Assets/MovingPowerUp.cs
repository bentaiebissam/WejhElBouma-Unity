using UnityEngine;
using System.Collections;

public class MovingPoweruP : MonoBehaviour
{
    //Confuse 
    Vector3 initalPosition;
    float speed;
    int done;
    int ConfusLeft;



    // Use this for initialization
    void Start()
    {
        speed = PlayerPrefs.GetFloat("Speed");
    }

    
    void Update()
    {
        //Translating arrows to the left
        transform.position = new Vector2(transform.position.x - 2f - 20f * speed, transform.position.y);


        
        if (transform.position.x < 250)
        {
            Destroy(gameObject);
            //Made it safely => adding the power-up 
            ConfusLeft = PlayerPrefs.GetInt("ConfusLeft");
            ConfusLeft = (ConfusLeft < 3) ? ConfusLeft++ : 3;
            PlayerPrefs.SetInt("ConfuseLeft", ConfusLeft);

        }
       done= PlayerPrefs.GetInt("NoPowerUp");
        if (done >= 1)
        {
            Debug.Log("I destroyed PowerUP");
            Destroy(gameObject);
            PlayerPrefs.SetInt("NoPowerUp", 0);
        }




    }
}
