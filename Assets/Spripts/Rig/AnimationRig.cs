using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRig : MonoBehaviour
{
    public Rig _rig;

    private int _idAttack;
    private void Update()
    {
        //var input = InputController.Instance;
        //if (input.GetMouseLeft())
        if (Input.GetKeyDown(KeyCode.R)) _idAttack = 1;
        else if (Input.GetKeyDown(KeyCode.T)) _idAttack = 2;
        
        if(_idAttack == 1)
        {
            _rig.weight = Mathf.MoveTowards(_rig.weight, Constants.ONE, Constants.TREE * Time.deltaTime);
            if (_rig.weight == 1) _idAttack = 0;
        }
        
        if(_idAttack == 2)
        {
            _rig.weight = Mathf.MoveTowards(_rig.weight, Constants.ZERO, Constants.TREE * Time.deltaTime);
            if (_rig.weight == 0) _idAttack = 0;
        }
    }
}
