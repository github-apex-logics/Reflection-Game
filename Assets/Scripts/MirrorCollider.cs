using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollder;
    void Awake()
    {
        edgeCollder = this.GetComponent<EdgeCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Because of colliders default physics behaviour, and the timing of it
        //you have to use the rigidbody's velocity from the previous frame to avoid using a "post default interaction" velocity
        //Throwable.velocity allows for that if it's updated regularly (in Throwable.Update(), Path.VisualizePath() for example)
        Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
        //collidingRB.velocity = Vector3.Reflect(collision.transform.GetComponent<Throwable>()._rb.velocity, -collision.contacts[0].normal);
        collidingRB.velocity = Vector3.Reflect(collision.transform.localPosition, -collision.contacts[0].normal);

    }


    //runs when colliding if collider set to Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        //contact point is gotten by raycasting in the colliders velocity direction at the colliders position.
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, colliderRB.velocity);
        //second one is being used because first one is self, could probably ignore self-layer and get as Physics2D.Raycast() instead
        Vector2 contactPoint = hit2D[1].point;
        //Get normal of contact point by creating a line from the contact point to the closest collider point and rotating 90°
        Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
        //reflect the current velocity at the edge normal
        colliderRB.velocity = Vector2.Reflect(collider.transform.localPosition, normal);
    }


    //Goes through edgeCollider Points and returns the one closest to position
    Vector2 GetClosestPoint(Vector2 position)
    {
        Vector2[] points = edgeCollder.points;
        float shortestDistance = Vector2.Distance(position, points[0]);
        Vector2 closestPoint = points[0];
        foreach (Vector2 point in points)
        {
            if (Vector2.Distance(position, point) < shortestDistance)
            {
                shortestDistance = Vector2.Distance(position, point);
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}
