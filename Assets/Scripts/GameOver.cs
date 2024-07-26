using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            GameOverPanel.SetActive(true);
            GameObject[] Lasers = GameObject.FindGameObjectsWithTag("Laser");
            foreach (GameObject laser in Lasers)
            {
                laser.GetComponent<Rigidbody2D>().freezeRotation = true;
                laser.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            GameManager.Instance.startTimer = false;

        }
    }

}
