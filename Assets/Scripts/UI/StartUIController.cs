using UnityEngine;
using TMPro;
using UnityEngine.UI;

// In the beggining of game show this UI
public class StartUIController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI titleText;

    // Show or Hide UI
    public void SetActive(bool isActive)
    {
        startButton.gameObject.SetActive(isActive);
        titleText.gameObject.SetActive(isActive);
    }

    // Button click "Start"
    public void OnStartGame()
    {
        GameManager.CurrentMode = GameManager.GameModes.Game;
    }

}
