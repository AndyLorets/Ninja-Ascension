using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { _jumper.JumpPress(); }
        _jumper.Run(/*EnemyCheckSphere(_followingDistance)*/_jumperActive);
        //_jumper.overJump = EnemyCheckSphere(); 
        _fighter.Run(_fighterActive);
        if (Input.GetKeyDown(KeyCode.T)) Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bool rotateGround = collision.transform.eulerAngles.x > 12 || collision.transform.eulerAngles.x < -12;
            if (rotateGround) _rb.constraints = RigidbodyConstraints.FreezeAll;
            _skin.SetLegChakra(rotateGround);
            _jumper.OnGroundEnter(); 
        }
    }

    /// <summary>
    /// ////////////////
    /// </summary>
}
