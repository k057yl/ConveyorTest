using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _conveyorPrefab;
    [SerializeField] private GameObject[] _fruits;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _uiBarPrefab;
    [SerializeField] private GameObject _cameraPrefab;


    private Camera _camera;
    public Camera Camera => _camera;
    private UIBarController _uiBarController;
    private Character _character;
    private Conveyor _conveyor;
    public Conveyor Conveyor => _conveyor;
    private TaskGenerator _taskGenerator;
    
    

    private void Awake()
    {
        InitializeManagers();
        StartCoroutine(SpawnObjects());
        GenerateRandomTask();
    }

    private void InitializeManagers()
    {
        _character = Instantiate(_characterPrefab).GetComponent<Character>();
        _conveyor = Instantiate(_conveyorPrefab).GetComponent<Conveyor>();

        _taskGenerator = new TaskGenerator(_fruits);

        _uiBarController = Instantiate(_uiBarPrefab).GetComponent<UIBarController>();
        _character.InitializeUIBar(_uiBarController);
        _character.InitializeObjectFactory(this);
        _camera = Instantiate(_cameraPrefab).GetComponent<Camera>();
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            int randomIndex = Random.Range(Constants.ZERO, _fruits.Length);
            Instantiate(_fruits[randomIndex], _spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(Constants.TWO);
        }
    }
    
    private void GenerateRandomTask()
    {
        Task task = _taskGenerator.GenerateTask();

        string taskDescription = $"Collect {task.TargetQuantity} {task.FruitName}";
        _uiBarController.UpdateText(taskDescription, _character.CharacterModel.CurrentFruit);

        _character.CharacterModel.FruitTarget = task.TargetQuantity;
        _character.CharacterModel.CurrentName = task.FruitName;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(Constants.ZERO);
    }
}