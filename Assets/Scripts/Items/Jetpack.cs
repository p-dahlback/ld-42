using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour {

    public PlayerController playerController;
    public float lifeTime = 10f;

    private float time = 0f;

	// Use this for initialization
	void Start () {
		if (playerController == null)
        {
            var playerControllers = transform.GetComponentsInParent<PlayerController>();
            playerController = playerControllers[0];
        }
        playerController.hasJetPack = true;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            playerController.hasJetPack = false;
            Destroy(gameObject);
        }
	}
}
