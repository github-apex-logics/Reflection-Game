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
    public Transform[] RandomPos;



    public GameObject MidCircleBoundary;
    public GameObject MidCircle;


    public float targetTime;
    [HideInInspector] public bool startTimer = false;
    int TotalMA;
    int TotalMirrors;
    int TotalAbsorbers;

    private void Start()
    {
        Instance = this;
        //TurnOnLevel();

        Application.targetFrameRate = -1;
        //Spawners = GameObject.FindGameObjectsWithTag("Spawner");
        //Mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        //Absorbers = GameObject.FindGameObjectsWithTag("Absorber");

        SetMirrors();
        SetAbsorbers();



        TotalMA = TotalMirrors + TotalAbsorbers;

        SetSpawners();

    }

    void SetSpawners()
    {
        int TotalSpawners = Random.Range(0, Spawners.Length);

        if(TotalSpawners > TotalMA)
        {
            SetSpawners();
            return;
        }
        
        int SpawnerCount = 0;
        for (int i = 0; i < Spawners.Length; i++)
        {
            if (SpawnerCount <= TotalSpawners)
            {
                int num = Random.Range(0, Spawners.Length);
                if (Spawners[num].activeInHierarchy == false)
                {
                    Spawners[num].SetActive(true);
                    SpawnerCount++;
                }
            }
        }
    }

    void SetMirrors()
    {
        float x;
        float y;

        TotalMirrors = Random.Range(0, Mirrors.Length);
        int MirrorCount = 0;
        foreach (GameObject mirror in Mirrors)
        {
            x = Random.Range(3.63f, -3.63f);
            y = Random.Range(3.95f, -3.95f);
            mirror.transform.position = new Vector3(x, y, 0);
        }

        for (int i = 0; i <= TotalMirrors; i++)
        {
            if (MirrorCount <= TotalMirrors)
            {
                int num = Random.Range(0, Mirrors.Length);
                if (Mirrors[num].activeInHierarchy == false)
                {
                    Mirrors[num].SetActive(true);
                    MirrorCount++;
                }
            }
        }

        foreach (GameObject mirror in Mirrors) //bounds.Intersects(collider2.bounds) IsTouching(MidCircleBoundary.GetComponent<PolygonCollider2D>()
        {
            if (mirror.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircleBoundary.GetComponent<PolygonCollider2D>().bounds) || mirror.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircle.GetComponent<CircleCollider2D>().bounds))
            {
                mirror.transform.position = RandomPos[Random.Range(0, RandomPos.Length)].position;
            }
        }

    }

    

    void SetAbsorbers()
    {
        float x;
        float y;


        TotalAbsorbers = Random.Range(0, Absorbers.Length);
        int AbsorberCount = 0;
        foreach (GameObject absorber in Absorbers)
        {
            x = Random.Range(3.63f, -3.63f);
            y = Random.Range(3.95f, -3.95f);
            absorber.transform.position = new Vector3(x, y, 0);
        }

        for (int i = 0; i < Absorbers.Length; i++)
        {
            if (AbsorberCount <= TotalAbsorbers)
            {
                int num = Random.Range(0, Absorbers.Length);
                if (Absorbers[num].activeInHierarchy == false)
                {
                    Absorbers[num].SetActive(true);
                    AbsorberCount++;
                }
            }
        }

        foreach (GameObject absorber in Absorbers)
        {
            if (absorber.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircleBoundary.GetComponent<PolygonCollider2D>().bounds) || absorber.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircle.GetComponent<CircleCollider2D>().bounds))
            {
                absorber.transform.position = RandomPos[Random.Range(0, RandomPos.Length)].position;
            }
        }
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
        foreach (GameObject Spawner in Spawners)
        {
            if (Spawner.activeInHierarchy == true)
            {
                Spawner.GetComponent<Spawner>().StartLaser();
            }
        }

        foreach (GameObject mirror in Mirrors)
        {
            mirror.GetComponent<PolygonCollider2D>().isTrigger = false;

            //mirror.AddComponent<MirrorCollider>();
        }

        foreach (GameObject absorber in Absorbers)
        {
            //absorber.GetComponent<BoxCollider2D>().enabled = false;
            absorber.GetComponent<PolygonCollider2D>().isTrigger = true;
            absorber.AddComponent<Absorber>();
        }

        MidCircleBoundary.GetComponent<PolygonCollider2D>().enabled = false;
        MidCircleBoundary.GetComponent<SpriteRenderer>().enabled = false;


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
