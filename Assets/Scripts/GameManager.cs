using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static StartUIController s_startController;
    private static NextLevelUIController s_nextLevelController;
    private static FailedUIController s_failedUIController;
    private static GameUIController s_gameUIController;

    private static PlayerController playerController;
    [SerializeField] private GameObject[] levels;

    public enum GameModes
    {
        Start, Finish, Lose, Game
    }

    private static GameModes s_mode = GameModes.Start;

    public static GameModes CurrentMode
    {
        get => s_mode;
        set
        {
            s_mode = value;
            ChangeUIState(s_mode);
        }
    }


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        s_startController = GetComponent<StartUIController>();
        s_nextLevelController = GetComponent<NextLevelUIController>();
        s_failedUIController = GetComponent<FailedUIController>();
        s_gameUIController = GetComponent<GameUIController>();

        CurrentMode = GameModes.Start;
        int levelIndex = DataManager.Instance.GetCurrentLevelNumber();
        Instantiate(levels[levelIndex]);
    }

    public static bool IsGameActive()
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
                s_startController.SetActive(true);
                s_nextLevelController.SetActive(false);
                s_failedUIController.SetActive(false);
                s_gameUIController.SetActive(false);

                playerController.StartMoving();
                break;
            case GameModes.Lose:
                s_startController.SetActive(false);
                s_nextLevelController.SetActive(false);
                s_failedUIController.SetActive(true);
                s_gameUIController.SetActive(false);
                break;
            case GameModes.Finish:
                s_startController.SetActive(false);
                s_nextLevelController.SetActive(true);
                s_failedUIController.SetActive(false);
                s_gameUIController.SetActive(false);

                playerController.StopMoving();
                break;
            case GameModes.Game:
                s_startController.SetActive(false);
                s_nextLevelController.SetActive(false);
                s_failedUIController.SetActive(false);
                s_gameUIController.SetActive(true);
                break;
        }
    }
}
