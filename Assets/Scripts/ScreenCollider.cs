using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    public int cornerSegments = 10; // Number of segments for the curve
    public float cornerRadius = 0.5f; // Radius of the corners

    void Awake()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        CreateEdgeCollider();
    }

    // Call this at start and whenever the resolution changes
    void CreateEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();

        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        // Create curved edges
        edges.AddRange(CreateCurvedCorner(bottomLeft, new Vector2(1, 0), new Vector2(0, 1))); // Bottom left
        edges.AddRange(CreateStraightEdge(bottomLeft + new Vector2(cornerRadius, 0), bottomRight - new Vector2(cornerRadius, 0))); // Bottom edge
        edges.AddRange(CreateCurvedCorner(bottomRight, new Vector2(0, 1), new Vector2(-1, 0))); // Bottom right
        edges.AddRange(CreateStraightEdge(bottomRight + new Vector2(0, cornerRadius), topRight - new Vector2(0, cornerRadius))); // Right edge
        edges.AddRange(CreateCurvedCorner(topRight, new Vector2(-1, 0), new Vector2(0, -1))); // Top right
        edges.AddRange(CreateStraightEdge(topRight - new Vector2(cornerRadius, 0), topLeft + new Vector2(cornerRadius, 0))); // Top edge
        edges.AddRange(CreateCurvedCorner(topLeft, new Vector2(0, -1), new Vector2(1, 0))); // Top left
        edges.AddRange(CreateStraightEdge(topLeft - new Vector2(0, cornerRadius), bottomLeft + new Vector2(0, cornerRadius))); // Left edge

        edgeCollider.SetPoints(edges);
    }

    // Create points for a straight edge between two points
    List<Vector2> CreateStraightEdge(Vector2 start, Vector2 end)
    {
        return new List<Vector2> { start, end };
    }

    // Create points for a curved corner
    List<Vector2> CreateCurvedCorner(Vector2 center, Vector2 startDir, Vector2 endDir)
    {
        List<Vector2> cornerPoints = new List<Vector2>();

        float angleStep = 90f / cornerSegments;
        Vector2 start = center + startDir * cornerRadius;
        for (int i = 0; i <= cornerSegments; i++)
        {
            float angle = i * angleStep;
            Vector2 offset = Quaternion.Euler(0, 0, angle) * (startDir * cornerRadius);
            Vector2 point = center + offset;
            cornerPoints.Add(point);
        }

        return cornerPoints;
    }

    // Runs when colliding if collider is Not set to Trigger
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
        collidingRB.velocity = Vector3.Reflect(collision.relativeVelocity, collision.contacts[0].normal);
    }

    // Runs when colliding if collider is set to Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        RaycastHit2D hit2D = Physics2D.Raycast(collider.transform.position, colliderRB.velocity);
        Vector2 contactPoint = hit2D.point;
        Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
        colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, normal);
    }

    // Goes through edgeCollider Points and returns the one closest to position
    Vector2 GetClosestPoint(Vector2 position)
    {
        Vector2[] points = edgeCollider.points;
        float shortestDistance = Vector2.Distance(position, points[0]);
        Vector2 closestPoint = points[0];
        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(position, point);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}
