using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour {

    protected abstract void Move();
    protected abstract void Act();

    public Entity entity;

    // Use this for initialization
    void Start () {
        entity = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        CheckIsAlive();
	}

    public void Damage(float value)
    {
        entity.health = entity.health - value;
    }

    protected void CheckIsAlive()
    {
        if (entity.health < 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {

    }
}
