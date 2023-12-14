using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;
    private const float _rotationPower = 3f;
    private void Awake() => _player = GameObject.FindGameObjectWithTag("Player").transform;
    private void Update() => ControllerRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    private void LateUpdate()
    {
        transform.position = _player.position;
    }
    private void ControllerRotation(float x, float y) 
    {
        //transform.position = _player.position;
        ////Rotate the Follow Target transform based on the input
        transform.rotation *= Quaternion.AngleAxis(x * _rotationPower, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(-y * _rotationPower, Vector3.right);

        var angles = transform.eulerAngles;
        angles.z = 0;//-Vector2.up.y;
        if (angles.x > 180 && angles.x < 310) { angles.x = 310; }
        else if (angles.x < 180 && angles.x > 70) angles.x = 70;
        transform.rotation = Quaternion.Euler(angles);
    }
}
