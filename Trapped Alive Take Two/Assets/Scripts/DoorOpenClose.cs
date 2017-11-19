using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour {


    float YScale = 1;

    [SerializeField]
    GameObject Control;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Control.GetComponent<Lever>().On)
        {
            if(this.transform.localScale.y > 0)
            {
                YScale -= 2 * Time.deltaTime;
                this.transform.localScale = new Vector2(this.transform.localScale.x, YScale);
            }
        }
        else
        {
            if(this.transform.localScale.y < 1)
            {
                YScale += 2 * Time.deltaTime;
                this.transform.localScale = new Vector2(this.transform.localScale.x, YScale);
            }
        }

	}
}
