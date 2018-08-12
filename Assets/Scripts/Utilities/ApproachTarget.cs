using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTarget : MonoBehaviour {

    public Vector2 startPosition;
    public Vector2 targetPosition;
    public float timeToApproach = 5f;

    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= timeToApproach)
        {
            transform.position = targetPosition;
            enabled = false;
            return;
        }
        var progress = Mathf.Sin((time / timeToApproach) * Mathf.PI / 2);
		transform.position = Vector2.Lerp(startPosition, targetPosition, progress);
	}
}
