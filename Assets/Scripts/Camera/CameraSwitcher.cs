using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraThrdPerson;
    [SerializeField] private CinemachineVirtualCamera _cameraTargetGroup;
    [SerializeField] private CinemachineVirtualCamera _cameraAction;

    [Header("Camera Effects")]
    [Space(5)]
    [SerializeField] private AnimationCurve _shootJutsuEffectCurve;
    [SerializeField, Range(1, 3)] private float _shootJutsuEffectCurveT = 1;
    private float _tCurve = 10;
    private float _maxTimeCurve;
    public CinemachineVirtualCamera cameraThrdPerson { get; set; } 
    public enum CameraVType { ThrdPerson, TargetGroup, Action, Other }
    private CinemachineVirtualCamera _currentCamera;
    private void Awake()
    {
        Init();
        _maxTimeCurve = _tCurve;
    }
    private void Start()
    {
        ChangeCamera(CameraVType.ThrdPerson); 
    }
    private void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = transform.GetChild(i).GetComponent<CinemachineVirtualCamera>();
            if (cinemachineVirtualCamera != null)
                cinemachineVirtualCamera.m_Priority = 0;
        }
    }
    public void ChangeCamera(CameraVType cameraType, Transform target = null, CinemachineVirtualCamera otherCamera = null)
    {
        switch(cameraType)
        {
            case CameraVType.ThrdPerson:
                SwitchCamera(_cameraThrdPerson); 
                break;
            case CameraVType.TargetGroup:
                SwitchCamera(_cameraTargetGroup);
                break;
            case CameraVType.Action:
                SwitchCamera(_cameraAction, target);
                break;
            case CameraVType.Other:
                SwitchCamera(otherCamera, target);
                break;
        }
    }
    private void SwitchCamera(CinemachineVirtualCamera newCamera, Transform target = null)
    {
        if (newCamera == _currentCamera) return; 
        if (target != null) newCamera.m_LookAt = target;
        if (_currentCamera != null) { _currentCamera.m_Priority = 0; }
        _currentCamera = newCamera;
        _currentCamera.m_Priority = 10;
    }
    public void SetShootJutsuEffect()
    {
        StopCoroutine(RunShootJutsuEffect()); 
        _tCurve = _maxTimeCurve;
        StartCoroutine(RunShootJutsuEffect());
    }
    private IEnumerator RunShootJutsuEffect()
    {
        _tCurve = 0;
        while (_tCurve < _maxTimeCurve)
        {
            _tCurve += Time.deltaTime * _maxTimeCurve * _shootJutsuEffectCurveT;  
            _currentCamera.m_Lens.FieldOfView = _shootJutsuEffectCurve.Evaluate(_tCurve);
            yield return null;
        }
    }
}
