using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTouchZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mirror") || collision.CompareTag("Absorber"))
        {
            collision.gameObject.GetComponent<MovingScript>().isBeingHeld = false;
        }
    }
}
