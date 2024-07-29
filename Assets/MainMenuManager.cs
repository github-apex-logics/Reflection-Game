using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{

    public Button[] LevelButtons;

    int CurrentLevelno;


    // Start is called before the first frame update
    void Start()
    {
        CurrentLevelno = PlayerPrefs.GetInt("Level",0);
        //UpdateLevelBtnsUI(CurrentLevelno);
    }


    void UpdateLevelBtnsUI(int currentlevel)
    {
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        }
        for (int i = 0; i <= currentlevel; i++)
        {
            LevelButtons[i].interactable = true;
        }
    }

    public void SelectLevel()
    {
        PlayerPrefs.SetInt("LevelToBePlayed", CurrentLevelno);
        SceneManager.LoadScene("Gameplay");
    }
    
}
