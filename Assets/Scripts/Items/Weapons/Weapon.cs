using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public Transform bulletSpawn;
    public WeaponBullet bulletPrefab;

    public Rigidbody2D holder;
    public Projectile throwable;
    public Collider2D throwableHitBox;

    public float maxAmmo = 100f;
    public float ammo = 100f;
    public int maxBulletsAtATime = 5;
    public bool infiniteAmmo = false;
    public float cooldown = 0.1f;
    public float knockback = 0f;

    private float cooldownTime = 0.0f;
    private float time = 0.0f;

    private List<WeaponBullet> bullets = new List<WeaponBullet>();
    private List<WeaponBullet> disabledBullets = new List<WeaponBullet>();

    // Use this for initialization
    void Start () {
        ammo = maxAmmo;
        if (holder == null)
        {
            var bodies = transform.parent.GetComponentsInParent<Rigidbody2D>();
            holder = bodies[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
        }

		if (cooldownTime > 0.0f)
        {
            time += Time.deltaTime;
            if (time >= cooldownTime)
            {
                cooldownTime = 0.0f;
                time = 0.0f;
            }
        }
        RetireBullets();
	}

    private void OnDestroy()
    {
        DestroyBullets();
    }

    private void DestroyBullets()
    {
        foreach (var bullet in disabledBullets)
        {
            if (bullet == null) { continue; }
            Destroy(bullet.gameObject);
        }
        foreach (var bullet in bullets)
        {
            if (bullet == null) { continue; }
            bullet.destroyWhenDone = true;
        }
    }

    private void RetireBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var bullet = bullets[i];
            if (!bullet.gameObject.activeSelf)
            {
                bullets.RemoveAt(i);
                disabledBullets.Add(bullet);
                i--;
            }
        }
    }

    public void Fire()
    {
        if (bullets.Count >= maxBulletsAtATime)
        {
            return;
        }
        if (cooldownTime > 0f)
        {
            return;
        }
        if (!infiniteAmmo)
        {
            if (ammo <= 0)
            {
                return;
            }
            ammo--;
        }
        GenerateBullet();
        WeaponAction();
        Knockback();
        cooldownTime = cooldown;
    }

    private void GenerateBullet()
    {
        WeaponBullet bullet;
        if (disabledBullets.Count > 0)
        {
            bullet = disabledBullets[0];
            bullet.transform.parent = bulletSpawn;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localScale = Vector3.one;
            bullet.transform.localRotation = Quaternion.identity;
            bullet.gameObject.SetActive(true);
            disabledBullets.RemoveAt(0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletSpawn);
        }
        bullet.transform.parent = null;
        bullets.Add(bullet);
    }

    private void Knockback()
    {
        var direction = transform.rotation * Vector2.left;
        holder.AddForce(direction * knockback);
    }

    protected virtual void WeaponAction()
    {

    }

    public void Remove()
    {
        DestroyBullets();
        Destroy(gameObject);
    }

    public void Throw()
    {
        DestroyBullets();
        transform.parent = null;
        var direction = transform.rotation * Vector3.right;
        if (direction.x < 0)
        {
            throwable.angularSpeed = -throwable.angularSpeed;
        }
        // Disable weapon usage
        enabled = false;
        // Enable thrown projectile
        throwable.enabled = true;
        throwableHitBox.enabled = true;
        throwable.gameObject.layer = (int)Layers.BULLET;
    }
}
