using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // The player game object
    GameObject Player;

    //The rigidbody2D attached to the player
    Rigidbody2D PlayerRigid;

    [SerializeField]
    //The speed at which the player moves
    float MoveSpeed = 5;

    [SerializeField]
    //The height the player will jump
    float JumpHeight;
    //The previous move speed the player was at
    float PrevMoveSpeed;
    //The Gravity scale on the Y axis
    float GravityY;
    //If the player can jump or not
    bool CanJump;
    //If the player is moving left or not
    public bool Left = false;
    //If the player is a square
    bool IsSquare;
    //If the player is a rectangle
    bool IsRect;
    //If the player is fallen or not
    bool Fallen;
    //If the player on a ramp
    bool OnRamp;
    //The Sprite Renderer attached to the player
    SpriteRenderer PlayerSprite;
    //The box collider attached to the player, is used for square and rectangle collisions
    BoxCollider2D RectCollider;
    //The polygon collider attached to the player, is used for triangle collisions
    PolygonCollider2D TriCollider;
    //The circle collider attached to the plyaer, is used for circle collisions
    CircleCollider2D CirCollider;

    [SerializeField]
    [Header("T=Triangle S=Square R=Rectangle C=Circle")]
    //The shape the player will start as
    char StartShape;

    [SerializeField]
    //The sprite for the Square
    Sprite Square;

    [SerializeField]
    //The sprite for the Rectangle
    Sprite Rectangle;

    [SerializeField]
    //The sprite for the Triangle
    Sprite Triangle;

    [SerializeField]
    //The sprite for the Circle
    Sprite Circle;

    [SerializeField]
    //The jump height for the rectangle
    float RectJumpHeight = 7;

    [SerializeField]
    //The jump height for the fallen rectangle
    float FallJumpHeight = 3;

    [SerializeField]
    //The offset for the square version of the box collider
    Vector2 SquareOffset;

    [SerializeField]
    //The size for the square version of the box collider
    Vector2 SquareSize;

    [SerializeField]
    //The offset for the rectangle version of the box collider
    Vector2 RectangleOffset;

    [SerializeField]
    //The size for the rectangle version of the box collider
    Vector2 RectangleSize;

    public KeyCode RightKey;
    public KeyCode LeftKey;
    public KeyCode JumpKey;
    public KeyCode FallKey;

    // When the game starts
    void Start()
    {
        //Set the Player variable to this gameobject
        Player = this.gameObject;
        //Set these next variables to their components attached to the player
        PlayerRigid = Player.GetComponent<Rigidbody2D>();
        PlayerSprite = Player.GetComponent<SpriteRenderer>();
        RectCollider = Player.GetComponent<BoxCollider2D>();
        TriCollider = Player.GetComponent<PolygonCollider2D>();
        CirCollider = Player.GetComponent<CircleCollider2D>();

        if(PlayerPrefs.GetString("Move Right Key") == "")
        {
            PlayerPrefs.SetString("Move Right Key", "d");
        }

        if(PlayerPrefs.GetString("Move Left Key") == "")
        {
            PlayerPrefs.SetString("Move Left Key", "a");
        }

        if(PlayerPrefs.GetString("Jump Key") == "")
        {
            PlayerPrefs.SetString("Jump Key", "space");
        }

        if(PlayerPrefs.GetString("Fall Key") == "")
        {
            PlayerPrefs.SetString("Fall Key", "left shift");
        }
        

        //Change the player shape to the starting shape
        ChangeShape(StartShape);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

            //Check if theres any right input
            if ((Input.GetKey(PlayerPrefs.GetString("Move Right Key"))))
            {
            if (CanJump)
            {
                //Move right
                if (Fallen)
                {
                    Move(-Player.transform.up);
                }
                else
                {
                    Move(Player.transform.right);
                }
                Left = false;
            }
            else
            {
                if ((Left) && PlayerRigid.velocity.x <=0)
                {
                    PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x + 20 * Time.deltaTime, PlayerRigid.velocity.y);
                }
            }
            }
            //Check if theres any left input
            else if ((Input.GetKey(PlayerPrefs.GetString("Move Left Key").ToLower())))
            {
                if (CanJump)
                {
                    //Move left
                    if (Fallen)
                    {
                        Move(Player.transform.up);
                    }
                    else
                    {
                        Move(-Player.transform.right);
                    }
                    Left = true;
                }
                else
                {
                    if ((Left == false) && PlayerRigid.velocity.x >=0)
                    {
                        PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x - 20 * Time.deltaTime, PlayerRigid.velocity.y);
                    }
                }
            }

        //Check if SPACE is pressed
        if (Input.GetKeyDown(PlayerPrefs.GetString("Jump Key")))
        {
            //Make sure the player is not a triangle
            if (TriCollider.enabled == false)
            {
                //Jump
                Jump();
            }
            //If the player is a triangle
            else
            {
                //Check if its on the ground
                if (CanJump)
                {
                    //Start the wall jump
                    TriangleWallJump(Left);
                }
            }
        }

        //Check if LEFT SHIFT is pressed
        if (Input.GetKey(PlayerPrefs.GetString("Fall Key")))
        {
            //If the player is not a square but a rectangle
            if (IsRect)
            {
                //Make the rectangle fall
                RectFall();
            }
        }
        //Check if the player stops pressing the horizontal move keys
        if (CanJump)
        {
            if (Input.GetKeyUp(PlayerPrefs.GetString("Move Right Key")) || Input.GetKeyUp(PlayerPrefs.GetString("Move Left Key")))
            {
                //Stop the player movement
                PlayerRigid.velocity = new Vector2(0, PlayerRigid.velocity.y);
            }
        }
    }

    /******************************************************************************
     *    The MOVE function handles the movement of the player based on input     *
     ******************************************************************************/
    void Move(Vector3 Direction)
    {
        //Move the player based on the direction provided by the Update function
        PlayerRigid.velocity = new Vector2(Direction.x * MoveSpeed * Time.deltaTime, PlayerRigid.velocity.y);
    }

    /******************************************************************************
     *          The JUMP function handles how the different shapes jump           *
     ******************************************************************************/
    void Jump()
    {
        //If the player can jump
        if (CanJump)
        {
            //If the player is a square
            if (IsSquare)
            {
                    //Jump
                    PlayerRigid.velocity = new Vector2(2 * PlayerRigid.velocity.x, JumpHeight);
                    //The player can no longer jump
                    CanJump = false;
                Debug.Log(2 * PlayerRigid.velocity.x);
            }
            //If the player is not a square
            else
            {
                //Jump
                PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, JumpHeight);
                //The player can no longer jump
                CanJump = false;
            }
        }
    }

    /******************************************************************************
     *     The STOP MOVEMENT function stops the player from being able to move    *
     ******************************************************************************/

    void StopMovement()
    {
        PrevMoveSpeed = MoveSpeed;
        MoveSpeed = 0;
    }

    /******************************************************************************
     *The CONTINUE MOVEMENT function is the opposite of STOP MOVEMENT and lets the*
     *                             player move again                              *
     ******************************************************************************/

    void ContinueMovement()
    {
        MoveSpeed = PrevMoveSpeed;
    }

    /******************************************************************************
     *   The CHANGE SHAPE function handles changing the shape based on a letter   *
     ******************************************************************************/

    void ChangeShape(char Shape)
    {
        //If the shape letter is 'S'
        if (Shape == 'S')
        {
            //Change the player to square
            ToSquare();
        }
        //If the shape letter is 'R'
        else if (Shape == 'R')
        {
            //Change the player to rectangle
            ToRectangle();
        }
        //If the shape letter is 'T'
        else if (Shape == 'T')
        {
            //Change the player to triangle
            ToTriangle();
        }
        //If the shape letter is 'C'
        else if (Shape == 'C')
        {
            //Change the player to circle
            ToCircle();
        }
    }

    /******************************************************************************
     *  The TRIANGLE WALL JUMP function handles the triangle wall jumping ability *
     ******************************************************************************/

    void TriangleWallJump(bool JumpLeft)
    {
        //If the player is a triangle
        if (TriCollider.enabled == true)
        {
            //The player cant jump
            CanJump = false;
            //If the player is moving down
            if (PlayerRigid.velocity.y < 0)
            {
                //If the player should jump left
                if (JumpLeft)
                {
                    //Jump down and left
                    PlayerRigid.velocity = new Vector2(-3, -7);
                }
                //If the player should jump right
                else
                {
                    //Jump down and right
                    PlayerRigid.velocity = new Vector2(3, -7);
                }
            }
            //If the player is moving up
            else
            {
                //If the player should jump left
                if (JumpLeft)
                {
                    //Jump up and left
                    PlayerRigid.velocity = new Vector2(-3, 7);
                }
                //If the player should jump right
                else
                {
                    //Jump up and right
                    PlayerRigid.velocity = new Vector2(3, 7);
                }
            }
        }
    }

    /******************************************************************************
     *          The TO SQUARE function changes the player to a square             *
     ******************************************************************************/

    void ToSquare()
    {
        //Change the sprite to a square
        PlayerSprite.sprite = Square;
        //Enable the rectangle collider and disable all the others
        RectCollider.enabled = true;
        TriCollider.enabled = false;
        CirCollider.enabled = false;
        //Refit the collider for the square
        RectCollider.offset = SquareOffset;
        RectCollider.size = SquareSize;
        //Set the jump height parameter for the square
        JumpHeight = 3;
        //Change the move speed for the square
        MoveSpeed = 300;
        //The player is now a square...
        IsSquare = true;
        //...not a rectangle
        IsRect = false;
    }

    /******************************************************************************
     *        The TO RECTANGLE function changes the player to a rectangle         *
     ******************************************************************************/

    void ToRectangle()
    {
        //Change the sprite to a rectangle
        PlayerSprite.sprite = Rectangle;
        //Enable the rectangle collider and disable all the rest
        RectCollider.enabled = true;
        TriCollider.enabled = false;
        CirCollider.enabled = false;
        //Refit the collider for the rectangle
        RectCollider.offset = RectangleOffset;
        RectCollider.size = RectangleSize;
        //Set the jump height parameter for the rectangle
        JumpHeight = RectJumpHeight;
        //Change the move speed for the rectangle
        MoveSpeed = 150;
        //The player is now a rectangle...
        IsRect = true;
        //...not a square
        IsSquare = false;
        //The rectangle is not fallen yet
        Fallen = false;
        //Set the rotation to 0, 0, 0
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /******************************************************************************
     *         The TO TRIANGLE function changes the player to a triangle          *
     ******************************************************************************/

    void ToTriangle()
    {
        //Change the sprite to a triangle
        PlayerSprite.sprite = Triangle;
        //Enable the triangle collider and disable the rest
        RectCollider.enabled = false;
        TriCollider.enabled = true;
        CirCollider.enabled = false;
        //The player is not a square...
        IsSquare = false;
        //...or a rectangle
        IsRect = false;
    }

    /******************************************************************************
     *          The TO CIRCLE function changes the player to a circle             *
     ******************************************************************************/

    void ToCircle()
    {
        //Change the sprite to a circle
        PlayerSprite.sprite = Circle;
        //Enable the circle collider and disable the rest
        RectCollider.enabled = false;
        TriCollider.enabled = false;
        CirCollider.enabled = true;
        //The player is not a square...
        IsSquare = false;
        //...or a rectangle
        IsRect = false;
    }

    /******************************************************************************
     *        The RECT FALL function handles the falling of the rectangle         *
     ******************************************************************************/

    void RectFall()
    {
        //Rotate the rectangle by 90 degrees
        Player.transform.rotation = Quaternion.Euler(0, 0, 90);
        //Set the jump height to the variable of the fallen jump height
        JumpHeight = FallJumpHeight;
        //The rectangle is now fallen
        Fallen = true;
    }

    //This function is called when this game object collides with anything
    void OnCollisionEnter2D(Collision2D Col)
    {
        //If the collision is with an object with a tag 'Ground'
        if (Col.gameObject.tag == "Ground")
        {
            //The player can now jump
            CanJump = true;
            //Stop the movement so it doesn't look like its slipping
            PlayerRigid.velocity = new Vector2(0, PlayerRigid.velocity.y);
        }
        //If the collision is with an object with a tag 'Rectangle Changer'
        else if (Col.gameObject.tag == "Rectangle Changer")
        {
            //Change the players shape to a rectangle
            ChangeShape('R');
        }
        //If the collision is with an object with a tag 'Triangle Changer'
        else if (Col.gameObject.tag == "Triangle Changer")
        {
            //Change the players shape to a triangle
            ChangeShape('T');
        }
        //If the collision is with an object with a tag 'Square Changer'
        else if (Col.gameObject.tag == "Square Changer")
        {
            //Change the players shape to a square
            ChangeShape('S');
        }
        //If the collision is with an object with a tag 'Circle Changer'
        else if (Col.gameObject.tag == "Circle Changer")
        {
            //Change the players shape to a circle
            ChangeShape('C');
        }
        //If the collision is with an object with a tag 'Left Wall'
        else if (Col.gameObject.tag == "Left Wall")
        {
            //Start the wall jump right
            TriangleWallJump(false);
        }
        //If the collision is with an object with a tag 'Right Wall'
        else if (Col.gameObject.tag == "Right Wall")
        {
            //Start the wall jump left
            TriangleWallJump(true);
        }

    }
}