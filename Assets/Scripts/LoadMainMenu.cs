
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadMenu();
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
