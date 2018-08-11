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
            Destroy(oldWeapon.gameObject);
        }
        weapon.transform.parent = player.weaponContainer;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        GrantPickup(player);
        Destroy(gameObject);
    }
}
