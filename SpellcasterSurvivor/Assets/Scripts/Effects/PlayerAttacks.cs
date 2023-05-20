using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttacks", fileName ="New")]
public class PlayerAttacks : ScriptableObject
{
    [SerializeField] private List<Attack> _attacks;

    public List<Attack> GetAttacks()
    {
        return _attacks;
    }

    public void SetAttacks(List<Attack> attacks)
    {
        _attacks = attacks;
    }
}
