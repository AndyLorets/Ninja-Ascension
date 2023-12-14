using UnityEngine;

public class ChildCollisionEvent : MonoBehaviour
{
    private ICollisionEvent _collisionEvent; 
    private void Start() => _collisionEvent = transform.parent.GetComponent<ICollisionEvent>();

    private void OnTriggerEnter(Collider other) => _collisionEvent.TriggerEnter();
    private void OnTriggerStay(Collider other) => _collisionEvent.TriggerStay();
    private void OnTriggerExit(Collider other)=> _collisionEvent.TriggerExit();
}
