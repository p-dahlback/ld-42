using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<WeaponBullet>();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == (int) Layers.PLAYER)
        {
            var player = collision.GetComponent<PlayerController>();
            player.Damage(float.MaxValue);
        }
        else if (!collision.CompareTag(Tags.BOSS))
        {
            Destroy(collision.gameObject);
        }
    }
}
