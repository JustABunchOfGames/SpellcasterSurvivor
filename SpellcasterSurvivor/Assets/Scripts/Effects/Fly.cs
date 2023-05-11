using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Effect
{
    [SerializeField] private float _jumpforce;
    [SerializeField] private float _gravity;

    private PlayerMovement _playerMovement;

    private new void Awake()
    {
        base.Awake();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void TriggerEffect()
    {
        _playerMovement.Fly(_jumpforce, _gravity);
    }
}
