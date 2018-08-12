using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    public MonoBehaviour[] components;

	// Use this for initialization
	void OnEnable () {
		foreach (var component in components)
        {
            component.enabled = true;
            if (!component.isActiveAndEnabled)
            {
                component.gameObject.SetActive(true);
            }
        }
	}
}
