using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxMana;
    [SerializeField] private int mana;
    
    // Scriptable for attacks
    [SerializeField] private PlayerAttacks _currentAttacks;
    
    private List<Attack> _attackList = new List<Attack>();

    private void Start()
    {
        // Clear existing list
        _attackList.Clear();

        // Get the attacks from the scriptable (updated from other scenes)
        List<Attack> attacks = new List<Attack>();
        attacks = _currentAttacks.GetAttacks();

        // Instantiate those attacks for usage
        for(int i = 0; i < attacks.Count; i++)
        {
            _attackList.Add(Instantiate(attacks[i], this.transform));
        }
    }

    void Update()
    {
        if (Input.GetButton("Attack1"))
        {
            _attackList[0].DoEffects(this, Camera.main.transform);
        }

        if (Input.GetButton("Attack2"))
        {
            _attackList[1].DoEffects(this, Camera.main.transform);
        }

        if(Input.GetButton("Attack3"))
        {
            _attackList[2].DoEffects(this, Camera.main.transform);
        }

        if (Input.GetButton("Attack4"))
        {
            _attackList[3].DoEffects(this, Camera.main.transform);
        }
    }

    // Used by attacks to drain mana, but can be used by effects to give mana back with negative values
    public void UseMana(int manaUsed)
    {
        mana -= manaUsed;

        if (mana < 0)
            mana = 0;

        if (mana > maxMana)
            mana = maxMana;
    }

    public int GetMana()
    {
        return mana;
    }
}
