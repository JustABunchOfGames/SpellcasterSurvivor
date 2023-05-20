using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeList", fileName = "New")]
public class UpgradeList : ScriptableObject
{
    public List<Upgrade> list;
}
