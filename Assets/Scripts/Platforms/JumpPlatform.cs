using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("Позиция для персонажа")] private Transform _characterPos;
    public Vector3 characterPos { get { return _characterPos.position; } private set { } }
    public Vector3 characterRot { get { return _characterPos.eulerAngles; } private set { } }
    private Collider _collider;
    private void Awake() => _collider = GetComponent<Collider>();
    public Character character { get; set; }
    public void SetLook(Vector3 pos)
    {
        _characterPos.LookAt(pos);
        _characterPos.eulerAngles = new Vector3(0, _characterPos.eulerAngles.y, 0);
    }
    public void ColliderEnabled(bool state) 
    {
        _collider.enabled = state;
        transform.GetChild(1).GetComponent<MeshRenderer>().materials[1].color = state ? Color.yellow : Color.black; 
    }
    public void SetSideChPos(JumpPlayforms.SideType sideType)
    {
        switch(sideType)
        {
            case JumpPlayforms.SideType.Left:
                _characterPos.transform.localPosition = new Vector3(0, 0.5f, -.4f);
                break;
            case JumpPlayforms.SideType.Center:
                _characterPos.transform.localPosition = new Vector3(0, 0.5f, 0);
                break;
            case JumpPlayforms.SideType.Right:
                _characterPos.transform.localPosition = new Vector3(0, 0.5f, .4f);
                break;
        }
    }
}


