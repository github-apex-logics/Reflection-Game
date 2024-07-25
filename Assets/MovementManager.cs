using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;
    private Plane dragPlane;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Replace with your desired input condition
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit hit = Physics.Raycast(mousePosition, mousePosition);

            //if (hit.collider != null)
            //{
            //    Debug.Log("name = " + hit.collider.name);
            //}
        }

    }
}
