using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlatformingActorController {

    // Use this for initialization
    void Start () {
		
	}

    protected override void Move()
    {
        var horizontalThrust = Input.GetAxis("Horizontal");
        var horizontalForce = speed * horizontalThrust * Time.deltaTime;
        Move(horizontalForce);

        if (horizontalThrust == 0)
        {
            CapMovement();
        }
    }

    protected override void Act()
    {

    }

    protected override void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(0, jumpForce);
        }
        if (Input.GetButtonUp("Jump"))
        {
            CapJump();
        }
    }
}
