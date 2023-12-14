using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RBVelocityClamp : MonoBehaviour
{
    [SerializeField] private Vector3 _clampVector;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>(); 
    }
    void FixedUpdate()
    {
        float x = Mathf.Clamp(_rb.velocity.x, -_clampVector.x, _clampVector.x);
        float y = Mathf.Clamp(_rb.velocity.y, -_clampVector.y, _clampVector.y);
        float z = Mathf.Clamp(_rb.velocity.z, -_clampVector.z, _clampVector.z);
        _rb.velocity = new Vector3(x, y, z);
    }
}
