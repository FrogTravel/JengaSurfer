using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Image input;

    public void SetActive(bool isActive)
    {
        input.gameObject.SetActive(isActive);
    }
}
