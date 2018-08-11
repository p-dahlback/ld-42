using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float speed = 20f;
    public float damage = 1f;
    public float lifeTime = 3f;
    public bool disappearOnImpact = true;

    protected Rigidbody2D body;
    private float time = 0.0f;
    private Vector3 oldVelocity;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        time = 0f;
    }

    protected virtual void OnEnable()
    {
        body.velocity = transform.rotation * (Vector2.right * speed);
        time = 0f;
    }

    // Update is called once per frame
    protected virtual void Update () {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            gameObject.SetActive(false);
        }
	}

    private void FixedUpdate()
    {
        oldVelocity = body.velocity;
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
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
            body.velocity = oldVelocity;
            StartCoroutine("ReactivateCollision", collision);
        }
    }

    private IEnumerator ReactivateCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
    }
}
