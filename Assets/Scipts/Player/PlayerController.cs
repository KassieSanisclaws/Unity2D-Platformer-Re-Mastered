using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Declare Required Componenets: 
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Test Mode Toggle;
      public bool testMode = true;

    //Component
      public Rigidbody2D rb;
      public SpriteRenderer sr;
      Animator animate;


    //Inspector vector variables:
    [SerializeField] float runSpeed = 7.0f;
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float airWalkSpeed = 3.0f;
    [SerializeField] int jumpForce = 10;

    //Ground Check:
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] bool isGroundedTarget;

    //MoveInput: 
    //Vector2 moveInput;
    TouchingDirections td;


    public float CurrentMovementSpeed
    {
        get
        {
            if (PlayerCanMove)
            {
                if (IsMoving && !td.IsOnWall)
                {
                    if (td.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        //Air Movement:
                        return airWalkSpeed;
                    }
                }
                else
                {
                    //Idle speed which is 0:
                    return 0;
                }
            }
            else
            {
                //Movement locked:
                return 0;
            }
        }
    }

    [SerializeField] private bool _isMoving = false;

    public bool IsMoving 
    { 
      get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animate.SetBool(AnimationVariable_Strings.AnimationVariable.ISMOVING, value);
        }
    }

    [SerializeField] private bool _isRunning = false;
    
    public bool IsRunning
    {
        get
        {
           return _isRunning;
        }

        set
        {
            _isRunning = value;
            animate.SetBool(AnimationVariable_Strings.AnimationVariable.ISRUNNING, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        
        private set
        {
            //Flip only if value is new or random
             if (_isFacingRight != value)
            {
                //Flip the local scale to havce the player position face to the opposite direction:
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool PlayerCanMove
     {
       get
        {
            return animate.GetBool(AnimationVariable_Strings.AnimationVariable.PLAYERCANMOVE);
        }
     }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
        td = GetComponent<TouchingDirections>();
  

        if (groundCheck == null)
        {
            // Creates Ground Cehcl object if not assigned 
            CreateGroundCheckObject();
        }
    }

     //Create Ground Check Object.
     private void CreateGroundCheckObject()
    {
        GameObject obj = new ("groundCheck");
        obj.transform.SetParent(gameObject.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.name = "groundCheck";
        groundCheck = obj.transform;

        if(testMode) Debug.Log("Groundcheck object was created on" + gameObject.name);
    } 

    // Update is called once per frame
    private void Update()
    {
        //Functions: 
        GroundCheck();
        MovementInput();
        JumpInput();
        AttackInput();
    }


    //Update Player Movement:
    void MovementInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * runSpeed, rb.velocity.y);

        Debug.Log(xInput);

        //Animator Input Checking:
          animate.SetFloat("Input", Mathf.Abs(xInput));

        //Sprite FLipping on the x verticies: 
        if (xInput != 0) sr.flipX = (xInput < 0);

        IsMoving = xInput == 0; //Revisit and debug.
    }

    //Update Player Jump Force: 
    void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && PlayerCanMove) //May have to remove PlayerCanMove variable 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            //Set Trigger of annimation for jump. 
            animate.SetTrigger("Jump");

            //Set Y Velocity when player is in air when falling or when from jump button pressed and is in air state.
            animate.SetFloat("yVelocity", rb.velocity.y);
        }
    }

    //Attack Input Animation Meele: 
   void AttackInput()
    {
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            animate.SetTrigger("Attack");

            // Get current animation clip information
            AnimatorClipInfo[] clipInfo = animate.GetCurrentAnimatorClipInfo(0);


            // Check if the current animation clip is named "Fire"
            if (clipInfo.Length > 0 && clipInfo[0].clip.name == "Fire")
            {
                rb.velocity = Vector2.zero; // Stop the player's movement
            }
            else
            {
                // Continue normal movement and trigger the animation
                float xInput = UnityEngine.Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(xInput * runSpeed, rb.velocity.y);
                if (Input.GetButtonDown("Fire1"))
                {
                    animate.SetTrigger("Fire");
                }
            }
         }
    }

    //Ground Check: 
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        animate.SetBool("isGrounded", isGrounded);
    }

     //Collision Checking on Enter:
     void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the player is on the ground:
        if (collision.gameObject.CompareTag("groundCheck"))
        {
            isGrounded = true;
        } 
        //else if (collision.gameObject.CompareTag("Diamonds"))
        //{
        //    //Animation for Diamonds being collected and destroyed.
        //      animate.SetTrigger("");
            
        //    //Destroy the diamon when player object comes into contact/touches/collides:
        //      Destroy(collision.gameObject);
        //}
    }
    
    //Collision checking on Exit:
     void OnCollisionExit2D(Collision2D collision)
    {
       //Update isGrounded variable when player object jumps/ is off the ground game object:
         if (collision.gameObject.CompareTag("groundCheck"))
        {
            isGrounded = false;
        }
    }
}
