using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimController : MonoBehaviour {

    public static class AnimatorStates
    {
        public static readonly string Rotation = "AimRotation";
    }

    protected abstract void Aim();

    public Animator animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Aim();
        UpdateAnimator();
    }

    protected void Aim(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected void Aim(Vector2 target)
    {
        var vectorToTarget = (Vector3)target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat(AnimatorStates.Rotation, transform.rotation.eulerAngles.z);
    }
}
