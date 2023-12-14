using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayforms : MonoBehaviour
{
    public enum SideType { Left, Center, Right }
    [SerializeField] private SideType _sideType;
    [SerializeField] private Color _gizmosColor;
    private List<JumpPlatform> _jumpPlatformsList = new List<JumpPlatform>();
#if UNITY_EDITOR
    private void OnValidate() => Init();
#endif
    private void Awake()
    {
        Init(); 
    }
    private void Init()
    {
        _jumpPlatformsList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            _jumpPlatformsList.Add(transform.GetChild(i).GetComponent<JumpPlatform>());
            _jumpPlatformsList[i].SetSideChPos(_sideType);
        }
        for (int i = 0; i < _jumpPlatformsList.Count - 1; i++)
            _jumpPlatformsList[i].SetLook(_jumpPlatformsList[i + 1].characterPos);
    }
    public void ResetJumpPlatforms()
    {
        for (int i = 0; i < transform.childCount; i++)
            _jumpPlatformsList[i].character = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        for (int i = 0; i < _jumpPlatformsList.Count - 1; i++)
            Gizmos.DrawLine(_jumpPlatformsList[i].characterPos, _jumpPlatformsList[i + 1].characterPos);
    }
}
