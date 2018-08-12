using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErasureBeam : WeaponBullet {

    public ParticleSystem[] particles;
    public Collider2D occluder;
    public bool followRadius = true;

    private bool isDone = false;

	// Use this for initialization
	void Start () {
        if (BurnController.GetInstance() != null)
        {
            BurnController.GetInstance().AddErasureBeam(this);
        }
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (occluder != null)
        {
            MoveFire();
        }
        else if (!isDone)
        {
            if (BurnController.GetInstance() != null)
            {
                BurnController.GetInstance().RemoveErasureBeam(this);
            }
            isDone = true;
            foreach (var system in particles)
            {
                if (system != null)
                {
                    system.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            }
            StartCoroutine("DestroySelf");
        }
	}

    private void MoveFire()
    {
        float radius = 0.0f;
        if (BurnController.GetInstance() != null)
        {
            radius = BurnController.GetInstance().burningZone.radius;
        }
        foreach (var system in particles)
        {
            var width = occluder.transform.localScale.x;
            float sign;
            if (system.transform.localPosition.x < 0)
            {
                // Left side
                sign = -1;
            } else
            {
                // Right Side
                sign = 1;
            }
            var position = system.transform.localPosition;
            position.x = sign * (width / 2);
            system.transform.localPosition = position;

            if (followRadius)
            {
                // Update radius
                var shapeModule = system.shape;
                shapeModule.radius = radius;
            }
        }
    }

    public bool IsOutsideZone(Vector2 point)
    {
        if (occluder == null)
        {
            return false;
        }
        return occluder.bounds.Contains(point);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
