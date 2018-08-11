using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteFrames : MonoBehaviour {

    public Animator animator;
    public int frames = 4;

    private int frameCount = 0;

	// Use this for initialization
	void Start () {
        frameCount = 0;
	}

    private void OnEnable()
    {
        frameCount = 0;
    }

    // Update is called once per frame
    void Update () {
        frameCount++;
        if (animator.GetBool(PlatformingActorController.AnimatorStates.IsGrounded))
        {
            enabled = false;
            return;
        }

        if (frameCount >= frames)
        {
            animator.SetBool(PlatformingActorController.AnimatorStates.CanJump, false);
            enabled = false;
        }
	}
}
