using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Rigidbody2D Fox_rb;
    float horizontalMovementValue/*, verticalMovementValue*/;
    [SerializeField] int FoxSpeed = 100;
    bool foxFacingRight = true;
    [SerializeField] bool isRunning = false;
    float runningSpeedModifier = 2;
    Animator foxAnimator;
    [SerializeField] bool isGrounded = false;
    [SerializeField] Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int jumpPower = 10;

    [SerializeField] bool crouchPressed = false;
    [SerializeField] Collider2D standingCollider, crouchingCollider;
    float crouchingSpeedModifier = 0.5f;
    [SerializeField] Transform overheadCheckCollider;
    const float overheadCheckRadius = 0.25f;
    [SerializeField] bool isDead = false;


    #region Unity_Functions
    void Awake()
    {
        Fox_rb = GetComponent<Rigidbody2D>();
        foxAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isDead /*|| CanMove()*/)
            return;

        horizontalMovementValue = Input.GetAxisRaw("Horizontal");     //-1 = left   --   0 = nothing   --   1 = right
        //verticalMovementValue = Input.GetAxisRaw("Vertical");

        /*Running*/
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        /*Jumping*/
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
            

        /*Crouching*/
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) crouchPressed = true;
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) crouchPressed = false;

        /*Jumping Animation*/
        foxAnimator.SetFloat("yVelocity", Fox_rb.velocity.y);         //-------
        //foxAnimator.SetFloat("yVelocity", verticalMovementValue);

        

    }
    void FixedUpdate()
    {
        GroundCheck();
        MoveFox(horizontalMovementValue, crouchPressed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
    } 
    #endregion

    #region UserDefined_Functions
    void GroundCheck()
    {
        isGrounded = false;
        //check if this point is overlapping/colliding with anything that has a ground layer in a 0.025 radius and return them in an array
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)  //if this array has elements = fox is grounded
        {
            isGrounded = true;

            //check if any of the colliders is a moving platform
            //if there are any, parent it to the player
            foreach (var c in colliders)
            {
                if (c.tag.Equals("MovingPlatform"))
                    transform.parent = c.transform;
            }
        }
        else
        {
            transform.parent = null;
        }

        /*Animation*/
        foxAnimator.SetBool("Jump", !isGrounded);   //Jumping is the opposite of being grounded
    }

    void MoveFox(float direction, bool CrouchFlag /*enable this to crouch*/)
    {
        #region Move & Run
        //Moving the Fox
        if (isRunning && !crouchPressed)   //Running only
        {
            Fox_rb.velocity = new Vector2(runningSpeedModifier * direction * FoxSpeed * Time.fixedDeltaTime, Fox_rb.velocity.y);      //-------
            //transform.Translate(new Vector3(direction * Time.deltaTime * FoxSpeed / 50 * runningSpeedModifier, 0, 0));
        }
        else if (crouchPressed)         //Crouching only
        {
            Fox_rb.velocity = new Vector2(crouchingSpeedModifier * direction * FoxSpeed * Time.fixedDeltaTime, Fox_rb.velocity.y);    //-------
            //transform.Translate(new Vector3(direction * Time.deltaTime * FoxSpeed / 50 * crouchingSpeedModifier, 0, 0));
        }
        else   //Walking
        {
            Fox_rb.velocity = new Vector2(direction * FoxSpeed * Time.deltaTime, Fox_rb.velocity.y);      //-------
            //transform.Translate(new Vector3(direction * Time.deltaTime * FoxSpeed / 50, 0, 0));
        }


        /*Flipping the sprite*/
        if (foxFacingRight && direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            foxFacingRight = false;
        }
        else if (!foxFacingRight && direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            foxFacingRight = true;
        }
        //Debug.Log(Fox_rb.velocity.x);   //0 idle,   2 walking,   4 running
        
        /*Animations*/
        foxAnimator.SetFloat("xVelocity", Mathf.Abs(Fox_rb.velocity.x));      //-------
        //foxAnimator.SetFloat("xVelocity", horizontalMovementValue);
        #endregion

        #region Crouch

        //if we are crouching + disabled crouching
        //check overhead for collision with ground items
        //if there are any, remain crouched, otherwise un-crouch
        if (!CrouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                //Debug.Log("OverheadOverlapping");
                CrouchFlag = true;
            }
        }
        
        /*Crouching*/
        //grounded + Press S = disable the standing collider + begin crouching animation
        //reduce speed
        //not grounded + Release S = resume original speed + enable standing collider + disable crouching animation
        standingCollider.enabled = !CrouchFlag;
        crouchingCollider.enabled = CrouchFlag;
        /*Crouch Animation*/
        foxAnimator.SetBool("Crouch", CrouchFlag);
        #endregion
    }

    void Jump()
    {
        if (isGrounded)
        {
            /*Jumping*/
            Fox_rb.velocity = Vector2.up * jumpPower;
            //transform.Translate(new Vector3(0, 1 * Time.deltaTime * jumpPower * 50, 0));     //-------
            foxAnimator.SetBool("Jump", true);
        }
    }

    /*
    bool CanMove()
    {
        bool can = true;
        if (isDead)
            can = false;
        return can;
    }
    */
    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        horizontalMovementValue = 0;
        isDead = false;
    }

    public void Win()
    {
        //Move to next scene
        Debug.Log("You won");
    }

    public void ForceStopForWinning()
    {
        FoxSpeed = 0;
    }
    #endregion

}
