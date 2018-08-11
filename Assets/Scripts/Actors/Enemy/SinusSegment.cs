using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusSegment : ActorController {

    public int index = 0;

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
        base.OnDeath();
        var sinusController = transform.parent.GetComponent<SinusController>();
        sinusController.OnSegmentDeath(index);
        Destroy(gameObject);
    }
}
