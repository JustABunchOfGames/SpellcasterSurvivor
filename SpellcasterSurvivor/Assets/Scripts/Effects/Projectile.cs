using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Effect
{
    [SerializeField] private int _speed;

    public override void TriggerEffect()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
    
    private new void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<TerrainCollider>() != null)
            Destruct();
        else if (collision.collider.tag == "Enemy")
        {
            base.OnCollisionEnter(collision);
            Destroy(gameObject);
        }
    }

    public override void SetData(EffectData effectData)
    {
        if (effectData.speed != 0)
            _speed = effectData.speed;
        
        base.SetData(effectData);
    }
}
