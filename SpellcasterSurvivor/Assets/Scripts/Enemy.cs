using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _hp;

    private SpawnerManager _spawnerManager;

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
            Die();
    }

    public void SetData(int hp, SpawnerManager spawnermanager)
    {
        _spawnerManager = spawnermanager;
        _hp = hp;
    }

    public void Die()
    {
        _spawnerManager.EnemyDie();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            player.TakeHit();
        }
    }
}
