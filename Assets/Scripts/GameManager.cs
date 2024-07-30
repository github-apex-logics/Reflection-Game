using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text TimerTxt;
    public Text LevelText;

    public GameObject GameCompPanel;
    public GameObject GamePausePanel;



    public GameObject[] Levels;

    public GameObject[] Spawners;
    public GameObject[] Mirrors;
    public GameObject[] Absorbers;

    public GameObject MidCircleBoundary;



    public float targetTime;
    [HideInInspector] public bool startTimer = false;


    private void Start()
    {
        Instance = this;
        TurnOnLevel();
    }

    private void Update()
    {
        if (startTimer)
        {
            targetTime -= Time.deltaTime;
            TimerTxt.text = ((int)targetTime).ToString();
            if (targetTime <=0)
            {
                GameCompPanel.SetActive(true);
                GameObject[] Lasers = GameObject.FindGameObjectsWithTag("Laser");
                foreach (GameObject laser in Lasers)
                {
                    laser.GetComponent<Rigidbody2D>().freezeRotation = true;
                    laser.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                startTimer = false;

                if (PlayerPrefs.GetInt("LevelToBePlayed") == 4)
                {
                    PlayerPrefs.SetInt("Level", 0);
                    PlayerPrefs.SetInt("LevelToBePlayed", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                    PlayerPrefs.SetInt("LevelToBePlayed", PlayerPrefs.GetInt("LevelToBePlayed") + 1);
                }
            }
            
        }
        else
            return;
    }

    public void StartGame()
    {
        Spawners = GameObject.FindGameObjectsWithTag("Spawner");
        Mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        Absorbers = GameObject.FindGameObjectsWithTag("Absorber");



        foreach (GameObject Spawner in Spawners)
        {
            Spawner.GetComponent<Spawner>().StartLaser();
        }

        foreach (GameObject mirror in Mirrors)
        {
            mirror.GetComponent<PolygonCollider2D>().isTrigger = false;

            mirror.AddComponent<MirrorCollider>();
        }

        foreach (GameObject absorber in Absorbers)
        {
            //absorber.GetComponent<BoxCollider2D>().enabled = false;
            absorber.GetComponent<PolygonCollider2D>().isTrigger = true;
            absorber.AddComponent<Absorber>();
        }

        MidCircleBoundary.GetComponent<PolygonCollider2D>().enabled = false;

        startTimer = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void TurnOnLevel()
    {
        Levels[PlayerPrefs.GetInt("LevelToBePlayed")].SetActive(true);
        LevelText.text = (PlayerPrefs.GetInt("LevelToBePlayed") + 1).ToString();
    }


    public void LevelPassNextBtnClicked()
    {
        if (PlayerPrefs.GetInt("LevelToBePlayed") == 4)
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("LevelToBePlayed", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("LevelToBePlayed", PlayerPrefs.GetInt("LevelToBePlayed") + 1);
        }
        
        Restart();
    } //not using it

    public void Pause()
    {
        GamePausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        GamePausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


}
