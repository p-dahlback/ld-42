using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTarget : MonoBehaviour {

    public Vector2 startPosition;
    public Vector2 targetPosition;
    public float timeToApproach = 5f;
    public bool ignoreX = false;
    public bool reverse = false;

    private float time = 0.0f;

	// Use this for initialization
	void OnEnable () {
        time = 0.0f;
        if (ignoreX)
        {
            targetPosition.x = transform.position.x;
            startPosition.x = transform.position.x;
        }
        transform.position = startPosition;
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
		transform.position = Vector2.Lerp(startPosition, targetPosition, reverse ? 1.0f - progress : progress);
	}
}
