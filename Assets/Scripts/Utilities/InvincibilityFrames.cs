using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityFrames : MonoBehaviour {

    public int frames = 60;
    public PlayerController player;

    private int frameCount = 0;

	// Use this for initialization
	void OnEnable () {
        player.isInvincible = true;
        frameCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;
        if (frameCount >= frames)
        {
            if (player != null)
            {
                player.isInvincible = false;
            }
            enabled = false;
        }
	}
}
