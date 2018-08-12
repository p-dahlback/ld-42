using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : ActorController {

    public RotateToTarget rotator;
    public Weapon weapon;

    public float startDelay = 5f;
    public float delayBetweenShots = 10f;
    public float shootDuration = 2f;

    private float shootTime = 0.0f;

    private bool initialized = false;
    private bool shooting = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    protected override void Update()
    {
        if (entity.health > 0)
        {
            Move();
            Act();
        }
        base.Update();
    }

    protected override void Move()
    {
        var player = GameController.GetInstance().player;
        if (player != null)
        {
            rotator.target = player.transform;
        }
    }

    protected override void Act()
    {
        shootTime += Time.deltaTime;
        if (!initialized)
        {
            if (shootTime >= startDelay)
            {
                shootTime -= startDelay;
                initialized = true;
            }
            return;
        }
        if (!shooting)
        {
            if (shootTime >= delayBetweenShots)
            {
                shootTime -= delayBetweenShots;
                shooting = true;
            }
            else
            {
                return;
            }
        }

        if (shooting)
        {
            animator.SetBool("IsShooting", true);
            weapon.Fire();
            if (shootTime >= shootDuration)
            {
                shootTime -= shootDuration;
                shooting = false;

                animator.SetBool("IsShooting", false);
            }
        }
    }
}
