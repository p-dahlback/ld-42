using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {

    public Vector2 startScale;
    public Vector2 goalScale;
    public float duration = 0.5f;
    public float delay = 1.0f;

    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        transform.localScale = startScale;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= delay)
        {
            var progress = (time - delay) / duration;
            if (progress >= 1.0f)
            {
                Destroy(gameObject);
                return;
            }
            var scale = startScale + (goalScale - startScale) * progress;
            transform.localScale = scale;
        }
	}
}
