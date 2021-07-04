using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking,
        Dead
    }

    public enum FacingDirection
    {
        Right,
        Left
    }

    [Header("Movement values")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Raycast length and layerMask")]
    public float isGorundedRayLength;
    public LayerMask platformLayerMask;

    [Header("Movement States")]
    public MovementStates movementState;
    public FacingDirection facingDirection;


    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private CharacterAnimationController animationController;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationController = GetComponent<CharacterAnimationController>();
    }


    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        PlayAnimationsBasedOnState();
        SetCharacterDirection();
    }


    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }
        
    private void HandleMovement()
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-movementSpeed, rigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(movementSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center, 
            spriteRenderer.bounds.size, 0f, Vector2.down, 
            isGorundedRayLength, platformLayerMask);

        return raycastHit2D.collider != null;
    }

    private void SetCharacterDirection()
    {

        switch (facingDirection)
        {
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                break;

            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                break;
        }
    }

    private void PlayAnimationsBasedOnState()
    {
        switch (movementState)
        {
            case MovementStates.Idle:
                animationController.PlayIdleAnim();
                break;
            case MovementStates.Running:
                animationController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
                animationController.PlayJumpingAnim();
                break;
            case MovementStates.Dead:
                animationController.PlayDeadAnim();
                break;
            case MovementStates.Attacking:
                animationController.PlayAttackAnim();
                break;
            default:
                break;
        }
    }

    public void SetMovementState(MovementStates movementStates)
    {
            movementState = movementStates;
    }

}
