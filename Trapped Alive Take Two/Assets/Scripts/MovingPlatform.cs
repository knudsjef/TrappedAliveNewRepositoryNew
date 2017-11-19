using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    //Whether or not the platform is running
    bool Running;

    [SerializeField]
    //Whether or not the platform is moving up
    bool Up;

    [SerializeField]
    [Header("If no lever leave blank!")]
    //The lever that controls this platform
    GameObject Lever;

    [SerializeField]
    [Header("The speed at which the platform moves.")]
    //The speed at which the platform moves
    float MoveSpeed;

    [SerializeField]
    [Header("The time in sec the elevator will pause.")]
    //The time in seconds the elevator will pause
    float PauseTime;

    //Whether or not the platform needs to pause now
    bool Pause;
    //The amount of time the platform is paused
    float ElapsedTime;

    // When the game starts
    void Start()
    {

        //Make sure the platform will move
        if (MoveSpeed == 0)
        {
            MoveSpeed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //If the platform is currently running
        if (Running)
        {
            //If the platform doesn't need to pause now
            if (Pause == false)
            {
                //If the platform is moving up
                if (Up)
                {
                    //Move towards the top point
                    this.transform.Find("Platform").position = Vector3.MoveTowards(this.transform.Find("Platform").position, this.transform.Find("TopPoint").position, Time.deltaTime * MoveSpeed);
                    //If the distance between the top point and the platform is less than 0.1
                    if (Vector2.Distance(this.transform.Find("TopPoint").position, this.transform.Find("Platform").position) < 0.1)
                    {
                        //Pause the platform
                        Pause = true;
                        //Start the platform moving down
                        Up = false;
                    }
                }
                //If the platform is moving down
                else
                {
                    //Move towards the bottom point
                    this.transform.Find("Platform").position = Vector3.MoveTowards(this.transform.Find("Platform").position, this.transform.Find("BottomPoint").position, Time.deltaTime * MoveSpeed);
                    //If the distance between the bottom point and the platform is less than 0.1
                    if (Vector2.Distance(this.transform.Find("BottomPoint").position, this.transform.Find("Platform").position) < 0.1)
                    {
                        //Pause the platform
                        Pause = true;
                        //Start the platform moving up
                        Up = true;
                    }
                }
            }
            //If the platform needs to pause now
            else
            {
                //Add to the variable equal to the amount of time passed
                ElapsedTime += Time.deltaTime;
                //If Elapsed Time is greater than or equal to Pause Time
                if (ElapsedTime >= PauseTime)
                {
                    //Unpause the platform
                    Pause = false;
                    //Reset Elapsed Time
                    ElapsedTime = 0.0f;
                }
            }
            //If there is a lever
            if (Lever != null)
            {
                //If the lever is not on
                if (!Lever.GetComponent<Lever>().On)
                {
                    //The platform is not running
                    Running = false;
                }
            }
        }
        //If the platform is not running
        else
        {
            //If there is a lever
            if (Lever != null)
            {
                //If the lever is on
                if (Lever.GetComponent<Lever>().On)
                {
                    //The platform is running
                    Running = true;
                }
            }
        }
    }
}
