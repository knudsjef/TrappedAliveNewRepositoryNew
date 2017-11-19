using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicks : MonoBehaviour {

    //[SerializeField]
    //GameObject OffLight;

    //[SerializeField]
    //GameObject OnLight;

    [SerializeField]
    public GameObject[] Lights = new GameObject[7];

    [SerializeField]
    Sprite OnLight;
    public bool Go;

    float TimeAmount;


	// Use this for initialization
	void Start () {

        //Start with the light flicks off until player is in the room
        Go = false;
        

        //Set all of the lights to off until player is in the room
        foreach(GameObject Light in Lights)
        {
            //Light.GetComponent<Light>().enabled = false; 
        }
	}
	
	// Update is called once per frame
	void Update () {

        //If player is in the room
        if (Go)
        {
            //Start counting for lights
            TimeAmount += 2 * Time.deltaTime;
            //Turn on lights corresponding with time
            Lights[Mathf.Clamp(Mathf.RoundToInt(TimeAmount), 0, 6)].transform.GetChild(3).transform.GetChild(0).GetComponent<Light>().enabled = true;
            Lights[Mathf.Clamp(Mathf.RoundToInt(TimeAmount), 0, 6)].transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = OnLight;
            if(TimeAmount >= 6)
            {
                Go = false;
            }
        }

	}

    //This function handles collisions
    void OnCollisionEnter2D(Collision2D Col)
    {
        //If the collision is tagged as Ground
        if(Col.gameObject.tag == "Ground")
        {
            //The player is in the room and the lights can start being flipped on
            Go = true;
        }
    }
}
