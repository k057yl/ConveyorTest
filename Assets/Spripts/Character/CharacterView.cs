using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void PlayWinAnimation()
    {
        _anim.SetTrigger(Constants.WIN);
    }
}