using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private StageData _stageData;

    public void Tutorial()
    {
        _stageData.currentStage = 1;
        _stageData.stageSelected = 1;

        SceneManager.LoadScene(2);
    }

    public void NewRun()
    {
        _stageData.currentStage = 1;
        _stageData.stageSelected = 1;

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
