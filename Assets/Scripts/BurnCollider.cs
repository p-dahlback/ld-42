using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnCollider : MonoBehaviour {

    private ContactPoint2D[] contacts;

	// Use this for initialization
	void Start () {
        contacts = new ContactPoint2D[4];
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollisionInZone(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckCollisionInZone(collision);
    }

    private void CheckCollisionInZone(Collision2D collision)
    {
        if (!collision.enabled)
        {
            return;
        }

        int count = collision.GetContacts(contacts);
        int pointsOutside = 0;
        bool wasEnabled = false;
        BurnController burnController = BurnController.GetInstance();
        for (int i = 0; i < count; i++)
        {
            if (!contacts[i].enabled)
            {
                pointsOutside++;
                continue;
            }

            wasEnabled = true;
            if (burnController.IsOutsideZone(contacts[i].point))
            {
                pointsOutside++;
            }
        }
        if (wasEnabled && pointsOutside == count)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
            StartCoroutine("ReactivateCollision", collision);
            Debug.Log("Ignoring Collision");
        }
    }

    private IEnumerator ReactivateCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Reactivating Collisions");
        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
    }
}
