using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<AttackData> _attacksData;
    [SerializeField] private List<Upgrade> _initialUpgrades;

    public IEnumerator StartBattle(int currentStage, Player player)
    {
        if (currentStage == 1)
            yield return StartCoroutine(StartFirstBattle());

        player.InitAttack();

        yield return null;
    }

    private IEnumerator StartFirstBattle()
    {
        foreach(AttackData attackData in _attacksData)
        {
            attackData.Clear();
        }

        foreach(Upgrade upgrade in _initialUpgrades)
        {
            upgrade.UpgradeAttack();
        }

        yield return null;
    }
}
