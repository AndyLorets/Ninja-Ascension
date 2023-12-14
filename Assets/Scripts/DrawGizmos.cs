using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawGizmos : MonoBehaviour
{
    [SerializeField] private Image _targetVisual; 
    private float _maxDistRayJP;
    private float _currentDistRayJP;
    private float _sphereRadius;
    private Vector3 _origin;
    private Vector3 _direction;
    private bool _freeRay; 
    private void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        _targetVisual = Instantiate(_targetVisual, canvas.transform); 
    }
    public void DrawingGizmos(float maxDist, float currentDist, float radius, Vector3 origin, Vector3 direction, Vector3 point, bool near)
    {
        _maxDistRayJP = maxDist; 
        _currentDistRayJP = currentDist;
        _sphereRadius = radius;
        _origin = origin;
        _direction = direction;

        _freeRay = _maxDistRayJP == _currentDistRayJP;
        _targetVisual.color = near ? Color.green : Color.grey; 
        _targetVisual.gameObject.SetActive(!_freeRay); 
        _targetVisual.transform.position = Camera.main.WorldToScreenPoint(point);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _freeRay ? Color.black : Color.yellow;
        Gizmos.DrawLine(_origin, _origin + _direction * _currentDistRayJP);
        Gizmos.DrawWireSphere(_origin + _direction * _currentDistRayJP, _sphereRadius); 
    }
}
