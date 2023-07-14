using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRig : MonoBehaviour
{
    public Rig Rig;

    private int _idAttack;
    private void Update()
    {
        //var input = InputController.Instance;
        //if (input.GetMouseLeft())
        if (Input.GetKeyDown(KeyCode.R)) _idAttack = 1;
        else if (Input.GetKeyDown(KeyCode.T)) _idAttack = 2;
        
        if(_idAttack == 1)
        {
            Rig.weight = Mathf.MoveTowards(Rig.weight, Constants.ONE, Constants.TREE * Time.deltaTime);
            if (Rig.weight == 1) _idAttack = 0;
        }
        
        if(_idAttack == 2)
        {
            Rig.weight = Mathf.MoveTowards(Rig.weight, Constants.ZERO, Constants.TREE * Time.deltaTime);
            if (Rig.weight == 0) _idAttack = 0;
        }
    }
}
