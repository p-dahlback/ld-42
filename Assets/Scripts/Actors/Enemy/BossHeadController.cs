using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadController : ActorController {

    public Transform beamPrefab;
    public Weapon weapon;
    public SpriteFlasher flasher;
    public bool isInvincible = true;
    public bool canGoInvincible = true;
    public float beamDelay = 2.0f;
    public float beamDuration = 2.0f;

    public float delayBetweenBeams = 20f;
    public float beamRandomness = 10f;

    private float beamTime = 0.0f;
    private float timeToBeam = 0.0f;

	// Use this for initialization
	void Start () {
        timeToBeam = delayBetweenBeams + beamRandomness;
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

    public override void Damage(float value)
    {
        if (isInvincible)
        {
            return;
        }
        base.Damage(value);
    }

    protected override void Move()
    {
    }

    protected override void Act()
    {
        beamTime += Time.deltaTime;
        if (beamTime >= timeToBeam)
        {
            beamTime %= timeToBeam;
            timeToBeam = delayBetweenBeams + Random.Range(0, beamRandomness);
            PrepareToFireBeam();
        }
    }

    private void PrepareToFireBeam()
    {
        animator.SetBool("IsShooting", true);
        flasher.enabled = true;
        isInvincible = false;
        StartCoroutine("FireBeamAfterDelay", beamDelay);
    }

    private IEnumerator FireBeamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        flasher.enabled = false;
        FireBeam();
        yield return new WaitForSeconds(beamDuration);
        if (canGoInvincible)
        {
            isInvincible = true;
        }

        animator.SetBool("IsShooting", false);
    }

    private void FireBeam()
    {
        var position = transform.position - Vector3.up * (GameController.GetInstance().levelHeight / 2);
        Instantiate(beamPrefab, position, Quaternion.identity);
    }
}
