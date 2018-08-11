using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningZone : MonoBehaviour {

    public float maxRadius = 12f;
    public float radius = 10f;
    public float maxParticleSize = 0.8f;
    public float minParticleSize = 0.4f;
    public float particleSystemOffset = 0.1f;
    public ParticleSystem fireRing;

    private OcclusionMesh occlusionMesh;
    private ParticleSystem.MainModule particleMainModule;

	// Use this for initialization
	void Start () {
        occlusionMesh = GetComponent<OcclusionMesh>();
        particleMainModule = fireRing.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (fireRing.shape.radius != radius - particleSystemOffset)
        {
            ParticleSystem.ShapeModule shape = fireRing.shape;
            shape.radius = radius - particleSystemOffset;
        }
        if (occlusionMesh.radius != radius)
        {
            occlusionMesh.radius = radius;
        }
        particleMainModule.startSize = radius / maxRadius * (maxParticleSize - minParticleSize) + minParticleSize;

        if (radius == 0 && fireRing.isPlaying)
        {
            fireRing.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        else if (radius > 0 && fireRing.isStopped)
        {
            fireRing.Play();
        }
	}

    public bool IsOutsideZone(Vector2 point)
    {
        return Vector2.Distance(point, (Vector2) transform.position) > radius;
    }
}
