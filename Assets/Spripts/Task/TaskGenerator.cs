using UnityEngine;

public class TaskGenerator
{
    private GameObject[] _fruits;

    public TaskGenerator(GameObject[] fruits)
    {
        _fruits = fruits;
    }

    public Task GenerateTask()
    {
        int randomIndex = Random.Range(Constants.ZERO, _fruits.Length);
        GameObject randomFruitPrefab = _fruits[randomIndex];

        string fruitName = randomFruitPrefab.name;
        int targetQuantity = Random.Range(Constants.ONE, Constants.SIX);

        return new Task(fruitName, targetQuantity);
    }
}