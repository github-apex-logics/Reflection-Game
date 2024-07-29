using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTouchZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mirror") || collision.CompareTag("Absorber"))
        {
            collision.gameObject.GetComponent<Drag>().isDragging = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror") || collision.gameObject.CompareTag("Absorber"))
        {
            collision.gameObject.GetComponent<Drag>().isDragging = false;
        }
    }



   
}
