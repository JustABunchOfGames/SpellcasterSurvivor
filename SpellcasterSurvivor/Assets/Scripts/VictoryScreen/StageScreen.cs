using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScreen : MonoBehaviour
{
    private StageData _stageData;

    public void InitStageScreen(StageData stageData)
    {
        _stageData = stageData;
    }

    public void SelectStage(int stage)
    {
        _stageData.currentStage += stage;
        _stageData.stageSelected = stage;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
