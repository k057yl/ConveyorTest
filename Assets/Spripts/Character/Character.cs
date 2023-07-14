using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GameObject _textPrefab;

    private CharacterView _characterView;
    private CharacterModel _characterModel;
    public CharacterModel CharacterModel => _characterModel;
    private InputController _inputController;
    private CharacterController _characterController;
    private UIBarController _uiBarController;
    private ObjectFactory _objectFactory;
    
    
    private void Awake()
    {
        InitializeComponents();
    }

    public void InitializeUIBar(UIBarController uiBarController)
    {
        _uiBarController = uiBarController;
    }
    
    public void InitializeObjectFactory(ObjectFactory objectFactory)
    {
        _objectFactory = objectFactory;
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
            _uiBarController.Win();
            _characterView.PlayWinAnimation();
            _objectFactory.Conveyor.gameObject.SetActive(false);
            //---------------
            _objectFactory.Camera.transform.position = new Vector3(Constants.ZERO, Constants.TREE, Constants.MINUS_TWO);
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
            Grab();
        }
    }

    private void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(_characterModel.CurrentName))
            {
                Destroy(hit.collider.gameObject);
                _characterView.PlayTakeItemAnimation();
                _characterModel.CollectFruit(Constants.ONE);
                CollectText(hit.point);
            }
        }
    }
    
    private void CollectText(Vector3 spawnPosition)
    {
        var text = Instantiate(_textPrefab, spawnPosition, Quaternion.identity);
        _uiBarController.UpdateScore(CharacterModel.CurrentFruit);
        StartCoroutine(MoveTextUpAndDestroy(text, Constants.TWO));
    }

    private IEnumerator MoveTextUpAndDestroy(GameObject textObject, float duration)
    {
        float elapsedTime = Constants.ZERO;
        Vector3 startPosition = textObject.transform.localPosition;
        Vector3 targetPosition = startPosition + Vector3.up;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            textObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(textObject);
    }
}