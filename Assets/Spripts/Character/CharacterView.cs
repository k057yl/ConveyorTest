using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _anim;


    public void PlayWalkAnimation(Vector3 direction)
    {
        _anim.SetFloat(Constants.WALK, direction.magnitude);
    }

    public void PlayTakeItemAnimation()
    {
        _anim.SetTrigger(Constants.TAKE);
    }
    
    public void PlayWinAnimation()
    {
        _anim.SetTrigger(Constants.WIN);
    }
}