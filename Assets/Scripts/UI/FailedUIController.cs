using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// When the user fail the level
public class FailedUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private Button restartButton;

    // Show or Hide UI
    public void SetActive(bool isActive)
    {
        loseText.gameObject.SetActive(isActive);
        restartButton.gameObject.SetActive(isActive);
    }


    // Click button "Restart" when lose a game 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.CurrentMode = GameManager.GameModes.Start;
    }

}
