using UnityEngine;

public class InputController
{
    private static InputController _instance;
    public static InputController Instance => _instance ?? (_instance = new InputController());

    private NewInput _newInput;

    private InputController()
    {
        _newInput = new NewInput();
        _newInput.Enable();
    }

    public bool GetTouch()
    {
        return _newInput.Gameplay.Touch.triggered;
    }

    public Vector2 GetTouchPosition()
    {
        if (_newInput.Gameplay.Touch.triggered)
        {
            int touchCount = Input.touchCount;
            if (touchCount > Constants.ZERO)
            {
                Touch touch = Input.GetTouch(Constants.ZERO);
                
                if (touch.position.x >= Constants.ZERO && touch.position.x <= Screen.width && touch.position.y >= Constants.ZERO && touch.position.y <= Screen.height)
                {
                    return touch.position;
                }
            }
        }

        return Vector2.zero;
    }
}
