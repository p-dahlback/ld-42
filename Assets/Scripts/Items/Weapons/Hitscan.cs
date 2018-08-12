using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : WeaponBullet {

    public bool pierce = true;
    
    protected override void OnEnable () {
        base.OnEnable();
        Beam();
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
            var actor = hit.collider.transform.GetComponent<ActorController>();
            if (actor != null)
            {
                actor.Damage(damage);
            }
        }
    }
}
