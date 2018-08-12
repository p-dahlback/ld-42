using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusController : ActorController {

    public SinusSegment headPrefab;
    public SinusSegment bodyPrefab;
    public SinusSegment tailPrefab;

    public int maxSegments = 7;
    public int segments = 4;
    public float distanceBetweenSegments = 1.5f;

    public float amplitude = 1f;
    public float period = 2f;

    public float speed = 10f;

    public float timeBetweenSegmentDeath = 0.2f;

    private SinusSegment[] bodySegments;

    private Vector2 target;
    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        DetermineTarget();
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

    private void DetermineTarget()
    {
        var target = new Vector2();
        if (transform.position.x < 0)
        {
            target.x = GameController.GetInstance().levelWidth;
        }
        else
        {
            target.x = -GameController.GetInstance().levelWidth;
        }
        var heightDiff = GameController.GetInstance().levelHeight / 3;
        var random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                target.y = heightDiff;
                break;
            case 1:
                target.y = 0;
                break;
            case 2:
                target.y = -heightDiff;
                break;
        }
        target.y += Random.Range(-1.5f, 1.5f);
        this.target = target;
    }

    private void GenerateBody()
    {
        var segments = Random.Range(this.segments, maxSegments + 1);
        var direction = target - (Vector2)transform.position;
        var xSign = Mathf.Sign(direction.x);
        bodySegments = new SinusSegment[segments];
        bodySegments[0] = Instantiate(headPrefab, transform);
        for (int i = 1; i < segments - 1; i++)
        {
            bodySegments[i] = Instantiate(bodyPrefab, transform.position - xSign * new Vector3(i * distanceBetweenSegments, 0, 0), Quaternion.identity, transform);
        }
        bodySegments[segments - 1] = Instantiate(tailPrefab, transform.position - xSign * new Vector3(distanceBetweenSegments * (segments - 1), 0, 0), Quaternion.identity, transform);

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
