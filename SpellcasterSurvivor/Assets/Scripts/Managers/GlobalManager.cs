using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private UIManager _uiManager;
    private BattleManager _battleManager;
    private SpawnerManager _spawnerManager;
    private StateManager _stateManager;

    [SerializeField] private Player _player;
    [SerializeField] private StageData _stageData;

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
        _battleManager = GetComponent<BattleManager>();
        _spawnerManager = GetComponent<SpawnerManager>();
        _stateManager = GetComponent<StateManager>();

        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        // BattleManager init attacks & player attacks
        yield return StartCoroutine(_battleManager.StartBattle(_stageData.currentStage, _player));

        // UIManager create UI for those attacks
        _uiManager.InitUI(_player, _stageData.currentStage, _spawnerManager);

        // Pass StateManager to Player (for defeat condition)
        _player.SetStateManager(_stateManager);

        // SpawnerManager start to spawn enemies (passed StateManager for victory condition)
        _spawnerManager.StartSpawner(_stageData.currentStage, _player.transform, _stateManager);

        yield return null;
    }
}
