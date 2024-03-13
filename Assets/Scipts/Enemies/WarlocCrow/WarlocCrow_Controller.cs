using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class WarlocCrow_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    TouchingDirections td;

    //Health Variables:
    protected int health;
    protected int maxHealth;
    
    //Movement Variables:
    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f;
    public DetectionRangeZone attackZone;
    //private bool playerDetected = false;
    
    //Walkable Direction Variables:
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;


    //Sprite and Animation Variables:
    protected SpriteRenderer sr;
    protected Animator anim;

    //Projectile variables:
    //public GameObject projectilePrefab;
    //public Transform shootPoint;
    //public float shootIntervals = 2f;
    //public GameObject magicBlastPrefab;
    //public Transform magicBlastSpawnPoint;
    //public float magicBlastInterval = 3f;

 
    

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }

        set
        {
            if (_walkDirection != value)
            {
                //Direction FLipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;

                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        } 
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }

        private set
        {
            _hasTarget = value;
            anim.SetBool(AnimationVariable_Strings.AnimationVariable.HASTARGETINRANGE, value);
        }
    }

    public bool EnemyCanMove
    {
        get
        {
            return anim.GetBool(AnimationVariable_Strings.AnimationVariable.ENEMYCANMOVE);
        }
    }

    //
    public void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if(maxHealth <= 0)
        {
            maxHealth = 10;
        }
        health = maxHealth;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        td = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();

        //Start Shooting Projectiles at intervals:
        //InvokeRepeating("Shoot", 0, 2);
        //InvokeRepeating("FlipDirection", 0, 2);
        //InvokeRepeating("Shoot", 0, shootIntervals);
    }

    // Start is called before the first frame update
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (td.IsGrounded && td.IsOnWall)
        {
            FlipDirection();
        }

        if (EnemyCanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
           rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right and or left");
        }
    }

    //private void OnPlayerDetected(bool detected)
    //{
    //    playerDetected = detected;

    //    if (detected)
    //    {
    //        // Start firing magic blasts when player is detected
    //        InvokeRepeating(nameof(FireMagicBlast), 0, magicBlastInterval);
    //    }
    //    else
    //    {
    //        // Stop firing magic blasts when player is not detected
    //        CancelInvoke(nameof(FireMagicBlast));
    //    }
    //}

    //private void FireMagicBlast()
    //{
    //    // Instantiate magic blast at the magicBlastSpawnPoint
    //    Instantiate(magicBlastPrefab, magicBlastSpawnPoint.position, Quaternion.identity);
    //    // Add sound effects or animations here if needed
    //}

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

       if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
          //Flip the sprite horizontally
           FlipDirection();

        }
        
    }

    
}
