using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBounce : MonoBehaviour {

    public float period = 0.5f;
    public float amplitude = .5f;

    private float time = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time %= period;

        var position = transform.localPosition;
        position.y = amplitude * Mathf.Sin(time / period * Mathf.PI * 2);
        transform.localPosition = position;
    }
}
