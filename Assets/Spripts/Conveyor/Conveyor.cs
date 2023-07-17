using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Material _mat;
    [SerializeField] private float _speed;
    
    private void FixedUpdate()
    {
        MoveningConveyor();
    }

    private void MoveningConveyor()
    {
        _mat.mainTextureOffset = new Vector2(-Time.time * Constants.TWO * Time.fixedDeltaTime, Constants.ZERO);
        Vector3 pos = _rb.position;
        _rb.position += Vector3.right * (_speed * Time.fixedDeltaTime);
        _rb.MovePosition(pos);
    }
}
