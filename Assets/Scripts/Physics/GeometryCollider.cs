using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryCollider : MonoBehaviour {

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
        
        if (AreAllContactsOutsideZone(collision))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
            var platformer = collision.gameObject.GetComponent<PlatformingActorController>();
            if (platformer != null) { platformer.Fall(); }
            StartCoroutine("ReactivateCollision", collision);
        }
    }

    private bool AreAllContactsOutsideZone(Collision2D collision)
    {
        int count = collision.GetContacts(contacts);
        int pointsOutside = 0;
        BurnController burnController = BurnController.GetInstance();
        for (int i = 0; i < count; i++)
        {
            if (!contacts[i].enabled)
            {
                pointsOutside++;
                continue;
            }
            
            if (burnController.IsOutsideZone(contacts[i].point))
            {
                pointsOutside++;
            }
        }
        return pointsOutside == count;
    }

    private IEnumerator ReactivateCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
    }
}
