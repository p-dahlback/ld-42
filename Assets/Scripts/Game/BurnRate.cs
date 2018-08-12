using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnRate : MonoBehaviour {
    
    public float startRate = 0.2f;
    public float maxRate = 0.6f;
    public float timeBetweenEachIncrease = 30f;
    public float rateIncrease = 0.2f;

    private BurnController burnController;
    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        burnController = BurnController.GetInstance();
        burnController.decreasePerSecond = startRate;
	}
	
	// Update is called once per frame
	void Update () {
        if (burnController.decreasePerSecond >= maxRate)
        {
            burnController.decreasePerSecond = maxRate;
            return;
        }

        time += Time.deltaTime;
        if (time >= timeBetweenEachIncrease)
        {
            time -= timeBetweenEachIncrease;
            burnController.decreasePerSecond += rateIncrease * (maxRate - startRate);
        }
	}
}
