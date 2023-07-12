public class Task
{
    public string FruitName { get; }
    public int TargetQuantity { get; }

    public Task(string fruitName, int targetQuantity)
    {
        FruitName = fruitName;
        TargetQuantity = targetQuantity;
    }
}