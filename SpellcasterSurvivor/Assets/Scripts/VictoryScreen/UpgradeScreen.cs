using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private UpgradeList _upgradeList;

    [SerializeField] private List<UpgradeCard> _upgradeCards;

    [SerializeField] private AnimationCurve _easyCurve;
    [SerializeField] private AnimationCurve _mediumCurve;
    [SerializeField] private AnimationCurve _hardCurve;

    public void InitUpgradeCard(StageData stageData)
    {
        AnimationCurve curve;
        switch (stageData.stageSelected)
        {
            case 1:
                curve = _easyCurve;
                break;
            case 2:
                curve = _mediumCurve;
                break;
            case 5:
                curve = _hardCurve;
                break;
            default:
                curve = _easyCurve;
                break;

        }

        foreach (UpgradeCard upgradeCard in _upgradeCards)
        {
            // Getting a random index weighted by the curve
            float random = curve.Evaluate(Random.value);
            int index = (int)(random * _upgradeList.list.Count);

            upgradeCard.SetUpgrade(_upgradeList.list[index]);
        }
    }
}
