using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private Text _defeatText;

    [SerializeField] private GameObject _victoryScreen;
    private bool _victory;

    private void Start()
    {
        // Resume the game because the last stage paused it
        ResumeGame();

        _victory = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            // MenuScreen already shown in case of defeat, no need to activate/deactivate it
            if (!_defeatText.gameObject.activeSelf)
            {
                if (_menuScreen.activeSelf)
                {
                    _menuScreen.SetActive(false);
                    ResumeGame();
                }
                else
                {
                    _menuScreen.SetActive(true);
                    PauseGame();
                }
            }
        }
    }

    public void Victory()
    {
        PauseGame();
        _victory = true;
        _victoryScreen.SetActive(true);
    }

    public void Defeat()
    {
        PauseGame();
        _defeatText.gameObject.SetActive(true);
        _menuScreen.SetActive(true);
    }

    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        gameIsPaused = true;
    }

    private void ResumeGame()
    {
        // If it's paused because of a vicory, can't resume
        if (_victory)
            return;

        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
