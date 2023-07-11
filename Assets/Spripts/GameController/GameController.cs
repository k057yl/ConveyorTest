using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _conveyorPrefab;
    [SerializeField] private GameObject[] _fruits;

    private Character _character;
    private Conveyor _conveyor;

    private void Awake()
    {
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        _character = Instantiate(_characterPrefab).GetComponent<Character>();
        _conveyor = Instantiate(_conveyorPrefab).GetComponent<Conveyor>();
    }
}
