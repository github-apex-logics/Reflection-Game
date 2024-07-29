using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private float timeDuration;
    [SerializeField] private Slider timerSlider;
    private bool stopTimer;
    private float timer;



    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();

        Debug.Log(timer);
    }
    void Update()
    {
        if (timer < timeDuration)
        {
            timer += Time.deltaTime;
            UpdateSlider(timer);
        }
        else
        {
            LoadMenu();
        }
        
    }
    private void ResetTimer()
    {
        timer = 0;
        timer = Mathf.RoundToInt(timer);
        timerSlider.maxValue = timeDuration;
        timerSlider.value = timer;
    }
    
    private void ResetSlider(float time)
    {
        timerSlider.maxValue = time;
    }
    private void UpdateSlider(float time)
    {
        timerSlider.value = time;
    }
    







    public void LoadMenu()
    {
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    // Coroutine to load the scene
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start loading the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // While the scene is loading, you can add a loading screen or show progress
        while (!asyncLoad.isDone)
        {
            // Optionally, you can use asyncLoad.progress to show loading progress
            //Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}
