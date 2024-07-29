using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drag : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    public bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false; // Make sure Rigidbody2D is Dynamic
        rb.bodyType = RigidbodyType2D.Static;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Use continuous collision detection
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.isKinematic = false;
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            targetPosition = GetMouseWorldPos() + (offset * Time.deltaTime);
            rb.MovePosition(targetPosition); // Use MovePosition in FixedUpdate for consistent physics
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord; // Set z to the object's z position to keep it on the same plane
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
