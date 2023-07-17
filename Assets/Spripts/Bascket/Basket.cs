using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private Transform[] _slots;

    public bool AddToSlot(GameObject item)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].childCount == 0)
            {
                item.transform.position = _slots[i].position;
                item.transform.SetParent(_slots[i]);
                return true;
            }
        }
        return false;
    }
}
