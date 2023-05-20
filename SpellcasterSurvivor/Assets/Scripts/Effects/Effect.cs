using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public int damage;

    [SerializeField] private float _timeBeforeDestruct;

    protected Rigidbody _rigidbody;
    private float _timer;

    private EffectData _effectData;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _timer = 0;
    }

    protected void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBeforeDestruct)
            Destruct();
    }

    // For effects that needs to not be a trigger (to collide with terrain)
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Enemy")
            return;

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        enemy.TakeDamage(damage);

        foreach (EffectData effectData in _effectData.onHitEffects)
        {
            Effect effect = Instantiate(effectData.effectPrefab, transform.position, Quaternion.identity);
            effect.SetData(effectData);
            effect.TriggerEffect();
        }
    }

    // For effect that can be trigger (no need to collide with terrain)
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        Enemy enemy = other.GetComponent<Enemy>();
        enemy.TakeDamage(damage);

        foreach (EffectData effectData in _effectData.onHitEffects)
        {
            Effect effect = Instantiate(effectData.effectPrefab, transform.position, Quaternion.identity);
            effect.SetData(effectData);
            effect.TriggerEffect();
        }
    }

    protected void Destruct()
    {
        foreach (EffectData effectData in _effectData.onDestructEffects)
        {
            Effect effect = Instantiate(effectData.effectPrefab, transform.position, Quaternion.identity);
            effect.SetData(effectData);
            effect.TriggerEffect();
        }

        Destroy(gameObject);
    }

    public virtual void TriggerEffect()
    {

    }

    public virtual void SetData(EffectData effectData)
    {
        damage = effectData.damage;
        _timeBeforeDestruct = effectData.lifeTime;

        // Save for other effects later
        _effectData = effectData;
    }
}
