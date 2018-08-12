using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public ClawController leftClaw;
    public ClawController rightClaw;
    public BossHeadController head;

	// Use this for initialization
	void Start () {
        GameController.GetInstance().OnBossSpawned();
	}
	
	// Update is called once per frame
	void Update () {
		if (leftClaw == null && rightClaw == null)
        {
            if (head != null && !head.weapon.isActiveAndEnabled)
            {
                head.weapon.gameObject.SetActive(true);
                head.canGoInvincible = false;
                head.isInvincible = false;
            }
            else if (head == null)
            {
                GameController.GetInstance().OnBossKilled();
            }
        }
	}

    
}
