using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{

    //If the lever is on or off
    public bool On;

    //The position it starts at
    char StartPos;

    bool Flipped = false;

    // When the game starts
    void Start()
    {

        //If the lever is inverted
        if (this.transform.localScale.x == -1)
        {
            //It starts left
            StartPos = 'L';
        }
        //If the lever is normal
        else
        {
            //The lever starts right
            StartPos = 'R';
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Flipped && StartPos == 'L' && Input.GetKeyDown(KeyCode.D))
        {
            SwitchIO();
        }
        else if(Flipped && StartPos == 'R' && Input.GetKeyDown(KeyCode.A))
        {
            SwitchIO();
        }
    }

    /******************************************************************************
     *          The SWITCH IO function handles turning on and off the lever       *
     ******************************************************************************/

    void SwitchIO()
    {
        //Flip the lever
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        Flipped = true;
        //If the lever is on
        if (On)
        {
            //Turn the lever off
            On = false;
        }
        //If the lever is off
        else
        {
            //Turn the lever on
            On = true;
        }

        //If the lever is Left
        if (StartPos == 'L')
        {
            //Turn it Right
            StartPos = 'R';
        }
        //If the lever is Right
        else
        {
            //Turn it Left
            StartPos = 'L';
        }
    }

    //This function is called when this game object collides with anything
    void OnTriggerEnter2D(Collider2D Col)
    {
        //If the player collides with this game object
        if (Col.transform.name == "Player")
        {
            //Check that the player is hitting it from the correct side
            if ((StartPos == 'L' && !Col.transform.GetComponent<PlayerMovement>().Left) || (StartPos == 'R' && Col.transform.GetComponent<PlayerMovement>().Left))
                //Turn the lever on or off
                SwitchIO();
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if(Col.transform.name == "Player")
        {
            Flipped = false;
        }
    }
}
