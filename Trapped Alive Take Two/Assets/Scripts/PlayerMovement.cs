using UnityEngine;
using System.Collections;

[System.Serializable]
public class HoveringLogic
{
    [SerializeField, Range(0, 5), Tooltip("The amount of height the player will be above the ground while floating")]
    public float heightMultiplier = 2;

    [SerializeField, Tooltip("Whether or not the player twists in air while floating")]
    public bool twisting;

    [SerializeField, Range(0, 10), Tooltip("The speed at which the player twists in air while floating")]
    public float rotationMultiplier = 3;

    [SerializeField, Range(0, 90), Tooltip("The distance (in degrees) the player twists in air while floating")]
    public float distanceOfRotation = 3;

    [SerializeField, Tooltip("Whether or not the player bobs in air while floating")]
    public bool bobbing;

    [SerializeField, Range(0, 50), Tooltip("The speed at which the player bobs in air while floating")]
    public float bobMultiplier = 2;

    [SerializeField, Range(0, 1), Tooltip("The highter this goes the less distance the player bobs in air while floating")]
    public float bobHeight = 0.055f;

    [HideInInspector]
    public bool isFloating;
}

[System.Serializable]
public class PlayerSprites
{
    [SerializeField, Tooltip("The sprite of the square")]
    public Sprite Square;

    [SerializeField, Tooltip("The sprite of the rectangle")]
    public Sprite Rectangle;

    [SerializeField, Tooltip("The sprite of the triangle")]
    public Sprite Triangle;

    [SerializeField, Tooltip("The sprite of the circle")]
    public Sprite Circle;
}

[System.Serializable]
public class PlayerStats
{

    [SerializeField, Tooltip("The starting letter of the starting shape (T = Triangle, R = Rectangle, S = Square, C = Circle)")]
    public char startShape = 'S';

    [SerializeField, Tooltip("The speed at which the square moves")]
    public float squareMovespeed = 300;

    [SerializeField, Tooltip("The speed at which the rectangle moves")]
    public float rectangleMovespeed = 150;

    [SerializeField, Tooltip("The speed at which the triangle moves")]
    public float triangleMovespeed = 150;

    [SerializeField, Tooltip("The speed at which the circle moves")]
    public float circleMovespeed = 150;

    [SerializeField, Tooltip("The height the rectangle will jump")]
    public float rectJumpHeight = 7;

    [SerializeField, Tooltip("The height the fallen rectangle will jump")]
    public float rectFallJumpHeight = 3;

    [SerializeField, Tooltip("The offset dimentions of the square collider (copy and paste this from the collider)")]
    public Vector2 squareColliderOffset = new Vector2(-0.00801706f, 6.02603e-05f);

    [SerializeField, Tooltip("The size dimentions of the square collider (copy and paste this from the collider)")]
    public Vector2 squareColliderSize = new Vector2(1.14614f, 1.166567f);

    [SerializeField, Tooltip("The offset dimentions of the rectangle collider (copy and paste this from the collider)")]
    public Vector2 rectangleColliderOffset = new Vector2(0.001571655f, 0.00452995f);

    [SerializeField, Tooltip("The size dimentions of the rectangle collider (copy and paste this from the collider)")]
    public Vector2 rectangleColliderSize = new Vector2(0.5038174f, 2.013272f);
}

[System.Serializable]
public class KeyPresses
{
    [SerializeField]
    public KeyCode rightKey;

    [SerializeField]
    public KeyCode leftKey;

    [SerializeField]
    public KeyCode jumpKey;

    [SerializeField]
    public KeyCode fallKey;
}

public class PlayerMovement : MonoBehaviour
{

    //The rigidbody2D attached to the player
    Rigidbody2D PlayerRigid;

    [SerializeField, Tooltip("The holder of all of the square hovering logic")]
    HoveringLogic hover;

    [SerializeField, Tooltip("The holder of all the sprites")]
    PlayerSprites sprites;

    [SerializeField, Tooltip("The holder of all the player variables such as move speed")]
    PlayerStats playerLogic;
        
    float MoveSpeed = 5;

    //The height the player will jump
    float JumpHeight = 7;
    //If the player can jump or not
    bool CanJump;
    //If the player is moving left or not
    [HideInInspector]
    public bool Left = false;
    //If the player is a square
    bool IsSquare;
    //If the player is a rectangle
    bool IsRect;
    //If the player is fallen or not
    bool Fallen;
    //The Sprite Renderer attached to the player
    SpriteRenderer PlayerSprite;
    //The box collider attached to the player, is used for square and rectangle collisions
    BoxCollider2D RectCollider;
    //The polygon collider attached to the player, is used for triangle collisions
    PolygonCollider2D TriCollider;
    //The circle collider attached to the plyaer, is used for circle collisions
    CircleCollider2D CirCollider;

