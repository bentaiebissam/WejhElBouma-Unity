using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConfuseScript : MonoBehaviour
{
    

        Vector3 initalPosition;
        float speed;
        int done;
        int ConfusLeft;



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



            if (transform.position.y < 550f)
            {
                Destroy(gameObject);
            //Made it safely => adding the power-up 
            ConfusLeft = PlayerPrefs.GetInt("ConfusLeft");
                if (ConfusLeft < 3) ConfusLeft++;
                PlayerPrefs.SetInt("ConfusLeft", ConfusLeft);

            }

            done = PlayerPrefs.GetInt("NoPowerUp");
            if (done >= 1)
            {
                
                Destroy(gameObject);
                PlayerPrefs.SetInt("NoPowerUp", 0);
            }




        }
    }
