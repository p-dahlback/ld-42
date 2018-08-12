using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformingActorController : ActorController {

    public static class AnimatorStates 
    {
        public static readonly string CanJump = "CanJump";
        public static readonly string IsJumping = "IsJumping";
        public static readonly string IsGrounded = "IsGrounded";
    }

    protected abstract void Jump();

    public float maxHorizontalSpeed = 10f;
    public float maxVerticalSpeed = 10f;
    public float cappedJumpSpeed = 1f;
    public float cappedMovementSpeed = 4f;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float actorHeight = 1.1f;

    private Vector2 oldVelocity;

    protected override void Update()
    {
        base.Update();
        Move();
        Jump();
        Act();
        ClampMaxSpeed();
    }

    private void FixedUpdate()
    {
        oldVelocity = body.velocity;
        CheckGrounded();
    }

    public override void RestoreVelocity()
    {
        base.RestoreVelocity();
        body.velocity = oldVelocity;
    }

    protected void CheckGrounded()
    {
        if (animator.GetBool(AnimatorStates.IsGrounded))
        {
            return;
        }

        int layerMask = 1 << (int) Layers.GEOMETRY;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, actorHeight / 2f, layerMask);
        if (hit.transform == null)
        {
            return;
        }
        var burnController = BurnController.GetInstance();
        if (burnController.IsOutsideZone(hit.point))
        {
            return;
        }
        Ground();
    }

    protected void Jump(float horizontalForce, float verticalForce)
    {
        if (!animator.GetBool(AnimatorStates.CanJump))
        {
            return;
        }
        var velocity = body.velocity;
        velocity.x += horizontalForce;
        velocity.y = verticalForce;
        body.velocity = velocity;
        animator.SetBool(AnimatorStates.CanJump, false);
        animator.SetBool(AnimatorStates.IsJumping, true);
        animator.SetBool(AnimatorStates.IsGrounded, false);
    }

    protected void CapMovement()
    {
        var velocity = body.velocity;
        float sign = Mathf.Sign(velocity.x);
        velocity.x = cappedMovementSpeed * sign;
        body.velocity = velocity;
        oldVelocity = velocity;
    }

    protected void CapJump()
    {
        if (!animator.GetBool(AnimatorStates.IsJumping))
        {
            return;
        }
        var velocity = body.velocity;
        if (velocity.y > cappedJumpSpeed)
        {
            velocity.y = cappedJumpSpeed;
        }
        body.velocity = velocity;
    }

    protected void Move(float horizontalForce)
    {
        var velocity = body.velocity;
        velocity.x += horizontalForce;
        body.velocity = velocity;
    }

    protected void ClampMaxSpeed()
    {
        var velocity = body.velocity;
        if (Mathf.Abs(velocity.x) >= maxHorizontalSpeed)
        {
            velocity.x = maxHorizontalSpeed * Mathf.Sign(velocity.x);
        }
        if (velocity.y >= maxVerticalSpeed)
        {
            velocity.y = maxVerticalSpeed;
        }
        body.velocity = velocity;
    }

    public void RestoreVelocityAfterCollision(bool x, bool y)
    {
        var velocity = body.velocity;
        if (x) { velocity.x = oldVelocity.x; }
        if (y) { velocity.y = oldVelocity.y; }
        body.velocity = velocity;
    }

    public void Fall()
    {
        animator.SetBool(AnimatorStates.IsGrounded, false);
        animator.SetBool(AnimatorStates.CanJump, false);
        RestoreVelocityAfterCollision(true, true);
    }

    public void Ground()
    {
        animator.SetBool(AnimatorStates.IsGrounded, true);
        animator.SetBool(AnimatorStates.CanJump, true);
        RestoreVelocityAfterCollision(true, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.GEOMETRY))
        {
            animator.SetBool(AnimatorStates.IsGrounded, false);
            var coyotes = GetComponent<CoyoteFrames>();
            if (coyotes != null)
            {
                coyotes.enabled = true;
            }
        }
    }
}
