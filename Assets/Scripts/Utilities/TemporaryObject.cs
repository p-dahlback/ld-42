using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour {

    public Transform replacementPrefab;
    public float lifeTime = 1f;

    private float time = 0.0f;

	// Use this for initialization
	void OnEnable () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            if (replacementPrefab != null)
            {
                Instantiate(replacementPrefab, transform.position, transform.rotation, transform.parent);
            }
            Destroy(gameObject);
        }
	}
}
