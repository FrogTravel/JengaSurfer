using System.Collections;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static StartUIController _startController;
    private static NextLevelUIController _nextLevelController;
    private static FailedUIController _failedUIController;

    private static PlayerController playerController;

    public enum GameModes
    {
        Start, Finish, Lose, Game
    }

    private static GameModes _mode = GameModes.Start;

    public static GameModes CurrentMode
    {
        get => _mode;
        set
        {
            _mode = value;
            ChangeUIState(_mode);
        }
    }


    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        _startController = GetComponent<StartUIController>();
        _nextLevelController = GetComponent<NextLevelUIController>();
        _failedUIController = GetComponent<FailedUIController>();

        CurrentMode = GameModes.Start;
    }

    public bool IsGameActive()
    {
        return CurrentMode == GameModes.Game;
    }

    // State when crossing the finish line 
    public void FinishGame()
    {
        CurrentMode = GameModes.Finish;

        DataManager.Instance.CoinsAmount += 100;
        DataManager.Instance.SaveData();
    }

    // State when lose a game 
    public void LoseGame()
    {
        playerController.StopMoving();

        CurrentMode = GameModes.Lose;
    }


    // Almost state machine. Each case is responsible to show and hide UI
    // Each state has its own class. Only one class must be shown at one case!!
    private static void ChangeUIState(GameModes m)
    {
        switch (m)
        {
            case GameModes.Start:
                _startController.SetActive(true);
                _nextLevelController.SetActive(false);
                _failedUIController.SetActive(false);

                playerController.StartMoving();
                break;
            case GameModes.Lose:
                _startController.SetActive(false);
                _nextLevelController.SetActive(false);
                _failedUIController.SetActive(true);
                break;
            case GameModes.Finish:
                _startController.SetActive(false);
                _nextLevelController.SetActive(true);
                _failedUIController.SetActive(false);

                playerController.StopMoving();
                break;
            case GameModes.Game:
                _startController.SetActive(false);
                _nextLevelController.SetActive(false);
                _failedUIController.SetActive(false);
                break;
        }
    }
}
