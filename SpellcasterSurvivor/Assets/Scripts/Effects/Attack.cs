using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    LeftClick,
    RightClick,
    E,
    R
}
public class Attack : MonoBehaviour
{
    [SerializeField] private Key _key;

    [SerializeField] private List<Effect> _onCastEffects;

    [SerializeField] private int _manaCost;

    [SerializeField] private float _cooldown;
    private float _timer = 0f;
    private bool _canCast = true;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _cooldown)
        {
            _timer = 0f;
            _canCast = true;
        }
    }

    // Used by effects to increase/decrease manaCost
    public void ReduceManaCost(int mana)
    {
        _manaCost -= mana;

        if (_manaCost < 0)
            _manaCost = 0;
    }

    public void ReduceCooldown(float cd)
    {
        _cooldown -= cd;

        if (_cooldown < 0)
            _cooldown = 0;
    }

    public Key GetKey()
    {
        return _key;
    }

    public void DoEffects(Player player, Transform target)
    {

        if (_canCast && _manaCost <= player.GetMana())
        {
            _canCast = false;
            player.UseMana(_manaCost);

            foreach (Effect effectPrefab in _onCastEffects)
            {
                Effect effect = Instantiate(effectPrefab, target.position, target.rotation);
                effect.TriggerEffect();
            }
        }
    }

    public void AddEffect(Effect effect, int index)
    {
        if(effect.trigger == EffectTrigger.OnHit)
            _onCastEffects[index].AddOnHitEffect(effect);
        
        if (effect.trigger == EffectTrigger.OnDestruct)
            _onCastEffects[index].AddOnDestructEffect(effect);
    }

    public void AddEffect(Effect effect)
    {
        if (effect.trigger == EffectTrigger.OnHit)
        {
            foreach (Effect onCastEffect in _onCastEffects)
            {
                onCastEffect.AddOnHitEffect(effect);
            }
        }

        if (effect.trigger == EffectTrigger.OnDestruct)
        {
            foreach (Effect onCastEffect in _onCastEffects)
            {
                onCastEffect.AddOnDestructEffect(effect);
            }
        }
    }
}