    RaycastHit2D hit;

    // When the game starts
    void Start()
    {

        //Set these next variables to their components attached to the player
        PlayerRigid = GetComponent<Rigidbody2D>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        RectCollider = GetComponent<BoxCollider2D>();
        TriCollider = GetComponent<PolygonCollider2D>();
        CirCollider = GetComponent<CircleCollider2D>();

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

        hit = Physics2D.Raycast(this.transform.position, Vector2.down);

        //Change the player shape to the starting shape
        ChangeShape(char.ToUpper(playerLogic.startShape));
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
                    Move(-transform.up);
                }
                else
                {
                    Move(transform.right);
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
                        Move(transform.up);
                    }
                    else
                    {
                        Move(-transform.right);
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

        if (hover.isFloating)
        {
            this.transform.position = new Vector2(this.transform.position.x, hit.point.y + hover.heightMultiplier);
            if (hover.twisting)
            {
                this.transform.position = new Vector2(this.transform.position.x, hit.point.y + hover.heightMultiplier);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, hover.distanceOfRotation * Mathf.Sin(Time.time * hover.rotationMultiplier)));
            }

            if (hover.bobbing)
            {
                this.transform.position = new Vector2(this.transform.position.x, hit.point.y + hover.heightMultiplier + (Mathf.Sin(Time.time * hover.bobMultiplier) * hover.bobHeight));
            }
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        //Check if LEFT SHIFT is pressed
        if (Input.GetKeyDown(PlayerPrefs.GetString("Fall Key")))
        {
            //If the player is not a square but a rectangle
            if (IsRect)
            {
                //Make the rectangle fall
                RectFall();
            }
            else if (IsSquare)
            {
                hit = Physics2D.Raycast(transform.position, -Vector2.up);
                hover.isFloating = !hover.isFloating;
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
        PlayerSprite.sprite = sprites.Square;
        //Enable the rectangle collider and disable all the others
        RectCollider.enabled = true;
        TriCollider.enabled = false;
        CirCollider.enabled = false;
        //Refit the collider for the square
        RectCollider.offset = playerLogic.squareColliderOffset;
        RectCollider.size = playerLogic.squareColliderSize;
        //Set the jump height parameter for the square
        JumpHeight = 3;
        //Change the move speed for the square
        MoveSpeed = playerLogic.squareMovespeed;
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
        PlayerSprite.sprite = sprites.Rectangle;
        //Enable the rectangle collider and disable all the rest
        RectCollider.enabled = true;
        TriCollider.enabled = false;
        CirCollider.enabled = false;
        //Refit the collider for the rectangle
        RectCollider.offset = playerLogic.rectangleColliderOffset;
        RectCollider.size = playerLogic.rectangleColliderSize;
        //Set the jump height parameter for the rectangle
        JumpHeight = playerLogic.rectJumpHeight;
        //Change the move speed for the rectangle
        MoveSpeed = playerLogic.rectangleMovespeed;
        //The player is now a rectangle...
        IsRect = true;
        //...not a square
        IsSquare = false;
        //The rectangle is not fallen yet
        Fallen = false;
        //Set the rotation to 0, 0, 0
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /******************************************************************************
     *         The TO TRIANGLE function changes the player to a triangle          *
     ******************************************************************************/

    void ToTriangle()
    {
        //Change the sprite to a triangle
        PlayerSprite.sprite = sprites.Triangle;
        //Enable the triangle collider and disable the rest
        RectCollider.enabled = false;
        TriCollider.enabled = true;
        CirCollider.enabled = false;
        //The player is not a square...
        IsSquare = false;
        //...or a rectangle
        IsRect = false;
        MoveSpeed = playerLogic.triangleMovespeed;
    }

    /******************************************************************************
     *          The TO CIRCLE function changes the player to a circle             *
     ******************************************************************************/

    void ToCircle()
    {
        //Change the sprite to a circle
        PlayerSprite.sprite = sprites.Circle;
        //Enable the circle collider and disable the rest
        RectCollider.enabled = false;
        TriCollider.enabled = false;
        CirCollider.enabled = true;
        //The player is not a square...
        IsSquare = false;
        //...or a rectangle
        IsRect = false;
        MoveSpeed = playerLogic.circleMovespeed;
    }

    /******************************************************************************
     *        The RECT FALL function handles the falling of the rectangle         *
     ******************************************************************************/

    void RectFall()
    {
        //Rotate the rectangle by 90 degrees
        transform.rotation = Quaternion.Euler(0, 0, 90);
        //Set the jump height to the variable of the fallen jump height
        JumpHeight = playerLogic.rectFallJumpHeight;
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