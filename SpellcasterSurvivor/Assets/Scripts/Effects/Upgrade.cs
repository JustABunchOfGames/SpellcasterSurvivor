using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTrigger
{
    OnCast,
    OnHit,
    OnDestruct
}

public class Upgrade : MonoBehaviour
{
    [SerializeField] private AttackData _attackData;

    public string attackName;
    public string description;

    public Effect effectToUpgrade;

    // Bool to know if it's applicative bonus or set values (only for mana/cooldown)
    public bool setValues;

    // Attack Upgrade
    public int mana;
    public int cooldown;

    // Effect Upgrade, CommonData
    public float damageMultiplier;
    public int damageMinimum;

    public int lifeTime;

    // Effect Upgrade, Explosion & Shockwave
    public float scaleMultiplier;
    public float scaleMinimum;

    // Effect Upgrade, Stat Given
    public int statBonus;

    // Effect Upgrade, Fly
    public int jumpForce;
    public int gravity;

    // Effects to Add
    public bool addEffect;
    public EffectTrigger trigger;
    public Effect effectPrefabToAdd;

    public void UpgradeAttack()
    {
        _attackData.UpgradeAttack(this);
    }
}
