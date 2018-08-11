using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour {

    public bool pierce = true;
    public float damage = 1f;
    public float lifeTime = 3f;

    private float time = 0.0f;
    private Vector3 oldVelocity;

    // Use this for initialization
    void OnEnable () {
        time = 0.0f;
        Beam();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void Beam()
    {
        Vector3 direction = transform.rotation * Vector3.right;
        int layerMask = 1 << (int)Layers.ENEMY;
        var hits = Physics2D.RaycastAll(transform.position, direction, float.MaxValue, layerMask);
        foreach (var hit in hits)
        {
            if (hit.transform == null)
            {
                continue;
            }
            var actor = hit.transform.GetComponent<ActorController>();
            if (actor != null)
            {
                actor.Damage(damage);
            }
        }
    }
}
