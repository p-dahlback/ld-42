using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public float damage = 1000f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag(Tags.BOSS) && collision.collider.CompareTag(Tags.BOSS))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            return;
        }

        var actor = collision.collider.GetComponent<ActorController>();
        if (actor != null)
        {
            actor.Damage(damage);
        }
    }
}
