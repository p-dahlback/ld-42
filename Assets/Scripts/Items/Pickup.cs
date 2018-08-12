using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public Transform pickupPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void GrantPickup(Player player)
    {
        var transform = Instantiate(pickupPrefab);
        var weapon = transform.GetComponent<Weapon>();
        if (weapon != null)
        {
            GrantWeapon(weapon, player);
        }
        var jetpack = transform.GetComponent<Jetpack>();
        if (jetpack != null)
        {
            GrantJetpack(jetpack, player);
        }
    }

    protected void GrantWeapon(Weapon weapon, Player player)
    {
        var oldWeapon = player.weaponContainer.GetComponentInChildren<Weapon>();
        if (oldWeapon)
        {
            oldWeapon.Remove();
        }
        weapon.transform.parent = player.weaponContainer;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.holder = player.GetComponent<Rigidbody2D>();
        GameController.GetInstance().currentWeapon = weapon;
    }

    protected void GrantJetpack(Jetpack jetpack, Player player)
    {
        var oldJetpack = player.jetpackContainer.GetComponentInChildren<Jetpack>();
        if (oldJetpack)
        {
            Destroy(oldJetpack.gameObject);
        }
        jetpack.transform.parent = player.jetpackContainer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            GrantPickup(player);
            Destroy(gameObject);
        }
    }
}
