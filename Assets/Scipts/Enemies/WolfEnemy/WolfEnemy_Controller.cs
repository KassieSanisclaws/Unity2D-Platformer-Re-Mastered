using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class WolfEnemy_Controller : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f;
    public DetectionRangeZone attackZone;

    Rigidbody2D rb;
    TouchingDirections td;
    Animator animator;

    public enum WalkableDirection { Right, Left}

    private WalkableDirection _walkDirection;

    public Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection 
    {  
        get { return _walkDirection; }

        set 
        { 
            if(_walkDirection != value)
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

             _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget 
    { 
       get { return _hasTarget; }

        private set 
        { 
          _hasTarget = value;
          animator.SetBool(AnimationVariable_Strings.AnimationVariable.HASTARGETINRANGE, value);
        } 
    }

    public bool EnemyCanMove
    {
        get
        {
            return animator.GetBool(AnimationVariable_Strings.AnimationVariable.ENEMYCANMOVE);
        }
    }

    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
      td = GetComponent<TouchingDirections>();
      animator = GetComponent<Animator>();
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

         rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
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

   

    
}
