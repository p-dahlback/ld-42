using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterDelay : MonoBehaviour {

    public Transform[] components;
    public float enableTime = 1f;

    private float time;

    void OnEnable()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= enableTime)
        {
            foreach (var component in components)
            {
                component.gameObject.SetActive(true);
            }
            enabled = false;
        }
    }
}
