using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Character : MonoBehaviour
{

    CharacterMovement characterMovement;
    CharacterAnimationController characterAnimation;
    Combat combat;
    Health health;
    InRange inRange;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimation = GetComponent<CharacterAnimationController>();
        combat = GetComponent<Combat>();
        health = GetComponent<Health>();
        inRange = GetComponent<InRange>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetCharacterState();
        if (Input.GetMouseButton(0) && characterMovement.movementState != CharacterMovement.MovementStates.Jumping)
        {
            StartCoroutine(AttackOrder());
        }

        StartCoroutine(InteractOrder());

        if(health.health == 0)
        {
            StartCoroutine(CheckDeath());
        }
    }

    private void SetCharacterState()
    {
        if (combat.isAttacking)
            return;
        if (characterMovement.IsGrounded())
        {
            if(rigidbody2D.velocity.x == 0)
            {
                characterMovement.SetMovementState(CharacterMovement.MovementStates.Idle);
            }
            else if (rigidbody2D.velocity.x > 0)
            {
                characterMovement.facingDirection= CharacterMovement.FacingDirection.Right;
                characterMovement.SetMovementState(CharacterMovement.MovementStates.Running);
            }
            else if (rigidbody2D.velocity.x < 0)
            {
                characterMovement.facingDirection = CharacterMovement.FacingDirection.Left;
                characterMovement.SetMovementState(CharacterMovement.MovementStates.Running);
            }
        }
        else 
        {
            characterMovement.SetMovementState(CharacterMovement.MovementStates.Jumping);
        }
        
    }

    private IEnumerator AttackOrder()
    {
        if (combat.isAttacking)
            yield break;

        combat.isAttacking = true;

        characterMovement.movementState = CharacterMovement.MovementStates.Attacking;


        yield return new WaitForSeconds(0.08f);

        combat.Attack();

        combat.isAttacking = false;


        yield break;
    }


    private IEnumerator InteractOrder()
    {
        if (inRange.isInRange)
            yield break;
        inRange.isInRange = true;

        inRange.inRange();

        inRange.isInRange = false;

        yield break;
    }



    private IEnumerator CheckDeath()
    {
        characterAnimation.PlayDeadAnim();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield break;
    }


}
