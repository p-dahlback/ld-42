using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour {

    public Transform target;
    public float rotationSpeed = 90;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null) { return; }

        transform.right = (target.position - transform.position).normalized;
    }
}
