using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public Transform pickupPrefab;
    public float lifeTime = 10f;
    public int extraLives = 0;

    private float time = 0f;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
	}

    protected void GrantPickup(Player player)
    {
        if (pickupPrefab != null)
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
        if (extraLives > 0) {
            GameController.GetInstance().OnExtraLife(extraLives);
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
        weapon.transform.localPosition = Vector3.zero;
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
        jetpack.transform.localPosition = Vector3.zero;
        jetpack.transform.localRotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            playerController.RestoreVelocityAfterCollision(true, true);
            GrantPickup(player);
            Destroy(gameObject);
        }
    }
}
