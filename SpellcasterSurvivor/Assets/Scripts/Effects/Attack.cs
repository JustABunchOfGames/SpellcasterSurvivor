using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    public float cooldown;
    public float timer;
    public bool canCast;

    public Cooldown(float cooldown)
    {
        this.cooldown = cooldown;
        timer = cooldown;
        canCast = true;
    }
}

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackData _data;

    // [SerializeField] private List<Effect> _onCastEffects;

    private int _manaCost;
    private float _cooldown;

    private float _timer;
    private bool _canCast;

    private Cooldown _attackCooldown;

    private void Awake()
    {
        SetData();

        _timer = _cooldown;
        _canCast = true;

        _attackCooldown = new Cooldown(_cooldown);
    }

    private void Update()
    {
        if (!_canCast)
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = _cooldown;
            _canCast = true;
        }

        _attackCooldown.timer = _timer;
        _attackCooldown.canCast = _canCast;
    }

    public Cooldown GetAttackCooldown()
    {
        return _attackCooldown;
    }

    public void DoEffects(Player player, Transform target)
    {

        if (_canCast && _manaCost <= player.GetMana())
        {
            _canCast = false;
            player.UseMana(_manaCost);

            foreach (EffectData effectData in _data.onCastEffects)
            {
                Effect effect = Instantiate(effectData.effectPrefab, target.position, target.rotation);
                effect.SetData(effectData);
                effect.TriggerEffect();
            }
        }
    }

    private void SetData()
    {
        _manaCost = _data.manaCost;
        _cooldown = _data.cooldown;
    }
}
