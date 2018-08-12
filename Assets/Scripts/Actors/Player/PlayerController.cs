using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlatformingActorController {

    public bool hasJetPack = false;
    public float jetpackThrust = 10f;

    private Player player;
    private bool hadJetPack = false;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
	}

    protected override void Update()
    {
        base.Update();
        ClampWithinLevel();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        GameController.GetInstance().OnPlayerDeath();
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
        if (Input.GetButton("Fire1"))
        {
            var weapon = player.weaponContainer.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.Fire();
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            var weapon = player.weaponContainer.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.Throw();
            }
        }
    }

    protected override void Jump()
    {
        if (hasJetPack)
        {
            Fly();
            hadJetPack = true;
        }
        else
        {
            if (hadJetPack && !animator.GetBool(AnimatorStates.IsGrounded))
            {
                animator.SetBool(AnimatorStates.CanJump, false);
            }

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

    protected virtual void Fly()
    {
        animator.SetBool(AnimatorStates.CanJump, true);
        var thrust = Input.GetButton("Jump");
        if (thrust)
        {
            Jump(0, jetpackThrust);
        }
    }

    private void ClampWithinLevel()
    {
        var width = GameController.GetInstance().levelWidth;
        var height = GameController.GetInstance().levelHeight;
        var position = transform.position;

        if (position.x - .5f < -width / 2)
        {
            position.x = -width / 2 + .5f;
        }
        else if (position.x + .5f >= width / 2)
        {
            position.x = width / 2 - .5f;
        }
        if (position.y >= height / 2 - .5f)
        {
            position.y = height / 2 - .5f;
        }
        transform.position = position;
    }
}
