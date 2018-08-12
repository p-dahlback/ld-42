using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour {

    public float disableTime = 1f;

    private float time;

    void OnEnable()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= disableTime)
        {
            gameObject.SetActive(false);
        }
    }
}
