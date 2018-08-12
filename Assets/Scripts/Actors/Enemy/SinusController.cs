using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusController : ActorController {

    public SinusSegment headPrefab;
    public SinusSegment bodyPrefab;
    public SinusSegment tailPrefab;

    public int segments = 5;
    public float distanceBetweenSegments = 1.5f;

    public float amplitude = 1f;
    public float period = 2f;

    public float speed = 10f;
    public Vector2 target;

    public float timeBetweenSegmentDeath = 0.2f;

    private SinusSegment[] bodySegments;

    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        GenerateBody();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        time += Time.deltaTime;
        time %= period;
        Move();
	}

    protected override void Move()
    {
        var direction = target - (Vector2)transform.position;
        var velocity = direction.normalized * speed;
        body.velocity = velocity;

        var change = 0f;
        var changePerSegment = period / segments;
        foreach(var segment in bodySegments)
        {
            if (segment == null || !segment.gameObject.activeSelf)
            {
                continue;
            }
            var localY = amplitude * Mathf.Sin(((time - change) / period) * Mathf.PI * 2);
            change += changePerSegment;
            var position = segment.transform.localPosition;
            position.y = localY;
            segment.transform.localPosition = position;
        }
    }

    protected override void Act()
    {

    }

    private void GenerateBody()
    {
        bodySegments = new SinusSegment[segments];
        bodySegments[0] = Instantiate(headPrefab, transform);
        for (int i = 1; i < segments - 1; i++)
        {
            bodySegments[i] = Instantiate(bodyPrefab, transform.position - new Vector3(i * distanceBetweenSegments, 0, 0), Quaternion.identity, transform);
        }
        bodySegments[segments - 1] = Instantiate(tailPrefab, transform.position - new Vector3(distanceBetweenSegments * (segments - 1), 0, 0), Quaternion.identity, transform);

        for (int i = 0; i < segments; i++)
        {
            var segment = bodySegments[i];
            segment.body = body;
            segment.index = i;
        }
        this.entity = bodySegments[0].GetComponent<Entity>();
    }

    protected override void OnDeath()
    {
        // Ignore top on death call. Segments will handle this themselves.
    }

    public void OnSegmentDeath(int index)
    {
        StartCoroutine("KillNearbySegments", index);
    }

    private IEnumerator KillNearbySegments(int index)
    {
        yield return new WaitForSeconds(timeBetweenSegmentDeath);
        var nextSegmentIndex = index + 1;
        var previousSegmentIndex = index - 1;

        var nextSegment = nextSegmentIndex < segments ? bodySegments[nextSegmentIndex] : null;
        var previousSegment = previousSegmentIndex >= 0 ? bodySegments[previousSegmentIndex] : null;

        if (nextSegment != null)
        {
            // Only continue destroying the enemy
            // if this wasn't the tail
            KillSegment(nextSegment);
            KillSegment(previousSegment);
        }
        bool stillExists = false;
        foreach (var segment in bodySegments)
        {
            if (segment != null && segment.gameObject.activeSelf)
            {
                stillExists = true;
            }
        }
        if (!stillExists)
        {
            Destroy(gameObject);
        }
    }

    private void KillSegment(SinusSegment segment)
    {
        if (segment != null && segment.entity.health > 0)
        {
            segment.entity.health--;
        }
    }
}
