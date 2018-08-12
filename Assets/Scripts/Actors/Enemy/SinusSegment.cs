using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusSegment : ActorController {

    public int index = 0;
    public bool ignoreCollisions = false;

	// Use this for initialization
	void Start () {
		
	}

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
    }

    protected override void Act()
    {
    }

    protected override void OnDeath()
    {
        var sinusController = transform.parent.GetComponent<SinusController>();
        sinusController.OnSegmentDeath(index);
        base.OnDeath();
    }

    public void IgnorePlayerCollisions()
    {
        var player = GameController.GetInstance().player;
        if (player != null)
        {
            var playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                var collider = GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(collider, playerCollider);
            }
        }
    }
}
