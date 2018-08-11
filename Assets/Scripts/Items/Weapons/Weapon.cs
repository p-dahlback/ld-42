using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public Transform bulletSpawn;
    public Transform bulletPrefab;

    public float maxAmmo = 100f;
    public float ammo = 100f;
    public int maxBulletsAtATime = 5;
    public bool infiniteAmmo = false;

    public float cooldown = 0.1f;

    private float cooldownTime = 0.0f;
    private float time = 0.0f;

    private List<Transform> bullets = new List<Transform>();
    private List<Transform> disabledBullets = new List<Transform>();

    // Use this for initialization
    void Start () {
        ammo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update () {
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
        cooldownTime = cooldown;
    }

    private void GenerateBullet()
    {
        Transform bullet;
        if (disabledBullets.Count > 0)
        {
            bullet = disabledBullets[0];
            bullet.parent = bulletSpawn;
            bullet.localPosition = Vector3.zero;
            bullet.localScale = Vector3.one;
            bullet.localRotation = Quaternion.identity;
            bullet.gameObject.SetActive(true);
            disabledBullets.RemoveAt(0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletSpawn);
        }
        bullet.parent = null;
        bullets.Add(bullet);
    }

    protected virtual void WeaponAction()
    {

    }
}
