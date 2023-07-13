using UnityEngine;

public class CharacterModel
{
    private int _fruitTarget;

    public bool IsWin = false;

    public int FruitTarget
    {
        get { return _fruitTarget; }
        set { _fruitTarget = value; }
    }
    //------------
    private int _currentFruit;
    public int CurrentFruit
    {
        get { return _currentFruit; }
        set { _currentFruit = value; }
    }
    
    private string _currentName;//---------
    public string CurrentName
    {
        get { return _currentName; }
        set { _currentName = value; }//------------
    }
    //-------------
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
