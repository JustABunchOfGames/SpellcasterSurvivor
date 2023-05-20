using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Effect
{
    [SerializeField] private Vector3 _scalePerFrame;
    [SerializeField] private float _maxScale;

    [SerializeField] private bool castOnSelf = false;

    
    private void FixedUpdate()
    {
        base.Update();

        // Testing only X bacause it's scaling everything at once anyway
        if (transform.localScale.x < _maxScale)
        {
            transform.localScale += _scalePerFrame;
        }
    }
    
    public override void TriggerEffect()
    {
        if (!castOnSelf)
        {
            // Ray forward from camera transform
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            // If colliding with terrain, cast here, else cast on self
            if (hit.collider != null && hit.collider.GetComponent<TerrainCollider>() != null)
            {
                transform.position = hit.point;
            }
        }

        // Resetting rotation anyway in case of casting on self
        transform.rotation = Quaternion.identity;
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
