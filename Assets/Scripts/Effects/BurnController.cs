using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnController : MonoBehaviour {

    private static BurnController sBurnController;

    public BurningZone burningZone;
    public float decreasePerSecond = 0.2f;
    public float minimumRadius = 0.2f;

    private float targetRadius;
    private float startRadius;

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
        burningZone.radius += value;
        startRadius = burningZone.radius;
        targetRadius = startRadius - decreasePerSecond;
    }

    public bool IsOutsideZone(Vector2 point)
    {
        return burningZone.IsOutsideZone(point);
    }
}
