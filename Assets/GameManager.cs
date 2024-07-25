using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawners;

    private void Start()
    {
       spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public void StartGame()
    {
        foreach (GameObject Spawner in spawners)
        {
            Spawner.GetComponent<Spawner>().StartLaser();
        }
    }
}
