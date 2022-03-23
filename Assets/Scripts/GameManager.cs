using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject congratulations;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject coinDestination;
    [SerializeField] private TextMeshProUGUI coinsAmountText;
    private Canvas canvas;

    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float pauseBetweenCoinsInstantiation = 0.29f;
    private PlayerController playerController;
    private bool _isStartGame = true;
    private bool _isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Button click Start
    public void OnStartGame()
    {
        playerController.StartMoving();

        _isStartGame = false;
        _isGameActive = true;

        DrawUI(false);
    }

    public bool IsGameActive()
    {
        return _isGameActive;
    }

    // State when crossing the finish line 
    public void FinishGame()
    {
        playerController.StopMoving();

        congratulations.SetActive(true);
        _isGameActive = false;

        DrawUI(true);

        DataManager.Instance.UpdateLevelByOne();

        StartCoroutine(StartCoinsAnimation());

        DataManager.Instance.CoinsAmount += 100;
        DataManager.Instance.SaveData();
    }

    // Draw UI with different game states
    private void DrawUI(bool isWin)
    {
        restartButton.gameObject.SetActive(!_isGameActive && !isWin);
        titleText.gameObject.SetActive(!_isGameActive && _isStartGame);
        finishText.gameObject.SetActive(!_isGameActive && isWin);
        loseText.gameObject.SetActive(!_isGameActive && !isWin);
        startButton.gameObject.SetActive(!_isGameActive && _isStartGame);
        nextLevelButton.gameObject.SetActive(!_isGameActive && isWin);
        coinPrefab.SetActive(!_isGameActive && isWin);
        coinDestination.SetActive(!_isGameActive && isWin);
        coinsAmountText.gameObject.SetActive(!_isGameActive && isWin);
    }

    // Animation for coins. The coin will fly from center of canvase to
    // Upper right corner where another sprite of coin waits
    IEnumerator StartCoinsAnimation()
    {
        Vector3 coinPos = coinDestination.transform.position;
        int initialCoins = DataManager.Instance.CoinsAmount;

        for (int i = 0; i < 10; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.SetParent(canvas.transform, false);
            coin.transform.DOMove(coinPos, animationDuration);
            initialCoins += 10;
            coinsAmountText.text = initialCoins.ToString();
            yield return new WaitForSeconds(pauseBetweenCoinsInstantiation);
        }
    }


    // State when lose a game 
    public void LoseGame()
    {
        playerController.StopMoving();

        _isGameActive = false;

        DrawUI(false);
    }

    // Click button "Restart" when lose a game 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Click button "Next level" when finish the level successfully
    public void OnNextLevel()
    {
        SceneManager.LoadScene(DataManager.Instance.GetNextLevelNumberAndUpdateLevel());
        _isStartGame = true;
    }
}
