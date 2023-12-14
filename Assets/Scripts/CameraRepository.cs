using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CameraRepository", menuName = "ScriptableObjects/Repository/CameraRepository")]
public class CameraRepository : ScriptableObject
{
    #region Camera
    [Header("Camera")]
    [SerializeField] private string[] _cameraNames;
    public string[] cameraNames { get { return _cameraNames; } private set { } }
        #endregion
}
