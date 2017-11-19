using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScrolling : MonoBehaviour {

    private const float PARALLAX_SPEED = 0.01f;

    [SerializeField]
    Rigidbody2D Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Player.velocity.x);

        this.transform.position = new Vector2(this.transform.position.x + (PARALLAX_SPEED * Player.velocity.x), this.transform.position.y + (PARALLAX_SPEED * Player.velocity.y));
	}
}
