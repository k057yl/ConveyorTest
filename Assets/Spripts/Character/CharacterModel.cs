using UnityEngine;

public class CharacterModel
{
    public int FruitTarget { get; set; }
    public int CurrentFruit { get; set; }
    public string CurrentName { get; set; }
    public bool IsWin { get; private set; }

    public void CollectFruit(int fruit)
    {
        CurrentFruit += fruit;

        if (FruitTarget == CurrentFruit)
        {
            Debug.Log("Win");
            IsWin = true;
        }
    }
}