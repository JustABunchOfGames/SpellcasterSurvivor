using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 1;

    [SerializeField] private int _maxMana;
    [SerializeField] private int _mana;
    
    // Scriptable for attacks
    [SerializeField] private PlayerAttacks _currentAttacks;
    
    private List<Attack> _attackList = new List<Attack>();

    private StateManager _stateManager;

    private void Awake()
    {
        
    }

    public void InitAttack()
    {
        // Clear existing list
        _attackList.Clear();

        // Get the attacks from the scriptable (updated from other scenes)
        List<Attack> attacks = new List<Attack>();
        attacks = _currentAttacks.GetAttacks();

        // Instantiate those attacks for usage
        for (int i = 0; i < attacks.Count; i++)
        {
            _attackList.Add(Instantiate(attacks[i], this.transform));
        }
    }

    public void SetStateManager(StateManager stateManager)
    {
        _stateManager = stateManager;
    }

    private void Update()
    {
        if (StateManager.gameIsPaused)
            return;

        if (Input.GetButton("Attack1"))
        {
            Attack(0);
        }

        if (Input.GetButton("Attack2"))
        {
            Attack(1);
        }

        if(Input.GetButton("Attack3"))
        {
            Attack(2);
        }

        if (Input.GetButton("Attack4"))
        {
            Attack(3);
        }
    }

    private void Attack(int index)
    {
        // Passing Player for manaCost
        // Passing Camera transform (linked to the player) to cast from self (either to ray from player or cast a projectile)
        // Camera has the Position/Rotation
        _attackList[index].DoEffects(this, Camera.main.transform);
    }

    // Used by attacks to drain mana, but can be used by effects to give mana back with negative values
    public void UseMana(int manaUsed)
    {
        _mana -= manaUsed;

        if (_mana < 0)
            _mana = 0;

        if (_mana > _maxMana)
            _mana = _maxMana;
    }

    public int GetMana()
    {
        return _mana;
    }

    public int GetMaxMana()
    {
        return _maxMana;
    }

    public void TakeHit()
    {
        _hp--;

        if (_hp <= 0)
            Die();
    }

    private void Die()
    {
        _stateManager.Defeat();
    }

    // Pass Attacks to the UIManager
    public List<Attack> GetAttacks()
    {
        return _attackList;
    }
}
