using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StageData", fileName = "New")]
public class StageData : ScriptableObject
{
    public int currentStage = 1;
    public int stageSelected = 1;
}
