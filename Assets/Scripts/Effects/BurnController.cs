using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnController : MonoBehaviour {

    private static BurnController sBurnController;

    [System.Serializable]
    public struct ScoreMultiplier
    {
        public float multiplier;
        public float radius;
    }

    public BurningZone burningZone;
    public float decreasePerSecond = 0.2f;
    public float minimumRadius = 0.2f;
    public ScoreMultiplier[] multipliers;

    private float targetRadius;
    private float startRadius;

    private List<ErasureBeam> erasureBeams = new List<ErasureBeam>();

    public static BurnController GetInstance()
    {
        return sBurnController;
    }

    void Awake()
    {
        if (sBurnController != null && sBurnController != this)
        {
            Destroy(sBurnController.gameObject);
        }
        sBurnController = this;
    }

    // Use this for initialization
    void Start () {
        targetRadius = burningZone.maxRadius - decreasePerSecond;
        startRadius = burningZone.maxRadius;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Approximately(burningZone.radius, targetRadius))
        {
            startRadius = burningZone.radius;
            targetRadius = burningZone.radius - decreasePerSecond;
        }
        burningZone.radius += (targetRadius - startRadius) * Time.deltaTime;
        if (burningZone.radius < minimumRadius)
        {
            burningZone.radius = minimumRadius;
            targetRadius = minimumRadius;
            startRadius = minimumRadius;
        }
	}

    public void AddToRadius(float value)
    {
        float multiplier = 1.0f;
        foreach (var multiplierConfig in multipliers)
        {
            if (burningZone.radius <= multiplierConfig.radius)
            {
                multiplier = multiplierConfig.multiplier;
                break;
            }
        }

        burningZone.radius += value * multiplier;
        startRadius = burningZone.radius;
        targetRadius = startRadius - decreasePerSecond;
    }

    public void AddErasureBeam(ErasureBeam beam)
    {
        this.erasureBeams.Add(beam);
    }

    public void RemoveErasureBeam(ErasureBeam beam)
    {
        this.erasureBeams.Remove(beam);
    }

    public bool IsOutsideZone(Vector2 point)
    {
        var result = burningZone.IsOutsideZone(point);
        if (!result)
        {
            foreach (var beam in erasureBeams)
            {
                if (beam != null)
                {
                    result = beam.IsOutsideZone(point);
                    if (result) { return true; }
                }
            }
        }
        return result;
    }
}
