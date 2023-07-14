using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    //-------
    [SerializeField] private Rig _rig;

    public void PlayWalkAnimation(Vector3 direction)
    {
        _anim.SetFloat(Constants.WALK, direction.magnitude);
    }

    public void PlayTakeItemAnimation()
    {
        _anim.SetTrigger(Constants.TAKE);
        _rig.weight = Mathf.MoveTowards(_rig.weight, Constants.ONE, Constants.TREE * Time.deltaTime);
    }
    
    public void PlayWinAnimation()
    {
        _anim.SetTrigger(Constants.WIN);
    }
}