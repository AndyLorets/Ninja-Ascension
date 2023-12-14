using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "AutoPlayerJumper", menuName = "ScriptableObjects/Jumper/AutoPlayerJumper")]
public class AutoPlayerJumper : PlayerJumper
{
    public override void Jump()
    {
        base.Jump();
        _character.rb.isKinematic = true;

        _character.transform.DOJump(GetJumpPoint().characterPos, _jumpPower, 1, _jumpSpeed).SetEase(Ease.Flash)
            .OnComplete(() => _character.rb.isKinematic = false);
        _character.transform.DORotate(GetJumpPoint().characterRot, 1).OnComplete(() => _readyToJump = true);
        GetJumpPoint().character = _character;
    }
    protected JumpPlatform GetJumpPoint()
    {
        float distance = Mathf.Infinity;
        Vector3 pos = _character.transform.position;
        foreach (JumpPlatform go in _jumpPlatformsList)
        {
            Vector3 diff = go.characterPos - pos;
            float currentDist = diff.sqrMagnitude;
            if (currentDist < distance && go.character == null)
            {
                _currentJumpPlatform = go;
                distance = currentDist;
            }
        }
        return _currentJumpPlatform;
    }
}
