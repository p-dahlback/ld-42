using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : WeaponBullet {

    public Transform hitPrefab;
    public float speed = 20f;
    public float angularSpeed = 0f;
    public float inaccuracy = 0f;
    public bool disappearOnImpact = true;

    protected Rigidbody2D body;
    private Vector3 oldVelocity;
    private float oldAngularVelocity;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        float inaccuracyValue = Random.Range(0, inaccuracy * 2);
        var vector = new Vector2(1, inaccuracyValue - inaccuracy);
        body.velocity = transform.rotation * vector.normalized * speed;
        body.angularVelocity = angularSpeed;
    }

    protected virtual void OnHit()
    {
        if (hitPrefab != null)
        {
            Instantiate(hitPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        oldVelocity = body.velocity;
        oldAngularVelocity = body.angularVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var actor = collision.transform.GetComponent<ActorController>();
        if (actor != null)
        {
            actor.Damage(damage);
        }

        if (disappearOnImpact)
        {
            gameObject.SetActive(false);
            if (destroyWhenDone)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
            body.velocity = oldVelocity;
            body.angularVelocity = oldAngularVelocity;
            StartCoroutine("ReactivateCollision", collision);
        }
        OnHit();
    }

    private IEnumerator ReactivateCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        if (collision.collider != null && collision.otherCollider != null)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
        }
    }
}
