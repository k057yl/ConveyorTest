using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _characterConfig;
    
    private CharacterView _characterView;
    private CharacterModel _characterModel;
    private InputController _inputController;
    private CharacterController _characterController;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _inputController = InputController.Instance;
        _characterModel = new CharacterModel();

        _characterView = GetComponent<CharacterView>();
        _characterController = GetComponent<CharacterController>();
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        Vector3 movement = _inputController.GetMovementInput();
        _characterController.Move(movement * (_characterConfig.Speed * Time.fixedDeltaTime));
        _characterView.PlayWalkAnimation(movement);
    }
}
