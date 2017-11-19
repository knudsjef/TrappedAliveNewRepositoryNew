using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    GameObject TopBound;

    [SerializeField]
    GameObject BottomBound;

    [SerializeField]
    GameObject RightBound;

    [SerializeField]
    GameObject LeftBound;

    [SerializeField]
    GameObject Player;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    /*void Update () {

        
        this.transform.position = new Vector3(Mathf.Clamp(GameObject.Find("Player").transform.position.x, LeftBound.transform.position.x, RightBound.transform.position.x), Mathf.Clamp(GameObject.Find("Player").transform.position.y, BottomBound.transform.position.y, TopBound.transform.position.y), this.transform.position.z);
        Mathf.Clamp(this.transform.position.x, LeftBound.transform.position.x, RightBound.transform.position.x);
        Mathf.Clamp(this.transform.position.y, BottomBound.transform.position.y, TopBound.transform.position.y);
        
        
	}*/
    private void LateUpdate()
    {

    }

}
