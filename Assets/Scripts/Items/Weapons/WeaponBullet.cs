using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour {

    public float damage = 1f;
    public float lifeTime = 3f;
    public bool destroyWhenDone = false;

    private float time = 0.0f;
    
    protected virtual void OnEnable()
    {
        time = 0.0f;
    }

    private void OnDisable()
    {
        if (destroyWhenDone)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
