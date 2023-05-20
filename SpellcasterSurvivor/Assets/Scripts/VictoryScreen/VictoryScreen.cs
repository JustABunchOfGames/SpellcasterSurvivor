using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private UpgradeScreen _upgradeScreen;
    [SerializeField] private StageScreen _stageScreen;

    [SerializeField] private StageData _stageData;

    private void OnEnable()
    {
        _stageScreen.gameObject.SetActive(false);

        _upgradeScreen.gameObject.SetActive(true);
        _upgradeScreen.InitUpgradeCard(_stageData);
    }

    public void ChooseStage()
    {
        _upgradeScreen.gameObject.SetActive(false);

        _stageScreen.gameObject.SetActive(true);
        _stageScreen.InitStageScreen(_stageData);
    }
}
