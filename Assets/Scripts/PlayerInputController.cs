using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour, IPointerMoveHandler
{
    private PlayerController _playerController;
    [SerializeField] float inputSensitivity = 1;
    

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        _playerController.InputMoveCube(eventData.delta.x * inputSensitivity);
    }
}
