using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionMesh : MonoBehaviour {

    public float radius = 4f;
    public float width = 10f;
    public float height = 10f;
    public int circlePoints = 12;

    private float oldRadius = 0f;
    private float oldWidth = 0f;
    private float oldHeight = 0f;
    private int oldPoints = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (radius == oldRadius && width == oldWidth && height == oldHeight && circlePoints == oldPoints)
        {
            return;
        }
        GenerateMesh();
        oldRadius = radius;
        oldWidth = width;
        oldHeight = height;
        oldPoints = circlePoints;
	}

    private void GenerateMesh()
    {
        if (Mathf.Approximately(radius, 0))
        {
            GenerateFlatSurface();
        }
        else
        {
            GenerateCircleCutOut();
        }

    }

    private void GenerateFlatSurface()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[4];
        // Four corners
        vertices[0] = new Vector3(-width / 2, height / 2, 0);
        vertices[1] = new Vector3(width / 2, height / 2, 0);
        vertices[2] = new Vector3(width / 2, -height / 2, 0);
        vertices[3] = new Vector3(-width / 2, -height / 2, 0);
        // Triangles
        int[] triangles = new int[] { 0, 1, 2, 2, 3, 0 };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void GenerateCircleCutOut()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[circlePoints + 4];
        // Four corners
        vertices[0] = new Vector3(-width / 2, height / 2, 0);
        vertices[1] = new Vector3(width / 2, height / 2, 0);
        vertices[2] = new Vector3(width / 2, -height / 2, 0);
        vertices[3] = new Vector3(-width / 2, -height / 2, 0);

        // Central circle
        float angleStep = -360f / circlePoints;
        float angle = angleStep;
        vertices[4] = new Vector3(0, radius, 0);
        for (int i = 1; i < circlePoints; i++)
        {
            Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angle);
            vertices[i + 4] = quaternion * vertices[4];
            angle += angleStep;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = GetCircleTriangles(vertices);
    }

    private int[] GetCircleTriangles(Vector3[] vertices)
    {
        List<int> triangles = new List<int>();
        int pointsPerCorner = circlePoints / 4;
        for (int i = 0; i < circlePoints; i++)
        {
            int circlePoint = i + 4;
            int previousPoint = circlePoint - 1;
            if (previousPoint < 4)
            {
                previousPoint = vertices.Length - 1;
            }
            int nextPoint = circlePoint + 1;
            if (nextPoint >= vertices.Length)
            {
                nextPoint = 4;
            }

            Vector3 point = vertices[circlePoint];
            Vector3 previousVertex = vertices[previousPoint];
            Vector3 nextVertex = vertices[nextPoint];
            if (previousVertex.y < point.y && nextVertex.y < point.y)
            {
                triangles.Add(circlePoint);
                triangles.Add(0);
                triangles.Add(1);
            }
            else if (previousVertex.y > point.y && nextVertex.y > point.y)
            {
                triangles.Add(circlePoint);
                triangles.Add(2);
                triangles.Add(3);
            }
            else if (previousVertex.x < point.x && nextVertex.x < point.x)
            {
                triangles.Add(circlePoint);
                triangles.Add(1);
                triangles.Add(2);
            } 
            else if (previousVertex.x > point.x && nextVertex.x > point.x)
            {
                triangles.Add(circlePoint);
                triangles.Add(3);
                triangles.Add(0);
            }
            int corner;
            if (i < pointsPerCorner)
            {
                corner = 1;
            }
            else if (i < pointsPerCorner * 2)
            {
                corner = 2;
            }
            else if (i < pointsPerCorner * 3)
            {
                corner = 3;
            }
            else
            {
                corner = 0;
            }

            triangles.Add(circlePoint);
            triangles.Add(corner);
            triangles.Add(nextPoint);
        }
        return triangles.ToArray();
    }

    private bool isZero(float value)
    {
        return Mathf.Approximately(value, 0);
    }
}
