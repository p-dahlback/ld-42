using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour {

    public static class AnimatorStates
    {
        public static readonly string Heading = "Heading";
    }

    protected abstract void Move();
    protected abstract void Act();

    public Transform deathPrefab;

    public Entity entity;
    public Rigidbody2D body;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        entity = GetComponent<Entity>();
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        CheckIsAlive();
	}

    private void LateUpdate()
    {
        UpdateHeading();
    }

    public virtual void RestoreVelocity()
    {

    }

    public void Damage(float value)
    {
        entity.health = entity.health - value;
        if (entity.health <= 0)
        {
            if (entity is Enemy)
            {
                var enemy = (Enemy)entity;
                BurnController.GetInstance().AddToRadius(enemy.radiusIncrease);
            }
        }
    }

    protected void UpdateHeading()
    {
        if (body.velocity.x < 0)
        {
            if (animator != null)
            {
                animator.SetInteger(AnimatorStates.Heading, -1);
            }
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (body.velocity.x > 0)
        {
            if (animator != null)
            {
                animator.SetInteger(AnimatorStates.Heading, 1);
            }
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    protected void CheckIsAlive()
    {
        if (entity.health <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
