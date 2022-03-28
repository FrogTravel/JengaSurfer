using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

// When we successfully finish level this UI should be shown
public class NextLevelUIController : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject coinDestination;
    [SerializeField] private GameObject congratulations;
    [SerializeField] private TextMeshProUGUI coinsAmountText;
    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float pauseBetweenCoinsInstantiation = 0.29f;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    // Click button "Next level" when finish the level successfully
    public void OnNextLevel()
    {
        SceneManager.LoadScene(DataManager.Instance.GetNextLevelNumberAndUpdateLevel());

        GameManager.CurrentMode = GameManager.GameModes.Start;
    }

    // Show or Hide UI 
    public void SetActive(bool isActive)
    {
        nextLevelButton.gameObject.SetActive(isActive);
        coinPrefab.gameObject.SetActive(isActive);
        coinDestination.gameObject.SetActive(isActive);
        congratulations.SetActive(isActive);
        coinsAmountText.gameObject.SetActive(isActive);
        finishText.gameObject.SetActive(isActive);
        if(isActive)
            StartCoroutine(StartCoinsAnimation());
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
}
