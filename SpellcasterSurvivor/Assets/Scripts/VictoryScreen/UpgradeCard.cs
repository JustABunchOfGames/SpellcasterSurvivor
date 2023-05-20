using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Text _header;
    [SerializeField] private Text _body;

    private Upgrade _upgrade;

    private void SetHeader(string attackName)
    {
        _header.text = _header.text + " " + attackName;
    }

    public void SetBody(string upgradeDescription)
    {
        _body.text = upgradeDescription;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;

        SetHeader(upgrade.attackName);
        SetBody(upgrade.description);
    }

    public void ApplyUpgrade()
    {
        _upgrade.UpgradeAttack();
        GetComponentInParent<VictoryScreen>().ChooseStage();
    }
}
