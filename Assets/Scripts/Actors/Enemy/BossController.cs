using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public ClawController leftClaw;
    public ClawController rightClaw;
    public BossHeadController head;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (leftClaw == null && rightClaw == null && head != null)
        {
            if (!head.weapon.enabled)
            {
                head.weapon.gameObject.SetActive(true);
                head.canGoInvincible = false;
                head.isInvincible = false;
            }
        }
	}

    
}
