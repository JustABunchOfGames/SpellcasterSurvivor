using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectTrigger
{
    OnCast,
    OnHit,
    OnDestruct
}

public class Effect : MonoBehaviour
{
    public int damage;
    public EffectTrigger trigger;

    [SerializeField] private float _timeBeforeDestruct;

    [SerializeField] private List<Effect> _onHitEffects = new List<Effect>();
    [SerializeField] private List<Effect> _onDestructEffects = new List<Effect>();

    protected Rigidbody _rigidbody;
    private float _timer;

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

        foreach (Effect effectPrefab in _onHitEffects)
        {
            Effect effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            effect.TriggerEffect();
        }
    }

    // For effect that can be trigger (no need to collide with terrain)
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        foreach (Effect effectPrefab in _onHitEffects)
        {
            Effect effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            effect.TriggerEffect();
        }
    }

    protected void Destruct()
    {
        foreach (Effect effectPrefab in _onDestructEffects)
        {
            Effect effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            effect.TriggerEffect();
        }

        Destroy(gameObject);
    }

    public virtual void TriggerEffect()
    {

    }

    public void AddOnHitEffect(Effect effect)
    {
        _onHitEffects.Add(effect);
    }

    public void AddOnDestructEffect(Effect effect)
    {
        _onDestructEffects.Add(effect);
    }
}
