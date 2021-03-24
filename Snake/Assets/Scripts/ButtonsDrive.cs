using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsDrive : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    [SerializeField] private bool _isRightButton;
    private SnakeController _snakeController;
    public void OnPointerDown(PointerEventData eventData)
    {
        _snakeController.ButtonTurnControl(_isRightButton);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _snakeController.ButtonStopTurn();
    }

    void Start()
    {
        _snakeController = FindObjectOfType<SnakeController>();
    }
}
