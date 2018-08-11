using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedProjectile : Projectile {

    public float fullSpeed = 20f;
    public float delayUntilAcceleration = 0.2f;
    public float timeUntilFullSpeed = 0.2f;

    private float delayTimer = 0.0f;
    private bool isAccelerating = false;
    

    protected override void OnEnable () {
        base.OnEnable();
        delayTimer = 0.0f;
        body.gravityScale = 1.0f;
        isAccelerating = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        delayTimer += Time.deltaTime;
        if (!isAccelerating && delayTimer >= delayUntilAcceleration)
        {
            isAccelerating = true;
            delayTimer -= delayUntilAcceleration;
        }
        
        if (isAccelerating)
        {
            if (delayTimer >= timeUntilFullSpeed)
            {
                delayTimer = timeUntilFullSpeed;
            }
            var velocity = body.velocity;
            var speed = delayTimer / timeUntilFullSpeed * (fullSpeed - this.speed);
            velocity = transform.rotation * (Vector2.right * speed);
            body.velocity = velocity;
            body.gravityScale = 0.0f;
        }
    }
}
