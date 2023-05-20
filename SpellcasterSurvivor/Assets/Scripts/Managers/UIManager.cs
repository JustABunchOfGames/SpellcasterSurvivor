using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Attacks Cooldown")]
    [SerializeField] private List<Image> _imageCooldown;
    [SerializeField] private List<Text> _textCooldown;

    [Header("Mana Bar")]
    [SerializeField] private Image _manabarImage;
    [SerializeField] private Text _manabarText;

    [Header("Stage Data")]
    [SerializeField] private Text _stageText;
    [SerializeField] private Text _enemyText;

    private Player _player;
    private SpawnerManager _spawnerManager;

    private List<Cooldown> _attacksCooldown;

    private bool _initFinished = false;

    public void InitUI(Player player, int stage, SpawnerManager spawnerManager)
    {
        // Save player
        _player = player;

        // Save SpawnerManager
        _spawnerManager = spawnerManager;

        //Init UI for stage
        _stageText.text = "Stage : " + stage;

        // Get attacks
        List<Attack> attacks = _player.GetAttacks();
        _attacksCooldown = new List<Cooldown>();

        // Init attacks
        for (int i = 0; i < attacks.Count; i++)
        {
            _attacksCooldown.Add(attacks[i].GetAttackCooldown());

            _textCooldown[i].gameObject.SetActive(false);
            _imageCooldown[i].fillAmount = 0;
            _imageCooldown[i].gameObject.SetActive(false);
        }
        
        // Update can start
        _initFinished = true;
    }

    private void Update()
    {
        if (!_initFinished)
            return;

        // Update Cooldowns
        for (int i = 0; i < _attacksCooldown.Count; i++)
        {
            if (_attacksCooldown[i].canCast)
            {
                _textCooldown[i].gameObject.SetActive(false);
                _imageCooldown[i].gameObject.SetActive(false);
            }
            else
            {
                _textCooldown[i].gameObject.SetActive(true);
                _imageCooldown[i].gameObject.SetActive(true);
            }

            _textCooldown[i].text = Math.Round(_attacksCooldown[i].timer, 1).ToString();
            _imageCooldown[i].fillAmount = _attacksCooldown[i].timer / _attacksCooldown[i].cooldown;
        }

        // Update Mana
        int maxMana = _player.GetMaxMana();
        int mana = _player.GetMana();
        _manabarText.text = mana + " / " + maxMana;
        _manabarImage.fillAmount = (float)mana / (float)maxMana;

        // Update number of enemy to kill
        _enemyText.text = "Enemy to kill : " + _spawnerManager.GetNumberOfEnemyToKill();
    }

    // Pause Buttons
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
