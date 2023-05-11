using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveManaBack : Effect
{
    [SerializeField] private int _manaGiven;

    [SerializeField] private Player _player;

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
}
