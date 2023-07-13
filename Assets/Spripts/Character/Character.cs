using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _characterConfig;
    
    private CharacterView _characterView;
    private CharacterModel _characterModel;
    public CharacterModel CharacterModel => _characterModel;
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

    private void Update()
    {
        HandleInput();
        
        if (_characterModel.IsWin)
        {
            _characterView.PlayWinAnimation();
        }
    }

    private void Move()
    {
        Vector3 movement = _inputController.GetMovementInput();
        _characterController.Move(movement * (_characterConfig.Speed * Time.fixedDeltaTime));
        _characterView.PlayWalkAnimation(movement);
    }

    private void HandleInput()
    {
        if (_inputController.GetMouseLeft())
        {
            _characterView.PlayTakeItemAnimation();
            Grab();
        }
    }

    private void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag(_characterModel.CurrentName))
            {
                Destroy(hit.collider.gameObject);
                _characterModel.CollectFruit(Constants.ONE);
            }
        }
    }
}
