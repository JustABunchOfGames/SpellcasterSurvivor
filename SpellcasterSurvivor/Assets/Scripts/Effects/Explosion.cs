using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Effect
{
    [SerializeField] private Vector3 _scalePerFrame;
    [SerializeField] private float _maxScale;

    public override void TriggerEffect()
    {
        
    }

    private void FixedUpdate()
    {
        // Testing only X bacause it's scaling everything at once anyway
        if (transform.localScale.x < _maxScale)
        {
            transform.localScale += _scalePerFrame;
        }
    }

    public override void SetData(EffectData effectData)
    {
        if (effectData.scalePerFrame != Vector3.zero)
            _scalePerFrame = effectData.scalePerFrame;

        if (effectData.maxScale != 0)
            _maxScale = effectData.maxScale;

        base.SetData(effectData);
    }
}
