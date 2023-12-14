using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour, ICollisionEvent
{
    private List<JumpPlayforms> _jumpPlayforms = new List<JumpPlayforms>();
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            JumpPlayforms jp = transform.GetChild(i).GetComponent<JumpPlayforms>();
            if (jp != null) _jumpPlayforms.Add(jp);
        }
    }
    private void ResetLocation()
    {
        for (int i = 0; i < _jumpPlayforms.Count; i++)
            _jumpPlayforms[i].ResetJumpPlatforms();
        //Debug.Log($"{transform.name}: reset"); 
    }

    public void TriggerEnter()
    {
        ResetLocation(); 
    }

    public void TriggerStay()
    {
    }

    public void TriggerExit()
    {
    }
}
