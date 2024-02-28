using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{  
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    BoxCollider2D touchCol;
    Animator animator;

    readonly RaycastHit2D[] groundHits = new RaycastHit2D[5];
    readonly RaycastHit2D[] wallHits = new RaycastHit2D[5];
    readonly RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField] private bool _isGrounded;

    public bool IsGrounded
    {
        get { return _isGrounded; } 
        
        private set 
        {
            _isGrounded = value;
            animator.SetBool(AnimationVariable_Strings.AnimationVariable.ISGROUNDED, value);
        }
    }

    [SerializeField] private bool _isOnWall;
    public bool IsOnWall 
    {
      get { return _isOnWall; }

      private set 
        {
         _isOnWall = value;
         animator.SetBool(AnimationVariable_Strings.AnimationVariable.ISONWALL, value);
        } 
    
    }

    [SerializeField] private bool _isOnCeiling;
    private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling 
    {
       get { return _isOnCeiling; }

       private set 
        { 
          _isOnCeiling = value;
          animator.SetBool(AnimationVariable_Strings.AnimationVariable.ISONCEILING, value);
        } 
    }

    // Start is called before the first frame update
    private void Awake()
    {
        touchCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = touchCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchCol.Cast(WallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
