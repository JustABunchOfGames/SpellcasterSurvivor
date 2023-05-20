using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveManaBack : Effect
{
    [SerializeField] private Player _player;

    [SerializeField] private int _manaGiven;

    private new void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void TriggerEffect()
    {
        // negative value to give mana, not to use it
        _player.UseMana(_manaGiven * (-1));
    }

    public override void SetData(EffectData effectData)
    {
        _manaGiven = effectData.statGiven;
        base.SetData(effectData);
    }
}
