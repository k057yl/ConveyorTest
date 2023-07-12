using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _conveyorPrefab;
    [SerializeField] private GameObject[] _fruits;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Text _taskText;//

    private Character _character;
    private Conveyor _conveyor;
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
    }
    
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            int randomIndex = Random.Range(Constants.ZERO, _fruits.Length);
            
            GameObject fruit = Instantiate(_fruits[randomIndex], _spawnPoint.position, transform.rotation);
            
            float spawnInterval = Random.Range(Constants.ONE, Constants.TWO);
            Destroy(fruit, Constants.TWENTY);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void GenerateRandomTask()
    {
        Task task = _taskGenerator.GenerateTask();

        string taskDescription = string.Format("Собери {0} {1}", task.TargetQuantity, task.FruitName);
        _taskText.text = taskDescription;
    }
}