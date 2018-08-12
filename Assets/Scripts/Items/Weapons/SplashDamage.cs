using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour {

    public float radius = 1f;
    public float damage = 1f;

	// Use this for initialization
	void OnEnable () {
        int layerMask = 1 << (int)Layers.ENEMY;
        var colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach (var collider in colliders)
        {
            var actor = collider.GetComponent<ActorController>();
            if (actor != null)
            {
                actor.Damage(damage);
            }
        }

        enabled = false;
	}
}
