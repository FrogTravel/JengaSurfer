using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private Button restartButton;

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
