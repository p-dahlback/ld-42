using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public float damage = 1000f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag(Tags.BOSS) && collision.CompareTag(Tags.BOSS))
        {
            return;
        }

        var actor = collision.GetComponent<ActorController>();
        if (actor != null)
        {
            actor.Damage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bettany Collision!");
        if (CompareTag(Tags.BOSS) && collision.collider.CompareTag(Tags.BOSS))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            return;
        }

        var actor = collision.collider.GetComponent<ActorController>();
        if (actor != null)
        {
            Debug.Log("Bettany Hit!");
            actor.Damage(damage);
        }
    }
}
