using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AttackData", fileName = "New")]
public class AttackData : ScriptableObject
{
    public int manaCost;
    public float cooldown;
    public List<EffectData> onCastEffects = new List<EffectData>();

    public void Clear()
    {
        manaCost = 0;
        cooldown = 0;
        onCastEffects.Clear();
    }

    public void UpgradeAttack(Upgrade upgrade)
    {
        if (upgrade.mana != 0)
        {
            if (upgrade.setValues)
                manaCost = upgrade.mana;
            else
                ReduceManaCost(upgrade.mana);
        }

        if (upgrade.cooldown != 0 )
        {
            if (upgrade.setValues)
                cooldown = upgrade.cooldown;
            else
                ReduceCooldown(upgrade.cooldown);
        }

        if (upgrade.addEffect && upgrade.trigger == EffectTrigger.OnCast && onCastEffects.Count < EffectLimit.MaxNumberOfEffect)
        {
            EffectData effectData = new EffectData(upgrade);
            onCastEffects.Add(effectData);
            return; // Adding OnCast effect needs to let the upgrade.effectToUpgrade at null
        }

        foreach (EffectData effectData in onCastEffects)
            effectData.UpgradeEffect(upgrade);
    }

    // Used by effects to increase/decrease manaCost
    private void ReduceManaCost(int mana)
    {
        manaCost -= mana;

        if (manaCost < 0)
            manaCost = 0;
    }

    private void ReduceCooldown(float cd)
    {
        cooldown -= cd;

        if (cooldown < 0)
            cooldown = 0;
    }
}

public class EffectData
{
    // Effect prefab that will be instantiated 
    [Header("Effect Prefab")]
    public Effect effectPrefab;

    // Common data for every effect
    [Header("Common Data")]
    public int damage;
    public float lifeTime;

    // Explosion & Shockwave data
    [Header("Explosion & Shockwave Data")]
    public Vector3 scalePerFrame;
    public float maxScale;

    // Projectile data
    [Header("Projectile Data")]
    public int speed;

    // Stats given data (GiveManaBack for example)
    [Header("Stat Giving Data")]
    public int statGiven;

    // Fly data
    [Header("Fly Data")]
    public float jumpforce;
    public float gravity;

    // Link to other effects
    [Header("Other effect link")]
    public List<EffectData> onHitEffects = new List<EffectData>();
    public List<EffectData> onDestructEffects = new List<EffectData>();

    public EffectData(Upgrade upgrade)
    {
        effectPrefab = upgrade.effectPrefabToAdd;
        damage = upgrade.damageMinimum;
        lifeTime = upgrade.lifeTime;
        maxScale = upgrade.scaleMinimum;
        statGiven = upgrade.statBonus;
        jumpforce = upgrade.jumpForce;
        gravity = upgrade.gravity;
    }

    public void UpgradeEffect(Upgrade upgrade)
    {
        // Apply those upgrades to all effects
        foreach (EffectData effectD in onHitEffects)
            effectD.UpgradeEffect(upgrade);

        foreach (EffectData effectD in onDestructEffects)
            effectD.UpgradeEffect(upgrade);

        // Upgrade this effect, and add one if needed
        if (upgrade.effectToUpgrade.GetType() == effectPrefab.GetType())
        {
            // ------------------------------------------------------------------ Add Effect
            if (upgrade.addEffect)
            {
                if (upgrade.trigger == EffectTrigger.OnHit && onHitEffects.Count < EffectLimit.MaxNumberOfEffect)
                {
                    // If the effect already exist, double its damage instead of recreating it
                    bool effectExist = false;
                    foreach(EffectData effectD in onHitEffects)
                    {
                        if (upgrade.effectPrefabToAdd.GetType() == effectD.effectPrefab.GetType())
                        {
                            effectD.damage *= 2;
                            effectExist = true;
                        }
                    }

                    if (!effectExist) {
                        EffectData effectData = new EffectData(upgrade);
                        onHitEffects.Add(effectData);
                    }
                }

                if (upgrade.trigger == EffectTrigger.OnDestruct && onDestructEffects.Count < EffectLimit.MaxNumberOfEffect)
                {
                    // If the effect already exist, double its damage instead of recreating it
                    bool effectExist = false;
                    foreach (EffectData effectD in onDestructEffects)
                    {
                        if (upgrade.effectPrefabToAdd.GetType() == effectD.effectPrefab.GetType())
                        {
                            effectD.damage *= 2;
                            effectExist = true;
                        }
                    }

                    if (!effectExist)
                    {
                        EffectData effectData = new EffectData(upgrade);
                        onDestructEffects.Add(effectData);
                    }
                }
            }
            // ------------------------------------------------------------------ Upgrade Effect
            else
            {
                // Damage
                int newDamage = (int) (damage * upgrade.damageMultiplier);
                if (newDamage < damage + upgrade.damageMinimum)
                    newDamage = damage + upgrade.damageMinimum;
                damage = newDamage;

                // lifeTime
                lifeTime += upgrade.lifeTime;

                // Scale
                float newScale = maxScale * upgrade.scaleMultiplier;
                if (newScale < maxScale + upgrade.scaleMinimum)
                    newScale = maxScale + upgrade.scaleMinimum;
                maxScale = newScale;

                // Stat Given
                statGiven += upgrade.statBonus;

                // Fly
                jumpforce += upgrade.jumpForce;
                gravity -= upgrade.gravity;
            }
        }
    }
}

public static class EffectLimit
{
    public const int MaxNumberOfEffect = 3;
}
