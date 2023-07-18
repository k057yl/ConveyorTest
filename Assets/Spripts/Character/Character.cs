using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private Basket _basket;
    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _targetObject;
    [SerializeField] private ParticleSystem _textParticlePrefab;
    

    private CharacterView _characterView;
    private CharacterModel _characterModel;
    public CharacterModel CharacterModel => _characterModel;
    private InputController _inputController;
    private UIBarController _uiBarController;
    private ObjectFactory _objectFactory;
    
    
    private bool _isRig = false;
    private float _grabRadius = Constants.ONE_AND_TWO_HUNDREDTHS;


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
    }

    private void Update()
    {
        HandleInput();

        if (_characterModel.IsWin)
        {
            Win();
        }
    }
    
    private void HandleInput()
    {
        if (_inputController.GetTouch())
        {
            TryGrab();
        }
    }
    
    private void TryGrab()
    {
        Vector2 touchPosition = _inputController.GetTouchPosition();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag(_characterModel.CurrentName))
        {
            float distanceToPlayer = Vector3.Distance(transform.position, hit.transform.position);
            if (distanceToPlayer <= _grabRadius)
            {
                Grab(hit.collider.gameObject);
            }
        }
    }

    private void Grab(GameObject item)
    {
        _targetObject.position = item.transform.position;

        bool addedToBasket = _basket.AddToSlot(item);
        if (!addedToBasket)
        {
            return;
        }

        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

        itemRigidbody.isKinematic = true;

        _characterModel.CollectFruit(Constants.ONE);
        CollectText(item.transform.position);

        if (!_isRig)
        {
            StartCoroutine(PlayRigAnimation());
        }
    }
    
    private IEnumerator PlayRigAnimation()
    {
        float elapsedTime = Constants.ZERO;
        float animationDuration = Constants.HALF_OF_ONE;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            _rig.weight = Mathf.Lerp(Constants.ZERO, Constants.ONE, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _rig.weight = Constants.ONE;
        _isRig = true;
        
        yield return new WaitForSeconds(Constants.HALF_OF_ONE);
        
        elapsedTime = Constants.ZERO;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            _rig.weight = Mathf.Lerp(Constants.ONE, Constants.ZERO, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _rig.weight = Constants.ZERO;
        _isRig = false;
    }

    private void CollectText(Vector3 spawnPosition)
    {
        var text = Instantiate(_textPrefab, spawnPosition, new Quaternion(Constants.ZERO, Constants.ONE_HUNDRED_AND_EIGHTY,Constants.ZERO,Constants.ZERO));
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
        
        Instantiate(_textParticlePrefab, textObject.transform.position, Quaternion.identity);
        
        Destroy(textObject);
    }

    private void Win()
    {
        _uiBarController.Win();
        _characterView.PlayWinAnimation();
        _objectFactory.Conveyor.gameObject.SetActive(false);
        _objectFactory.Camera.transform.position = new Vector3(Constants.ZERO, Constants.ONE_AND_A_HALF, Constants.TWO_AND_A_HALF);
    }
}