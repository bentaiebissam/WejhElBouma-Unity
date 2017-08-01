using UnityEngine;
using System.Collections;

public class MovingArrow : MonoBehaviour {

    Vector3 initalPosition;
    float speed;
    
    

	// Use this for initialization
	void Start () {
         speed = PlayerPrefs.GetFloat("Speed");
    }
	
	// Update is called once per frame
	void Update () {
        //Translating arrows to the left
        speed = PlayerPrefs.GetFloat("Speed");
        transform.position = new Vector2(transform.position.x , transform.position.y - 2f - 20f * speed);

       
        //  Debug.Log(transform.position.x);
        if (transform.position.y < 600f)
        {
        //    Debug.Log("Jinéheshi?");
          PlayerPrefs.SetInt("done", 2);
            PlayerPrefs.SetInt("AcrossTheEdge", 2);
         //   Destroy(gameObject);


        }




    }
}
